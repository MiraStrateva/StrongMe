namespace StrongMe.Web.ViewModels.Instructor.TemplatePrograms
{
    using StrongMe.Data.Models;
    using StrongMe.Services.Mapping;
    using System.Collections.Generic;

    public class SingleTemplateProgramViewModel : IMapFrom<TemplateProgram>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<TemplateProgramDetailInputModel> Details { get; set; }
    }
}
