using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iWriter.ViewModels.FeatureViewModels
{
    public class CreateFeatureViewModel
    {
        [Display(Name = "Feature text")]
        public string FeatureText { get; set; }
    }
}
