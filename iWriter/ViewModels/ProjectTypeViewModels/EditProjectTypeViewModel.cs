using iWriter.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iWriter.ViewModels.ProjectTypeViewModels
{
    public class EditProjectTypeViewModel
    {
        public int ProjectTypeId { get; set; }

        [Required]
        [Display(Name = "Project Type Name")]
        [MaxLength(50, ErrorMessage = "Name cannot exceed {0} characters")]
        public string ProjectTypeName { get; set; }

        [Required]
        [Display(Name = "Rate")]
        public decimal Rate { get; set; }

        [Required]
        [Display(Name = "Star Quality")]
        public float StarQuality { get; set; }

        [Required]
        [Display(Name = "Turn Around Time")]
        public int DaysToDeliver { get; set; }

        // all options to select from
        public IList<SelectListItem> Features { get; set; }


        public string[] SelectedFeatures { get; set; }
    }
}
