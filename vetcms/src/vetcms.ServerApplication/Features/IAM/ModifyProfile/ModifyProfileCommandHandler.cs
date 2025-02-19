using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.ServerApplication.Common.Abstractions.Data;
using vetcms.ServerApplication.Domain.Entity;
using vetcms.SharedModels.Features.IAM;

namespace vetcms.ServerApplication.Features.IAM.ModifyProfile
{
    internal class ModifyProfileCommandHandler(IUserRepository userRepository, IMapper mapper) : IRequestHandler<ModifyProfileApiCommand, ModifyProfileApiCommandResponse>
    {

        public Task<ModifyProfileApiCommandResponse> Handle(ModifyProfileApiCommand request, CancellationToken cancellationToken)
        {
            User modifiedProfile = mapper.Map<User>(request.ModifiedProfile);
            
            userRepository.UpdateAsync(modifiedProfile);
            return Task.FromResult(new ModifyProfileApiCommandResponse(true));

        }
    }
}
