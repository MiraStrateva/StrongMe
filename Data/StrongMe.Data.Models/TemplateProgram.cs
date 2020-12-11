namespace StrongMe.Data.Models
{
    using System.Collections.Generic;

    using StrongMe.Data.Common.Models;

    public class TemplateProgram : BaseDeletableModel<int>
    {
        public TemplateProgram()
        {
            this.Details = new HashSet<TemplateProgramDetail>();
        }

        public string Name { get; set; }

        public string Notes { get; set; }

        public string TrainerId { get; set; }

        public virtual ICollection<TemplateProgramDetail> Details { get; set; }
    }
}
