namespace StrongMe.Web.ViewModels.Instructor.TemplatePrograms
{
    using System.Collections.Generic;

    using StrongMe.Data.Models;
    using StrongMe.Services.Mapping;

    public class EditTemplateProgramInputModel : BaseTemplateProgramInputModel, IMapFrom<TemplateProgram>
    {
        public int Id { get; set; }

        public IEnumerable<EditTemplateProgramDetailInputModel> Details { get; set; }
    }
}
