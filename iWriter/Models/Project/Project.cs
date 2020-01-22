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
        [MaxLength(50, ErrorMessage = "{0} cannot exceed {1} characters")]
        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "{0} cannot exceed {1} characters")]
        [Display(Name = "General Topic")]
        public string GeneralTopic { get; set; }

        [Required]
        [MaxLength(10000, ErrorMessage = "{0} cannot exceed {1} characters")]
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

        [Display(Name = "File Path")]
        [MaxLength(150, ErrorMessage = "{0} must be less than {1} characters long. Rename the file so that it has less characters.")]
        public string FilePath { get; set; }

        public int ProjectTypeId { get; set; }
        public ProjectType ProjectType { get; set; }
    }
}