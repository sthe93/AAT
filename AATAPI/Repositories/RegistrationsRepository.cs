using AATAPI.Entities;
using AATMODELS.DTOs;
using AATAPI.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AATAPI.Repositories
{
    public class RegistrationsRepository : IRegistrationsRepository
    {
        private readonly List<RegistrationDto> registrations = new List<RegistrationDto>();
        private readonly AppDbContext _context; // Replace ApplicationDbContext with your actual context

        public RegistrationsRepository(AppDbContext context)
        {
            _context = context;
        }
        public Task<List<RegistrationDto>> GetRegistrationsAsync()
        {
            // Implement your code to fetch registrations here
            return Task.FromResult(registrations);
        }

        public async Task<bool> CheckUserRegistration(int eventId, Guid userId)
        {
            // Implement your code to check user registration here asynchronously
            var isRegistered = await Task.Run(() => registrations.Exists(r => r.EventId == eventId && r.UserId == userId));
            return isRegistered;
        }

        public async Task<RegistrationDto> RegisterForEvent(RegistrationDto registration)
        {
            // Implement your code to register for an event here asynchronously
            registration.ReferenceNumber = GenerateUniqueReferenceNumber();
            registrations.Add(registration);
            return await Task.FromResult(registration);
        }

        public async Task<bool> IsUserRegisteredAsync(int eventId, Guid userId)
        {
            // Implement your code to check if the user is registered for the event asynchronously
            var isRegistered = await Task.Run(() => registrations.Exists(r => r.EventId == eventId && r.UserId == userId));
            return isRegistered;
        }

        public async Task<RegistrationDto> AddRegistrationAsync(RegistrationDto registration)
        {
            // Implement your code to add a registration asynchronously
            // For example, you can save the registration to a database here
            // Ensure that you map the RegistrationDto to your entity before saving it to the database
            // Return the added registration (you may assign an ID if needed)

            // Example code (assuming you have an entity named RegistrationEntity):
            // var registrationEntity = MapDtoToEntity(registration);
            // _dbContext.Registrations.Add(registrationEntity);
            // await _dbContext.SaveChangesAsync();
            // var addedRegistration = MapEntityToDto(registrationEntity);
            // return addedRegistration;

            throw new NotImplementedException("AddRegistrationAsync method is not implemented.");
        }

        private string GenerateUniqueReferenceNumber()
        {
            // Generate a timestamp in the format "yyyyMMddHHmmssfff"
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");

            // Generate a random 4-digit number (between 1000 and 9999)
            int randomNum = new Random().Next(1000, 9999);

            // Combine the timestamp and random number to create a unique reference number
            string referenceNumber = timestamp + randomNum.ToString();

            return referenceNumber;
        }
        public async Task<RegistrationDto> RegisterForEventAsync(RegistrationDto registrationDto)
        {
            // Map RegistrationDto to Registration entity
            var registrationEntity = new Registration
            {
                EventId = registrationDto.EventId,
                UserId = registrationDto.UserId,
                RegistrationDate = DateTime.Now, // Set the registration date
                ReferenceNumber = registrationDto.ReferenceNumber,
                Name = registrationDto.Name,
                Email = registrationDto.Email
            };

            // Add the entity to the context
            _context.Registrations.Add(registrationEntity);

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Map the added entity back to RegistrationDto
            var addedDto = new RegistrationDto
            {
                Id = registrationEntity.Id,
                EventId = registrationEntity.EventId,
                UserId = registrationEntity.UserId,
                RegistrationDate = registrationEntity.RegistrationDate,
                ReferenceNumber = registrationEntity.ReferenceNumber,
                Name = registrationEntity.Name,
                Email = registrationEntity.Email
            };

            return addedDto;
        }
    }
}
