using System;
using System.Collections.Generic;

namespace CouchbaseDelivery.Tridion.ModularTemplates.Data.Models.Fields
{
    /// <summary>
    /// Models a single date time field
    /// </summary>
    public class DateFieldModel : IFieldModel
    {
        public IEnumerable<DateTime> Values { get; set; }
    }
}
