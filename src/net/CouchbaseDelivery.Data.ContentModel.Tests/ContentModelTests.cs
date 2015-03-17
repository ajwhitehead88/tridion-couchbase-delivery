using CouchbaseDelivery.Data.ContentModel.Model;
using CouchbaseDelivery.Data.ContentModel.Serializers;
using CouchbaseDelivery.Data.ContentModel.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CouchbaseDelivery.Data.ContentModel.Tests
{
    [TestClass]
    public class ContentModelTests
    {
        [TestMethod]
        public void CanRoundtripSerialisePage()
        {
            var contentSerializer = new ContentSerializer<PagePublishedDataModel>();

            var page = ModelHelper.GetPublishedPageModel();

            var json = contentSerializer.Serialize(page);

            var pageRoundtrip = contentSerializer.Deserialize(json);

            var jsonRoundtrip = contentSerializer.Serialize(pageRoundtrip);

            Assert.IsTrue(json.Equals(jsonRoundtrip, StringComparison.Ordinal));
        }
    }
}
