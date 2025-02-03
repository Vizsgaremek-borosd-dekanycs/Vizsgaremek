using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FluentUI.AspNetCore.Components;
using vetcms.ClientApplication.Common.Abstract;
using vetcms.ClientApplication.Features.IAM.GetUser;
using vetcms.SharedModels.Features.IAM;

namespace vetcms.ClientApplication.Features.IAM.FirstTimeSignin
{
    internal class GetUserClientQueryHandler(IMediator mediator, IDialogService dialogService, IMapper mapper) : IRequestHandler<GetUserClientQuery, GetUserClientQueryResponse>
    {
        public async Task<GetUserClientQueryResponse> Handle(GetUserClientQuery request, CancellationToken cancellationToken)
        {
            GetUserApiQuery query = new GetUserApiQuery
            {
                UserId = request.UserId
            };

            GetUserApiQueryResponse response = await mediator.Send(query);
            if(response.Success)
            {
                return mapper.Map<GetUserClientQueryResponse>(response);
            }
            {
                await dialogService.ShowErrorAsync(response.Message);
                return new GetUserClientQueryResponse();
            }
        }
    }

    internal class GetUserApiQueryHandler : GenericApiCommandHandler<GetUserApiQuery, GetUserApiQueryResponse>
    {
        public GetUserApiQueryHandler(IServiceScopeFactory serviceScopeFactory)
            : base(serviceScopeFactory)
        {
        }
    }
}