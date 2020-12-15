namespace StrongMe.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using StrongMe.Web.ViewModels.Trainee.Trainings;

    public interface ITrainingsService
    {
        Task CreateAsync(CreateTrainingInputModel input, string userId);

        int GetCount();

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage, string userId);

        T GetById<T>(int id);

        Task UpdateAsync(TrainingInputModel input);

        Task DeleteAsync(int id);
    }
}
