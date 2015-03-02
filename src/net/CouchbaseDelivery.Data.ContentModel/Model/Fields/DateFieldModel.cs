using System;
using System.Collections.Generic;

namespace CouchbaseDelivery.Data.ContentModel.Model.Fields
{
    /// <summary>
    /// Models a single date time field
    /// </summary>
    public class DateFieldModel : IFieldModel
    {
        public IEnumerable<DateTime> Values { get; set; }
    }
}
