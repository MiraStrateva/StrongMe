namespace StrongMe.Data.Models
{
    using System.Collections.Generic;

    using StrongMe.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.Excercises = new HashSet<Excercise>();
        }

        public string Name { get; set; }

        public ICollection<Excercise> Excercises { get; set; }
    }
}
