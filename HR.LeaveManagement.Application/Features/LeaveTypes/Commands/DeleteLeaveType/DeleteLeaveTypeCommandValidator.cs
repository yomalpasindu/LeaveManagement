using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Commands.DeleteLeaveType
{
    public class DeleteLeaveTypeCommandValidator:AbstractValidator<DeleteLeaveTypeCommand>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public DeleteLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            RuleFor(p=>p.Id)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull();

            RuleFor(p => p)
                .MustAsync(LeaveTypeAvailable)
                .WithMessage("Leave Type does not exists");
            
            _leaveTypeRepository = leaveTypeRepository;
        }

        private async Task<bool> LeaveTypeAvailable(DeleteLeaveTypeCommand command, CancellationToken token)
        {
            var leaveType= await _leaveTypeRepository.GetByIdAsync(command.Id);
            return leaveType != null;
        }
    }
}
