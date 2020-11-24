namespace StrongMe.Data.Models
{
    using System.Collections.Generic;

    using StrongMe.Data.Common.Models;

    public class BodyPart : BaseDeletableModel<int>
    {
        public BodyPart()
        {
            this.Excercises = new HashSet<Excercise>();
        }

        public string Name { get; set; }

        public ICollection<Excercise> Excercises { get; set; }
    }
}
