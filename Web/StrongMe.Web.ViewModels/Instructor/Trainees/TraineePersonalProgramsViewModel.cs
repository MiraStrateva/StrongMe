namespace StrongMe.Web.ViewModels.Instructor.Trainees
{
    using System.Collections.Generic;

    using StrongMe.Web.ViewModels.Trainee.PersonalPrograms;

    public class TraineePersonalProgramsViewModel
    {
        public string TraineeId { get; set; }

        public string TraineeName { get; set; }

        public ICollection<PersonalProgramViewModel> PersonalPrograms { get; set; }
    }
}
