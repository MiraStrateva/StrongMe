namespace StrongMe.Web.ViewModels.Trainee.Trainings
{
    using System;

    using StrongMe.Data.Models;
    using StrongMe.Services.Mapping;

    public class CreateTrainingInputModel : BaseTrainingInputModel, IMapTo<Training>
    {
        public CreateTrainingInputModel()
        {
            this.Date = DateTime.Now;
        }
    }
}
