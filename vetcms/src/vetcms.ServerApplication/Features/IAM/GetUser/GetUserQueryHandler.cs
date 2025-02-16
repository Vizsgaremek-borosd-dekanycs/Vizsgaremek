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

namespace vetcms.ServerApplication.Features.IAM.GetUser
{
    internal class GetUserQueryHandler(IUserRepository userRepository, IMapper mapper) : IRequestHandler<GetUserApiQuery, GetUserApiQueryResponse>
    {
        public async Task<GetUserApiQueryResponse> Handle(GetUserApiQuery request, CancellationToken cancellationToken)
        {
            if (await userRepository.ExistAsync(request.UserId))
            {
                User user = await userRepository.GetByIdAsync(request.UserId);
                UserDto userDto = mapper.Map<UserDto>(user);
                userDto.Password = "";
                return new GetUserApiQueryResponse(true)
                {
                    User = userDto
                };
            }
            else
            {
                return new GetUserApiQueryResponse(false, "Felhasználó nem található");
            }
        }
    }
}
