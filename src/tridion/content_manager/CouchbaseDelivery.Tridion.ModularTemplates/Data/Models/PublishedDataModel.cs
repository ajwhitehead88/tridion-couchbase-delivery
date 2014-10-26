using System;
using System.Runtime.Serialization;
using CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Structure;

namespace CouchbaseDelivery.Tridion.ModularTemplates.Data.Models
{
    [DataContract]
    [KnownType(typeof(PageModel))]
    public class PublishedDataModel
    {
        [DataMember(IsRequired = true)]
        public PageModel Page { get; set; }

        [DataMember(IsRequired = true)]
        public StructureGroupModel Parent { get; set; }

        [DataMember(IsRequired = true)]
        public PublicationModel Publication { get; set; }

        [DataMember(IsRequired = true)]
        public DateTime PublishDate { get; set; }
    }
}
