using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.ServerApplication.Common.Abstractions.Data;
using vetcms.SharedModels.Features.IAM;

namespace vetcms.ServerApplication.Features.IAM.DeleteUser
{
    internal class DeleteUserCommandHandler(IUserRepository userRepository) : IRequestHandler<DeleteUserApiCommand, DeleteUserApiCommandResponse>
    {
        public async Task<DeleteUserApiCommandResponse> Handle(DeleteUserApiCommand request, CancellationToken cancellationToken)
        {
            List<int> nonExistentIds = new();
            request.Ids.ForEach(async id =>
            {
                if (!await userRepository.ExistAsync(id))
                {
                    nonExistentIds.Add(id);
                }
            });

            if (nonExistentIds.Any())
            {
                return new DeleteUserApiCommandResponse(false)
                {
                    Message = $"Nem létező felhasználó ID(s): {string.Join(",", nonExistentIds)}"
                };
            }

            foreach (int id in request.Ids)
            {
                await userRepository.DeleteAsync(id);
            }

            return new DeleteUserApiCommandResponse()
            {
                Success = true
            };

        }
    }
}
