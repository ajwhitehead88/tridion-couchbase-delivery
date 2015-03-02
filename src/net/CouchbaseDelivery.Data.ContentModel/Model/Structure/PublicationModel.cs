﻿namespace CouchbaseDelivery.Data.ContentModel.Model.Structure
{
    /// <summary>
    /// Models a publication
    /// </summary>
    public class PublicationModel : IdentifiableObjectModel
    {
        public string PublicationUrl { get; set; }

        public string MultimediaUrl { get; set; }

        public FieldSetModel Metadata { get; set; }
    }
}