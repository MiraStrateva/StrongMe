namespace StrongMe.Web.ViewModels.Instructor.TemplatePrograms
{
    using StrongMe.Common;
    using System;
    using System.ComponentModel.DataAnnotations;

    public abstract class BaseTemplateProgramDetailInputModel
    {
        [Required]
        public int ExerciseId { get; set; }

        public string ExerciseName { get; set; }

        [Required]
        [Range(1, 10, ErrorMessage = GlobalConstants.ValidationRageMessage)]
        public int SeriesCount { get; set; }

        [Range(0, 30, ErrorMessage = GlobalConstants.ValidationRageMessage)]
        public int Repetitions { get; set; }

        [Range(0, 120, ErrorMessage = GlobalConstants.ValidationRageMessage)]
        public int Break { get; set; }
    }
}
