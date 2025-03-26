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
    internal class DeleteAnimalBreedClientCommandHandler(IMediator mediator, IMapper mapper, IDialogService dialogService) : IRequestHandler<DeleteAnimalBreedClientCommand, bool>
    {
        public async Task<bool> Handle(DeleteAnimalBreedClientCommand request, CancellationToken cancellationToken)
        {
            DeleteAnimalBreedApiCommand command = mapper.Map<DeleteAnimalBreedApiCommand>(request);
            DeleteAnimalBreedApiCommandResult response = await mediator.Send(command);
            if (response.Success)
            {
                _ = await (await dialogService.ShowSuccessAsync(response.Message, "Siker")).Result;
                return true;
            }
            else
            {
                _ = await (await dialogService.ShowErrorAsync(response.Message, "Hiba")).Result;
                return false;
            }
        }
    }

    internal class DeleteAnimalBreedApiCommandHandler : GenericApiCommandHandler<DeleteAnimalBreedApiCommand, DeleteAnimalBreedApiCommandResult>
    {
        public DeleteAnimalBreedApiCommandHandler(IServiceScopeFactory serviceScopeFactory)
            : base(serviceScopeFactory)
        {
        }
    }
}
