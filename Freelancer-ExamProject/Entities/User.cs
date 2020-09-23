using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freelancer_Exam.Entities
{
    public class User : IdentityUser<string>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public Country Country { get; set; }
        public string ProfilePicture { get; set; }

    }
}
