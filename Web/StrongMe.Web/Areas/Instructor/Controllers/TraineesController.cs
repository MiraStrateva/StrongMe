namespace StrongMe.Web.Areas.Instructor.Controllers
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
    using StrongMe.Web.ViewModels.Instructor.Trainees;
    using StrongMe.Web.ViewModels.Trainee.PersonalPrograms;

    [Area("Instructor")]
    [Authorize(Roles = GlobalConstants.InstructurRoleName)]
    public class TraineesController : BaseController
    {
        private readonly ITraineesService traineesService;
        private readonly IPersonalProgramsService personalProgramsService;
        private readonly ITemplateProgramsService templateProgramsService;
        private readonly UserManager<ApplicationUser> userManager;

        public TraineesController(
            ITraineesService traineesService,
            IPersonalProgramsService personalProgramsService,
            ITemplateProgramsService templateProgramsService,
            UserManager<ApplicationUser> userManager)
        {
            this.traineesService = traineesService;
            this.personalProgramsService = personalProgramsService;
            this.templateProgramsService = templateProgramsService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> All()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var viewModel = new TraineePersonalProgramsListViewModel
            {
                TraineePersonalPrograms = this.traineesService.GetAll(user.Id),
            };

            return this.View(viewModel);
        }

        public IActionResult AddPersonalProgram(string id)
        {
            var viewModel = new CreatePersonalProgramInputModel()
            {
                TraineeId = id,
                TemplatePrograms = this.templateProgramsService.GetAllAsKeyValuePairs(),
            };
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddPersonalProgram(CreatePersonalProgramInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.TemplatePrograms = this.templateProgramsService.GetAllAsKeyValuePairs();
                return this.View(input);
            }

            try
            {
                await this.personalProgramsService.CreateAsync(input);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                input.TemplatePrograms = this.templateProgramsService.GetAllAsKeyValuePairs();
                return this.View(input);
            }

            this.TempData["Message"] = "Personal program added successfully.";

            return this.RedirectToAction("All");
        }

        [HttpPost]
        public async Task<IActionResult> DeletePersonalProgram(int id)
        {
            await this.personalProgramsService.DeleteAsync(id);

            this.TempData["Message"] = "Personal program deleted successfully.";

            return this.RedirectToAction(nameof(this.All));
        }

        public IActionResult EditPersonalProgram(int id)
        {
            var inputModel = this.personalProgramsService.GetById<EditPersonalProgramInputModel>(id);
            inputModel.TemplatePrograms = this.templateProgramsService.GetAllAsKeyValuePairs();

            return this.View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditPersonalProgram(int id, EditPersonalProgramInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.TemplatePrograms = this.templateProgramsService.GetAllAsKeyValuePairs();
                return this.View(input);
            }

            await this.personalProgramsService.UpdateAsync(id, input);
            return this.RedirectToAction(nameof(this.All));
        }
    }
}
