using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.Contracts.Persistance
{
    public interface ILeaveTypeRepository : IGenericRepository<LeaveType>
    {
        Task<bool> IsLeaveTypeAvailable(int id);
        Task<bool> IsLeaveTypeUnique(string name);
    }
}