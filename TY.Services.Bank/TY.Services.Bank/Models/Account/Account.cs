using System;
using Newtonsoft.Json;

namespace TY.Services.Bank.Models.Account
{
    public class Account : IBaseEntity
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("isActive")]
        public bool IsActive { get; set; }

        [JsonProperty("createdAt")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public DateTime? UpdatedAt { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("age")]
        public int Age { get; set; }

        [JsonProperty("salt")]
        public string Salt { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("balance")]
        public decimal Balance { get; set; }
    }
}