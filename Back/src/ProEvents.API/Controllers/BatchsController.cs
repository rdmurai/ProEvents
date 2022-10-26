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
    public class BatchsController : ControllerBase
    {

        private readonly IBatchsService _batchsService;
        private readonly ILogger<BatchsController> _logger;
        public BatchsController(IBatchsService batchsService, ILogger<BatchsController> logger)
        {
            _batchsService = batchsService;

            _logger = logger;
        }

        [HttpGet("{eventId}")]
        public async Task<IActionResult> GetById(int eventId)
        {
            try
            {
                var existBatch = await _batchsService.GetBatchsByEventAsync(eventId);

                if (existBatch == null) return NoContent();

                return Ok(existBatch);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Error when retrieving batches. Error:{ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> SaveBatch(int id, BatchDto[] models)
        {
            try
            {
                var existBatch = await _batchsService.SaveBatch(id, models);

                if (existBatch == null) return BadRequest("Error when Updating Batchs");

                return Ok(existBatch);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Error when updating Batchs. Error:{ex.Message}");
            }
        }

        [HttpDelete("{eventId}/{batchId}")]
        public async Task<IActionResult> Delete(int eventId, int batchId)
        {
            try
            {
                var batch = await _batchsService.GetBatchByIdsAsync(eventId, batchId);
                if (batch == null) return NoContent();

                return await _batchsService.DeleteBatch(batch.EventId, batch.Id) ? Ok(new { isDeleted = true }) : throw new Exception("Error while deleting batch!");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Error when deleting events. Error:{ex.Message}");
            }
        }

    }
}
