using vetcms.ClientApplication.Common.Abstract;
using vetcms.ClientApplication.Features.IAM.UserList;

namespace vetcms.ClientApplication.Features.IAM.GetUser
{
    public class GetUserClientQuery : IClientCommand<GetUserClientQueryResponse>
    {
        public int UserId { get; set; } = 0;

        public GetUserClientQuery()
        {
            
        }

        public GetUserClientQuery(int userId)
        {
            UserId = userId;
        }
    }
}
