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
        private readonly IOwnerService ownerService;

        public TestingController(IFreelancerService freelancerService, IOwnerService ownerService)
        {
            this.freelancerService = freelancerService;
            this.ownerService = ownerService;
        }

        [HttpGet]
        public string GetProjects()
        {
            var projects = freelancerService.GetAllProjects();
            return projects.ToString();
        }

        [HttpGet]
        public string GetOwnerProjects([FromQuery]string ownerId)
        {
            var projects = ownerService.GetProjects(ownerId);
            return projects.ToString();
        }

        public string AddOwnerProject()
        {
            var faker = new Bogus.Faker();

            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = faker.Person.UserName,
                Email = faker.Person.Email,
                Name = faker.Person.FirstName,
                Surname = faker.Person.LastName,
                ProfilePicture = faker.Person.Avatar,
                Country = faker.Address.Country()
            };

            var project = new Project
            {
                Owner = new Owner { OwnerId = Guid.NewGuid().ToString(), User = user },
                Title = faker.Name.JobTitle(),
                Description = faker.Name.JobDescriptor(),
                MinPrice = 10,
                MaxPrice = 100,
                Status = Status.Pending,  // WTF is this???
                RequiredSkill = new Skill { SkillId = Guid.NewGuid().ToString(), Name = faker.Name.JobType() }                
            };

            ownerService.AddProject(project);
            return "Added";
        }
    }
}
