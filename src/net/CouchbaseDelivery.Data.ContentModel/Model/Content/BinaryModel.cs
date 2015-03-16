using CouchbaseDelivery.Data.ContentModel.Contract.Model.Content;

namespace CouchbaseDelivery.Data.ContentModel.Model.Content
{
    /// <summary>
    /// Represents the published out model for a dynamic presentation
    /// </summary>
    public class BinaryModel : PublishedDataModel, IBinaryModel
    {
        public string TcmUri { get; set; }

        public string Title { get; set; }

        public string SchemaName { get; set; }

        public string PublishedUrl { get; set; }

        public string Content { get; set; }
    }
}
