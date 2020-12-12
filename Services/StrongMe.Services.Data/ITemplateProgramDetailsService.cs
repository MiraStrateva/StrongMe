namespace StrongMe.Services.Data
{
    using System.Threading.Tasks;

    using StrongMe.Web.ViewModels.Instructor.TemplatePrograms;

    public interface ITemplateProgramDetailsService
    {
        T GetById<T>(int id);

        Task UpdateAsync(int id, EditTemplateProgramDetailInputModel input);

        Task DeleteAsync(int id);
    }
}
