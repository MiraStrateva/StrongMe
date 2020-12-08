namespace StrongMe.Web.ViewModels.Instructor.Exercises
{
    using AutoMapper;
    using StrongMe.Data.Models;
    using StrongMe.Services.Mapping;

    public class ImageInputModel : IMapFrom<Image>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string ImageUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Image, ImageInputModel>()
                .ForMember(x => x.ImageUrl, opt =>
                    opt.MapFrom(x =>
                        x.RemoteImageUrl != null ?
                        x.RemoteImageUrl :
                        "/images/exercises/" + x.Id + "." + x.Extension));
        }
    }
}
