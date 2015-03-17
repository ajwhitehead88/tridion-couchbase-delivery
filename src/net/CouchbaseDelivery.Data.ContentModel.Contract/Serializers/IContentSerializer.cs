using CouchbaseDelivery.Data.ContentModel.Contract.Model;

namespace CouchbaseDelivery.Data.ContentModel.Contract.Serializers
{
    /// <summary>
    /// Helper serialiser class to convert published data to JSON and back
    /// </summary>
    public interface IContentSerializer<T> where T : IPublishedDataModel
    {
        /// <summary>
        /// Serialise published data to JSON
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        string Serialize(T model);

        /// <summary>
        /// Deserialise published data to from JSON
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        T Deserialize(string json);
    }
}
