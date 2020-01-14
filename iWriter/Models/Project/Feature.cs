using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iWriter.Models
{
    /* Joining Class between ProjectType and Feature classes */
    public class Feature
    {
        public int FeatureId { get; set; }

        [Display(Name = "Feature text")]
        public string FeatureText { get; set; }

        public IList<ProjectTypeFeature> ProjectTypeFeature { get; set; }
    }
}
