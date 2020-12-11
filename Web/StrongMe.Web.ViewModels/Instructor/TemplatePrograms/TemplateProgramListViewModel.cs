namespace StrongMe.Web.ViewModels.Instructor.TemplatePrograms
{
    using System.Collections.Generic;

    public class TemplateProgramListViewModel : PagingViewModel
    {
        public IEnumerable<TemplateProgramInListViewModel> TemplatePrograms { get; set; }
    }
}
