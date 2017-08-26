using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace SIMSService.Models
{
    public class LookupModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("value")]
        public string Value { get; set; }
        [JsonProperty("lookupTypeId")]
        public int LookupTypeId { get; set; }
        [JsonProperty("lookupType")]
        public LookupTypeModel LookupType { get; set; }
    }
}