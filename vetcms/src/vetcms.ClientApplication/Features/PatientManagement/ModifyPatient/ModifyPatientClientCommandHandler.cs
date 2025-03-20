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
using vetcms.ClientApplication.Features.IAM.ModifyUser;
using vetcms.ClientApplication.Features.PatientClassification.ModifyAnimalBreed;
using vetcms.SharedModels.Features.PatientClassification;

namespace vetcms.ClientApplication.Features.PatientManagement.ModifyPatient
{
    internal class ModifyAnimalClientCommandHandler(IMediator mediator, IMapper mapper, IDialogService dialogService) : IRequestHandler<ModifyAnimalBreedClientCommand, bool>
    {
        public async Task<bool> Handle(ModifyAnimalBreedClientCommand request, CancellationToken cancellationToken)
        {
            if(true)
            {
                _ = await (await dialogService.ShowSuccessAsync("Sikeres módosítás", "Siker")).Result;
                return true;
            }
            else
            {
                //_ = await (await dialogService.ShowErrorAsync(response.Message, "Hiba")).Result;
                //return false;
            }
        }
    }

    //internal class ModifyAnimalBreedApiCommandHandler : GenericApiCommandHandler<ModifyAnimalBreedApiCommand, ModifyAnimalBreedApiCommandResult>
    //{
    //    public ModifyAnimalBreedApiCommandHandler(IServiceScopeFactory serviceScopeFactory)
    //        : base(serviceScopeFactory)
    //    {
    //    }
    //}
}