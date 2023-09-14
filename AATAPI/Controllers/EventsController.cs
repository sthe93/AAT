using AATAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using AATAPI.Repositories;
using System;
using System.Threading.Tasks;

namespace AATAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventsRepository _eventsRepository;

        public EventsController(IEventsRepository eventsRepository)
        {
            _eventsRepository = eventsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEvents()
        {
            var events = await _eventsRepository.GetAllEventsAsync();
            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventById(int id)
        {
            var @event = await _eventsRepository.GetEventByIdAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            return Ok(@event);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(Event eventToAdd)
        {
            try
            {
                // Attempt to add the event
                await _eventsRepository.AddEventAsync(eventToAdd);

                // Return a 201 Created response with the newly created event
                return CreatedAtAction(nameof(GetEventById), new { id = eventToAdd.Id }, eventToAdd);
            }
            catch (Exception ex)
            {
                // Handle exceptions and return a 400 Bad Request response with an error message
                return BadRequest($"Failed to create the event. {ex.Message}");
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, Event eventToUpdate)
        {
            if (id != eventToUpdate.Id)
            {
                return BadRequest();
            }

            // Attempt to update the event
            try
            {
                await _eventsRepository.UpdateEventAsync(eventToUpdate);

                // Retrieve the updated event from the repository
                var updatedEvent = await _eventsRepository.GetEventByIdAsync(id);

                if (updatedEvent == null)
                {
                    return NotFound();
                }

                return Ok(updatedEvent); // Return the updated event as JSON
            }
            catch (Exception ex)
            {
                // Handle exceptions and return a 400 Bad Request response with an error message
                return BadRequest($"Failed to update the event. {ex.Message}");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            await _eventsRepository.DeleteEventAsync(id);
            return NoContent();
        }
    }
}
