using iWriter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iWriter.Interfaces.FeatureAndProjectType
{
    public interface IProjectTypeRepository
    {
        Task<ProjectType> GetFeature(int Id);
        IEnumerable<ProjectType> GetAllFeatures();
        Task<ProjectType> Add(ProjectType projectType);
        Task<ProjectType> Update(ProjectType projectTypeChanges);
        Task<ProjectType> Delete(int id);
    }
}
