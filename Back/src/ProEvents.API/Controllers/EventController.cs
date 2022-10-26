using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEvents.Application.Dtos;
using ProEvents.Application.Interfaces;

namespace ProEvents.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {

        private readonly IEventsService _eventsService;
        private readonly ILogger<EventsController> _logger;
        public EventsController(IEventsService eventsService, ILogger<EventsController> logger)
        {
            _eventsService = eventsService;

            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var existEvent = await _eventsService.GetAllEventsAsync(true);

                if (existEvent == null) return NoContent();

                return Ok(existEvent);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Error when retrieving events. Error:{ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var existEvent = await _eventsService.GetEventByIdAsync(id, true);

                if (existEvent == null) return NoContent();

                return Ok(existEvent);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Error when retrieving events. Error:{ex.Message}");
            }
        }

        [HttpGet("theme/{theme}")]
        public async Task<IActionResult> GetByTheme(string theme)
        {
            try
            {
                var existEvent = await _eventsService.GetAllEventsByThemeAsync(theme, true);

                if (existEvent == null) return NoContent();

                return Ok(existEvent);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Error when retrieving events. Error:{ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(EventDto model)
        {
            try
            {
                var existEvent = await _eventsService.AddEvent(model);

                if (existEvent == null) return BadRequest("Error when Adding Event");

                return Ok(existEvent);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Error when adding events. Error:{ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, EventDto model)
        {
            try
            {
                var existEvent = await _eventsService.UpdateEvent(id, model);

                if (existEvent == null) return BadRequest("Error when Updating Event");

                return Ok(existEvent);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Error when updating events. Error:{ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (!await _eventsService.DeleteEvent(id)) return BadRequest("Error when Deleting Event");

                return Ok(new { isDeleted = true });
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Error when deleting events. Error:{ex.Message}");
            }
        }

    }
}
