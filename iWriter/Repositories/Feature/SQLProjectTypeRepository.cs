using iWriter.Interfaces.FeatureAndProjectType;
using iWriter.Models;
using Microsoft.EntityFrameworkCore;
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
            return projectType;
        }

        public async Task<ProjectType> Delete(int id)
        {
            ProjectType projectType = await _iWriterContext.ProjectTypes.FindAsync(id);
            if(projectType != null)
            {
                _iWriterContext.Remove(projectType);
            }

            return projectType;
        }

        public IEnumerable<ProjectType> GetAllProjectTypes()
        {
            return _iWriterContext.ProjectTypes.Include("ProjectTypeFeatures.Feature");
        }

        public async Task<ProjectType> GetProjectType(int Id)
        {
            return await _iWriterContext.ProjectTypes.Include("ProjectTypeFeatures.Feature").FirstOrDefaultAsync(x => x.ProjectTypeId == Id);
        }

        public async Task<ProjectType> Update(ProjectType projectTypeChanges)
        {
            var projectType = _iWriterContext.ProjectTypes.Attach(projectTypeChanges);
            projectType.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return projectTypeChanges;
        }
    }
}