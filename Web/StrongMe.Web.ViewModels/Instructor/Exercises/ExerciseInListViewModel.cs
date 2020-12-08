namespace StrongMe.Web.ViewModels.Instructor.Exercises
{
    using System.Linq;

    using AutoMapper;
    using StrongMe.Data.Models;
    using StrongMe.Services.Mapping;

    public class ExerciseInListViewModel : IMapFrom<Exercise>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public int BodyPartId { get; set; }

        public string BodyPartName { get; set; }

        public string ImageUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Exercise, ExerciseInListViewModel>()
                .ForMember(x => x.ImageUrl, opt =>
                    opt.MapFrom(x =>
                        x.Images.OrderByDescending(i => i.CreatedOn).FirstOrDefault().RemoteImageUrl != null ?
                        x.Images.OrderByDescending(i => i.CreatedOn).FirstOrDefault().RemoteImageUrl :
                        "/images/exercises/" + x.Images.OrderByDescending(i => i.CreatedOn).FirstOrDefault().Id + "." + x.Images.OrderByDescending(i => i.CreatedOn).FirstOrDefault().Extension));
        }
    }
}
