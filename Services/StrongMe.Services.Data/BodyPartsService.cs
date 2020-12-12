namespace StrongMe.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using StrongMe.Data.Common.Repositories;
    using StrongMe.Data.Models;

    public class BodyPartsService : IBodyPartsService
    {
        private readonly IDeletableEntityRepository<BodyPart> bodyPartsRepository;

        public BodyPartsService(IDeletableEntityRepository<BodyPart> bodyPartsRepository)
        {
            this.bodyPartsRepository = bodyPartsRepository;
        }

        public async Task CreateAsync(BodyPart input)
        {
            await this.bodyPartsRepository.AddAsync(input);
            await this.bodyPartsRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var bodyPart = this.bodyPartsRepository.AllWithDeleted().FirstOrDefault(x => x.Id == id);
            this.bodyPartsRepository.Delete(bodyPart);
            await this.bodyPartsRepository.SaveChangesAsync();
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs()
        {
            return this.bodyPartsRepository.AllAsNoTracking()
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                })
                .OrderBy(x => x.Name)
                .ToList().Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }

        public IEnumerable<BodyPart> GetAllWithDeleted()
        {
            return this.bodyPartsRepository.AllWithDeleted().ToList();
        }

        public BodyPart GetById(int id)
        {
            var bodyPart = this.bodyPartsRepository.AllAsNoTrackingWithDeleted()
                .Where(x => x.Id == id)
                .FirstOrDefault();

            return bodyPart;
        }

        public async Task UpdateAsync(int id, BodyPart input)
        {
            this.bodyPartsRepository.Update(input);
            await this.bodyPartsRepository.SaveChangesAsync();
        }
    }
}
