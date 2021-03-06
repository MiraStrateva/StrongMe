﻿namespace StrongMe.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using StrongMe.Web.ViewModels.Instructor.TemplatePrograms;

    public interface ITemplateProgramsService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs(string userId);

        int GetCount();

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage, string userId);

        T GetById<T>(int id);

        Task UpdateAsync(int id, EditTemplateProgramInputModel input);

        Task CreateAsync(CreateTemplateProgramInputModel input, string userId);

        Task DeleteAsync(int id);
    }
}
