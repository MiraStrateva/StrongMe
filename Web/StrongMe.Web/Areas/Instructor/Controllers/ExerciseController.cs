namespace StrongMe.Web.Areas.Instructor.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using StrongMe.Services.Data;
    using StrongMe.Web.Controllers;
    using StrongMe.Web.ViewModels.Instructor.Exercises;

    public class ExerciseController : ApiController
    {
        private readonly IExercisesService exercisesService;

        public ExerciseController(IExercisesService exercisesService)
        {
            this.exercisesService = exercisesService;
        }

        [Route(nameof(Details) + PathSeparator + Id)]
        public IActionResult Details(int id)
            => this.Ok(this.exercisesService.GetById<SingleExerciseViewModel>(id));
    }
}
