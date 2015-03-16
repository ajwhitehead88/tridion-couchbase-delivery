using System;
using System.Collections.Generic;
using CouchbaseDelivery.Data.ContentModel.Contract.Enums;
using CouchbaseDelivery.Data.ContentModel.Model;
using CouchbaseDelivery.Data.ContentModel.Model.Content;
using CouchbaseDelivery.Data.ContentModel.Model.Layout;
using CouchbaseDelivery.Data.ContentModel.Model.Structure;

namespace CouchbaseDelivery.Data.ContentModel.Tests.Helpers
{
    /// <summary>
    /// Helper classes to generate test models
    /// </summary>
    internal static class ModelHelper
    {
        /// <summary>
        /// Generate test data for a published page
        /// </summary>
        /// <returns></returns>
        internal static PagePublishedDataModel GetPublishedPageModel()
        {
            return new PagePublishedDataModel
                   {
                       PublicationId = 1,
                       Publication = GetPublicationModel(),
                       Parent = GetStructureGroupModel(),
                       PublishDate = new DateTime(2015, 2, 1),
                       Page = GetPageModel()
                   };
        }

        /// <summary>
        /// Generate test data for a page
        /// </summary>
        /// <returns></returns>
        private static PageModel GetPageModel()
        {
            return new PageModel
                   {
                       PublishedUrl = "/folder/page.html",
                       Title = "Test Page",
                       SchemaName = "Page Schema",
                       TcmUri = "tcm:1-3-64",
                       TemplateModel = GetTemplateModel(),
                       Metadata = new []
                                  {
                                      new FieldModel("title", FieldTypes.Text) { StringValues = new[] { "Page Title" } },
                                      new FieldModel("created_date", FieldTypes.Date) { DateTimeValues = new[] { new DateTime(2013, 3, 2) } },
                                      new FieldModel("tags", FieldTypes.Keyword)
                                                  {
                                                      KeywordValues = new[]
                                                               {
                                                                   new KeywordModel { Title = "Tag 1", Key = "tag_1" },
                                                                   new KeywordModel { Title = "Tag 2", Key = "tag_2" },
                                                                   new KeywordModel { Title = "Tag 3", Key = "tag_3" }
                                                               }
                                                  }
                                  },
                       ComponentPresentations = new[]
                                                {
                                                    GetComponentPresentationModel(1),
                                                    GetComponentPresentationModel(2),
                                                    GetComponentPresentationModel(3),
                                                    GetComponentPresentationModel(4)
                                                }
                   };
        }

        /// <summary>
        /// Generate test data for a component presentation
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private static ComponentPresentationModel GetComponentPresentationModel(int i = 0)
        {
            return new ComponentPresentationModel
                   {
                       TemplateModel = GetTemplateModel(),
                       ComponentModel = GetComponentModel(i)
                   };
        }

        /// <summary>
        /// Generate test data for a component
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private static ComponentModel GetComponentModel(int i = 0)
        {
            return new ComponentModel
                   {
                       Title = string.Format("Component {0}", i),
                       TcmUri = string.Format("tcm:1-{0}-16", i),
                       SchemaName = "Component Schema",
                       Content = new []
                                 {
                                     new FieldModel("title", FieldTypes.Text) { StringValues = new[] { string.Format("Component Title {0}", i) } },
                                     new FieldModel("subtitle", FieldTypes.Text) { StringValues = new[] { string.Format("Component Subtitle {0}", i) } },
                                     new FieldModel("text", FieldTypes.Text) { StringValues = new[] { string.Format("Component Text {0}", i) } },
                                     new FieldModel("embedded", FieldTypes.Embedded)
                                                     {
                                                         EmbeddedValues = new[]
                                                                  {
                                                                      new [] { new FieldModel("embedded_title", FieldTypes.Text) { StringValues = new[] { "Embedded title" } } }
                                                                  }
                                                     }
                                 },
                       Metadata = new []
                                  {
                                          new FieldModel("tags", FieldTypes.Keyword)
                                                  {
                                                      KeywordValues = new[]
                                                               {
                                                                   new KeywordModel { Title = "Tag 1", Key = "tag_1" },
                                                                   new KeywordModel { Title = "Tag 2", Key = "tag_2" },
                                                                   new KeywordModel { Title = "Tag 3", Key = "tag_3" }
                                                               }
                                                  }
                                  }
                   };
        }

        /// <summary>
        /// Generate test data for a template
        /// </summary>
        /// <returns></returns>
        private static TemplateModel GetTemplateModel()
        {
            return new TemplateModel
                   {
                       Title = "Test Template",
                       SchemaName = "Template Schema",
                       TcmUri = "tcm:1-4-128",
                       Metadata = GetTemplateMetadataModel()
                   };
        }

        /// <summary>
        /// Generate test data for the template schema
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<FieldModel> GetTemplateMetadataModel()
        {
            return new []
                   {
                       new FieldModel("view", FieldTypes.Text) { StringValues = new[] { "View" } },
                       new FieldModel("action", FieldTypes.Text) { StringValues = new[] { "Action" } },
                       new FieldModel("controller", FieldTypes.Text) { StringValues = new[] { "Controller" } },
                       new FieldModel("link_levels", FieldTypes.Number) { NumberValues = new[] { 1d } }
                   };
        }

        /// <summary>
        /// Generate test data for a structure group
        /// </summary>
        /// <returns></returns>
        private static StructureGroupModel GetStructureGroupModel()
        {
            return new StructureGroupModel
                   {
                       PublishedUrl = "/folder",
                       Title = "Test Structure Group",
                       SchemaName = "Structure Group Schema",
                       TcmUri = "tcm:1-2-4",
                       Metadata = new []
                                  {
                                      new FieldModel("title", FieldTypes.Text) { StringValues = new[] { "Structure Group Title" } } 
                                  }
                   };
        }

        /// <summary>
        /// Generate test data for a publication
        /// </summary>
        /// <returns></returns>
        private static PublicationModel GetPublicationModel()
        {
            return new PublicationModel
                   {
                       PublicationUrl = "/",
                       MultimediaUrl = "/media",
                       SchemaName = "Publication Schema",
                       TcmUri = "tcm:0-1-2",
                       Title = "Test Publication",
                       Metadata = new []
                                  {
                                      new FieldModel("title", FieldTypes.Text) { StringValues = new[] { "Publication Title" } } 
                                  }
                   };
        }
    }
}
