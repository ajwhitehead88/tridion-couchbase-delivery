using System;
using System.Collections.Generic;

namespace CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Fields
{
    public class DateFieldModel : IFieldModel
    {
        public IEnumerable<DateTime> Values { get; set; }
    }
}
