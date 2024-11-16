using HR.LeaveManagement.Application.Contracts.Persistance;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
    {
        public LeaveTypeRepository(HrDbContext context):base(context)
        {
            
        }
        public async Task<bool> IsLeaveTypeAvailable(int id)
        {
            return await _dbContext.LeaveTypes.AnyAsync(t => t.Id == id);
        }

        public async Task<bool> IsLeaveTypeUnique(string name)
        {
            return await _dbContext.LeaveTypes.AnyAsync(t => t.Name != name);
        }
    }
}
