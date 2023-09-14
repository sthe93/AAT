using AATAPI.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AATAPI.Entities
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int TotalSeats { get; set; }
        public int AvailableSeats { get; set; }

        public ICollection<Registration>? Registrations { get; set; }
    }
}
