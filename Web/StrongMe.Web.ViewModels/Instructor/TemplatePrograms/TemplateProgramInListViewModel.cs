namespace StrongMe.Web.ViewModels.Instructor.TemplatePrograms
{
    using StrongMe.Data.Models;
    using StrongMe.Services.Mapping;

    public class TemplateProgramInListViewModel : IMapFrom<TemplateProgram>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
