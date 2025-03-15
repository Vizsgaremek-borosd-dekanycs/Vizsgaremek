using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.ServerApplication.Common.Abstractions.Data;
using vetcms.ServerApplication.Domain.Entity.PatientManagement;
using vetcms.ServerApplication.Infrastructure.Presistence.Repository;
using vetcms.SharedModels.Features.PatientManagement;

namespace vetcms.ServerApplication.Features.PatientManagement.ModifyTreatment
{
    internal class ModifyPatientTreatmentCommandHandler(IMapper mapper, ITreatmentRepository treatmentRepository) : IRequestHandler<ModifyPatientTreatmentApiCommand, ModifyPatientTreatmentApiCommandResponse>
    {
        public async Task<ModifyPatientTreatmentApiCommandResponse> Handle(ModifyPatientTreatmentApiCommand request, CancellationToken cancellationToken)
        {
            if (!await treatmentRepository.ExistAsync(request.Id))
            {
                return new ModifyPatientTreatmentApiCommandResponse
                {
                    Success = false,
                    Message = "Nem létezik ilyen bejegyzés az adatbázisban."
                };
            }

            Treatment updatedTreatment = mapper.Map<Treatment>(request.TreatmentModel);
            updatedTreatment.Id = request.Id;
            await treatmentRepository.UpdateAsync(updatedTreatment);

            return new ModifyPatientTreatmentApiCommandResponse(true);
        }
    }
}
