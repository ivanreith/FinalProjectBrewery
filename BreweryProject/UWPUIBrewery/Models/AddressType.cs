using System;
using System.Collections.Generic;

namespace BreweryLibraryClasses.Models
{
    public partial class AddressType
    {
        public AddressType()
        {
            SupplierAddress = new HashSet<SupplierAddress>();
        }

        public int AddressTypeId { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return AddressTypeId + ", " + Name ;
        }

        public virtual ICollection<SupplierAddress> SupplierAddress { get; set; }
    }
}
