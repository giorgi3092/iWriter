using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iWriter.Models
{
    public class ProjectTypeFeature
    {
        // navigation properties
        public int FeatureId { get; set; }
        public Feature Feature { get; set; }

        // navigation properties
        public int ProjectTypeId { get; set; }
        public ProjectType ProjectType { get; set; }
    }
}
