namespace StrongMe.Web.ViewModels.Instructor.Common
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using StrongMe.Web.ViewModels.Instructor.Exercises;

    public class SelectExerciseViewModel
    {
        [Display(Name = "Select Category")]
        public int CategoryId { get; set; }

        [Display(Name = "Select Body Part")]
        public int BodyPartId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> CategoriesItems { get; set; }

        public IEnumerable<KeyValuePair<string, string>> BodyPartsItems { get; set; }

        public IEnumerable<SingleExerciseViewModel> ExercisesItems { get; set; }
    }
}
