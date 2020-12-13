namespace StrongMe.Web.ViewModels.Trainee.PersonalPrograms
{
    using System.ComponentModel.DataAnnotations;

    public abstract class BasePersonalProgramInputModel
    {
        [Required]
        public int TemplateProgramId { get; set; }

        [Required]
        [MinLength(20)]
        public string Description { get; set; }
    }
}
