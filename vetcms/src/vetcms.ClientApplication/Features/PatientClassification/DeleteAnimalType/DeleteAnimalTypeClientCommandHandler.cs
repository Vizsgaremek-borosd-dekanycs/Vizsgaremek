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
using vetcms.ClientApplication.Features.PatientClassification.ListAnimalType;
using vetcms.SharedModels.Features.PatientClassification;

namespace vetcms.ClientApplication.Features.PatientClassification.DeleteAnimalType
{
    public class DeleteAnimalTypeClientCommandHandler(IMediator mediator, IMapper mapper, IDialogService dialogService) : IRequestHandler<DeleteAnimalTypeClientCommand, bool>
    {
        public async Task<bool> Handle(DeleteAnimalTypeClientCommand request, CancellationToken cancellationToken)
        {
            DeleteAnimalTypeApiCommand command = mapper.Map<DeleteAnimalTypeApiCommand>(request);

            DeleteAnimalTypeApiCommandResponse response = await mediator.Send(command);
            if(response.Success)
            {
                _ = await (await dialogService.ShowSuccessAsync("Sikeres törlés!", "Siker")).Result;
                return true;
            }
            else
            {
                dialogService.ShowError(response.Message, "Hiba");
                return false;
            }
            return true;
        }
    }
    internal class DeleteAnimalTypeApiCommandHandler : GenericApiCommandHandler<DeleteAnimalTypeApiCommand, DeleteAnimalTypeApiCommandResponse>
    {
        public DeleteAnimalTypeApiCommandHandler(IServiceScopeFactory serviceScopeFactory)
            : base(serviceScopeFactory)
        {
        }
    }
}
