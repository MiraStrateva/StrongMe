namespace StrongMe.Web.ViewModels.Trainee.Measurements
{
    using StrongMe.Data.Models;
    using StrongMe.Services.Mapping;

    public class MeasurementInputModel : BaseMeasurementInputModel, IMapFrom<Measurement>, IMapTo<Measurement>
    {
        public int Id { get; set; }

        public string TraineeId { get; set; }
    }
}
