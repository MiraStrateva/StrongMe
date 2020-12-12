namespace StrongMe.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using StrongMe.Data.Common.Repositories;
    using StrongMe.Data.Models;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoriesRepository;

        public CategoriesService(IDeletableEntityRepository<Category> categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        public async Task CreateAsync(Category input)
        {
            await this.categoriesRepository.AddAsync(input);
            await this.categoriesRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category = this.categoriesRepository.AllWithDeleted().FirstOrDefault(x => x.Id == id);
            this.categoriesRepository.Delete(category);
            await this.categoriesRepository.SaveChangesAsync();
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs()
        {
            return this.categoriesRepository.AllAsNoTracking()
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                })
                .OrderBy(x => x.Name)
                .ToList().Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }

        public IEnumerable<Category> GetAllWithDeleted()
        {
            return this.categoriesRepository.AllWithDeleted().ToList();
        }

        public Category GetById(int id)
        {
            var category = this.categoriesRepository.AllAsNoTrackingWithDeleted()
                .Where(x => x.Id == id)
                .FirstOrDefault();

            return category;
        }

        public async Task UpdateAsync(int id, Category input)
        {
            this.categoriesRepository.Update(input);
            await this.categoriesRepository.SaveChangesAsync();
        }
    }
}
