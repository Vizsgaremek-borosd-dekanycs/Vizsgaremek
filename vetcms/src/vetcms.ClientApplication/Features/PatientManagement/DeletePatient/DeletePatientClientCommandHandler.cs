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
    internal class DeletePatientClientCommandHandler(IMediator mediator, IMapper mapper, IDialogService dialogService) : IRequestHandler<DeletePatientClientCommand, bool>
    {
        public async Task<bool> Handle(DeletePatientClientCommand request, CancellationToken cancellationToken)
        {
            DeletePatientApiCommand deletePatientApiCommand = new DeletePatientApiCommand()
            {
                Ids = request.AnimalIds
            };
            DeletePatientApiCommandResponse response = await mediator.Send(deletePatientApiCommand);
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

    internal class DeletePatientApiCommandHandler : GenericApiCommandHandler<DeletePatientApiCommand, DeletePatientApiCommandResponse>
    {
        public DeletePatientApiCommandHandler(IServiceScopeFactory serviceScopeFactory)
            : base(serviceScopeFactory)
        {
        }
    }
}
