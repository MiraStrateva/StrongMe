namespace StrongMe.Services.Data
{
    using System.Collections.Generic;

    using StrongMe.Web.ViewModels.Instructor.Trainees;

    public interface ITraineesService
    {
        IEnumerable<TraineePersonalProgramsViewModel> GetAll(string userId);
    }
}
