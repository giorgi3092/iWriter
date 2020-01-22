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

        [Required]
        [Display(Name = "Feature text")]
        [MaxLength(100, ErrorMessage = "{0} cannot be more than {1} characters long")]
        public string FeatureText { get; set; }

        public IList<ProjectTypeFeature> ProjectTypeFeatures { get; set; }
    }
}
