using AutoMapper;
using iWriter.Models;
using iWriter.ViewModels.FeatureViewModels;
using iWriter.ViewModels.ProjectTypeViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iWriter.Helpers
{
    public class Helper : Profile
    {
        public Helper()
        {
            CreateMap<Feature, FeatureViewModel>().ReverseMap();
            CreateMap<CreateFeatureViewModel, Feature>();
            CreateMap<Task<Feature>, FeatureViewModel>();
            CreateMap<ProjectType, ProjectTypeViewModel>().ReverseMap();
        }
    }
}
