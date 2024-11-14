using HR.LeaveManagement.Application.Contracts.Persistance;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
    {
        public LeaveRequestRepository(HrDbContext context) : base(context)
        {

        }
        public async Task<LeaveRequest> GetLeaveRequestWithDetails(int id)
        {
            var leaveRequest= await _dbContext.LeaveRequests
                .Include(p=>p.LeaveType) // innder join
                .FirstOrDefaultAsync(p=>p.Id == id);
            return leaveRequest;
        }

        public async Task<List<LeaveRequest>> GetLeaveRequestWithDetails()
        {
            var leaveRequests = await _dbContext.LeaveRequests
                .Include(p => p.LeaveType) //helps to fetch data from LeaveType table (innder join)
                .ToListAsync();
            return leaveRequests;
        }

        public Task<List<LeaveRequest>> GetLeaveRequestWithDetails(string userId)
        {
            var leaveRequests=_dbContext.LeaveRequests.Where(p=>p.RequestingEmployeeId==userId)
                .Include(p=>p.LeaveType)
                .ToListAsync() ;
            return leaveRequests;
        }
    }
}
