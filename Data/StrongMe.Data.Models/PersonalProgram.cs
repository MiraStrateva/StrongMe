namespace StrongMe.Data.Models
{
    using StrongMe.Data.Common.Models;

    public class PersonalProgram : BaseDeletableModel<int>
    {
        public int TemplateProgramId { get; set; }

        public virtual TemplateProgram TemplateProgram { get; set; }

        public string TraineeId { get; set; }

        public virtual ApplicationUser Trainee { get; set; }

        public string Description { get; set; }
    }
}
