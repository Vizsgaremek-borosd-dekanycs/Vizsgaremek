using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using vetcms.ClientApplication.Common.Abstract;
using vetcms.ClientApplication.Features.IAM.ResetPassword;
using vetcms.SharedModels.Common.Dto;
using vetcms.SharedModels.Common.IAM.Authorization;
using vetcms.SharedModels.Features.IAM;

namespace vetcms.ClientApplication.Features.IAM.UserList
{
    internal class UserListClientQueryHandler(IMediator mediator) : IRequestHandler<UserListClientQuery, UserListClientQueryResponse>
    {
        public async Task<UserListClientQueryResponse> Handle(UserListClientQuery request, CancellationToken cancellationToken)
        {
            ListUsersApiCommand command = new ListUsersApiCommand()
            {
                Skip = request.Skip,
                Take = request.Take,
                SearchTerm = request.SearchTerm
            };
            ListUsersApiCommandResponse? response = await mediator.Send(command);

            return new UserListClientQueryResponse
            {
                Users = response.Users,
                ResultCount = response.ResultCount
            };
        }
    }

    internal class ListUsersApiCommandHandler : GenericApiCommandHandler<ListUsersApiCommand, ListUsersApiCommandResponse>
    {
        public ListUsersApiCommandHandler(IServiceScopeFactory serviceScopeFactory)
            : base(serviceScopeFactory)
        {
        }
    }
}
