//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SIMSData
{
    using System;
    using System.Collections.Generic;
    
    public partial class ContactPersonAddress
    {
        public int Id { get; set; }
        public int ContactPersonId { get; set; }
        public int ContactAddressId { get; set; }
        public int ContactAddressTypeId { get; set; }
        public System.DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime LastUpdated { get; set; }
        public string LastUpdatedBy { get; set; }
    
        public virtual ContactAddress ContactAddress { get; set; }
        public virtual ContactPerson ContactPerson { get; set; }
        public virtual Lookup Lookup { get; set; }
    }
}