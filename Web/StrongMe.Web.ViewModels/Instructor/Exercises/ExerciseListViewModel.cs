namespace StrongMe.Web.ViewModels.Instructor.Exercises
{
    using System.Collections.Generic;

    public class ExerciseListViewModel : PagingViewModel
    {
        public IEnumerable<ExerciseByGroupListViewModel> ExerciseGroups { get; set; }
    }
}
