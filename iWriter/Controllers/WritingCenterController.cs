using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using iWriter.Areas.Identity.Data;
using iWriter.Interfaces.FeatureAndProjectType;
using iWriter.Models;
using iWriter.ViewModels;
using iWriter.ViewModels.FeatureViewModels;
using iWriter.ViewModels.ProjectTypeViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace iWriter.Controllers
{
    [Authorize]
    public class WritingCenterController : Controller
    {
        private readonly UserManager<iWriterUser> userManager;
        private readonly SignInManager<iWriterUser> signInManager;
        private readonly IFeatureUnitOfWorkRepository featureUnitOfWorkRepository;
        private readonly IMapper mapper;
        private readonly ILogger<WritingCenterController> logger;

        public WritingCenterController(UserManager<iWriterUser> userManager,
                                       SignInManager<iWriterUser> signInManager,
                                       IFeatureUnitOfWorkRepository featureUnitOfWorkRepository,
                                       IMapper mapper,
                                       ILogger<WritingCenterController> logger)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.featureUnitOfWorkRepository = featureUnitOfWorkRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }




        /************************************** MANAGER/ADMIN ONLY - Features & Project Types **********************************/
        [HttpGet]
        public IActionResult GetAllProjectFeatures()
        {
            var model = featureUnitOfWorkRepository.featureRepository.GetAllFeatures();
            var vm = mapper.Map<List<FeatureViewModel>>(model);
            return PartialView(vm);
        }

        public IActionResult CreateFeatures()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateFeatures(CreateFeatureViewModel createFeatureViewModel)
        {
            try
            {
                var model = mapper.Map<Feature>(createFeatureViewModel);
                featureUnitOfWorkRepository.featureRepository.Add(model);
                featureUnitOfWorkRepository.Save();
                return RedirectToAction("Index", "WritingCenter");
            }
            catch (InvalidOperationException ex)
            {
                logger.LogError(ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError("General Exception Thrown" + ex.Message);
            }

            return View();
        }

        public async Task<IActionResult> FeatureDetails(int id)
        {
            var model = await featureUnitOfWorkRepository.featureRepository.GetFeature(id);
            var vm = mapper.Map<FeatureViewModel>(model);
            return View(vm);
        }

        public async Task<IActionResult> EditFeature(int id)
        {
            var model = await featureUnitOfWorkRepository.featureRepository.GetFeature(id);
            var vm = mapper.Map<FeatureViewModel>(model);
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> EditFeature(FeatureViewModel vm)
        {
            if (vm == null)
            {
                return View();
            }

            var model = mapper.Map<Feature>(vm);
            await featureUnitOfWorkRepository.featureRepository.Update(model);
            featureUnitOfWorkRepository.Save();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteFeature(int id)
        {
            var model = await featureUnitOfWorkRepository.featureRepository.GetFeature(id);
            var vm = mapper.Map<FeatureViewModel>(model);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteFeaturePost(int id)
        {
            await featureUnitOfWorkRepository.featureRepository.Delete(id);
            featureUnitOfWorkRepository.Save();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetAllProjectTypes()
        {
            var model = featureUnitOfWorkRepository.projectTypeRepository.GetAllProjectTypes();
            var vm = mapper.Map<IEnumerable<ProjectTypeViewModel>>(model);
            return PartialView(vm);
        }

        [HttpGet]
        public IActionResult CreateProjectType()
        {
            // get all features from the Db
            var featuresFromRepo = featureUnitOfWorkRepository.featureRepository.GetAllFeatures();
            
            // this list of Select List items will be displayed. User will choose which ones to add
            var selectList = new List<SelectListItem>();

            // populate select list items with entries retrieved from the Db
            foreach (var item in featuresFromRepo)
            {
                selectList.Add(new SelectListItem(item.FeatureText, item.FeatureId.ToString()));
            }

            var vm = new CreateProjectTypeViewModel()
            {
                Features = selectList
            };

            return PartialView(vm);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProjectType(CreateProjectTypeViewModel vm)
        {
            try
            {
                ProjectType projectType = new ProjectType()
                {
                    StarQuality = vm.StarQuality,
                    DaysToDeliver = vm.DaysToDeliver,
                    Rate = vm.Rate,
                    ProjectTypeName = vm.ProjectTypeName
                };

                foreach (var item in vm.SelectedTags)
                {
                    if (!Int32.TryParse(item, out int result))
                    {
                        ModelState.AddModelError(string.Empty, "Someone might've interfered with the inspect element :))");
                        return RedirectToAction("Index");
                    }
                    projectType.ProjectTypeFeatures.Add(new ProjectTypeFeature()
                    {
                        FeatureId = result
                    });
                }

                await featureUnitOfWorkRepository.projectTypeRepository.Add(projectType);
                featureUnitOfWorkRepository.Save();

                return RedirectToAction("Index");
            } catch
            {
                return RedirectToAction("Index");
            }
        }

        /*
        [HttpPost]
        public IActionResult CreateProjectTypePost(CreateProjectTypeViewModel vm)
        {
            var model = mapper.Map<ProjectType>(vm);

        }*/


        /********************************* Actions for Tab Action Views ************************************/
        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            var user = await userManager.GetUserAsync(User);

            // current user's manager
            var usersManager = await userManager.FindByIdAsync(user.AccountManagerID);

            var model = new DashBoardViewModel
            {
                Name = user.Name,
                AccountId = user.Id,
                AccountBalance = user.AccountBalance,
                AccountManagerName = usersManager.Name,
                AccountType = user.AccountType,
                CompletedProjectsCount = user.CompletedProjectsCount,
                LatestProjectID = user.LatestProjectID,
                NewProjectsCount = user.NewProjectsCount,
                PendingProjectsCount = user.PendingProjectsCount,
                TicketCount = user.TicketCount,
                TicketReplyCount = user.TicketReplyCount,
                Birthday = user.Birthday,
                JoinDate = user.JoinDate,
                LastLogin = user.LastLogin
            };

            return PartialView(model);
        }

        [HttpGet]
        public IActionResult ProjectManagement()
        {
            return PartialView();
        }
    }
}