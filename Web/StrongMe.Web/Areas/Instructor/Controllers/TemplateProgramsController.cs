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
        private readonly ITemplateProgramDetailsService templateProgramDetailsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICategoriesService categoriesService;
        private readonly IBodyPartsService bodyPartsService;
        private readonly IExercisesService exercisesService;

        public TemplateProgramsController(
            ITemplateProgramsService templateProgramsService,
            ITemplateProgramDetailsService templateProgramDetailsService,
            UserManager<ApplicationUser> userManager,
            ICategoriesService categoriesService,
            IBodyPartsService bodyPartsService,
            IExercisesService exercisesService)
        {
            this.templateProgramsService = templateProgramsService;
            this.templateProgramDetailsService = templateProgramDetailsService;
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

        public IActionResult ById(int id)
        {
            var templateProgram = this.templateProgramsService.GetById<SingleTemplateProgramViewModel>(id);
            return this.View(templateProgram);
        }

        public async Task<IActionResult> Create()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var categoriesItems = new List<KeyValuePair<string, string>>()
                                        {
                                            new KeyValuePair<string, string>("0", "All"),
                                        };
            categoriesItems.AddRange(this.categoriesService.GetAllAsKeyValuePairs());

            var bodyPartsItems = new List<KeyValuePair<string, string>>()
                                        {
                                            new KeyValuePair<string, string>("0", "All"),
                                        };
            bodyPartsItems.AddRange(this.bodyPartsService.GetAllAsKeyValuePairs());
            var viewModel = new CreateTemplateProgramInputModel()
            {
                ExerciseList = new SelectExerciseViewModel()
                {
                    CategoriesItems = categoriesItems,
                    BodyPartsItems = bodyPartsItems,
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

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await this.templateProgramsService.DeleteAsync(id);
            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var inputModel = this.templateProgramsService.GetById<EditTemplateProgramInputModel>(id);

            var categoriesItems = new List<KeyValuePair<string, string>>()
                                        {
                                            new KeyValuePair<string, string>("0", "All"),
                                        };
            categoriesItems.AddRange(this.categoriesService.GetAllAsKeyValuePairs());

            var bodyPartsItems = new List<KeyValuePair<string, string>>()
                                        {
                                            new KeyValuePair<string, string>("0", "All"),
                                        };
            bodyPartsItems.AddRange(this.bodyPartsService.GetAllAsKeyValuePairs());

            var user = await this.userManager.GetUserAsync(this.User);
            inputModel.ExerciseList = new SelectExerciseViewModel()
            {
                CategoriesItems = categoriesItems,
                BodyPartsItems = bodyPartsItems,
                ExercisesItems = this.exercisesService.GetAll<SingleExerciseViewModel>(user.Id),
            };

            return this.View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditTemplateProgramInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.templateProgramsService.UpdateAsync(id, input);
            return this.RedirectToAction(nameof(this.All));
        }

        public IActionResult EditDetail(int id)
        {
            var inputModel = this.templateProgramDetailsService.GetById<EditTemplateProgramDetailInputModel>(id);
            return this.View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditDetail(int id, EditTemplateProgramDetailInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.templateProgramDetailsService.UpdateAsync(id, input);

            return this.RedirectToAction(nameof(this.ById), new { id = input.TemplateProgramId });
        }

        public IActionResult DeleteDetail(int id)
        {
            var inputModel = this.templateProgramDetailsService.GetById<EditTemplateProgramDetailInputModel>(id);
            return this.View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDetail(int id, EditTemplateProgramDetailInputModel input)
        {
            await this.templateProgramDetailsService.DeleteAsync(id);
            return this.RedirectToAction(nameof(this.ById), new { id = input.TemplateProgramId });
        }
    }
}
