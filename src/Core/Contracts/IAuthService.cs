using TodoApi2.Features.Login;
using TodoApi2.Features.User;

namespace TodoApi2.src.Core.Contracts;

public interface IAuthService
{
    Task<AdminModel?> Auth(LoginRequest request);
}