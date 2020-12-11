namespace StrongMe.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using StrongMe.Web.ViewModels.Instructor.TemplatePrograms;

    public interface ITemplateProgramsService
    {
        int GetCount();

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage, string userId);

        T GetById<T>(int id);

        Task CreateAsync(CreateTemplateProgramInputModel input, string userId);
    }
}
