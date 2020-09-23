using Freelancer_Exam.Models;
using Freelancer_Exam.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freelancer_Exam.Services.Abstract
{
    public interface IAccountService
    {
        Task<BaseResponse> SignIn(LoginViewModel userModel);
        Task<BaseResponse> SignUp(RegisterViewModel userModel);
    }
}
