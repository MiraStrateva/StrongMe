namespace StrongMe.Web.ViewModels.Instructor.Exercises
{
    using System.Collections.Generic;

    using StrongMe.Data.Models;
    using StrongMe.Services.Mapping;

    public class EditExerciseInputModel : BaseExerciseInputModel, IMapFrom<Exercise>
    {
        public int Id { get; set; }

        public IEnumerable<ImageInputModel> Images { get; set; }
    }
}
