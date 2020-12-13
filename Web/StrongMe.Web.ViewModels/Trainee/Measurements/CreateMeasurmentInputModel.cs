namespace StrongMe.Web.ViewModels.Trainee.Measurements
{
    using System;

    using StrongMe.Data.Models;
    using StrongMe.Services.Mapping;

    public class CreateMeasurmentInputModel : BaseMeasurementInputModel, IMapTo<Measurement>
    {
        public CreateMeasurmentInputModel()
        {
            this.Date = DateTime.Now;
        }
    }
}
