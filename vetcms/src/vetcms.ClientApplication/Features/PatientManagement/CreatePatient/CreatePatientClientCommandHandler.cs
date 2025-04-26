using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FluentUI.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.ClientApplication.Common.Abstract;
using vetcms.SharedModels.Features.IAM;
using vetcms.SharedModels.Features.PatientClassification;
using vetcms.SharedModels.Features.PatientManagement;

namespace vetcms.ClientApplication.Features.PatientManagement.CreatePatient
{
    internal class CreatePatientClientCommandHandler(IMediator mediator, IDialogService dialogService) : IRequestHandler<CreatePatientCommand, bool>
    {
        public async Task<bool> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
        {
            CreatePatientApiCommand createPatientApiCommand = new CreatePatientApiCommand
            {
                NewPatient = request.NewAnimalModel
            };
            var response = await mediator.Send(createPatientApiCommand);

            if (response.Success)
            {
                await dialogService.ShowSuccessAsync($"{response.Message}", "Siker");
                return true;
            }
            else
            {
                dialogService.ShowError(response.Message, "Hiba");
                return false;
            }
        }
    }

    internal class CreatePatientApiCommandHandler : GenericApiCommandHandler<CreatePatientApiCommand, CreatePatientApiCommandResponse>
    {
        public CreatePatientApiCommandHandler(IServiceScopeFactory serviceScopeFactory)
            : base(serviceScopeFactory)
        {
        }
    }
}
