using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Ui.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public string TenantId { get; set; }
        public string CompanyId { get; set; }
        public bool IsActive { get; set; }
    }

}
