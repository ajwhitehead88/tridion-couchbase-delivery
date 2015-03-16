using CouchbaseDelivery.Data.ContentModel.Contract.Model.Content;
using CouchbaseDelivery.Data.ContentModel.Contract.Model.Structure;
using CouchbaseDelivery.Data.ContentModel.Model.Content;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CouchbaseDelivery.Data.ContentModel.Model.Structure
{
    /// <summary>
    /// Models a structure group
    /// </summary>
    public class StructureGroupModel : IdentifiableObjectModel, IStructureGroupModel
    {
        public string PublishedUrl { get; set; }

        public IEnumerable<FieldModel> Metadata { get; set; }

        [JsonIgnore]
        IEnumerable<IFieldModel> IStructureGroupModel.Metadata { get { return Metadata; } }
    }
}
