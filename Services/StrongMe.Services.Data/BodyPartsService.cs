namespace StrongMe.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using StrongMe.Data.Common.Repositories;
    using StrongMe.Data.Models;

    public class BodyPartsService : IBodyPartsService
    {
        private readonly IDeletableEntityRepository<BodyPart> bodyPartsRepository;

        public BodyPartsService(IDeletableEntityRepository<BodyPart> bodyPartsRepository)
        {
            this.bodyPartsRepository = bodyPartsRepository;
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
    }
}
