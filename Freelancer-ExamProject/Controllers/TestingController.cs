using Freelancer_Exam.Entities;
using Freelancer_Exam.Services.Abstract;
using Freelancer_Exam.ViewModels;
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

        public string AddRandomProject()
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
                // RequiredSkill = new Skill { SkillId = Guid.NewGuid().ToString(), Name = faker.Name.JobType() }
            };

            //ownerService.AddRandomProject(project);
            return "Added";
        }

        public string AddOwnerProject()
        {
            var faker = new Bogus.Faker();

            var project = new AddProjectViewModel
            {
                // Title = faker.Name.JobTitle(),
                // Description = faker.Name.JobDescriptor(),
                // MinPrice = 10,
                // MaxPrice = 100,
                // Status = Status.Pending,  // WTF is this???
                // RequiredSkill = new Skill
                // {
                //     SkillId = Guid.NewGuid().ToString(),
                //     Name = faker.Name.JobType()
                // }
            };
            var userId = "75a63a94-ec99-460b-b75f-04282a044035";

            ownerService.AddProject(userId, project);
            return "Added";
        }
    }
}
