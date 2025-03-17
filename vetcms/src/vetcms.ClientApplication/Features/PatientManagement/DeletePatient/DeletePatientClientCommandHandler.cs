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
using vetcms.SharedModels.Features.PatientClassification;

namespace vetcms.ClientApplication.Features.PatientClassification.DeleteAnimalBreed
{
    internal class DeletePatientClientCommandHandler(IMediator mediator, IMapper mapper, IDialogService dialogService) : IRequestHandler<DeletePatientClientCommand, bool>
    {
        public async Task<bool> Handle(DeletePatientClientCommand request, CancellationToken cancellationToken)
        {
            //DeleteAnimalBreedApiCommand command = mapper.Map<DeleteAnimalBreedApiCommand>(request);
            //DeleteAnimalBreedApiCommandResult response = await mediator.Send(command);
            if (true)
            {
                _ = await (await dialogService.ShowSuccessAsync("Sikeres törlés", "Siker")).Result;
                return true;
            }
            else
            {
                //_ = await (await dialogService.ShowErrorAsync(response.Message, "Hiba")).Result;
                return false;
            }
        }
    }

    //internal class DeleteAnimalBreedApiCommandHandler : GenericApiCommandHandler<DeleteAnimalBreedApiCommand, DeleteAnimalBreedApiCommandResult>
    //{
    //    public DeleteAnimalBreedApiCommandHandler(IServiceScopeFactory serviceScopeFactory)
    //        : base(serviceScopeFactory)
    //    {
    //    }
    //}
}
