namespace StrongMe.Data.Models
{
    using System;

    using StrongMe.Data.Common.Models;

    public class Measurement : BaseDeletableModel<int>
    {
        public string TraineeId { get; set; }

        public virtual ApplicationUser Trainee { get; set; }

        public DateTime Date { get; set; }

        public decimal Weight { get; set; }

        public decimal Chest { get; set; }

        public decimal RightArmBiceps { get; set; }

        public decimal LeftArmBiceps { get; set; }

        public decimal UpWaist { get; set; }

        public decimal Waist { get; set; }

        public decimal DownWaist { get; set; }

        public decimal Hips { get; set; }

        public decimal RightThigh { get; set; }

        public decimal LeftThigh { get; set; }

        public decimal RightCalf { get; set; }

        public decimal LeftCalf { get; set; }

        public decimal Neck { get; set; }
    }
}
