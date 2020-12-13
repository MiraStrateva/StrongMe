namespace StrongMe.Web.ViewModels.Trainee.PersonalPrograms
{
    using System.Collections.Generic;

    using StrongMe.Data.Models;
    using StrongMe.Services.Mapping;
    using StrongMe.Web.ViewModels.Instructor.TemplatePrograms;

    public class SinglePersonalProgramViewModel : IMapFrom<PersonalProgram>
    {
        public string TemplateProgramName { get; set; }

        public string Description { get; set; }

        public ICollection<TemplateProgramDetailInputModel> TemplateProgramDetails { get; set; }
    }
}
