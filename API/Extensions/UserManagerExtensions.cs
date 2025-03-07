using System.Security.Claims;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class UserManagerExtensions
    {
        public static async Task<Client> FindUserByClaimsPrincipleWithAddress(this UserManager<Client> userManager, 
            ClaimsPrincipal user)
        {
            var email = user.FindFirstValue(ClaimTypes.Email);

            return await userManager.Users.Include(x => x.AddressList)
                .SingleOrDefaultAsync(x => x.Email == email);
        }

        public static async Task<Client> FindByEmailFromClaimsPrincipal(this UserManager<Client> userManager, 
            ClaimsPrincipal user)
        {
            return await userManager.Users
                .SingleOrDefaultAsync(x => x.Email == user.FindFirstValue(ClaimTypes.Email));
        }
    }
