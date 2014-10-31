using CouchbaseDelivery.Tridion.ModularTemplates.Data;
using CouchbaseDelivery.Tridion.ModularTemplates.Data.Models;
using CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Content;
using CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Layout;
using CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Structure;
using CouchbaseDelivery.Tridion.ModularTemplates.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using Tridion.ContentManager.Templating;
using Tridion.ContentManager.Templating.Assembly;

namespace CouchbaseDelivery.Tridion.ModularTemplates.Tridion.Templates
{
    /// <summary>
    /// Generates JSON data for a page or a component template
    /// </summary>
    [TcmTemplateTitle("Generate Data")]
    public class GenerateData : BaseTemplate
    {
        private int _pageLinkLevels;
        private Mapper _mapper;

        protected override void Transform()
        {
            _pageLinkLevels = Page.PageTemplate.GetLinkLevels();
            _mapper = new Mapper(Engine, Package);

            var json = JsonConvert.SerializeObject(new PublishedDataModel
                                                   {
                                                       Page = CreatePageModel(),
                                                       Publication = CreatePublicationModel(),
                                                       Parent = CreateStructureGroupModel(),
                                                       PublishDate = DateTime.UtcNow
                                                   },
                                                   new JsonSerializerSettings
                                                   {
                                                       ContractResolver = new CamelCasePropertyNamesContractResolver(),
                                                       DateFormatHandling = DateFormatHandling.IsoDateFormat,
                                                       Formatting = Formatting.Indented
                                                   });

            Package.PushItem(Package.OutputName, Package.CreateStringItem(ContentType.Text, json));
        }

        /// <summary>
        /// Create the structure group model
        /// </summary>
        /// <returns></returns>
        private StructureGroupModel CreateStructureGroupModel()
        {
            return new StructureGroupModel
                   {
                       TcmUri = Parent.Id,
                       Title = Parent.Title,
                       PublishedUrl = Parent.PublishLocationUrl,
                       SchemaName = Parent.MetadataSchema != null
                                        ? Parent.MetadataSchema.Title
                                        : null,
                       Metadata = _mapper.MapItemFields(Parent.Metadata, Parent.MetadataSchema, 1)
                   };
        }

        /// <summary>
        /// Create the presentation model array
        /// </summary>
        /// <returns></returns>
        private List<ComponentPresentationModel> CreateComponentPresentationModels()
        {
            var presentations = new List<ComponentPresentationModel>();
            foreach (var cp in Page.ComponentPresentations)
            {
                // Ensure template is added to the link info (in case the existing broker is still necessary)
                Engine.RenderComponentPresentation(cp.Component.Id, cp.ComponentTemplate.Id);

                var linkLevels = cp.ComponentTemplate.GetLinkLevels();

                var presentation = new ComponentPresentationModel
                                   {
                                       ComponentModel = new ComponentModel
                                                        {
                                                            TcmUri = cp.Component.Id,
                                                            Title = cp.Component.Title,
                                                            SchemaName = cp.Component.Schema.Title,
                                                            Content = _mapper.MapItemFields(cp.Component.Content,
                                                                                            cp.Component.Schema,
                                                                                            linkLevels),
                                                            Metadata = _mapper.MapItemFields(cp.Component.Metadata,
                                                                                             cp.Component.MetadataSchema,
                                                                                             linkLevels),
                                                            BinaryUrl = cp.Component.BinaryContent != null
                                                                            ? cp.Component.PublishBinary(Engine, Package)
                                                                            : null
                                                        },
                                       TemplateModel = new TemplateModel
                                                       {
                                                           TcmUri = cp.Component.Id,
                                                           Title = cp.Component.Title,
                                                           SchemaName = cp.ComponentTemplate.MetadataSchema != null
                                                                            ? cp.ComponentTemplate.MetadataSchema.Title
                                                                            : null,
                                                           Metadata = _mapper.MapItemFields(cp.ComponentTemplate.Metadata,
                                                                                            cp.ComponentTemplate.MetadataSchema,
                                                                                            linkLevels),
                                                           Priority = cp.ComponentTemplate.Priority
                                                       }
                                   };

                presentations.Add(presentation);
            }

            return presentations;
        }

        /// <summary>
        /// Create the page model
        /// </summary>
        /// <returns></returns>
        private PageModel CreatePageModel()
        {
            return new PageModel
                   {
                       TcmUri = Page.Id,
                       Title = Page.Title,
                       PublishedUrl = Page.PublishLocationUrl,
                       TemplateModel = new TemplateModel
                                       {
                                           TcmUri = Page.PageTemplate.Id,
                                           Title = Page.PageTemplate.Title,
                                           SchemaName = Page.MetadataSchema != null
                                                            ? Page.MetadataSchema.Title
                                                            : null,
                                           Metadata = _mapper.MapItemFields(Page.Metadata,
                                                                            Page.MetadataSchema,
                                                                            _pageLinkLevels)
                                       },
                       SchemaName = Page.MetadataSchema != null
                                        ? Page.MetadataSchema.Title
                                        : null,
                       Metadata = _mapper.MapItemFields(Page.Metadata, Page.MetadataSchema, _pageLinkLevels),
                       ComponentPresentations = CreateComponentPresentationModels().ToArray()
                   };
        }

        /// <summary>
        /// Create the publication model
        /// </summary>
        /// <returns></returns>
        private PublicationModel CreatePublicationModel()
        {
            return new PublicationModel
                   {
                       TcmUri = Publication.Id,
                       Title = Publication.Title,
                       PublicationUrl = Publication.PublicationUrl,
                       MultimediaUrl = Publication.MultimediaUrl,
                       SchemaName = Publication.MetadataSchema != null
                                        ? Publication.MetadataSchema.Title
                                        : null,
                       Metadata = _mapper.MapItemFields(Publication.Metadata, Publication.MetadataSchema, _pageLinkLevels)
                   };
        }
    }
}
