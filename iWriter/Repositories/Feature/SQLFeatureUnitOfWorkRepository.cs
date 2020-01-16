using iWriter.Interfaces.FeatureAndProjectType;
using iWriter.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iWriter.Repositories.Feature
{
    public class SQLFeatureUnitOfWorkRepository : IFeatureUnitOfWorkRepository
    {
        private readonly iWriterContext _iWriterContext;
        private readonly ILogger UnitOfWorkLogger;
        private IFeatureRepository _featureRepo;
        private IProjectTypeRepository _projectTypeRepo;

        public SQLFeatureUnitOfWorkRepository(iWriterContext iWriterContext,
                                       ILogger<SQLFeatureUnitOfWorkRepository> UnitOfWorkLogger,
                                       ILogger<SQLFeatureRepository> FeatureLogger,
                                       ILogger<SQLProjectTypeRepository> ProjectTypeLogger)
        {
            _iWriterContext = iWriterContext;
            this.UnitOfWorkLogger = UnitOfWorkLogger;
            this.FeatureLogger = FeatureLogger;
            this.ProjectTypeLogger = ProjectTypeLogger;
        }

        public IFeatureRepository featureRepository 
        {
            get
            {
                return _featureRepo ??= new SQLFeatureRepository(_iWriterContext, FeatureLogger);
            }
        }

        public IProjectTypeRepository projectTypeRepository 
        {
            get
            {
                return _projectTypeRepo ??= new SQLProjectTypeRepository(_iWriterContext, ProjectTypeLogger);
            }
        }

        public ILogger<SQLFeatureRepository> FeatureLogger { get; }
        public ILogger<SQLProjectTypeRepository> ProjectTypeLogger { get; }

        public void Save()
        {
            _iWriterContext.SaveChanges();
        }
    }
}