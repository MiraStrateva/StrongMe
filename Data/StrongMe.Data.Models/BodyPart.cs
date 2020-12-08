namespace StrongMe.Data.Models
{
    using System.Collections.Generic;

    using StrongMe.Data.Common.Models;

    public class BodyPart : BaseDeletableModel<int>
    {
        public BodyPart()
        {
            this.Exercises = new HashSet<Exercise>();
        }

        public string Name { get; set; }

        public ICollection<Exercise> Exercises { get; set; }
    }
}
