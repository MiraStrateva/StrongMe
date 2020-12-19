namespace StrongMe.Web.ViewModels.Trainee.Measurements
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using StrongMe.Common;

    public abstract class BaseMeasurementInputModel
    {
        [Required]
        public DateTime Date { get; set; }

        [Range(30, 999.99, ErrorMessage = GlobalConstants.ValidationRageMessage)]
        public decimal Weight { get; set; }

        [Range(30, 199.99, ErrorMessage = GlobalConstants.ValidationRageMessage)]
        public decimal Chest { get; set; }

        [Range(10, 99.99, ErrorMessage = GlobalConstants.ValidationRageMessage)]
        public decimal RightArmBiceps { get; set; }

        [Range(10, 99.99, ErrorMessage = GlobalConstants.ValidationRageMessage)]
        public decimal LeftArmBiceps { get; set; }

        [Range(30, 199.99, ErrorMessage = GlobalConstants.ValidationRageMessage)]
        public decimal UpWaist { get; set; }

        [Range(30, 199.99, ErrorMessage = GlobalConstants.ValidationRageMessage)]
        public decimal Waist { get; set; }

        [Range(30, 199.99, ErrorMessage = GlobalConstants.ValidationRageMessage)]
        public decimal DownWaist { get; set; }

        [Range(30, 199.99, ErrorMessage = GlobalConstants.ValidationRageMessage)]
        public decimal Hips { get; set; }

        [Range(10, 99.99, ErrorMessage = GlobalConstants.ValidationRageMessage)]
        public decimal RightThigh { get; set; }

        [Range(10, 99.99, ErrorMessage = GlobalConstants.ValidationRageMessage)]
        public decimal LeftThigh { get; set; }

        [Range(10, 99.99, ErrorMessage = GlobalConstants.ValidationRageMessage)]
        public decimal RightCalf { get; set; }

        [Range(10, 99.99, ErrorMessage = GlobalConstants.ValidationRageMessage)]
        public decimal LeftCalf { get; set; }

        [Range(10, 99.99, ErrorMessage = GlobalConstants.ValidationRageMessage)]
        public decimal Neck { get; set; }
    }
}
