//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Models.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class bill
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public bill()
        {
            this.billDetails = new HashSet<billDetail>();
        }
    
        public int id { get; set; }
        public Nullable<int> customerID { get; set; }
        public Nullable<int> amountOfPeople { get; set; }
        public Nullable<int> child { get; set; }
        public string payment { get; set; }
        public Nullable<bool> paystatus { get; set; }
        public Nullable<bool> confirm { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<billDetail> billDetails { get; set; }
        public virtual customer customer { get; set; }
    }
}