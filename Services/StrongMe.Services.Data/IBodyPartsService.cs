namespace StrongMe.Services.Data
{
    using System.Collections.Generic;

    public interface IBodyPartsService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs();
    }
}
