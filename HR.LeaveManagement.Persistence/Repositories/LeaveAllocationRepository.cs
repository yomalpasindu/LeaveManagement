using HR.LeaveManagement.Application.Contracts.Persistance;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
    {
        public LeaveAllocationRepository(HrDbContext context) : base(context)
        {

        }
        public async Task AddAllocations(List<LeaveAllocation> allocations)
        {
            await _dbContext.AddRangeAsync(allocations);//AddRangeAsync allowes to pass list of objects
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> AllocationExists(string userId, int leaveTypeId, int period)
        {
            return await _dbContext.LeaveAllocations.AnyAsync(p => p.EmployeeId == userId
            && p.LeaveTypeId == leaveTypeId
            && p.Period == period);
        }

        public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id)
        {
            var leaveAllocation = await _dbContext.LeaveAllocations.Include(p => p.LeaveType).FirstOrDefaultAsync(p => p.Id == id);
            return leaveAllocation;
        }

        public async Task<List<LeaveAllocation>> GetLeaveAllocationWithDetails()
        {
            var leaveAllocations=await _dbContext.LeaveAllocations.Include(p=>p.LeaveType).ToListAsync();
            return leaveAllocations;
        }

        public async Task<List<LeaveAllocation>> GetLeaveAllocationWithDetails(string userId)
        {
            var leaveAllocations = await _dbContext.LeaveAllocations.Where(p=>p.EmployeeId==userId)
                .Include(p => p.LeaveType).ToListAsync();
            return leaveAllocations;
        }

        public async Task<LeaveAllocation> GetUserAllocations(string userId, int leaveTypeId)
        {
            var leaveAllocations = await _dbContext.LeaveAllocations.FirstOrDefaultAsync(p => p.EmployeeId == userId && p.LeaveTypeId==leaveTypeId);
            return leaveAllocations;
        }
    }
}
