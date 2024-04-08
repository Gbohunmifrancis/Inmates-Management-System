using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using ServerLibrary.Data;
using ServerLibrary.Helpers;
using ServerLibrary.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Repositories.Implementations
{
    internal class UserAccountRepository(IOptions<JwtSection> config, ApplicationDbContext applicationDbContext) : IUserAccount
    {
        public async Task<GeneralResponse> CreateAsync(Register user)
        {
            if (user is null) return new GeneralResponse(false, "User is null");


            var checkUser = await FindUserByEmail(user.EmailAddress!);
            if (checkUser != null) return new GeneralResponse(false, "User already exists");


            var applicationUser = await AddToDatabase(new ApplicationUser()
            {
                FullName = user.FullName,
                Email = user.EmailAddress,
                Password = BCrypt.Net.BCrypt.HashPassword(user.Password),
            });


            var CheckAdminRole = await applicationDbContext.SystemRoles.FirstOrDefaultAsync(_ => _.Name!.Equals(Helpers.Constants.Admin));
            if (CheckAdminRole is null)
            {
                 var CreateAdminRole = await AddToDatabase(new SystemRole() { Name = Helpers.Constants.Admin });
                await AddToDatabase(new UserRole() { RoleId = CreateAdminRole.Id, UserId = applicationUser.Id });
                return new GeneralResponse(true, "User created successfully!!!!!");
            }


        }


        public Task<LoginResponse> SignInAsync(Login user)
        {
            throw new NotImplementedException();
        }

        private async Task<ApplicationUser> FindUserByEmail(string email) =>
            await applicationDbContext.ApplicationUsers.FirstOrDefaultAsync(_ => _.Email!.ToLower()!.Equals(email!.ToLower()));

            
        private async Task<T> AddToDatabase<T>(T model)
        {
            var result = await applicationDbContext.AddAsync(model!);
            await applicationDbContext.SaveChangesAsync();
            return (T)result.Entity;
        }
    }
}










