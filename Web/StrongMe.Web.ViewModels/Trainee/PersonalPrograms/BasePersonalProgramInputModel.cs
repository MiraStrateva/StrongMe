namespace StrongMe.Web.ViewModels.Trainee.PersonalPrograms
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public abstract class BasePersonalProgramInputModel
    {
        [Required]
        public string TraineeId { get; set; }

        [Required]
        public int TemplateProgramId { get; set; }

        [Required]
        [MinLength(20)]
        public string Description { get; set; }

        public IEnumerable<KeyValuePair<string, string>> TemplatePrograms { get; set; }
    }
}
