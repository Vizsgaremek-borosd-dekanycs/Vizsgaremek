using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.ServerApplication.Common.Abstractions.Data;
using vetcms.ServerApplication.Domain.Entity.PatientManagement;
using vetcms.SharedModels.Features.PatientManagement;

namespace vetcms.ServerApplication.Features.PatientManagement.CreateTreatment
{
    internal class CreatePatientTreatmentCommandHandler(IMapper mapper, ITreatmentRepository treatmentRepository) : IRequestHandler<CreatePatientTreatmentApiCommand, CreatePatientTreatmentApiCommandResponse>
    {
        public async Task<CreatePatientTreatmentApiCommandResponse> Handle(CreatePatientTreatmentApiCommand request, CancellationToken cancellationToken)
        {
            Treatment newTreatment = mapper.Map<Treatment>(request.NewTreatment);
            await treatmentRepository.AddAsync(newTreatment);
            return await Task.FromResult(new CreatePatientTreatmentApiCommandResponse(true));
        }
    }
}
