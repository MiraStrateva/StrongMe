namespace StrongMe.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using StrongMe.Data.Models;

    public interface ICategoriesService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs();

        IEnumerable<Category> GetAllWithDeleted();

        Category GetById(int id);

        Task CreateAsync(Category input);

        Task UpdateAsync(int id, Category input);

        Task DeleteAsync(int id);
    }
}
