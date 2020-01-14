using iWriter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iWriter.Interfaces.FeatureAndProjectType
{
    public interface IFeatureRepository
    {
        Task<Feature> GetFeature(int Id);
        IEnumerable<Feature> GetAllFeatures();
        Task<Feature> Add(Feature feature);
        Task<Feature> Update(Feature featureChanges);
        Task<Feature> Delete(int id);
    }
}
