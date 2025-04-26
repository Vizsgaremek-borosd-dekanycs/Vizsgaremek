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
    internal class CreatePatientTreatmentClientCommandHandler(IMediator mediator, IDialogService dialogService) : IRequestHandler<CreatePatientTreatmentClientCommand, bool>
    {
        public async Task<bool> Handle(CreatePatientTreatmentClientCommand request, CancellationToken cancellationToken)
        {
            CreatePatientTreatmentApiCommand createPatientTreatmentApiCommand = new CreatePatientTreatmentApiCommand
            {
                NewTreatment = request.Treatmentmodel
            };
            var response = await mediator.Send(createPatientTreatmentApiCommand);

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

        internal class CreatePatientTreatmentApiCommandHandler : GenericApiCommandHandler<CreatePatientTreatmentApiCommand, CreatePatientTreatmentApiCommandResponse>
        {
            public CreatePatientTreatmentApiCommandHandler(IServiceScopeFactory serviceScopeFactory)
                : base(serviceScopeFactory)
            {
            }
        }
    }
