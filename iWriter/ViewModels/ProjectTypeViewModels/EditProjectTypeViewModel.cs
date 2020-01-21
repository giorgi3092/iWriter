using iWriter.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace iWriter.ViewModels.ProjectTypeViewModels
{
    public class EditProjectTypeViewModel
    {
        [Display(Name = "Project Type ID")]
        public int ProjectTypeId { get; set; }

        [Required]
        [Display(Name = "Project Type Name")]
        [MaxLength(50, ErrorMessage = "Name cannot exceed {1} characters")]
        public string ProjectTypeName { get; set; }

        [Required]
        [Display(Name = "Rate (per 100 words)")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Rate { get; set; }

        [Required]
        [Display(Name = "Star Quality")]
        public float StarQuality { get; set; }

        [Required]
        [Display(Name = "Turn Around Time")]
        public int DaysToDeliver { get; set; }

        // all options to select from
        public IList<SelectListItem> Features { get; set; }


        [Display(Name = "Select Features")]
        public string[] SelectedFeatures { get; set; }
    }
}
