using EmpiteIMS.Models.DTO;

namespace EmpiteIMS.Repositories.Abstract
{
    public interface IUserAuthenticationService
    {
        Task<Status> LoginAsync(LoginModel model);
        Task<Status> UserCreationAsync(UserCreationModel model);
        Task LogoutAsync();
    }
}
