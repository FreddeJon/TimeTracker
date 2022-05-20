using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Services;
using IdentityModel;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer;

public class ProfileService : IProfileService
{
    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _claimsFactory;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<ProfileService> Logger;

    public ProfileService(UserManager<ApplicationUser> userManager,
        IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory,
        ILogger<ProfileService> logger)
    {
        _userManager = userManager;
        _claimsFactory = claimsFactory;
        Logger = logger;
    }

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var sub = context.Subject?.GetSubjectId();
        if (sub == null) throw new Exception("No sub claim present");
        var user = await _userManager.FindByIdAsync(sub);
        if (user == null)
        {
            Logger?.LogWarning("No user found matching subject Id: {0}", sub);
        }
        else
        {
            var principal = await _claimsFactory.CreateAsync(user);
            if (principal == null) throw new Exception("ClaimsFactory failed to create a principal");
            var claims = principal.Claims.ToList();

            claims = claims.Where(x => context.RequestedClaimTypes.Contains(x.Type)).ToList();

            if (_userManager.SupportsUserRole)
            {
                IList<string> roles = await _userManager.GetRolesAsync(user);
                foreach (var roleName in roles)
                {
                    claims.Add(new Claim("role", roleName));
                }
            }

            context.IssuedClaims = claims;
        }

    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        var sub = context.Subject?.GetSubjectId();
        if (sub == null) throw new Exception("No subject Id claim present");

        var user = await _userManager.FindByIdAsync(sub);
        if (user == null)
        {
            Logger?.LogWarning("No user found matching subject Id: {0}", sub);
        }

        context.IsActive = user != null;
    }
}
