namespace StrongMe.Web.ViewModels.Instructor.TemplatePrograms
{
    using System.Linq;

    using AutoMapper;
    using StrongMe.Data.Models;
    using StrongMe.Services.Mapping;

    public class TemplateProgramDetailInputModel : IMapFrom<TemplateProgramDetail>, IHaveCustomMappings
    {
        public int ExerciseId { get; set; }

        public string ExerciseName { get; set; }

        public string ImageUrl { get; set; }

        public int SeriesCount { get; set; }

        public int Repetitions { get; set; }

        public int Break { get; set; }

        public int SortOrder { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<TemplateProgramDetail, TemplateProgramDetailInputModel>()
                .ForMember(x => x.ImageUrl, opt =>
                    opt.MapFrom(x =>
                        x.Exercise.Images.OrderByDescending(i => i.CreatedOn).FirstOrDefault().RemoteImageUrl != null ?
                        x.Exercise.Images.OrderByDescending(i => i.CreatedOn).FirstOrDefault().RemoteImageUrl :
                        "/images/exercises/" + x.Exercise.Images.OrderByDescending(i => i.CreatedOn).FirstOrDefault().Id + "." + x.Exercise.Images.OrderByDescending(i => i.CreatedOn).FirstOrDefault().Extension));
        }
    }
}
