using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freelancer_Exam.Models
{
    public class BaseResponse
    {
        public object Response { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }

        public BaseResponse(bool success, string error)
        {
            Success = success;
            ErrorMessage = error;
        }
        public BaseResponse(bool success, object response = null)
        {
            Success = success;
            Response = response;
        }
    }
}
