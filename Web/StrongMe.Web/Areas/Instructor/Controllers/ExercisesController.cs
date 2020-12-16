namespace StrongMe.Web.Areas.Instructor.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using StrongMe.Common;
    using StrongMe.Data.Models;
    using StrongMe.Services.Data;
    using StrongMe.Web.Controllers;
    using StrongMe.Web.ViewModels.Instructor.Common;
    using StrongMe.Web.ViewModels.Instructor.Exercises;

    [Area("Instructor")]
    [Authorize(Roles = GlobalConstants.InstructurRoleName)]
    public class ExercisesController : BaseController
    {
        private readonly ICategoriesService categoriesService;
        private readonly IBodyPartsService bodyPartsService;
        private readonly IExercisesService exercisesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;

        public ExercisesController(
            ICategoriesService categoriesService,
            IBodyPartsService bodyPartsService,
            IExercisesService exercisesService,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment environment)
        {
            this.categoriesService = categoriesService;
            this.bodyPartsService = bodyPartsService;
            this.exercisesService = exercisesService;
            this.userManager = userManager;
            this.environment = environment;
        }

        public async Task<IActionResult> All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            this.ViewBag.All = true;
            var user = await this.userManager.GetUserAsync(this.User);
            var viewModel = new ExerciseListViewModel
            {
                ItemsPerPage = GlobalConstants.ItemsPerPage,
                PageNumber = id,
                ItemsCount = this.exercisesService.GetCount(),
                ExerciseGroups = new List<ExerciseByGroupListViewModel>()
                {
                    new ExerciseByGroupListViewModel
                    {
                        Name = string.Empty,
                        Exercises = this.exercisesService.GetAll<ExerciseInListViewModel>(id, GlobalConstants.ItemsPerPage, user.Id),
                    },
                },
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> AllByCategory(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            this.ViewBag.ByCategory = true;
            var user = await this.userManager.GetUserAsync(this.User);
            var viewModel = new ExerciseListViewModel
            {
                ItemsPerPage = GlobalConstants.ItemsPerPage,
                PageNumber = id,
                ItemsCount = this.exercisesService.GetCount(),
                ExerciseGroups = this.exercisesService
                .GetAll<ExerciseInListViewModel>(id, GlobalConstants.ItemsPerPage, user.Id)
                .GroupBy(e => e.CategoryName)
                .Select(g => new ExerciseByGroupListViewModel
                {
                    Name = g.Key,
                    Exercises = g,
                }),
            };

            return this.View(nameof(this.All), viewModel);
        }

        public async Task<IActionResult> AllByBodyPart(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            this.ViewBag.ByBodyPart = true;
            var user = await this.userManager.GetUserAsync(this.User);
            var viewModel = new ExerciseListViewModel
            {
                ItemsPerPage = GlobalConstants.ItemsPerPage,
                PageNumber = id,
                ItemsCount = this.exercisesService.GetCount(),
                ExerciseGroups = this.exercisesService
                .GetAll<ExerciseInListViewModel>(id, GlobalConstants.ItemsPerPage, user.Id)
                .GroupBy(e => e.BodyPartName)
                .Select(g => new ExerciseByGroupListViewModel
                {
                    Name = g.Key,
                    Exercises = g,
                }),
            };

            return this.View(nameof(this.All), viewModel);
        }

        public async Task<IActionResult> List([FromQuery] int categoryId, [FromQuery] int bodyPartId)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var exercisesItems = this.exercisesService.GetAll<SingleExerciseViewModel>(user.Id, categoryId, bodyPartId);

            return this.PartialView("_ExerciseListPartial", exercisesItems);
        }

        public IActionResult Create()
        {
            var viewModel = new CreateExerciseInputModel();
            viewModel.CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs();
            viewModel.BodyPartsItems = this.bodyPartsService.GetAllAsKeyValuePairs();

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateExerciseInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs();
                input.BodyPartsItems = this.bodyPartsService.GetAllAsKeyValuePairs();
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.exercisesService.CreateAsync(input, user.Id, $"{this.environment.WebRootPath}/images");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                input.CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs();
                input.BodyPartsItems = this.bodyPartsService.GetAllAsKeyValuePairs();
                return this.View(input);
            }

            this.TempData["Message"] = "Exercise added successfully.";

            return this.RedirectToAction("All");
        }

        [Authorize]
        public IActionResult ById(int id)
        {
            var exercise = this.exercisesService.GetById<SingleExerciseViewModel>(id);
            return this.View(exercise);
        }

        public IActionResult Edit(int id)
        {
            var inputModel = this.exercisesService.GetById<EditExerciseInputModel>(id);
            inputModel.CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs();
            inputModel.BodyPartsItems = this.bodyPartsService.GetAllAsKeyValuePairs();
            return this.View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditExerciseInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs();
                input.BodyPartsItems = this.bodyPartsService.GetAllAsKeyValuePairs();
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.exercisesService.UpdateAsync(id, input, user.Id, $"{this.environment.WebRootPath}/images");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                input.CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs();
                input.BodyPartsItems = this.bodyPartsService.GetAllAsKeyValuePairs();
                return this.View(input);
            }

            return this.RedirectToAction(nameof(this.ById), new { id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await this.exercisesService.DeleteAsync(id);
            return this.RedirectToAction(nameof(this.All));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteImage(string id)
        {
            var exerciseId = await this.exercisesService.DeleteImageAsync(id);
            return this.RedirectToAction(nameof(this.Edit), new { id = exerciseId });
        }
    }
}
