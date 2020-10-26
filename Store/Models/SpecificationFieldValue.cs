using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Models
{
    public class SpecificationFieldValue
    {
        public int SpecificationFieldValueID { get; set; }
        public virtual SpecificationField SpecificationField { get; set; }
        public virtual int ProductID { get; set; }

        public string Value { get; set; }
    }
}
