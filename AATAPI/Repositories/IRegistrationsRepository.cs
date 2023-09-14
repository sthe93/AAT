using AATMODELS.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AATAPI.Repositories
{
    public interface IRegistrationsRepository
    {
        Task<List<RegistrationDto>> GetRegistrationsAsync();
        Task<bool> CheckUserRegistration(int eventId, Guid userId);
        Task<RegistrationDto> RegisterForEvent(RegistrationDto registration);
        Task<bool> IsUserRegisteredAsync(int eventId, Guid userId);
        Task<RegistrationDto> AddRegistrationAsync(RegistrationDto registration);
        Task<RegistrationDto> RegisterForEventAsync(RegistrationDto registration);

    }
}
