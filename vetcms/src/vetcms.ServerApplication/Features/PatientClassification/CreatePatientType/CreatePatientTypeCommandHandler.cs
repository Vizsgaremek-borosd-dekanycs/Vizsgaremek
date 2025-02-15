using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using vetcms.ServerApplication.Domain.Entity;
using vetcms.SharedModels.Features.IAM;
using vetcms.SharedModels.Features.PatientClassification;

namespace vetcms.ServerApplication.Features.PatientClassification.CreatePatientType
{
    internal class CreatePatientTypeCommandHandler(IMapper mapper) : IRequestHandler<CreatePatientTypeApiCommand, CreatePatientTypeApiCommandResponse>
    {
        public Task<CreatePatientTypeApiCommandResponse> Handle(CreatePatientTypeApiCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
