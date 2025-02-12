using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FluentUI.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.ClientApplication.Common.Abstract;
using vetcms.ClientApplication.Features.IAM.DeleteUser;
using vetcms.ClientApplication.Features.IAM.LoginUser;
using vetcms.SharedModels.Features.IAM;

namespace vetcms.ClientApplication.Features.IAM.FirstTimeSignin
{
    internal class FirstTimeSigninCommandHandler(IMediator mediator,IDialogService dialogService) : IRequestHandler<FirstTimeSigninCommand, bool>
    {
        public async Task<bool> Handle(FirstTimeSigninCommand request, CancellationToken cancellationToken)
        {
            if(request.NewPassword != request.NewPasswordConfirmation)
            {
                await dialogService.ShowErrorAsync("Jelszó és a megerősítése sajnos nem egyezik.");
                return false;
            }

            FirstTimeAuthenticateUserApiCommand firstTimeAuthenticateUserApiCommand = new FirstTimeAuthenticateUserApiCommand()
            {
                AuthenticationCode = request.VerificationCode,
                Password = request.NewPassword
            };

            FirstTimeAuthenticateUserApiCommandResponse response = await mediator.Send(firstTimeAuthenticateUserApiCommand);

            if (response.Success)
            {
                await dialogService.ShowSuccessAsync("Sikeres jelszó beállítás, kérjük jelentkezzen be.", "Siker!");
                return true;
            }
            else
            {
                await dialogService.ShowErrorAsync(response.Message, "Hiba!");
                return false;
            }

            //var reference = await dialogService.ShowSuccessAsync("Sikeres jelszó beállítás, kérjük jelentkezzen be.", "Siker!");
            //var result = await reference.Result;
            //return true;
        }
    }


    internal class FirstTimeSignInApiCommandHandler : GenericApiCommandHandler<FirstTimeAuthenticateUserApiCommand, FirstTimeAuthenticateUserApiCommandResponse>
    {
        public FirstTimeSignInApiCommandHandler(IServiceScopeFactory serviceScopeFactory)
            : base(serviceScopeFactory)
        {
        }
    }

}
