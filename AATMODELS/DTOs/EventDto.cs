using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AATMODELS.DTOs
{
    public class EventDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int TotalSeats { get; set; }
        public int AvailableSeats { get; set; }
        // Add any other properties needed for communication
        // Add this property to map the "registrations" array
        public List<RegistrationDto> Registrations { get; set; }
    }
}