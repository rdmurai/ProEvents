using System.Threading.Tasks;
using ProEvents.Domain;

namespace ProEvents.Persistence.Interfaces
{
    public interface ILecturersPersist
    {
        Task<Lecturer[]> GetAllLecturersByNameAsync(string name, bool includeEvents);
        Task<Lecturer[]> GetAllLecturerssAsync(bool includeEvents);
        Task<Lecturer> GetLecturerByIdAsync(int lecturerid, bool includeEvents);
    }
}