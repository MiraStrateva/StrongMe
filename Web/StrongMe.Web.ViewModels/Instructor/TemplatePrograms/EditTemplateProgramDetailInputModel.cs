namespace StrongMe.Web.ViewModels.Instructor.TemplatePrograms
{
    using StrongMe.Data.Models;
    using StrongMe.Services.Mapping;

    public class EditTemplateProgramDetailInputModel : BaseTemplateProgramDetailInputModel, IMapFrom<TemplateProgramDetail>
    {
        public int Id { get; set; }

        public int TemplateProgramId { get; set; }
    }
}
