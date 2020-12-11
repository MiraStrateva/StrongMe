namespace StrongMe.Web.ViewModels.Instructor.TemplatePrograms
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using StrongMe.Web.ViewModels.Instructor.Common;

    public class CreateTemplateProgramInputModel
    {
        [Required]
        [MinLength(2)]
        public string Name { get; set; }

        public IEnumerable<CreateTemplateProgramDetailInputModel> Details { get; set; }

        public SelectExerciseViewModel ExerciseList { get; set; }
    }
}
