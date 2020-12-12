namespace StrongMe.Web.ViewModels.Instructor.TemplatePrograms
{
    using System.ComponentModel.DataAnnotations;

    using StrongMe.Web.ViewModels.Instructor.Common;

    public abstract class BaseTemplateProgramInputModel
    {
        [Required]
        [MinLength(2)]
        public string Name { get; set; }

        public SelectExerciseViewModel ExerciseList { get; set; }
    }
}
