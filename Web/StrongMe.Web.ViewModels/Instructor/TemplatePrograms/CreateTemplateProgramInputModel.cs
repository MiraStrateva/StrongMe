namespace StrongMe.Web.ViewModels.Instructor.TemplatePrograms
{
    using System.Collections.Generic;

    public class CreateTemplateProgramInputModel : BaseTemplateProgramInputModel
    {
        public IEnumerable<CreateTemplateProgramDetailInputModel> Details { get; set; }
    }
}
