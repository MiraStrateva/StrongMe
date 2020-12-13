namespace StrongMe.Web.ViewModels.Trainee.Measurements
{
    using System.Collections.Generic;

    public class MeasurementListViewModel : PagingViewModel
    {
        public IEnumerable<MeasurementInputModel> Measurements { get; set; }
    }
}
