namespace StrongMe.Web.Areas.Instructor.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using StrongMe.Common;
    using StrongMe.Data.Models;
    using StrongMe.Services.Data;
    using StrongMe.Web.Controllers;
    using StrongMe.Web.ViewModels.Instructor.Common;
    using StrongMe.Web.ViewModels.Instructor.Exercises;
    using StrongMe.Web.ViewModels.Instructor.TemplatePrograms;

    [Area("Instructor")]
    [Authorize(Roles = GlobalConstants.InstructurRoleName)]
    public class TemplateProgramsController : BaseController
    {
        private readonly ITemplateProgramsService templateProgramsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICategoriesService categoriesService;
        private readonly IBodyPartsService bodyPartsService;
        private readonly IExercisesService exercisesService;

        public TemplateProgramsController(
            ITemplateProgramsService templateProgramsService,
            UserManager<ApplicationUser> userManager,
            ICategoriesService categoriesService,
            IBodyPartsService bodyPartsService,
            IExercisesService exercisesService)
        {
            this.templateProgramsService = templateProgramsService;
            this.userManager = userManager;
            this.categoriesService = categoriesService;
            this.bodyPartsService = bodyPartsService;
            this.exercisesService = exercisesService;
        }

        public async Task<IActionResult> All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            var user = await this.userManager.GetUserAsync(this.User);

            var viewModel = new TemplateProgramListViewModel
            {
                ItemsPerPage = GlobalConstants.ItemsPerPage,
                PageNumber = id,
                ItemsCount = this.templateProgramsService.GetCount(),
                TemplatePrograms = this.templateProgramsService.GetAll<TemplateProgramInListViewModel>(id, GlobalConstants.ItemsPerPage, user.Id),
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> Create()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            // var categoriesItems = new List<KeyValuePair<string, string>>()
            //                            {
            //                                new KeyValuePair<string, string>("0", "All"),
            //                            };
            // categoriesItems.AddRange(this.categoriesService.GetAllAsKeyValuePairs());

            // var bodyPartsItems = new List<KeyValuePair<string, string>>()
            //                            {
            //                                new KeyValuePair<string, string>("0", "All"),
            //                            };
            // bodyPartsItems.AddRange(this.bodyPartsService.GetAllAsKeyValuePairs());
            var viewModel = new CreateTemplateProgramInputModel()
            {
                ExerciseList = new SelectExerciseViewModel()
                {
                    // CategoriesItems = categoriesItems,
                    // BodyPartsItems = bodyPartsItems,
                    ExercisesItems = this.exercisesService.GetAll<SingleExerciseViewModel>(user.Id),
                },
            };
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTemplateProgramInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.templateProgramsService.CreateAsync(input, user.Id);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            this.TempData["Message"] = "Program template added successfully.";

            return this.RedirectToAction("All");
        }
    }
}
