﻿using CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Content;
using CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Fields;
using CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Structure;
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
    public class Mapper
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

        public Mapper(Engine engine)
        {
            _engine = engine;
        }

        public FieldSetModel MapItemFields(XmlElement xmlElement, Schema schema, int levels)
        {
            if (xmlElement == null || schema == null)
            {
                return null;
            }

            var fields = new ItemFields(xmlElement, schema);
            return MapItemFields(fields, levels);
        }

        public FieldSetModel MapItemFields(ItemFields fields, int levels)
        {
            var fieldSet = new FieldSetModel();

            foreach (var field in fields)
            {
                fieldSet.Add(field.Name, CreateField(field, levels));
            }

            return fieldSet;
        }

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
                                                                         BinaryContent = x.BinaryContent != null
                                                                                             ? new BinaryContentModel
                                                                                               {
                                                                                                   Filename =
                                                                                                       ComponentExtensions.GetUniqueFilename
                                                                                                       (x),
                                                                                                   BinaryContent =
                                                                                                       Convert.ToBase64String(
                                                                                                                              x
                                                                                                                                  .BinaryContent
                                                                                                                                  .GetByteArray
                                                                                                                                  ())
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
