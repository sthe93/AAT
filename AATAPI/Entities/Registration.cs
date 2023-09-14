using AATMODELS.DTOs;
using System;
using System.ComponentModel.DataAnnotations;

namespace AATAPI.Entities
{
    public class Registration
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public Guid UserId { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string ReferenceNumber { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public Event Event { get; set; }
        public Guid UserIdentifier { get; set; }
    }
}
