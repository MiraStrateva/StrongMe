namespace StrongMe.Data.Models
{
    using System;
    using System.Collections.Generic;

    using StrongMe.Data.Common.Models;

    public class Excercise : BaseDeletableModel<int>
    {
        public Excercise()
        {
            this.Images = new HashSet<Image>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public Guid TrainerId { get; set; }

        public virtual ApplicationUser Trainer { get; set; }

        public int CategoryId { get; set; }

        public int BodyPartId { get; set; }

        public virtual Category Category { get; set; }

        public virtual BodyPart BodyPart { get; set; }

        public virtual ICollection<Image> Images { get; set; }
    }
}
