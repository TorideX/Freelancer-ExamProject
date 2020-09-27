﻿using Freelancer_Exam.Entities;
using Freelancer_Exam.Entities.Db_Context;
using Freelancer_Exam.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using Freelancer_Exam.ViewModels;

namespace Freelancer_Exam.Services.Concrete
{
    public class FreelancerService : IFreelancerService
    {
        private readonly FreelancerDbContext freelancerDb;

        public FreelancerService(FreelancerDbContext freelancerDb)
        {
            this.freelancerDb = freelancerDb;
        }

        public bool CreateBidRequest(string userId, CreateBidRequestViewModel requestModel)
        {
            var dev = freelancerDb.Developers.FirstOrDefault(t => t.DeveloperId == userId);
            var project = freelancerDb.Projects.FirstOrDefault(t => t.ProjectId == requestModel.ProjectId);
            if (dev == null || project == null) return false;

            var bidRequest = new BidRequest
            {
                BidRequestId = Guid.NewGuid().ToString(),
                CreationDate = DateTime.Now,
                Developer = dev,
                Project = project,
                Note = requestModel.Note,
                Price = requestModel.Price,
                DaysToFinish = requestModel.DaysToFinish,
                RequestStatus = RequestStatus.Pending
            };
            freelancerDb.BidRequests.Add(bidRequest);
            freelancerDb.SaveChanges();
            return true;
        }

        public bool CompleteProject(string userId, string requestId)
        {
            var dev = freelancerDb.Developers.FirstOrDefault(t => t.DeveloperId == userId);
            var confirmedRequest = freelancerDb.ConfirmedRequests
                .Include(t => t.BidRequest.Project)
                .Include(t => t.BidRequest.Developer)
                .FirstOrDefault(t => t.ConfirmedRequestId == requestId);
            if (dev == null || confirmedRequest == null) return false;
            if (confirmedRequest.BidRequest.Developer != dev) return false;

            confirmedRequest.BidRequest.Project.Status = Status.Completed;
            freelancerDb.SaveChanges();
            return false;
        }

        public void UpdateSkills(string dId, IEnumerable<string> skillNames) {
            var developer = freelancerDb.Developers.FirstOrDefault(d => d.DeveloperId == dId);
            if (developer == null) return;

            var dbDeveloperSkills = freelancerDb.DeveloperSkills;
            var dbSkills = freelancerDb.Skills;
            freelancerDb.SaveChanges();

            foreach (var skillName in skillNames) {

                if (dbDeveloperSkills?.FirstOrDefault(ds =>
                    ds.DeveloperId == developer.DeveloperId && ds.Skill.Name == skillName) != null) {
                    continue;
                }

                var existSkill = dbSkills.FirstOrDefault(s => s.Name == skillName);
                if (existSkill == null) { 
                    existSkill = (freelancerDb.Skills.Add(new Skill {
                        Name = skillName,
                        SkillId = Guid.NewGuid().ToString(),
                        DeveloperSkill = new List<DeveloperSkill>()
                    })).Entity;
                }

                freelancerDb.DeveloperSkills.Add(new DeveloperSkill {
                    Developer = developer,
                    Skill = existSkill,
                    DeveloperId = developer.DeveloperId,
                    SkillId = existSkill.SkillId
                });
            }

            freelancerDb.SaveChanges();
        }
    }
}
