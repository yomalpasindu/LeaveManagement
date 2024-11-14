﻿using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistance;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Commands.UpdateLeaveType
{
    public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, Unit>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;
        private readonly IAppLogger<UpdateLeaveTypeCommandHandler> _logger; 

        public UpdateLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper,IAppLogger<UpdateLeaveTypeCommandHandler> logger)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            //validate incoming data
            var validator = new UpdateLeaveTypeCommandValidator(_leaveTypeRepository);
            var validationResult=await validator.ValidateAsync(request);

            if (validationResult.Errors.Any()) 
            {
                _logger.LogWarning("Validation errors in update request for {0} - {1}",nameof(LeaveTypes),request.Id);
                throw new BadRequestException("Invalid Leave Type", validationResult); 
            }
            //mapping objects
            var data = _mapper.Map<Domain.LeaveType>(request);
            //add to db
            await _leaveTypeRepository.UpdateAsync(data);
            //return
            return Unit.Value;
        }
    }
}
