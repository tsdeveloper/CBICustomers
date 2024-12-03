using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string RetrieveEmailFromPrincipal(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.Email);
        }
        public static async Task<Client> GetUserByEmailWithAddress(this UserManager<Client> userManager, string email)
        {
            var userToReturn = await userManager.Users
                .Include(x => x.Address)
                .FirstOrDefaultAsync(x => x.Email == email);

            if (userToReturn == null) throw new AuthenticationException("User not found");

            return userToReturn;
        }

        public static async Task<Client> GetUserByEmailWithAddress(this UserManager<Client> userManager,
        ClaimsPrincipal user)
        {
            var userToReturn = await userManager.Users
                .Include(x => x.Address)
                .FirstOrDefaultAsync(x => x.Email == user.GetEmail());

            if (userToReturn == null) throw new AuthenticationException("User not found");

            return userToReturn;
        }

        public static string GetEmail(this ClaimsPrincipal user)
        {
            var email = user.FindFirstValue(ClaimTypes.Email)
                ?? throw new AuthenticationException("Email claim not found");

            return email;
        }
    }
}