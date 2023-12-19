using System.Security.Claims;

namespace WebApp.Ui.Services
{
    public interface ICurrentUserService
    {
        string UserId { get; set; }
        string UserName { get; set; }
        string DisplayName { get; set; }
        string TenantId { get; set; }
        string CompanyId { get; set; }
        bool IsAuthenticated { get; set; }
        void SetUser(ClaimsPrincipal user);
        ClaimsPrincipal GetUser();
    }
}
