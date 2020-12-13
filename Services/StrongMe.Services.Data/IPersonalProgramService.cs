﻿namespace StrongMe.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using StrongMe.Web.ViewModels.Trainee.PersonalPrograms;

    public interface IPersonalProgramService
    {
        IEnumerable<T> GetAll<T>(string userId);

        T GetById<T>(int id);

        Task UpdateAsync(int id, EditPersonalProgramInputModel input);

        Task CreateAsync(CreatePersonalProgramInputModel input, string userId);

        Task DeleteAsync(int id);
    }
}