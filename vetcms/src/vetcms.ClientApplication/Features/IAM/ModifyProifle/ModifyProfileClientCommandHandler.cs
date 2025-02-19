using MediatR;
using Microsoft.FluentUI.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vetcms.ClientApplication.Features.IAM.ModifyProifle
{
    internal class ModifyProfileClientCommandHandler(IMediator mediator, IDialogService dialogService) : IRequestHandler<ModifyProfileClientCommand, bool>
    {
        public Task<bool> Handle(ModifyProfileClientCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
