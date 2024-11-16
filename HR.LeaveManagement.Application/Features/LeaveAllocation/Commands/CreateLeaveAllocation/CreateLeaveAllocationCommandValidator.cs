using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistance;
using HR.LeaveManagement.Application.Features.LeaveTypes.Commands.CreateLeaveType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation
{
    public class CreateLeaveAllocationCommandValidator : AbstractValidator<CreateLeaveAllocationCommand>
    {
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;

        public CreateLeaveAllocationCommandValidator(ILeaveAllocationRepository leaveAllocationRepository)
        {
            _leaveAllocationRepository = leaveAllocationRepository;
            
            RuleFor(p=>p.LeaveTypeId)
                .NotEmpty()
                .WithMessage("{PropertyName} is required");

            RuleFor(p => p.Period)
                .LessThan(5).WithMessage("{PropertyName} should be less than 5")
                .GreaterThan(1).WithMessage("{PropertyName} should be grater than 1");
        }
    }
}
