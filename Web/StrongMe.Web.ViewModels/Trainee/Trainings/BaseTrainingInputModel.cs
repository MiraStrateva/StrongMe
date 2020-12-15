namespace StrongMe.Web.ViewModels.Trainee.Trainings
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public abstract class BaseTrainingInputModel
    {
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int PersonalProgramId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> PersonalPrograms { get; set; }
    }
}
