namespace StrongMe.Web.ViewModels.Instructor.TemplatePrograms
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public abstract class BaseTemplateProgramDetailInputModel
    {
        [Required]
        public int ExerciseId { get; set; }

        public string ExerciseName { get; set; }

        [Required]
        [Range(1, 10, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int SeriesCount { get; set; }

        [Range(0, 30, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int Repetitions { get; set; }

        [Range(0, 120, ErrorMessage = "Value for {0} must be between {1} and {2} seconds.")]
        public int Break { get; set; }
    }
}
