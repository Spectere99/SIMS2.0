using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace SIMSService.Models
{
    public class PermissionModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("permission")]
        public string Permission { get; set; }
        [JsonProperty("permissionModuleKey")]
        public int PermissionModuleKey { get; set; }
        [JsonProperty("canAccess")]
        public bool CanAccess { get; set; }
        [JsonProperty("canUpdate")]
        public bool CanUpdate { get; set; }
        [JsonProperty("canDelete")]
        public bool CanDelete { get; set; }
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