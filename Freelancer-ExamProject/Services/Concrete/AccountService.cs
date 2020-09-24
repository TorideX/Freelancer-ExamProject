using Freelancer_Exam.Entities;
using Freelancer_Exam.Entities.Db_Context;
using Freelancer_Exam.Models;
using Freelancer_Exam.Services.Abstract;
using Freelancer_Exam.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace Freelancer_Exam.Services.Concrete
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> userManager;
        private readonly FreelancerDbContext freelancerDb;
        private readonly SignInManager<User> signInManager;
        private readonly RoleManager<Role> roleManager;
        public AccountService(UserManager<User> userManager, FreelancerDbContext freelancerDb, SignInManager<User> signInManager, RoleManager<Role> roleManager)
        {
            this.userManager = userManager;
            this.freelancerDb = freelancerDb;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        public async Task<BaseResponse> SignIn(LoginViewModel userModel)
        {
            var user = await userManager.FindByEmailAsync(userModel.Email);
            if (user == null) return new BaseResponse(false, "Email does not exist");
            
            var isUser = await userManager.CheckPasswordAsync(user, userModel.Password);
            if (!isUser) return new BaseResponse(false, "Password is incorrect");

            var result = await signInManager.PasswordSignInAsync(user,userModel.Password,false,false);
            return result.Succeeded ? new BaseResponse(true, user) : new BaseResponse(false, "Fail");
        }

        public async Task<BaseResponse> SignUp(RegisterViewModel userModel)
        {
            var tempUser = await userManager.FindByEmailAsync(userModel.Email);
            if (tempUser != null) return new BaseResponse(false, "Email already exist");
            var newUser = new User
            {
                Id = Guid.NewGuid().ToString(),
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
            if (userModel.Job == Job.Developer) {
                var roleExist = await roleManager.FindByNameAsync("Developer");
                if (roleExist == null) {
                    await roleManager.CreateAsync(new Role{Name = "Developer", Id = Guid.NewGuid().ToString()});
                }
                await userManager.AddToRoleAsync(user, "Developer");
                await freelancerDb.Developers.AddAsync(new Developer { User = user,DeveloperId = Guid.NewGuid().ToString()});
            }
            else
            {
                var roleExist = await roleManager.FindByNameAsync("Owner");
                if (roleExist == null) {
                    await roleManager.CreateAsync(new Role{Name = "Owner", Id = Guid.NewGuid().ToString()});
                }
                await userManager.AddToRoleAsync(user, "Owner");
                await freelancerDb.Owners.AddAsync(new Owner { User = user, OwnerId = Guid.NewGuid().ToString()});
            }
            await freelancerDb.SaveChangesAsync();
        }
    }
}
