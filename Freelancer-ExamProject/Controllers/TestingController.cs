using Freelancer_Exam.Entities;
using Freelancer_Exam.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freelancer_Exam.Controllers
{
    public class TestingController : Controller
    {
        private readonly IFreelancerService freelancerService;

        public TestingController(IFreelancerService freelancerService)
        {
            this.freelancerService = freelancerService;
        }

        [HttpGet]
        public string GetProjects()
        {
            var projects = freelancerService.GetAllProjects();
            return projects.ToString();
        }

        public string AddProject()
        {
            var faker = new Bogus.Faker();

            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = faker.Person.UserName,
                Name = faker.Person.FirstName,
                Surname = faker.Person.LastName,
                ProfilePicture = faker.Person.Avatar,
                Country = new Country { CountryId = Guid.NewGuid().ToString(), Name = faker.Address.Country() }
            };

            var project = new Project
            {
                Owner = new Owner { OwnerId = Guid.NewGuid().ToString(), User = user },
                Title = faker.Name.JobTitle(),
                Description = faker.Name.JobDescriptor(),
                MinPrice = 10,
                MaxPrice = 100,
                Status = Status.Pending,  // WTF is this???
                RequiredSkill = new Skill { SkillId = Guid.NewGuid().ToString(), Name = faker.Random.Int() }                
            };

            freelancerService.AddProject(project);
            return "Added";
        }
    }
}
