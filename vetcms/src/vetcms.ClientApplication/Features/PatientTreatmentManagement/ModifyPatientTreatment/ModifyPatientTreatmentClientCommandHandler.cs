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
using vetcms.SharedModels.Features.PatientManagement;

namespace vetcms.ClientApplication.Features.PatientManagement.ModifyPatient
{
    internal class ModifyPatientTreatmentClientCommandHandler(IMediator mediator, IMapper mapper, IDialogService dialogService) : IRequestHandler<ModifyPatientTreatmentClientCommand, bool>
    {
        public async Task<bool> Handle(ModifyPatientTreatmentClientCommand request, CancellationToken cancellationToken)
        {
            ModifyPatientTreatmentApiCommand command = mapper.Map<ModifyPatientTreatmentApiCommand>(request);
            ModifyPatientTreatmentApiCommandResponse response = await mediator.Send(command);
            if (response.Success)
            {
                _ = await (await dialogService.ShowSuccessAsync("Sikeres módosítás", "Siker")).Result;
                return true;
            }
            else
            {
                _ = await (await dialogService.ShowErrorAsync(response.Message, "Hiba")).Result;
                return false;
            }
        }
    }

    internal class ModifyPatientTreatmentApiCommandHandler : GenericApiCommandHandler<ModifyPatientTreatmentApiCommand, ModifyPatientTreatmentApiCommandResponse>
    {
        public ModifyPatientTreatmentApiCommandHandler(IServiceScopeFactory serviceScopeFactory)
            : base(serviceScopeFactory)
        {
        }
    }
}