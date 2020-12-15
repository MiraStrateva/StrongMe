namespace StrongMe.Web.Areas.Trainee.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using StrongMe.Common;
    using StrongMe.Data.Models;
    using StrongMe.Services.Data;
    using StrongMe.Web.Controllers;
    using StrongMe.Web.ViewModels.Trainee.Trainings;

    [Area("Trainee")]
    [Authorize(Roles = GlobalConstants.TraineeRoleName)]
    public class TrainingsController : BaseController
    {
        private readonly ITrainingsService trainingsService;
        private readonly IPersonalProgramsService personalProgramsService;
        private readonly UserManager<ApplicationUser> userManager;

        public TrainingsController(
            ITrainingsService trainingsService,
            IPersonalProgramsService personalProgramsService,
            UserManager<ApplicationUser> userManager)
        {
            this.trainingsService = trainingsService;
            this.personalProgramsService = personalProgramsService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            var user = await this.userManager.GetUserAsync(this.User);
            var viewModel = new TrainingListViewModel
            {
                ItemsPerPage = GlobalConstants.ItemsPerPage,
                PageNumber = id,
                ItemsCount = this.trainingsService.GetCount(),
                Trainings = this.trainingsService.GetAll<TrainingInputModel>(id, GlobalConstants.ItemsPerPage, user.Id),
            };

            return this.View(viewModel);
        }

        public IActionResult Create()
        {
            var viewModel = new CreateTrainingInputModel()
            {
                PersonalPrograms = this.personalProgramsService.GetAllAsKeyValuePairs(),
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTrainingInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.PersonalPrograms = this.personalProgramsService.GetAllAsKeyValuePairs();
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.trainingsService.CreateAsync(input, user.Id);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                input.PersonalPrograms = this.personalProgramsService.GetAllAsKeyValuePairs();
                return this.View(input);
            }

            this.TempData["Message"] = "Training added successfully.";

            return this.RedirectToAction("All");
        }

        public IActionResult Edit(int id)
        {
            var inputModel = this.trainingsService.GetById<TrainingInputModel>(id);
            inputModel.PersonalPrograms = this.personalProgramsService.GetAllAsKeyValuePairs();

            return this.View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, TrainingInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.PersonalPrograms = this.personalProgramsService.GetAllAsKeyValuePairs();
                return this.View(input);
            }

            try
            {
                await this.trainingsService.UpdateAsync(input);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                input.PersonalPrograms = this.personalProgramsService.GetAllAsKeyValuePairs();
                return this.View(input);
            }

            this.TempData["Message"] = "Training updated successfully.";

            return this.RedirectToAction("All");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await this.trainingsService.DeleteAsync(id);
            return this.RedirectToAction(nameof(this.All));
        }
    }
}
