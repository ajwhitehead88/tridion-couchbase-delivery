using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Fields
{
    [DataContract]
    [KnownType(typeof(DateFieldModel))]
    public class DateFieldModel :BaseFieldModel
    {
        [DataMember(IsRequired = true)]
        public IEnumerable<DateTime> Values { get; set; }
    }
}
