using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProEvents.Application.Dtos;
using ProEvents.Application.Interfaces;
using ProEvents.Domain;
using ProEvents.Persistence.Interfaces;

namespace ProEvents.Application.Services
{
    public class BatchsService : IBatchsService

    {
        private readonly IProEventsPersist _proEventsPersist;
        private readonly IBatchsPersist _batchPersist;
        private readonly IMapper _mapper;

        public BatchsService(IProEventsPersist proEventsPersist, IBatchsPersist batchPersist, IMapper mapper)
        {
            _batchPersist = batchPersist;
            _proEventsPersist = proEventsPersist;
            _mapper = mapper;
        }

        public async Task<bool> DeleteBatch(int eventId, int batchId)
        {
            try
            {
                var existBatch = await _batchPersist.GetBatchByIdsAsync(eventId, batchId);

                if (existBatch == null) throw new Exception("Batch Not Found");

                _proEventsPersist.Delete<Batch>(existBatch);
                return await _proEventsPersist.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<BatchDto[]> GetBatchsByEventAsync(int eventId)
        {
            try
            {
                var existBatch = await _batchPersist.GetBatchByEventAsync(eventId);
                if (existBatch == null) return null;
                var res = _mapper.Map<BatchDto[]>(existBatch);
                return res;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<BatchDto> GetBatchByIdsAsync(int eventId, int batchId)
        {
            try
            {
                var existBatch = await _batchPersist.GetBatchByIdsAsync(eventId, batchId);
                if (existBatch == null) return null;

                var res = _mapper.Map<BatchDto>(existBatch);
                return res;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task AddBatch(int eventoId, BatchDto model)
        {
            try
            {
                var lote = _mapper.Map<Batch>(model);
                lote.EventId = eventoId;

                _proEventsPersist.Add<Batch>(lote);

                await _proEventsPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<BatchDto[]> SaveBatch(int eventId, BatchDto[] models)
        {
            try
            {
                var batchs = await _batchPersist.GetBatchByEventAsync(eventId);
                if (batchs == null) return null;

                foreach (var model in models)
                {
                    if (model.Id == 0)
                    {
                        await AddBatch(eventId, model);
                    }
                    else
                    {
                        var lote = batchs.FirstOrDefault(lote => lote.Id == model.Id);
                        model.EventId = eventId;

                        _mapper.Map(model, lote);

                        _proEventsPersist.Update<Batch>(lote);

                        await _proEventsPersist.SaveChangesAsync();
                    }
                }

                var batchReturn = _batchPersist.GetBatchByEventAsync(eventId);

                return _mapper.Map<BatchDto[]>(batchReturn);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}