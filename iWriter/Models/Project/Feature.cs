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
        [MaxLength(100, ErrorMessage = "Feature text must be less then {1} characters")]
        public string FeatureText { get; set; }

        public IList<ProjectTypeFeature> ProjectTypeFeatures { get; set; }
    }
}
