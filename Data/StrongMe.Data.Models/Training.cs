namespace StrongMe.Data.Models
{
    using System;

    using StrongMe.Data.Common.Models;

    public class Training : BaseDeletableModel<int>
    {
        public string TraineeId { get; set; }

        public virtual ApplicationUser Trainee { get; set; }

        public int PersonalProgramId { get; set; }

        public virtual PersonalProgram PersonalProgram { get; set; }

        public DateTime Date { get; set; }
    }
}
