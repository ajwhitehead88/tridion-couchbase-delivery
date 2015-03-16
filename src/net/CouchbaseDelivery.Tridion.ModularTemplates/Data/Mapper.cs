using CouchbaseDelivery.Data.ContentModel.Contract.Enums;
using CouchbaseDelivery.Data.ContentModel.Model.Content;
using CouchbaseDelivery.Tridion.ModularTemplates.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Tridion.ContentManager.ContentManagement;
using Tridion.ContentManager.ContentManagement.Fields;
using Tridion.ContentManager.Templating;

namespace CouchbaseDelivery.Tridion.ModularTemplates.Data
{
    /// <summary>
    /// Mapper for item fields to the custom JSON model
    /// </summary>
    internal class Mapper
    {
        private static readonly IDictionary<Type, FieldTypes> TypeMap = new Dictionary<Type, FieldTypes>
                                                                       {
                                                                           { typeof (SingleLineTextField), FieldTypes.Text },
                                                                           { typeof (MultiLineTextField), FieldTypes.Text },
                                                                           { typeof (XhtmlField), FieldTypes.Text },
                                                                           { typeof (ExternalLinkField), FieldTypes.Text },
                                                                           { typeof (NumberField), FieldTypes.Number },
                                                                           { typeof (DateField), FieldTypes.Date },
                                                                           { typeof (EmbeddedSchemaField), FieldTypes.Embedded },
                                                                           { typeof (KeywordField), FieldTypes.Keyword },
                                                                           { typeof (ComponentLinkField), FieldTypes.Link },
                                                                           { typeof (MultimediaLinkField), FieldTypes.Link },
                                                                       };

        private readonly Engine _engine;
        private readonly Package _package;

        public Mapper(Engine engine, Package package)
        {
            _engine = engine;
            _package = package;
        }

        /// <summary>
        /// Map item fields to field set model when given the content and schema
        /// </summary>
        /// <param name="xmlElement"></param>
        /// <param name="schema"></param>
        /// <param name="levels"></param>
        /// <returns></returns>
        public IEnumerable<FieldModel> MapItemFields(XmlElement xmlElement, Schema schema, int levels)
        {
            if (xmlElement == null || schema == null)
            {
                return null;
            }

            var fields = new ItemFields(xmlElement, schema);
            return MapItemFields(fields, levels);
        }

        /// <summary>
        /// Map item fields to field set model
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="levels"></param>
        /// <returns></returns>
        public IEnumerable<FieldModel> MapItemFields(ItemFields fields, int levels)
        {
            return fields.Select(field => CreateField(field, levels)).ToList();
        }

        /// <summary>
        /// Create a single field from an item field
        /// </summary>
        /// <param name="field"></param>
        /// <param name="levels"></param>
        /// <returns></returns>
        private FieldModel CreateField(ItemField field, int levels)
        {
            if (levels-- < 0)
            {
                return null;
            }

            var type = TypeMap[field.GetType()];

            var fieldModel = new FieldModel(field.Name, type);
            switch (type)
            {
                case FieldTypes.Text:
                    var textField = (TextField) field;
                    fieldModel.StringValues = textField.Values;
                    break;
                case FieldTypes.Number:
                    var numberField = (NumberField) field;
                    fieldModel.NumberValues = numberField.Values;
                    break;
                case FieldTypes.Date:
                    var dateField = (DateField) field;
                    fieldModel.DateTimeValues = dateField.Values;
                    break;
                case FieldTypes.Keyword:
                    var keywordField = (KeywordField) field;
                    fieldModel.KeywordValues = keywordField.Values.Select(x => new KeywordModel
                                                                               {
                                                                                   TcmUri = x.Id,
                                                                                   Key = x.Key,
                                                                                   Title = x.Title,
                                                                                   SchemaName = x.MetadataSchema != null
                                                                                                    ? x.MetadataSchema.Title
                                                                                                    : null,
                                                                                   Metadata =
                                                                                       MapItemFields(x.Metadata, x.MetadataSchema, levels)
                                                                               });
                    break;
                case FieldTypes.Embedded:
                    var embeddedField = (EmbeddedSchemaField) field;
                    fieldModel.EmbeddedValues = embeddedField.Values.Select(x => MapItemFields(x, levels));
                    break;
                case FieldTypes.Link:
                    var linkField = (ComponentLinkField) field;
                    fieldModel.ComponentLinkValues = linkField.Values.Select(x => new ComponentModel
                                                                                  {
                                                                                      TcmUri = x.Id,
                                                                                      Title = x.Title,
                                                                                      SchemaName = x.Schema.Title,
                                                                                      Content = MapItemFields(x.Content, x.Schema, levels),
                                                                                      Metadata =
                                                                                          MapItemFields(x.Metadata, x.MetadataSchema, levels),
                                                                                      BinaryUrl = x.BinaryContent != null
                                                                                                      ? x.PublishBinary(_engine, _package)
                                                                                                      : null
                                                                                  });
                    break;
                default:
                    return null;
            }

            return fieldModel;
        }
    }
}
