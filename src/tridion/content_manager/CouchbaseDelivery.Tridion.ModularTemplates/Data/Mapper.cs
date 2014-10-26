using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Tridion.ContentManager.ContentManagement;
using Tridion.ContentManager.ContentManagement.Fields;
using Tridion.ContentManager.Templating;
using CouchbaseDelivery.Tridion.ModularTemplates.Data.Models;
using CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Content;
using CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Fields;
using CouchbaseDelivery.Tridion.ModularTemplates.Helpers;

namespace CouchbaseDelivery.Tridion.ModularTemplates.Data
{
    public class Mapper
    {
        private static readonly IDictionary<Type, FieldType> TypeMap = new Dictionary<Type,FieldType>
            {
                {typeof(SingleLineTextField), FieldType.Text},
                {typeof(MultiLineTextField), FieldType.Text},
                {typeof(XhtmlField), FieldType.Text},
                {typeof(ExternalLinkField), FieldType.Text},
                {typeof(NumberField), FieldType.Number},
                {typeof(DateField), FieldType.Date},
                {typeof(EmbeddedSchemaField), FieldType.Embedded},
                {typeof(KeywordField), FieldType.Keyword},
                {typeof(ComponentLinkField), FieldType.Link},
                {typeof(MultimediaLinkField), FieldType.Link},
            };

        private readonly Engine _engine;

        public Mapper(Engine engine)
        {
            _engine = engine;
        }

        public IEnumerable<BaseFieldModel> MapItemFields(ItemFields fields)
        {
            // TODO: set the levels?
            return MapItemFields(fields, 3);
        }

        public IEnumerable<BaseFieldModel> MapItemFields(XmlElement xmlElement, Schema schema)
        {
            // TODO: set the default levels?
            return MapItemFields(xmlElement, schema, 3);
        }

        public IEnumerable<BaseFieldModel> MapItemFields(XmlElement xmlElement, Schema schema, int levels)
        {
            if (xmlElement == null || schema == null)
            {
                return null;
            }

            var fields = new ItemFields(xmlElement, schema);
            return MapItemFields(fields);
        }

        public IEnumerable<BaseFieldModel> MapItemFields(ItemFields fields, int levels)
        {
            return fields.Select(x => CreateField(x, levels));
        }

        private BaseFieldModel CreateField(ItemField field, int levels)
        {
            var type = TypeMap[field.GetType()];
            switch (type)
            {
                case FieldType.Text:
                    var textField = (TextField) field;
                    return new StringFieldModel
                        {
                            Key = textField.Name,
                            Values = textField.Values
                        };
                case FieldType.Number:
                    var numberField = (NumberField) field;
                    return new NumberFieldModel
                        {
                            Key = numberField.Name,
                            Values = numberField.Values
                        };
                case FieldType.Date:
                    var dateField = (DateField) field;
                    return new DateFieldModel
                        {
                            Key = dateField.Name,
                            Values = dateField.Values
                        };
                case FieldType.Keyword:
                    var keywordField = (KeywordField) field;
                    return new KeywordFieldModel
                        {
                            Key = keywordField.Name,
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
                            Key = embeddedField.Name,
                            Values = embeddedField.Values.Select(x => MapItemFields(x, levels)).ToArray()
                        };
                case FieldType.Link:
                    var linkField = (ComponentLinkField) field;
                    return new ComponentLinkFieldModel
                        {
                            Key = linkField.Name,
                            Values = linkField.Values.Select(x => new ComponentModel
                                {
                                    TcmUri = x.Id,
                                    Title = x.Title,
                                    SchemaName = x.Schema.Title,
                                    Content = MapItemFields(x.Content, x.Schema, levels - 1),
                                    Metadata = MapItemFields(x.Metadata, x.MetadataSchema, levels - 1),
                                    BinaryContent = x.BinaryContent != null
                                                        ? new BinaryContentModel
                                                            {
                                                                Filename = ComponentHelper.GetUniqueFilename(x),
                                                                BinaryContent = Convert.ToBase64String(x.BinaryContent.GetByteArray())
                                                            }
                                                        : null
                                }).ToArray()
                        };
                default:
                    return null;
            }
        }

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
