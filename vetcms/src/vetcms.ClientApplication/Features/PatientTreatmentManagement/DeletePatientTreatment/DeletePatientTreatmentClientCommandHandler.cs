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
using vetcms.SharedModels.Features.PatientManagement;

namespace vetcms.ClientApplication.Features.PatientManagement.DeletePatient
{
    internal class DeletePatientTreatmentClientCommandHandler(IMediator mediator, IMapper mapper, IDialogService dialogService) : IRequestHandler<DeletePatientTreatmentClientCommand, bool>
    {
        public async Task<bool> Handle(DeletePatientTreatmentClientCommand request, CancellationToken cancellationToken)
        {
            DeletePatientTreatmentApiCommand command = mapper.Map<DeletePatientTreatmentApiCommand>(request);
            DeletePatientTreatmentApiCommandResponse response = await mediator.Send(command);
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

    internal class DeletePatientTreatmentApiCommandHandler : GenericApiCommandHandler<DeletePatientTreatmentApiCommand, DeletePatientTreatmentApiCommandResponse>
    {
        public DeletePatientTreatmentApiCommandHandler(IServiceScopeFactory serviceScopeFactory)
            : base(serviceScopeFactory)
        {
        }
    }
}
