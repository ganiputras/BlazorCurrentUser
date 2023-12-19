// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using WebApp.Ui.Data;
using WebApp.Ui.Services.ClaimTypes;

namespace WebApp.Ui.Services;

public class ApplicationUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
{
    public ApplicationUserClaimsPrincipalFactory(UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IOptions<IdentityOptions> optionsAccessor) : base(userManager, roleManager, optionsAccessor)
    {
    }

    public override async Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
    {
        var principal = await base.CreateAsync(user);
        if (!string.IsNullOrEmpty(user.TenantId))
            ((ClaimsIdentity)principal.Identity)?.AddClaims(new[]
            {
                new Claim(ApplicationClaimTypes.TenantId, user.TenantId)
            });

        if (!string.IsNullOrEmpty(user.CompanyId))
            ((ClaimsIdentity)principal.Identity)?.AddClaims(new[]
            {
                new Claim(ApplicationClaimTypes.CompanyId, user.CompanyId)
            });

       
        return principal;
    }
}