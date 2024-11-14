using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Commands.UpdateLeaveType
{
    public class UpdateLeaveTypeCommandValidator:AbstractValidator<UpdateLeaveTypeCommand>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public UpdateLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            RuleFor(p => p.Id)
                .NotNull()
                .MustAsync(LeaveTypeMustExists);

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .MaximumLength(70).WithMessage("{PropertyName} lenth should be less than 70 charactors");

            RuleFor(p => p.DefaultDays)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .GreaterThan(100).WithMessage("{PropertyName} cannot exceed 100")
                .LessThan(1).WithMessage("{PropertyName} cannot ne less than 1");

            RuleFor(p => p)
                .MustAsync(LeaveTypeNameUnique)
                .WithMessage("Leave type is already exists");
            
            _leaveTypeRepository = leaveTypeRepository;
        }

        private async Task<bool> LeaveTypeNameUnique(UpdateLeaveTypeCommand command, CancellationToken token)
        {
            return await _leaveTypeRepository.IsLeaveTypeUnique(command.Name);
        }
        private async Task<bool> LeaveTypeMustExists(int id, CancellationToken token)
        {
            var leaveType= await _leaveTypeRepository.GetByIdAsync(id);
            return leaveType != null;
        }
    }
}
