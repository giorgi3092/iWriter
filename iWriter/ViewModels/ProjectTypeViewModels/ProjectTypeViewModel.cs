using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace iWriter.ViewModels.ProjectTypeViewModels
{
    public class ProjectTypeViewModel
    {
        [Display(Name = "Id")]
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
    }
}
