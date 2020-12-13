namespace StrongMe.Web.ViewModels.Trainee.Measurements
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public abstract class BaseMeasurementInputModel
    {
        [Required]
        public DateTime Date { get; set; }

        [Range(30, 999.99, ErrorMessage = "{0} Must be between {1} to {2}")]
        public decimal Weight { get; set; }

        [Range(30, 199.99, ErrorMessage = "{0} Must be between {1} to {2}")]
        public decimal Chest { get; set; }

        [Range(10, 99.99, ErrorMessage = "{0} Must be between {1} to {2}")]
        public decimal RightArmBiceps { get; set; }

        [Range(10, 99.99, ErrorMessage = "{0} Must be between {1} to {2}")]
        public decimal LeftArmBiceps { get; set; }

        [Range(30, 199.99, ErrorMessage = "{0} Must be between {1} to {2}")]
        public decimal UpWaist { get; set; }

        [Range(30, 199.99, ErrorMessage = "{0} Must be between {1} to {2}")]
        public decimal Waist { get; set; }

        [Range(30, 199.99, ErrorMessage = "{0} Must be between {1} to {2}")]
        public decimal DownWaist { get; set; }

        [Range(30, 199.99, ErrorMessage = "{0} Must be between {1} to {2}")]
        public decimal Hips { get; set; }

        [Range(10, 99.99, ErrorMessage = "{0} Must be between {1} to {2}")]
        public decimal RightThigh { get; set; }

        [Range(10, 99.99, ErrorMessage = "{0} Must be between {1} to {2}")]
        public decimal LeftThigh { get; set; }

        [Range(10, 99.99, ErrorMessage = "{0} Must be between {1} to {2}")]
        public decimal RightCalf { get; set; }

        [Range(10, 99.99, ErrorMessage = "{0} Must be between {1} to {2}")]
        public decimal LeftCalf { get; set; }

        [Range(10, 99.99, ErrorMessage = "{0} Must be between {1} to {2}")]
        public decimal Neck { get; set; }
    }
}
