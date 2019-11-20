using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using NgNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NgNetCore.Config.Seguridad
{
    public class ProfileService : IProfileService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            context.IssuedClaims.AddRange(context.Subject.Claims);
            var user = await _userManager.FindByIdAsync(context.Subject.GetSubjectId());
            var claims = GetClaimsAsync(user).Result;
            claims.Add(new Claim("ClientId", context.Client.ClientId));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim(JwtClaimTypes.Role, "UserLogged"));
            context.IssuedClaims.AddRange(claims);
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = _userManager.FindByIdAsync(sub).Result;
            context.IsActive = (user != null) && (!(user.LockoutEnd.HasValue) || (user.LockoutEnd.Value <= DateTime.Now));
            return Task.FromResult(0);
        }

        private Task<List<Claim>> GetClaimsAsync(ApplicationUser user)
        {
            var claims = new List<Claim>();
            var claimsUser = _userManager.GetClaimsAsync(user).Result;
            claims.AddRange(claimsUser);
            var roles = _userManager.GetRolesAsync(user);
            roles.Wait();
            claims.AddRange(roles.Result.Select(role => new Claim(JwtClaimTypes.Role, role)));
            return Task.FromResult(claims);
        }
    }
}
