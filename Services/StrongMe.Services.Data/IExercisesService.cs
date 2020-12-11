namespace StrongMe.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using StrongMe.Web.ViewModels.Instructor.Exercises;

    public interface IExercisesService
    {
        Task CreateAsync(CreateExerciseInputModel input, string userId, string imagePath);

        int GetCount();

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage, string userId);

        IEnumerable<T> GetAll<T>(string userId);

        T GetById<T>(int id);

        Task UpdateAsync(int id, EditExerciseInputModel input, string userId, string imagePath);

        Task DeleteAsync(int id);

        Task<int> DeleteImageAsync(string id);
    }
}
