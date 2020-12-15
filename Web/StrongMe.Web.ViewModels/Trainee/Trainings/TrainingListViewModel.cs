namespace StrongMe.Web.ViewModels.Trainee.Trainings
{
    using System.Collections.Generic;

    public class TrainingListViewModel : PagingViewModel
    {
        public IEnumerable<TrainingInputModel> Trainings { get; set; }
    }
}
