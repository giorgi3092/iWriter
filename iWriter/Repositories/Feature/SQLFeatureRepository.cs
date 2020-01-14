using iWriter.Interfaces.FeatureAndProjectType;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iWriter.Models
{
    public class SQLFeatureRepository : IFeatureRepository
    {
        private readonly iWriterContext _iWriterContext;
        private readonly ILogger<SQLFeatureRepository> logger;

        public SQLFeatureRepository(iWriterContext iWriterContext,
                                    ILogger<SQLFeatureRepository> logger)
        {
            _iWriterContext = iWriterContext;
            this.logger = logger;
        }

        public async Task<Feature> Add(Feature feature)
        {
            await _iWriterContext.Features.AddAsync(feature);
            return feature;
        }

        public async Task<Feature> Delete(int id)
        {
            Feature feature = await _iWriterContext.Features.FindAsync(id);
            if(feature != null)
            {
                _iWriterContext.Remove(feature);
            }
            return feature;
        }

        public IEnumerable<Feature> GetAllFeatures()
        {
            return _iWriterContext.Features;
        }

        public async Task<Feature> GetFeature(int Id)
        {
            return await _iWriterContext.Features.FindAsync(Id);
        }

        public async Task<Feature> Update(Feature featureChanges)
        {
            var feature = _iWriterContext.Features.Attach(featureChanges);
            feature.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return featureChanges;
        }
    }
}
