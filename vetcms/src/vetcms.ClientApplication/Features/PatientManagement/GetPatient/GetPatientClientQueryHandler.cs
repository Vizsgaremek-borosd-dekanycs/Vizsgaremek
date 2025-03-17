using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FluentUI.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.ClientApplication.Common.Abstract;
using vetcms.ClientApplication.Features.IAM.GetUser;
using vetcms.ClientApplication.Features.PatientClassification.AnimalBreedList;
using vetcms.ClientApplication.Features.PatientManagement.ListPatients;
using vetcms.SharedModels.Common.Dto;
using vetcms.SharedModels.Features.PatientClassification;

namespace vetcms.ClientApplication.Features.PatientManagement.GetPatient
{
    internal class GetPatientClientQueryHandler(IMediator mediator, IDialogService dialogService, IMapper mapper) : IRequestHandler<GetPatientClientQuery, GetPatientClientQueryResponse>
    {
        public async Task<GetPatientClientQueryResponse> Handle(GetPatientClientQuery request, CancellationToken cancellationToken)
        {
            GetPatientClientQueryResponse getPatientClientQueryResponse = new GetPatientClientQueryResponse();
            getPatientClientQueryResponse.PatientModel = AnimalListClientQueryHandler.animals.FirstOrDefault(x => x.Id == request.PatientId);
            if (getPatientClientQueryResponse.PatientModel == null)
            {
                dialogService.ShowError("Patient not found.");
                return getPatientClientQueryResponse;
            }
            return await Task.FromResult(getPatientClientQueryResponse);
        }
    }

    //internal class GetPatientByIdApiQueryHandler : GenericApiCommandHandler<GetAnimalBreedByIdApiQuery, GetAnimalBreedByIdApiQueryResponse>
    //{
    //    public GetPatientByIdApiQueryHandler(IServiceScopeFactory serviceScopeFactory)
    //        : base(serviceScopeFactory)
    //    {
    //    }
    //}
}
