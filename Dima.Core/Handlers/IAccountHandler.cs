using Dima.Core.BaseResponses;
using Dima.Core.Requests.Account;

namespace Dima.Core.Handlers
{
    public interface IAccountHandler
    {
        Task<BaseResponse<string>> LoginAsync(LoginRequest request);
        Task<BaseResponse<string>> RegisterAsync(RegisterRequest request);
        Task LogoutAsync();
    }
}
