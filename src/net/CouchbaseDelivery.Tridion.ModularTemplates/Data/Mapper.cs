using CouchbaseDelivery.Data.ContentModel.Model.Content;
using CouchbaseDelivery.Data.ContentModel.Model.Fields;
using CouchbaseDelivery.Data.ContentModel.Model.Structure;
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
        private static readonly IDictionary<Type, FieldType> TypeMap = new Dictionary<Type, FieldType>
                                                                       {
                                                                           { typeof (SingleLineTextField), FieldType.Text },
                                                                           { typeof (MultiLineTextField), FieldType.Text },
                                                                           { typeof (XhtmlField), FieldType.Text },
                                                                           { typeof (ExternalLinkField), FieldType.Text },
                                                                           { typeof (NumberField), FieldType.Number },
                                                                           { typeof (DateField), FieldType.Date },
                                                                           { typeof (EmbeddedSchemaField), FieldType.Embedded },
                                                                           { typeof (KeywordField), FieldType.Keyword },
                                                                           { typeof (ComponentLinkField), FieldType.Link },
                                                                           { typeof (MultimediaLinkField), FieldType.Link },
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
        public FieldSetModel MapItemFields(XmlElement xmlElement, Schema schema, int levels)
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
        public FieldSetModel MapItemFields(ItemFields fields, int levels)
        {
            var fieldSet = new FieldSetModel();

            foreach (var field in fields)
            {
                fieldSet.Add(field.Name, CreateField(field, levels));
            }

            return fieldSet;
        }

        /// <summary>
        /// Create a single field from an item field
        /// </summary>
        /// <param name="field"></param>
        /// <param name="levels"></param>
        /// <returns></returns>
        private IFieldModel CreateField(ItemField field, int levels)
        {
            if (levels-- < 0)
            {
                return null;
            }

            var type = TypeMap[field.GetType()];
            switch (type)
            {
                case FieldType.Text:
                    var textField = (TextField) field;
                    return new StringFieldModel
                           {
                               Values = textField.Values
                           };
                case FieldType.Number:
                    var numberField = (NumberField) field;
                    return new NumberFieldModel
                           {
                               Values = numberField.Values
                           };
                case FieldType.Date:
                    var dateField = (DateField) field;
                    return new DateFieldModel
                           {
                               Values = dateField.Values
                           };
                case FieldType.Keyword:
                    var keywordField = (KeywordField) field;
                    return new KeywordFieldModel
                           {
                               Values = keywordField.Values.Select(x => new KeywordModel
                                                                        {
                                                                            TcmUri = x.Id,
                                                                            Key = x.Key,
                                                                            Title = x.Title,
                                                                            SchemaName = x.MetadataSchema != null
                                                                                             ? x.MetadataSchema.Title
                                                                                             : null,
                                                                            Metadata = MapItemFields(x.Metadata, x.MetadataSchema, levels)
                                                                        }).ToArray()
                           };
                case FieldType.Embedded:
                    var embeddedField = (EmbeddedSchemaField) field;
                    return new EmbeddedFieldModel
                           {
                               Values = embeddedField.Values.Select(x => MapItemFields(x, levels))
                           };
                case FieldType.Link:
                    var linkField = (ComponentLinkField) field;
                    return new ComponentLinkFieldModel
                           {
                               Values = linkField.Values.Select(x => new ComponentModel
                                                                     {
                                                                         TcmUri = x.Id,
                                                                         Title = x.Title,
                                                                         SchemaName = x.Schema.Title,
                                                                         Content = MapItemFields(x.Content, x.Schema, levels),
                                                                         Metadata = MapItemFields(x.Metadata, x.MetadataSchema, levels),
                                                                         BinaryUrl = x.BinaryContent != null
                                                                                         ? x.PublishBinary(_engine, _package)
                                                                                         : null
                                                                     }).ToArray()
                           };
                default:
                    return null;
            }
        }

        /// <summary>
        /// Field types used for mapping
        /// </summary>
        private enum FieldType
        {
            Text,
            Number,
            Date,
            Keyword,
            Embedded,
            Link
        }
    }
}
