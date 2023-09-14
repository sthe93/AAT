using AATAPI.Entities;
using AATAPI.Repositories;
using AATMODELS.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AATAPI.Services
{
    public class RegistrationService 
    {
        private readonly IRegistrationsRepository _repository;

        public RegistrationService(IRegistrationsRepository repository)
        {
            _repository = repository;
        }
        public class RegistrationFailedException : Exception
        {
            public RegistrationFailedException() { }

            public RegistrationFailedException(string message) : base(message) { }

            public RegistrationFailedException(string message, Exception innerException) : base(message, innerException) { }
        }
        public async Task<List<RegistrationDto>> GetRegistrationsAsync()
        {
            return await _repository.GetRegistrationsAsync();
        }

        public async Task<bool> IsUserRegisteredAsync(int eventId, Guid userId)
        {
            return await _repository.IsUserRegisteredAsync(eventId, userId);
        }

        public async Task<RegistrationDto> RegisterForEventAsync(RegistrationDto registrationDto)
        {
            // Implement your code to register for an event here
            try
            {
                // Pass the DTO directly to the repository method
                var addedEntity = await _repository.RegisterForEventAsync(registrationDto);

                // Convert the added entity back to a DTO
                var addedDto = new RegistrationDto
                {
                    Id = addedEntity.Id,
                    EventId = addedEntity.EventId,
                    UserId = addedEntity.UserId,
                    RegistrationDate = addedEntity.RegistrationDate,
                    ReferenceNumber = addedEntity.ReferenceNumber,
                    Name = addedEntity.Name,
                    Email = addedEntity.Email
                };

                return addedDto;
            }
            catch (Exception ex)
            {
                // Handle errors and possibly log them
                throw new RegistrationFailedException("Failed to add registration", ex);
            }
        }
    }
}
