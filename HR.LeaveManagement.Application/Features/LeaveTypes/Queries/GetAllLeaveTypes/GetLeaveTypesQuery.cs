using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Queries.GetAllLeaveTypes
{
    public record GetLeaveTypesQuery : IRequest<List<LeaveTypeDto>>;
}


/*
 
Use class for traditional object-oriented programming where mutability, encapsulation, and reference equality are important.
Use record when working with immutable data-centric structures where value-based equality is the main focus.
 
*/