using iWriter.Interfaces.FeatureAndProjectType;
using iWriter.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iWriter.Repositories.Feature
{
    public class SQLProjectTypeRepository : IProjectTypeRepository
    {
        private readonly iWriterContext _iWriterContext;
        private readonly ILogger logger;

        public SQLProjectTypeRepository(iWriterContext iWriterContext,
                                        ILogger<SQLProjectTypeRepository> logger)
        {
            _iWriterContext = iWriterContext;
            this.logger = logger;
        }

        public async Task<ProjectType> Add(ProjectType projectType)
        {
            await _iWriterContext.ProjectTypes.AddAsync(projectType);
            await _iWriterContext.SaveChangesAsync();
            return projectType;
        }

        public async Task<ProjectType> Delete(int id)
        {
            ProjectType projectType = await _iWriterContext.ProjectTypes.FindAsync(id);
            if(projectType != null)
            {
                _iWriterContext.Remove(projectType);
                await _iWriterContext.SaveChangesAsync();
            }

            return projectType;
        }

        public IEnumerable<ProjectType> GetAllFeatures()
        {
            return _iWriterContext.ProjectTypes;
        }

        public async Task<ProjectType> GetFeature(int Id)
        {
            return await _iWriterContext.ProjectTypes.FindAsync(Id);
        }

        public async Task<ProjectType> Update(ProjectType projectTypeChanges)
        {
            var projectType = _iWriterContext.ProjectTypes.Attach(projectTypeChanges);
            projectType.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _iWriterContext.SaveChangesAsync();
            return projectTypeChanges;
        }
    }
}