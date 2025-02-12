using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using vetcms.ServerApplication.Common.Abstractions.Data;
using vetcms.ServerApplication.Domain.Entity;
using vetcms.SharedModels.Common.Dto;
using vetcms.SharedModels.Features.IAM;

namespace vetcms.ServerApplication.Features.IAM.ListUsers
{
    internal class ListUsersCommandHandler(IUserRepository userRepository, IMapper mapper) : IRequestHandler<ListUsersApiCommand, ListUsersApiCommandResponse>
    {
        public async Task<ListUsersApiCommandResponse> Handle(ListUsersApiCommand request, CancellationToken cancellationToken)
        {
            int count = await userRepository.Search(request.SearchTerm).CountAsync();

            List<User> users = await userRepository.SearchAsync(request.SearchTerm, request.Skip, request.Take);
            List<UserDto> userDtos = mapper.Map<List<UserDto>>(users);
            return new ListUsersApiCommandResponse(true)
            {
                Users = userDtos,
                ResultCount = count
            };
        }
    }
}
