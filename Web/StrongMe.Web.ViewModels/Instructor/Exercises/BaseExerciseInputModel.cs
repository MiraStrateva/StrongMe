namespace StrongMe.Web.ViewModels.Instructor.Exercises
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public abstract class BaseExerciseInputModel
    {
        [Required]
        [MinLength(2)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int BodyPartId { get; set; }

        public IEnumerable<IFormFile> ImageFiles { get; set; }

        public IEnumerable<KeyValuePair<string, string>> CategoriesItems { get; set; }

        public IEnumerable<KeyValuePair<string, string>> BodyPartsItems { get; set; }
    }
}
