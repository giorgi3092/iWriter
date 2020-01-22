using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace iWriter.Models
{
    public class ProjectType
    {
        public ProjectType()
        {
            ProjectTypeFeatures = new List<ProjectTypeFeature>();
        }

        public int ProjectTypeId { get; set; }

        [Required]
        [Display(Name = "Project Type Name")]
        [MaxLength(50, ErrorMessage = "Name cannot exceed {0} characters")]
        public string ProjectTypeName { get; set; }

        [Required]
        [Display(Name = "Rate (per 100 words)")]
        [Column(TypeName = "decimal(18, 2)")]
        [DataType(DataType.Currency)]
        public decimal Rate { get; set; }

        [Required]
        [Display(Name = "Star Quality")]
        public float StarQuality { get; set; }

        [Required]
        [Display(Name = "Turn Around Time")]
        public int DaysToDeliver { get; set; }

        // navigation properties
        public IList<ProjectTypeFeature> ProjectTypeFeatures { get; set; }

        public IList<Project> Projects { get; set; }
    }
}