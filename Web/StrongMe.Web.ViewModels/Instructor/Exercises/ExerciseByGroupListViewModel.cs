namespace StrongMe.Web.ViewModels.Instructor.Exercises
{
    using System.Collections.Generic;

    public class ExerciseByGroupListViewModel
    {
        public string Name { get; set; }

        public IEnumerable<ExerciseInListViewModel> Exercises { get; set; }
    }
}
