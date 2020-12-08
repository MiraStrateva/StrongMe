namespace StrongMe.Data.Models
{
    using System.Collections.Generic;

    using StrongMe.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.Exercises = new HashSet<Exercise>();
        }

        public string Name { get; set; }

        public ICollection<Exercise> Exercises { get; set; }
    }
}
