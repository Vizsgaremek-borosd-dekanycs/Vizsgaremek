using AutoMapper;
using MediatR;
using Microsoft.FluentUI.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.ClientApplication.Features.IAM.UserList;
using vetcms.SharedModels.Common.Dto;
using vetcms.SharedModels.Features.PatientClassification;

namespace vetcms.ClientApplication.Features.PatientClassification.AnimalBreedList
{
    internal class AnimalBreedListClientQueryHandler(IMediator mediator, IDialogService dialogService, IMapper mapper) : IRequestHandler<AnimalBreedListClientQuery, AnimalBreedListClientQueryResponse>
    {
        public async Task<AnimalBreedListClientQueryResponse> Handle(AnimalBreedListClientQuery request, CancellationToken cancellationToken)
        {
            ListAnimalBreedApiQuery query = new ListAnimalBreedApiQuery
            {
                SearchTerm = request.SearchTerm,
                Skip = request.Skip,
                Take = request.Take
            };
            ListAnimalBreedApiQueryResponse response = await mediator.Send(query);
            if (response.Success)
            {
                return mapper.Map<AnimalBreedListClientQueryResponse>(response);
            }
            else
            {
                dialogService.ShowError(response.Message, "Hiba");
                return new AnimalBreedListClientQueryResponse();
            }
        }
    }
}
