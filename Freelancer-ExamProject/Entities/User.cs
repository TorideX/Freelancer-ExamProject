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
        public string Country { get; set; }
        public string ProfilePicture { get; set; }
        public DateTime JoinedDate { get; set; }
    }
}
