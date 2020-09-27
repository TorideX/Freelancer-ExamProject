using Freelancer_Exam.Entities;
using Freelancer_Exam.Entities.Db_Context;
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
        private readonly FreelancerDbContext freelancerDb;
        private readonly IFreelancerService freelancerService;
        private readonly IOwnerService ownerService;
        private readonly IHomeService homeService;

        public TestingController(FreelancerDbContext freelancerDb, IFreelancerService freelancerService, IOwnerService ownerService, IHomeService homeService)
        {
            this.freelancerDb = freelancerDb;
            this.freelancerService = freelancerService;
            this.ownerService = ownerService;
            this.homeService = homeService;
        }

        [HttpGet]
        public string GetProjects()
        {
            var projects = homeService.GetAllProjects();
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
                ProjectId = Guid.NewGuid().ToString(),
                Owner = new Owner { OwnerId = Guid.NewGuid().ToString(), User = user },
                Title = faker.Name.JobTitle(),
                Description = faker.Company.CatchPhrase(),
                MinPrice = faker.Random.Int(1,100),
                MaxPrice = faker.Random.Int(100,10000),
                Status = Status.Pending                
                //RequiredSkill = new Skill { SkillId = Guid.NewGuid().ToString(), Name = faker.Name.JobType() }
            };
            project.ProjectSkill = new List<ProjectSkill>
            {
                new ProjectSkill
                {
                    ProjectSkillId = Guid.NewGuid().ToString(),
                    Skill = new Skill
                    {
                        SkillId = Guid.NewGuid().ToString(),
                        Name = faker.Company.CompanySuffix()
                    },
                    Project = project
                },
                new ProjectSkill
                {
                    ProjectSkillId = Guid.NewGuid().ToString(),
                    Skill = new Skill
                    {
                        SkillId = Guid.NewGuid().ToString(),
                        Name = faker.Company.CompanySuffix()
                    },
                    Project = project
                },
                new ProjectSkill
                {
                    ProjectSkillId = Guid.NewGuid().ToString(),
                    Skill = new Skill
                    {
                        SkillId = Guid.NewGuid().ToString(),
                        Name = faker.Company.CompanySuffix()
                    },
                    Project = project
                }
            };

            freelancerDb.Projects.Add(project);
            freelancerDb.SaveChanges();
            return "Added";
        }

        public string AddOwnerProject()
        {
            var faker = new Bogus.Faker();

            var project = new AddProjectViewModel
            {
                Title = faker.Name.JobTitle(),
                Description = faker.Name.JobDescriptor(),
                MinPrice = 10,
                MaxPrice = 100,
                Status = Status.Pending,  // WTF is this???
                RequiredSkill = new List<string>
                {
                    faker.Company.CompanySuffix(),
                    faker.Company.CompanySuffix()
                }
            };
            var userId = "75a63a94-ec99-460b-b75f-04282a044035";

            ownerService.AddProject(userId, project);
            return "Added";
        }

        public string AddProjectBidRequest(string projectId)
        {
            var faker = new Bogus.Faker();

            var project = freelancerDb.Projects.FirstOrDefault(t => t.ProjectId == projectId);

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

            var developer = new Developer
            {
                DeveloperId = Guid.NewGuid().ToString(),
                Rating = Convert.ToInt16(faker.Random.Int(1,10)),
                RatingCount = Convert.ToInt16(faker.Random.Int(1, 10)),
                User = user
            };
            developer.DeveloperSkill = new List<DeveloperSkill>
            {
                new DeveloperSkill
                {
                    Developer = developer,
                    Skill = new Skill
                    {
                        SkillId = Guid.NewGuid().ToString(),
                        Name = faker.Company.CompanySuffix()
                    }
                },
                new DeveloperSkill
                {
                    Developer = developer,
                    Skill = new Skill
                    {
                        SkillId = Guid.NewGuid().ToString(),
                        Name = faker.Company.CompanySuffix()
                    }
                },
                new DeveloperSkill
                {
                    Developer = developer,
                    Skill = new Skill
                    {
                        SkillId = Guid.NewGuid().ToString(),
                        Name = faker.Company.CompanySuffix()
                    }
                },
                new DeveloperSkill
                {
                    Developer = developer,
                    Skill = new Skill
                    {
                        SkillId = Guid.NewGuid().ToString(),
                        Name = faker.Company.CompanySuffix()
                    }
                }
            };

            var bidRequest = new BidRequest
            {
                BidRequestId = Guid.NewGuid().ToString(),
                CreationDate = faker.Date.Recent(),
                DaysToFinish = faker.Random.Int(1,300),
                Price = faker.Random.Int(10,10000),
                Project = project,
                RequestStatus = RequestStatus.Pending,
                Note = faker.Lorem.Letter(),
                Developer = developer
            };

            freelancerDb.BidRequests.Add(bidRequest);
            freelancerDb.SaveChanges();
            return "Added";
        }
    }
}
