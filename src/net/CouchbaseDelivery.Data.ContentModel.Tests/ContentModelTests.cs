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
        public void CanSerialisePage()
        {
            var contentSerializer = new ContentSerializer<PagePublishedDataModel>();

            var page = ModelHelper.GetPublishedPageModel();

            var json = contentSerializer.Serialize(page);

            Assert.IsTrue(!string.IsNullOrEmpty(json));
        }

        [TestMethod]
        public void CanSerialisePresentation()
        {
            var contentSerializer = new ContentSerializer<PresentationPublishedDataModel>();

            var presentation = ModelHelper.GetPublishedPresentationModel();

            var json = contentSerializer.Serialize(presentation);

            Assert.IsTrue(!string.IsNullOrEmpty(json));
        }

        [TestMethod]
        public void CanRoundtripSerialisePage()
        {
            var contentSerializer = new ContentSerializer<PagePublishedDataModel>();

            var page = ModelHelper.GetPublishedPageModel();

            var json = contentSerializer.Serialize(page);

            var pageRoundtrip = contentSerializer.Deserialize(json);

            var jsonRoundtrip = contentSerializer.Serialize(pageRoundtrip);

            // Ensure the JSON matches the original serialisation
            Assert.IsTrue(json.Equals(jsonRoundtrip, StringComparison.Ordinal));
        }

        [TestMethod]
        public void CanRoundtripSerialisePresentation()
        {
            var contentSerializer = new ContentSerializer<PresentationPublishedDataModel>();

            var presentation = ModelHelper.GetPublishedPresentationModel();

            var json = contentSerializer.Serialize(presentation);

            var presentationRoundtrip = contentSerializer.Deserialize(json);

            var jsonRoundtrip = contentSerializer.Serialize(presentationRoundtrip);

            // Ensure the JSON matches the original serialisation
            Assert.IsTrue(json.Equals(jsonRoundtrip, StringComparison.Ordinal));
        }
    }
}
