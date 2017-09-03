using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace SIMSService.Models
{
    public class ContactPersonAddressModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("contactPersonId")]
        public int contactPersonId { get; set; }
        [JsonProperty("contactPerson")]
        public ContactPersonModel ContactPerson { get; set; }
        [JsonProperty("contactAddressId")]
        public int ContactAddressId { get; set; }
        [JsonProperty("contactAddress")]
        public ContactAddressModel ContactAddress { get; set; }
        [JsonProperty("contactAddressTypeId")]
        public int ContactAddressTypeId { get; set; }
        [JsonProperty("contactAddressType")]
        public LookupModel ContactAddressType { get; set; }
        [JsonProperty("isActive")]
        public bool IsActive { get; set; }
        [JsonProperty("created")]
        public DateTime Created { get; set; }
        [JsonProperty("createdBy")]
        public string CreatedBy { get; set; }
        [JsonProperty("lastUpdated")]
        public DateTime LastUpdated { get; set; }
        [JsonProperty("lastUpdatedBy")]
        public string LastUpdatedBy { get; set; }
    }
}