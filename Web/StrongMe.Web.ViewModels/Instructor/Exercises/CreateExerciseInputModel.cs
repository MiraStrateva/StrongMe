namespace StrongMe.Web.ViewModels.Instructor.Exercises
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class CreateExerciseInputModel
    {
        [Required]
        [MinLength(2)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public int CategoryId { get; set; }

        public int BodyPartId { get; set; }

        public IEnumerable<IFormFile> Images { get; set; }

        public IEnumerable<KeyValuePair<string, string>> CategoriesItems { get; set; }

        public IEnumerable<KeyValuePair<string, string>> BodyPartItems { get; set; }
    }
}
