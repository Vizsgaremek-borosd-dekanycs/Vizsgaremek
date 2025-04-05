using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.ServerApplication.Common.Abstractions.Data;
using vetcms.ServerApplication.Infrastructure.Presistence.Repository;
using vetcms.SharedModels.Features.PatientClassification;
using vetcms.SharedModels.Features.PatientManagement;

namespace vetcms.ServerApplication.Features.PatientManagement.DeleteTreatment
{
    internal class DeletePatientTreatmentCommandHandler(ITreatmentRepository treatmentRepository) : IRequestHandler<DeletePatientTreatmentApiCommand, DeletePatientTreatmentApiCommandResponse>
    {
        public async Task<DeletePatientTreatmentApiCommandResponse> Handle(DeletePatientTreatmentApiCommand request, CancellationToken cancellationToken)
        {
            List<int> nonExistentIds = new();
            request.Ids.ForEach(async id =>
            {
                if (!await treatmentRepository.ExistAsync(id))
                {
                    nonExistentIds.Add(id);
                }
            });

            if (nonExistentIds.Any())
            {
                return new DeletePatientTreatmentApiCommandResponse(false)
                {
                    Message = $"Nem létező kezelés ID(s): {string.Join(",", nonExistentIds)}"
                };
            }

            foreach (int id in request.Ids)
            {
                await treatmentRepository.DeleteAsync(id);
            }

            return new DeletePatientTreatmentApiCommandResponse()
            {
                Success = true,
                Message = "Kezelés(ek) sikeresen törölve!"
            };
        }
    }
}
