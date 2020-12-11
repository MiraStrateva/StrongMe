namespace StrongMe.Data.Models
{
    using StrongMe.Data.Common.Models;

    public class TemplateProgramDetail : BaseDeletableModel<int>
    {
        public int TemplateProgramId { get; set; }

        public int ExerciseId { get; set; }

        public int SeriesCount { get; set; }

        public int Repetitions { get; set; }

        public int Break { get; set; }

        public int SortOrder { get; set; }

        public virtual Exercise Exercise { get; set; }

        public virtual TemplateProgram TemplateProgram { get; set; }
    }
}
