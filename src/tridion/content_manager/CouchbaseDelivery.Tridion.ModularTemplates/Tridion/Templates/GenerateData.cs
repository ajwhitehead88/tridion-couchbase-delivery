using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using Tridion.ContentManager.CommunicationManagement;
using Tridion.ContentManager.Templating;
using Tridion.ContentManager.Templating.Assembly;
using CouchbaseDelivery.Tridion.ModularTemplates.Data;
using CouchbaseDelivery.Tridion.ModularTemplates.Data.Models;
using CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Content;
using CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Layout;
using CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Structure;
using CouchbaseDelivery.Tridion.ModularTemplates.Helpers;

namespace CouchbaseDelivery.Tridion.ModularTemplates.Tridion.Templates
{
    [TcmTemplateTitle("Generate Data")]
    public class GenerateData : BaseTemplate
    {
        private Page _page;
        private Publication _publication;
        private StructureGroup _parent;
        private Mapper _mapper;

        protected override void Transform()
        {
            // Initialise the necessary components
            _page = GetPage();
            _publication = (Publication) _page.ContextRepository;
            _parent = (StructureGroup) _page.OrganizationalItem;
            _mapper = new Mapper(Engine);

            using (var mem = new MemoryStream())
            {
                using (var reader = new StreamReader(mem))
                {
                    var ser = new DataContractJsonSerializer(typeof(PublishedDataModel));
                    ser.WriteObject(mem, new PublishedDataModel
                        {
                            Page = CreatePageModel(),
                            Publication = CreatePublicationModel(),
                            Parent = CreateStructureGroupModel(),
                            PublishDate = DateTime.UtcNow
                        });

                    mem.Position = 0;

                    Package.PushItem(Package.OutputName, Package.CreateStringItem(ContentType.Text, reader.ReadToEnd()));
                }
            }
        }

        /// <summary>
        /// Create the structure group model
        /// </summary>
        /// <returns></returns>
        private StructureGroupModel CreateStructureGroupModel()
        {
            return new StructureGroupModel
                {
                    TcmUri = _parent.Id,
                    Title = _parent.Title,
                    PublishedUrl = _parent.PublishLocationUrl,
                    SchemaName = _parent.MetadataSchema != null
                                     ? _parent.MetadataSchema.Title
                                     : null,
                    Metadata = _mapper.MapItemFields(_parent.Metadata, _parent.MetadataSchema)
                };
        }

        /// <summary>
        /// Create the presentation model array
        /// </summary>
        /// <returns></returns>
        private List<ComponentPresentationModel> CreateComponentPresentationModels()
        {
            var presentations = new List<ComponentPresentationModel>();
            foreach (var cp in _page.ComponentPresentations)
            {
                // Ensure template is added to the link info
                Engine.RenderComponentPresentation(cp.Component.Id, cp.ComponentTemplate.Id);

                var presentation = new ComponentPresentationModel
                    {
                        ComponentModel = new ComponentModel
                            {
                                TcmUri = cp.Component.Id,
                                Title = cp.Component.Title,
                                SchemaName = cp.Component.Schema.Title,
                                Content = _mapper.MapItemFields(cp.Component.Content, cp.Component.Schema),
                                Metadata = _mapper.MapItemFields(cp.Component.Metadata, cp.Component.MetadataSchema),
                                BinaryContent = cp.Component.BinaryContent != null
                                                    ? new BinaryContentModel
                                                        {
                                                            Filename = ComponentHelper.GetUniqueFilename(cp.Component),
                                                            BinaryContent = Convert.ToBase64String(cp.Component.BinaryContent.GetByteArray())
                                                        }
                                                    : null
                            },
                        ComponentTemplateModel = new ComponentTemplateModel
                            {
                                TcmUri = cp.Component.Id,
                                Title = cp.Component.Title,
                                SchemaName = cp.ComponentTemplate.MetadataSchema != null
                                                 ? cp.ComponentTemplate.MetadataSchema.Title
                                                 : null,
                                Metadata = _mapper.MapItemFields(cp.ComponentTemplate.Metadata,
                                                                 cp.ComponentTemplate.MetadataSchema)
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
                    TcmUri = _page.Id,
                    Title = _page.Title,
                    PublishedUrl = _page.PublishLocationUrl,
                    PageTemplateModel = new PageTemplateModel
                        {
                            TcmUri = _page.PageTemplate.Id,
                            Title = _page.PageTemplate.Title,
                        },
                    SchemaName = _page.MetadataSchema != null
                                     ? _page.MetadataSchema.Title
                                     : null,
                    Metadata = _mapper.MapItemFields(_page.Metadata, _page.MetadataSchema),
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
                    TcmUri = _publication.Id,
                    Title = _publication.Title,
                    PublicationUrl = _publication.PublicationUrl,
                    MultimediaUrl = _publication.MultimediaUrl,
                    SchemaName = _publication.MetadataSchema != null
                                     ? _publication.MetadataSchema.Title
                                     : null,
                    Metadata = _mapper.MapItemFields(_publication.Metadata, _publication.MetadataSchema)
                };
        }
    }
}
