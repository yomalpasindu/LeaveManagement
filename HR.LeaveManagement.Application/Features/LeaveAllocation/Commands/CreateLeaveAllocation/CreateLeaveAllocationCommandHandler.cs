using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistance;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation
{
    public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, int>
    {
        private readonly ILeaveAllocationRepository _leaveAllocation;
        private readonly IMapper _mapper;

        public CreateLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocation, IMapper mapper)
        {
            _leaveAllocation = leaveAllocation;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateLeaveAllocationCommandValidator(_leaveAllocation);
            var validatorResult = await validator.ValidateAsync(request);

            if ((validatorResult.Errors.Any()))
                throw new BadRequestException("Invalid Leave Allocation", validatorResult);

            var data = _mapper.Map<Domain.LeaveAllocation>(request);
            await _leaveAllocation.CreateAsync(data);
            return data.Id;
        }
    }
}
