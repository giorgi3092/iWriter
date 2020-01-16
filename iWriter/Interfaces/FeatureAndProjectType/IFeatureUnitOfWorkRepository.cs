using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iWriter.Interfaces.FeatureAndProjectType
{
    public interface IFeatureUnitOfWorkRepository
    {
        IFeatureRepository featureRepository { get; }
        IProjectTypeRepository projectTypeRepository { get; }

        void Save();
    }
}