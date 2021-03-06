﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freelancer_Exam.Entities.Db_Context
{
    public class FreelancerDbContext : IdentityDbContext<User, Role, string>
    {
        public FreelancerDbContext(DbContextOptions<FreelancerDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var owner = builder.Entity<Owner>();
            owner.HasKey(t => t.OwnerId);
            owner.HasMany(t => t.Projects).WithOne(t => t.Owner);

            builder.Entity<DeveloperSkill>().HasKey(ds => new {ds.DeveloperId, ds.SkillId});

            builder.Entity<DeveloperSkill>()
                .HasOne(ds => ds.Developer)
                .WithMany(d => d.DeveloperSkill)
                .HasForeignKey(ds => ds.DeveloperId);            
            
            builder.Entity<DeveloperSkill>()
                .HasOne(ds => ds.Skill)
                .WithMany(s => s.DeveloperSkill)
                .HasForeignKey(ds => ds.SkillId);
            
            
            builder.Entity<ProjectSkill>().HasKey(ds => new {ds.ProjectId, ds.SkillId});

            builder.Entity<ProjectSkill>()
                .HasOne(ds => ds.Project)
                .WithMany(d => d.ProjectSkill)
                .HasForeignKey(ds => ds.ProjectId);            
            
            builder.Entity<ProjectSkill>()
                .HasOne(ds => ds.Skill)
                .WithMany(s => s.ProjectSkill)
                .HasForeignKey(ds => ds.SkillId);

            builder.Entity<Country>()
                .HasKey(c => c.CountryId);

            builder.Entity<Developer>()
                .HasKey(d => d.DeveloperId);

            builder.Entity<Skill>()
                .HasKey(s => s.SkillId);

            var bidRequest = builder.Entity<BidRequest>();
            bidRequest.HasKey(t => t.BidRequestId);
            bidRequest.HasOne(t => t.Developer).WithMany(t => t.BidRequests);
            bidRequest.HasOne(t => t.Project);

            var confirmedRequest = builder.Entity<ConfirmedRequest>();
            confirmedRequest.HasKey(t => t.ConfirmedRequestId);
            confirmedRequest.HasOne(t => t.BidRequest);

            builder.Entity<Project>().HasKey(t => t.ProjectId);

            base.OnModelCreating(builder);
        }
        public DbSet<DeveloperSkill> DeveloperSkills { get; set; }
        public DbSet<ProjectSkill> ProjectSkills { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<BidRequest> BidRequests { get; set; }
        public DbSet<ConfirmedRequest> ConfirmedRequests { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Skill> Skills { get; set; }
    }    
}
