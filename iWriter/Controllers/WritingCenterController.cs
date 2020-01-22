using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using iWriter.Areas.Identity.Data;
using iWriter.CustomExceptions;
using iWriter.Helpers;
using iWriter.Interfaces.FeatureAndProjectType;
using iWriter.Models;
using iWriter.ViewModels;
using iWriter.ViewModels.FeatureViewModels;
using iWriter.ViewModels.ProjectTypeViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        private IActionFeedback actionFeedback;

        public WritingCenterController(UserManager<iWriterUser> userManager,
                                       SignInManager<iWriterUser> signInManager,
                                       IFeatureUnitOfWorkRepository featureUnitOfWorkRepository,
                                       IMapper mapper,
                                       ILogger<WritingCenterController> logger,
                                       IActionFeedback actionFeedback)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.featureUnitOfWorkRepository = featureUnitOfWorkRepository;
            this.mapper = mapper;
            this.logger = logger;
            this.actionFeedback = actionFeedback;
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

        [HttpGet]
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
                actionFeedback.SuccessUnsuccess = true;
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

            actionFeedback.SuccessUnsuccess = false;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> FeatureDetails(int id)
        {
            var model = await featureUnitOfWorkRepository.featureRepository.GetFeature(id);
            var vm = mapper.Map<FeatureViewModel>(model);
            return View(vm);
        }

        [HttpGet]
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
            actionFeedback.SuccessUnsuccess = true;
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
            actionFeedback.SuccessUnsuccess = true;
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProjectType(CreateProjectTypeViewModel vm)
        {
            try
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

                vm.Features = selectList;

                if (!ModelState.IsValid)
                {
                    return View(vm);
                }

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
                        return View(vm);
                    }
                    projectType.ProjectTypeFeatures.Add(new ProjectTypeFeature()
                    {
                        FeatureId = result
                    });
                }

                await featureUnitOfWorkRepository.projectTypeRepository.Add(projectType);
                featureUnitOfWorkRepository.Save();

                actionFeedback.SuccessUnsuccess = true;
                return RedirectToAction("Index");
            } catch (NullReferenceException ex)
            {
                if (vm.SelectedTags == null)
                {
                    ModelState.AddModelError(string.Empty, "Please select at least one feature to add.");
                }

                logger.LogError(ex, "Null reference during Project Type creation. Most likely, no featured selected. Exception message: " + ex.Message);
                actionFeedback.SuccessUnsuccess = false;
                return View(vm);
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditProjectType(int id)
        {
            ProjectType projectType = await featureUnitOfWorkRepository.projectTypeRepository.GetProjectType(id);
            IEnumerable<Feature> features = featureUnitOfWorkRepository.featureRepository.GetAllFeatures();

            IEnumerable<Feature> selectedFeatures = projectType.ProjectTypeFeatures.Select(x => new Feature() 
            { 
                FeatureId = x.Feature.FeatureId,
                FeatureText = x.Feature.FeatureText
            });

            var selectList = new List<SelectListItem>();
            foreach (Feature feature in features)
            {
                selectList.Add(new SelectListItem(
                        feature.FeatureText, 
                        feature.FeatureId.ToString(), 
                        selectedFeatures.Select(x => x.FeatureId).Contains(feature.FeatureId)
                    ));
            }

            var vm = new EditProjectTypeViewModel()
            {
                ProjectTypeId = projectType.ProjectTypeId,
                ProjectTypeName = projectType.ProjectTypeName,
                DaysToDeliver = projectType.DaysToDeliver,
                Rate = projectType.Rate,
                StarQuality = projectType.StarQuality,
                Features = selectList
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProjectType (EditProjectTypeViewModel vm)
        {
            List<SelectListItem> itemsInSelectList = new List<SelectListItem>();

            try
            {
                ProjectType projectType;
                projectType = await featureUnitOfWorkRepository.projectTypeRepository.GetProjectType(vm.ProjectTypeId);

                if(projectType == null)
                {
                    throw new ProjectTypeNotFoundException("The requested Project Type does not exist", vm.ProjectTypeId);
                }

                var retrievedFeatures = featureUnitOfWorkRepository.featureRepository.GetAllFeatures();

                var AllFeaturesInprojectType = projectType.ProjectTypeFeatures.Select(x => new Feature()
                {
                    FeatureId = x.Feature.FeatureId,
                    FeatureText = x.Feature.FeatureText
                });
                foreach (var item in retrievedFeatures)
                {
                    itemsInSelectList.Add(new SelectListItem
                    {
                        Value = item.FeatureId.ToString(),
                        Text = item.FeatureText,
                        Selected = AllFeaturesInprojectType.Select(x => x.FeatureId).Contains(item.FeatureId)
                    });
                }

                if (!ModelState.IsValid)
                {
                    vm.Features = itemsInSelectList;
                    return View(vm);
                }

                // update according to the View Model
                projectType.ProjectTypeName = vm.ProjectTypeName;
                projectType.Rate = vm.Rate;
                projectType.StarQuality = vm.StarQuality;

                var selectedFeatures = vm.SelectedFeatures;
                var existingFeatures = projectType.ProjectTypeFeatures.Select(x => x.FeatureId).ToList();
                var existingFeaturesString = new List<string>();
                existingFeatures.ForEach(x => existingFeaturesString.Add(x.ToString()));

                var toAdd = selectedFeatures.Except(existingFeaturesString).ToList();
                var toRemove = existingFeaturesString.Except(selectedFeatures).ToList();

                // using toAdd and toRemove
                projectType.ProjectTypeFeatures = projectType.ProjectTypeFeatures.Where(x => !toRemove.Contains(x.FeatureId.ToString())).ToList();
                foreach (var item in toAdd)
                {
                    if(!Int32.TryParse(item, out int resultId))
                    {
                        ModelState.AddModelError(string.Empty, "Someone might've interfered with the inspect element :))");
                        return View();
                    }


                    projectType.ProjectTypeFeatures.Add(new ProjectTypeFeature()
                    {
                        FeatureId = resultId,
                        ProjectTypeId = projectType.ProjectTypeId
                    });
                }

                featureUnitOfWorkRepository.Save();
                actionFeedback.SuccessUnsuccess = true;
                logger.LogInformation($"A Project Type with Id: {projectType.ProjectTypeId} was successfully edited");
                return RedirectToAction("index");
            }

            catch (ProjectTypeNotFoundException ex)
            {
                actionFeedback.SuccessUnsuccess = false;
                logger.LogError(ex, $"Unsuccessful operation. User {User.Identity.Name} requested ProjectTypeId: {ex.ProjectTypeId}. {ex.Message}");
                return RedirectToAction("Index");
            }

            catch(Exception ex)
            {
                vm.Features = itemsInSelectList;
                ModelState.AddModelError(string.Empty, "Unsuccessful operation. A strange error occured. Try again later.");
                logger.LogError(ex, $"Unsuccessful operation. User {User.Identity.Name} requested ProjectTypeId: {vm.ProjectTypeId}. The task was unsuccessful. Message: {ex.Message}");
                return View(vm);
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteProjectType (int Id)
        {
            var projectType = await featureUnitOfWorkRepository.projectTypeRepository.GetProjectType(Id);
            var vm = mapper.Map<ProjectTypeViewModel>(projectType);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProjectTypePost(int id)
        {
            try
            {
                var projectType = await featureUnitOfWorkRepository.projectTypeRepository.GetProjectType(id);

                if(projectType == null)
                {
                    throw new ProjectTypeNotFoundException("Project Type Id not found.", id);
                }

                await featureUnitOfWorkRepository.projectTypeRepository.Delete(projectType.ProjectTypeId);
                featureUnitOfWorkRepository.Save();
                actionFeedback.SuccessUnsuccess = true;
                logger.LogInformation($"A Project Type with Id: {projectType.ProjectTypeId} was successfully deleted");
                return RedirectToAction("Index");
            }

            catch(ProjectTypeNotFoundException ex)
            {
                actionFeedback.SuccessUnsuccess = false;
                logger.LogError(ex, $"Unsuccessful operation. User {User.Identity.Name} requested deletion of ProjectTypeId: {ex.ProjectTypeId}. {ex.Message}");
                return RedirectToAction("Index");
            }

            catch(Exception ex)
            {
                actionFeedback.SuccessUnsuccess = false;
                logger.LogError(ex, $"Unsuccessful operation. User {User.Identity.Name} requested deletion of ProjectTypeId: . {ex.Message}");
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<IActionResult> ProjectTypeDetails(int id)
        {
            try
            {
                var projectType = await featureUnitOfWorkRepository.projectTypeRepository.GetProjectType(id);
                if (projectType == null)
                {
                    throw new ProjectTypeNotFoundException("Project Type Id not found.", id);
                }
                var vm = mapper.Map<ProjectTypeViewModel>(projectType);
                return View(vm);
            }
            catch (ProjectTypeNotFoundException ex)
            {
                actionFeedback.SuccessUnsuccess = false;
                logger.LogError(ex, $"Unsuccessful operation. User {User.Identity.Name} requested details of ProjectTypeId: {ex.ProjectTypeId}. Message: {ex.Message}");
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                actionFeedback.SuccessUnsuccess = false;
                logger.LogError(ex, $"Unsuccessful operation. User {User.Identity.Name} requested details of ProjectTypeId: {id}. Message: {ex.Message}");
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult ProjectTypesPreview()
        {
            var ProjectTypes = featureUnitOfWorkRepository.projectTypeRepository.GetAllProjectTypes();
            return PartialView(ProjectTypes);
        }



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