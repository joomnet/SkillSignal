﻿namespace SkillSignal.BusinessLayer
{
    using System;

    using SkillSignal.Domain;
    using SkillSignal.IBusinessLayer;
    using SkillSignal.IRepository;

    public class ProjectService : IProjectService
    {
        readonly IDALContext dalContext;

        public ProjectService(IDALContext dalContext)
        {
            this.dalContext = dalContext;
        }

        public Project Load(string projectId)
        {
            return !this.dalContext.Projects.Contains(x => x.ID == projectId) ? Project.NullProject : dalContext.Projects.Find(p => p.ID == projectId);
        }

        public Project Create(string projectName)
        {
           return this.dalContext.Projects.Create(new Project {Name = projectName, ID = Guid.NewGuid().ToString()});
        }
    }
}