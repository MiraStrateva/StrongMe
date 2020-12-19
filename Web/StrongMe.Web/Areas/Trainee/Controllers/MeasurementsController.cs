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
    using StrongMe.Web.ViewModels.Trainee.Measurements;

    [Area("Trainee")]
    [Authorize(Roles = GlobalConstants.TraineeRoleName)]
    public class MeasurementsController : BaseController
    {
        private readonly IMeasurementsService measurementService;
        private readonly UserManager<ApplicationUser> userManager;

        public MeasurementsController(IMeasurementsService measurementService, UserManager<ApplicationUser> userManager)
        {
            this.measurementService = measurementService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            var user = await this.userManager.GetUserAsync(this.User);
            var viewModel = new MeasurementListViewModel
            {
                ItemsPerPage = GlobalConstants.ItemsPerPage,
                PageNumber = id,
                ItemsCount = this.measurementService.GetCount(user.Id),
                Measurements = this.measurementService.GetAll<MeasurementInputModel>(id, GlobalConstants.ItemsPerPage, user.Id),
            };

            return this.View(viewModel);
        }

        public IActionResult Create()
        {
            var viewModel = new CreateMeasurmentInputModel();

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMeasurmentInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.measurementService.CreateAsync(input, user.Id);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            this.TempData["Message"] = "Measurement added successfully.";

            return this.RedirectToAction("All");
        }

        public IActionResult Edit(int id)
        {
            var inputModel = this.measurementService.GetById<MeasurementInputModel>(id);

            return this.View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, MeasurementInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            try
            {
                await this.measurementService.UpdateAsync(input);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            this.TempData["Message"] = "Measurement updated successfully.";

            return this.RedirectToAction("All");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await this.measurementService.DeleteAsync(id);
            return this.RedirectToAction(nameof(this.All));
        }
    }
}
