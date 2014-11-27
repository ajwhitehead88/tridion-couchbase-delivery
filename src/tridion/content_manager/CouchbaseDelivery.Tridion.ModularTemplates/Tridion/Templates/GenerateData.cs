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
using Tridion.ContentManager.CommunicationManagement;
using Tridion.ContentManager.ContentManagement;
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
            _mapper = new Mapper(Engine, Package);

            PublishedDataModel model;
            if (Page != null)
            {
                model = CreatePublishedPage();
            }
            else if (Template != null)
            {
                model = CreatePublishedPresentation();
            }
            else
            {
                throw new InvalidOperationException("Cannot run this template without a page or a component template");
            }

            var json = JsonConvert.SerializeObject(model,
                                                   new JsonSerializerSettings
                                                   {
                                                       ContractResolver = new CamelCasePropertyNamesContractResolver(),
                                                       DateFormatHandling = DateFormatHandling.IsoDateFormat,
                                                       Formatting = Formatting.Indented
                                                   });

            Package.PushItem(Package.OutputName, Package.CreateStringItem(ContentType.Text, json));
        }

        /// <summary>
        /// Create a publish model on page publish
        /// </summary>
        /// <returns></returns>
        private PagePublishedDataModel CreatePublishedPage()
        {
            _pageLinkLevels = Page.PageTemplate.GetLinkLevels();

            return new PagePublishedDataModel
                   {
                       Page = CreatePageModel(),
                       Publication = CreatePublicationModel(),
                       Parent = CreateStructureGroupModel(),
                       PublishDate = DateTime.UtcNow
                   };
        }

        /// <summary>
        /// Create a single published presentation
        /// </summary>
        /// <returns></returns>
        private PresentationPublishedDataModel CreatePublishedPresentation()
        {
            return new PresentationPublishedDataModel
                   {
                       Presentation = CreateComponentPresentation(Component, Template)
                   };
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

                var presentation = CreateComponentPresentation(cp.Component, cp.ComponentTemplate);

                presentations.Add(presentation);
            }

            return presentations;
        }

        /// <summary>
        /// Create a single component presentation model
        /// </summary>
        /// <param name="component"></param>
        /// <param name="componentTemplate"></param>
        /// <returns></returns>
        private ComponentPresentationModel CreateComponentPresentation(Component component, ComponentTemplate componentTemplate)
        {
            var linkLevels = componentTemplate.GetLinkLevels();

            var presentation = new ComponentPresentationModel
                               {
                                   ComponentModel = new ComponentModel
                                                    {
                                                        TcmUri = component.Id,
                                                        Title = component.Title,
                                                        SchemaName = component.Schema.Title,
                                                        Content = _mapper.MapItemFields(component.Content,
                                                                                        component.Schema,
                                                                                        linkLevels),
                                                        Metadata = _mapper.MapItemFields(component.Metadata,
                                                                                         component.MetadataSchema,
                                                                                         linkLevels),
                                                        BinaryUrl = component.BinaryContent != null
                                                                        ? component.PublishBinary(Engine, Package)
                                                                        : null
                                                    },
                                   TemplateModel = new TemplateModel
                                                   {
                                                       TcmUri = component.Id,
                                                       Title = component.Title,
                                                       SchemaName = componentTemplate.MetadataSchema != null
                                                                        ? componentTemplate.MetadataSchema.Title
                                                                        : null,
                                                       Metadata = _mapper.MapItemFields(componentTemplate.Metadata,
                                                                                        componentTemplate.MetadataSchema,
                                                                                        linkLevels),
                                                       Priority = componentTemplate.Priority
                                                   }
                               };
            return presentation;
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
