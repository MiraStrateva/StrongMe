﻿namespace StrongMe.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using StrongMe.Web.ViewModels.Trainee.Measurements;

    public interface IMeasurementsService
    {
        Task CreateAsync(CreateMeasurmentInputModel input, string userId);

        int GetCount(string userId);

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage, string userId);

        T GetById<T>(int id);

        Task UpdateAsync(MeasurementInputModel input);

        Task DeleteAsync(int id);
    }
}
