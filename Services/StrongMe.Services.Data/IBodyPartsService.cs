namespace StrongMe.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using StrongMe.Data.Models;

    public interface IBodyPartsService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs();

        IEnumerable<BodyPart> GetAllWithDeleted();

        BodyPart GetById(int id);

        Task CreateAsync(BodyPart input);

        Task UpdateAsync(int id, BodyPart input);

        Task DeleteAsync(int id);
    }
}
