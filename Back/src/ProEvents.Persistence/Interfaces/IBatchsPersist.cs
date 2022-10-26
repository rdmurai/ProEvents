using System.Threading.Tasks;
using ProEvents.Domain;

namespace ProEvents.Persistence.Interfaces
{
    public interface IBatchsPersist
    {
        /// <summary>
        /// Return all batchs by Event ID
        /// </summary>
        /// <param name="eventId">Key from Event table</param>
        /// <returns></returns>
        Task<Batch[]> GetBatchByEventAsync(int eventId);

        /// <summary>
        /// Return 1 batch
        /// </summary>
        /// <param name="eventId">Key from Event table</param>
        /// <param name="batchId">Key from Batch table</param>
        /// <returns></returns>
        Task<Batch> GetBatchByIdsAsync(int eventId, int batchId);

    }
}