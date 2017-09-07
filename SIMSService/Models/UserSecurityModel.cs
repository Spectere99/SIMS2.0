using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace SIMSService.Models
{
    public class UserSecurityModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("userId")]
        public int UserId { get; set; }
        [JsonProperty("user")]
        public UserModel User { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonProperty("isLocked")]
        public Boolean IsLocked { get; set; }
        [JsonProperty("loginAttempts")]
        public int LoginAttempts { get; set; }
        [JsonProperty("token")]
        public string Token { get; set; }
        [JsonProperty("tokenExpires")]
        public DateTime TokenExpires { get; set; }
        [JsonProperty("created")]
        public DateTime Created { get; set; }
        [JsonProperty("LastUpdated")]
        public DateTime LastUpdated { get; set; }
    }
}