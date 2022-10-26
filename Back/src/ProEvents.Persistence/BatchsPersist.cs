using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEvents.Domain;
using ProEvents.Persistence.Context;
using ProEvents.Persistence.Interfaces;

namespace ProEvents.Persistence
{
    public class BatchsPersist : IBatchsPersist
    {
        private readonly ProEventsContext _context;

        public BatchsPersist(ProEventsContext context)
        {
            _context = context;
        }

        public async Task<Batch[]> GetBatchByEventAsync(int eventId)
        {
            IQueryable<Batch> query = _context.Batchs;

            query = query.AsNoTracking()
            .Where(batch => batch.EventId == eventId);
            return await query.ToArrayAsync();
        }

        public async Task<Batch> GetBatchByIdsAsync(int eventId, int batchId)
        {
            IQueryable<Batch> query = _context.Batchs;
            query = query.AsNoTracking()
                         .Where(batch => batch.EventId == eventId
                                && batch.Id == batchId);

            return await query.FirstOrDefaultAsync();
        }


    }
}