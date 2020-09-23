using Freelancer_Exam.Entities;
using Freelancer_Exam.Entities.Db_Context;
using Freelancer_Exam.Models;
using Freelancer_Exam.Services.Abstract;
using Freelancer_Exam.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freelancer_Exam.Services.Concrete
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> userManager;
        private readonly FreelancerDbContext freelancerDb;

        public AccountService(UserManager<User> userManager, FreelancerDbContext freelancerDb)
        {
            this.userManager = userManager;
            this.freelancerDb = freelancerDb;
        }

        public async Task<BaseResponse> SignIn(LoginViewModel userModel)
        {
            var user = await userManager.FindByEmailAsync(userModel.Email);
            if (user == null) return new BaseResponse(false, "Email does not exist");

            var isUser = await userManager.CheckPasswordAsync(user, userModel.Password);
            if (!isUser) return new BaseResponse(false, "Password is incorrect");

            return new BaseResponse(true, user);
        }

        public async Task<BaseResponse> SignUp(RegisterViewModel userModel)
        {
            var tempUser = await userManager.FindByEmailAsync(userModel.Email);
            if (tempUser != null) return new BaseResponse(false, "Email already exist");
            var newUser = new User
            {
                UserName = userModel.Email,
                Name = userModel.FirstName,
                Surname = userModel.LastName,
                Country = userModel.Country,
                Email = userModel.Email
            };            
            var result = await userManager.CreateAsync(newUser, userModel.Password);
            if (!result.Succeeded) return new BaseResponse(false, "Couldn't create User");

            await CreateUserWithRole(userModel);
            return new BaseResponse(true);
        }

        private async Task CreateUserWithRole(RegisterViewModel userModel)
        {
            var user = await userManager.FindByEmailAsync(userModel.Email);
            if (userModel.Job == Job.Developer)
            {
                await userManager.AddToRoleAsync(user, "Developer");
                freelancerDb.Developers.Add(new Developer { User = user });
            }
            else
            {
                await userManager.AddToRoleAsync(user, "Owner");
                freelancerDb.Owners.Add(new Owner { User = user });
            }
            freelancerDb.SaveChanges();
        }
    }
}
