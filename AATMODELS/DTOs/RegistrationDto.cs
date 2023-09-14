using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AATMODELS.DTOs
{
    public class RegistrationDto
    {
        public int Id { get; set; }

        [JsonPropertyName("eventId")]
        public int EventId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("userId")]
        public Guid UserId { get; set; }

        [JsonPropertyName("registrationDate")]
        public DateTime RegistrationDate { get; set; }

        [JsonPropertyName("referenceNumber")]
        public string ReferenceNumber { get; set; }
        //public Guid UserId { get; set; }
    }
}
