using CouchbaseDelivery.Data.ContentModel.Contract.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CouchbaseDelivery.Data.ContentModel.Serializers
{
    /// <summary>
    /// Helper serialiser class to convert published data to JSON and back
    /// </summary>
    public static class ContentSerializer
    {
        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
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
        public static string Serialize(IPublishedDataModel model)
        {
            return JsonConvert.SerializeObject(model, Settings);
        }

        /// <summary>
        /// Deserialise published data to from JSON
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string json) where T : IPublishedDataModel
        {
            return JsonConvert.DeserializeObject<T>(json, Settings);
        }
    }
}
