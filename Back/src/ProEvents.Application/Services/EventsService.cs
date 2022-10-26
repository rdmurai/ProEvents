using System;
using System.Threading.Tasks;
using AutoMapper;
using ProEvents.Application.Dtos;
using ProEvents.Application.Interfaces;
using ProEvents.Domain;
using ProEvents.Persistence.Interfaces;

namespace ProEvents.Application.Services
{
    public class EventsService : IEventsService

    {
        private readonly IProEventsPersist _proEventsPersist;
        private readonly IEventsPersist _eventPersist;
        private readonly IMapper _mapper;

        public EventsService(IProEventsPersist proEventsPersist, IEventsPersist eventPersist, IMapper mapper)
        {
            _eventPersist = eventPersist;
            _proEventsPersist = proEventsPersist;
            _mapper = mapper;
        }

        public async Task<EventDto> AddEvent(EventDto model)
        {
            try
            {
                var ev = _mapper.Map<Event>(model);
                _proEventsPersist.Add<Event>(ev);

                if (await _proEventsPersist.SaveChangesAsync())
                {
                    var res = await _eventPersist.GetEventByIdAsync(ev.Id, false);
                    return _mapper.Map<EventDto>(res);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteEvent(int eventId)
        {
            try
            {
                var existEvent = await _eventPersist.GetEventByIdAsync(eventId, false);

                if (existEvent == null) throw new Exception("Event Not Found");

                _proEventsPersist.Delete<Event>(existEvent);
                return await _proEventsPersist.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventDto> UpdateEvent(int eventId, EventDto model)
        {
            try
            {
                var existEvent = await _eventPersist.GetEventByIdAsync(eventId, false);

                if (existEvent == null) return null;
                model.Id = eventId;

                _mapper.Map(model, existEvent);

                _proEventsPersist.Update<Event>(existEvent);
                if (await _proEventsPersist.SaveChangesAsync())
                {
                    var res = await _eventPersist.GetEventByIdAsync(existEvent.Id, false);
                    return _mapper.Map<EventDto>(res);
                }
                return null;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventDto[]> GetAllEventsAsync(bool includeLecturers = false)
        {
            try
            {
                var existEvent = await _eventPersist.GetAllEventsAsync(includeLecturers);
                if (existEvent == null) return null;
                var res = _mapper.Map<EventDto[]>(existEvent);
                return res;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<EventDto[]> GetAllEventsByThemeAsync(string theme, bool includeLecturers = false)
        {
            try
            {
                var existEvent = await _eventPersist.GetAllEventsByThemeAsync(theme, includeLecturers);
                if (existEvent == null) return null;
                var res = _mapper.Map<EventDto[]>(existEvent);
                return res;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<EventDto> GetEventByIdAsync(int eventId, bool includeLecturers = false)
        {
            try
            {
                var existEvent = await _eventPersist.GetEventByIdAsync(eventId, includeLecturers);
                if (existEvent == null) return null;

                var res = _mapper.Map<EventDto>(existEvent);
                return res;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

    }
}