using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iWriter.Models
{
    public class Project
    {
        public int ProjectId { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Project name cannot exceed {0} characters")]
        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "General topic cannot exceed {0} characters")]
        [Display(Name = "General Topic")]
        public string GeneralTopic { get; set; }

        [Required]
        [MaxLength(10000, ErrorMessage = "Project details cannot exceed {0} characters")]
        [Display(Name = "Project details")]
        public string ProjectDetails { get; set; }

        [Required]
        [Display(Name = "Keyword Density")]
        public float KeywordDensity { get; set; }

        [Required]
        [Display(Name = "Quantity")]
        public int ArticleQuantity { get; set; }

        [Required]
        [Display(Name = "Word Count")]
        public int WordCount { get; set; }

        public IList<ProjectProjectType> ProjectProjectType { get; set; }
    }
}