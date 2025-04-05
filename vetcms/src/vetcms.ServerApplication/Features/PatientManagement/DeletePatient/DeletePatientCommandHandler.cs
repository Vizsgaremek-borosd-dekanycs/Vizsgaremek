using AutoMapper;
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

namespace vetcms.ServerApplication.Features.PatientManagement.DeletePatient
{
    internal class DeletePatientCommandHandler(IPatientRepository patientRepository) : IRequestHandler<DeletePatientApiCommand, DeletePatientApiCommandResponse>
    {
        public async Task<DeletePatientApiCommandResponse> Handle(DeletePatientApiCommand request, CancellationToken cancellationToken)
        {
            List<int> nonExistentIds = new();
            request.Ids.ForEach(async id =>
            {
                if (!await patientRepository.ExistAsync(id))
                {
                    nonExistentIds.Add(id);
                }
            });

            if (nonExistentIds.Any())
            {
                return new DeletePatientApiCommandResponse(false)
                {
                    Message = $"Nem létező páciens ID(s): {string.Join(",", nonExistentIds)}"
                };
            }

            foreach (int id in request.Ids)
            {
                await patientRepository.DeleteAsync(id);
            }

            return new DeletePatientApiCommandResponse()
            {
                Success = true,
                Message = "Páciens(ek) sikeresen törölve!"
            };
        }
    }
}
