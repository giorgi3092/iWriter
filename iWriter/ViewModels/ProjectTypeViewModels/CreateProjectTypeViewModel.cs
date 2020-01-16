using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iWriter.ViewModels.ProjectTypeViewModels
{
    public class CreateProjectTypeViewModel
    {
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

        // used to display all features in the Db
        [Display(Name = "Select Features for this project type")]
        public IList<SelectListItem> Features { get; set; }

        // stores selected Select List items from the createProjectType page
        public string[] SelectedTags { get; set; }
    }
}
