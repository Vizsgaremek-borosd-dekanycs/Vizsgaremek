using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.ClientApplication.Common.Abstract;
using vetcms.SharedModels.Features.IAM;

namespace vetcms.ClientApplication.Features.IAM.ResetPassword
{
    internal class ConfirmResetPasswordClientCommandHandler(IMediator mediator) : IRequestHandler<ConfirmResetPasswordClientCommand, bool>
    {
        /// <summary>
        /// Kezeli a jelszó visszaállításának megerősítésére vonatkozó kérést.
        /// </summary>
        /// <param name="request">A jelszó visszaállításának megerősítésére vonatkozó kérés.</param>
        /// <param name="cancellationToken">A lemondási token.</param>
        /// <returns>Igaz, ha a jelszó visszaállítása sikeres, különben hamis.</returns>
        public async Task<bool> Handle(ConfirmResetPasswordClientCommand request, CancellationToken cancellationToken)
        {
            if(request.Password1 != request.Password2)
            {
                System.Console.WriteLine(request.Password1);
                System.Console.WriteLine(request.Password2);
                Console.WriteLine("A jelszavak nem egyeznek!");
                request.DialogService.ShowError("A jelszavak nem egyeznek!", "Hiba");
                return false;
            }
            else
            {
                ConfirmResetPasswordApiCommand confirmResetPasswordApiCommand = new ConfirmResetPasswordApiCommand()
                {
                    Email = request.Email,
                    ConfirmationCode = request.VerificationCode,
                    NewPassword = request.Password1
                };
                ConfirmResetPasswordApiCommandResponse response = await mediator.Send(confirmResetPasswordApiCommand);
                if (response.Success)
                {
                    Console.WriteLine("A jelszó visszaállítása sikeres!");
                    CancellationToken token = new CancellationToken();
                    var dialogRef = await request.DialogService.ShowSuccessAsync("Jelszó visszaállítása sikeres!", "Siker");
                    await dialogRef.Result.WaitAsync(token);
                    return true;
                }
                else
                {
                    request.DialogService.ShowError(response.Message, "Sikertelen jelszó változtatás!");
                    Console.WriteLine("A jelszó visszaállítása sikertelen!");
                    Console.WriteLine(response.Message);
                    return false;
                }
            }
        }
    }

    internal class ConfirmResetPasswordApiCommandHandler : GenericApiCommandHandler<ConfirmResetPasswordApiCommand, ConfirmResetPasswordApiCommandResponse>
    {
        public ConfirmResetPasswordApiCommandHandler(IServiceScopeFactory serviceScopeFactory)
            : base(serviceScopeFactory)
        {
        }
    }
}
