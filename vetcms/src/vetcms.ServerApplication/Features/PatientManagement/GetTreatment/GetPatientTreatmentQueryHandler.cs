using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.ServerApplication.Common.Abstractions.Data;
using vetcms.ServerApplication.Domain.Entity;
using vetcms.ServerApplication.Domain.Entity.PatientManagement;
using vetcms.ServerApplication.Infrastructure.Presistence.Repository;
using vetcms.SharedModels.Common.Dto;
using vetcms.SharedModels.Features.PatientClassification;
using vetcms.SharedModels.Features.PatientManagement;

namespace vetcms.ServerApplication.Features.PatientManagement.GetTreatment
{
    internal class GetPatientTreatmentQueryHandler(IMapper mapper, ITreatmentRepository treatmentRepository) : IRequestHandler<GetPaitentTreatmentApiQuery, GetPaitentTreatmentApiQueryResponse>
    {
        public async Task<GetPaitentTreatmentApiQueryResponse> Handle(GetPaitentTreatmentApiQuery request, CancellationToken cancellationToken)
        {
            if (await treatmentRepository.ExistAsync(request.TreatmentId))
            {
                Treatment treatment = await treatmentRepository.GetByIdAsync(request.TreatmentId);
                TreatmentDto treatmentDto = mapper.Map<TreatmentDto>(treatment);
                return new GetPaitentTreatmentApiQueryResponse(true)
                {
                    TreatmentModel = treatmentDto,
                };
            }
            else
            {
                return new GetPaitentTreatmentApiQueryResponse(false, "Kezelés nem található");
            }
        }
    }
}
