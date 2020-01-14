using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iWriter.Models
{
    public class ProjectProjectType
    {
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public int ProjectTypeId { get; set; }
        public ProjectType ProjectType { get; set; }
    }
}
