using CouchbaseDelivery.Data.ContentModel.Contract.Model;
using CouchbaseDelivery.Data.ContentModel.Contract.Serializers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CouchbaseDelivery.Data.ContentModel.Serializers
{
    /// <summary>
    /// Helper serialiser class to convert published data to JSON and back
    /// </summary>
    public class ContentSerializer<T> : IContentSerializer<T> where T : IPublishedDataModel
    {
        private readonly JsonSerializerSettings _settings = new JsonSerializerSettings
                                                            {
                                                                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                                                                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                                                                Formatting = Formatting.Indented,
                                                                NullValueHandling = NullValueHandling.Ignore,
                                                                TypeNameHandling = TypeNameHandling.None
                                                            };

        /// <summary>
        /// Serialise published data to JSON
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Serialize(T model)
        {
            return JsonConvert.SerializeObject(model, _settings);
        }

        /// <summary>
        /// Deserialise published data to from JSON
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public T Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, _settings);
        }
    }
}
