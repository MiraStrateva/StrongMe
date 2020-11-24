namespace StrongMe.Web.Areas.Instructor.Controllers
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using StrongMe.Data.Models;
    using StrongMe.Services.Data;
    using StrongMe.Web.Controllers;
    using StrongMe.Web.ViewModels.Instructor.Exercises;

    [Area("Instructor")]
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

        public IActionResult Create()
        {
            var viewModel = new CreateExerciseInputModel();
            viewModel.CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs();
            viewModel.BodyPartItems = this.bodyPartsService.GetAllAsKeyValuePairs();
            return this.View(viewModel);
        }
    }
}
