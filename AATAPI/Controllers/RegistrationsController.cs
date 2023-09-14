using AATAPI.Repositories;
using AATMODELS.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AATAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationsController : ControllerBase
    {
        private readonly IRegistrationsRepository _repository;

        public RegistrationsController(IRegistrationsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<RegistrationDto>>> GetRegistrationsAsync()
        {
            var registrations = await _repository.GetRegistrationsAsync();
            return Ok(registrations);
        }

        [HttpGet("check/{eventId}/{userId}")]
        public async Task<ActionResult<bool>> CheckUserRegistration(int eventId, Guid userId)
        {
            var isRegistered = await _repository.CheckUserRegistration(eventId, userId);
            return Ok(isRegistered);
        }

        [HttpPost]
        public async Task<ActionResult<RegistrationDto>> RegisterForEvent(RegistrationDto registration)
        {
            var registeredUser = await _repository.RegisterForEvent(registration);
            return Ok(registeredUser);
        }

    }
}
