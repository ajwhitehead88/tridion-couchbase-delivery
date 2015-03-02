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
            var page = ModelHelper.GetPublishedPageModel();

            var json = ContentSerializer.Serialize(page);

            var pageRoundtrip = ContentSerializer.Deserialize<PagePublishedDataModel>(json);

            var jsonRoundtrip = ContentSerializer.Serialize(pageRoundtrip);

            Assert.IsTrue(json.Equals(jsonRoundtrip, StringComparison.Ordinal));
        }
    }
}
