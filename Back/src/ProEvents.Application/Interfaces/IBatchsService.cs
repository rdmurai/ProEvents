using System.Threading.Tasks;
using ProEvents.Application.Dtos;
using ProEvents.Domain;

namespace ProEvents.Application.Interfaces
{
    public interface IBatchsService
    {
        Task<BatchDto[]> SaveBatch(int eventId, BatchDto[] model);
        Task<bool> DeleteBatch(int eventId, int batchId);
        Task<BatchDto[]> GetBatchsByEventAsync(int eventId);
        Task<BatchDto> GetBatchByIdsAsync(int eventId, int batchId);

    }
}