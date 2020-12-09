namespace StrongMe.Services.Data
{
    using System.Linq;

    using StrongMe.Data.Common.Repositories;
    using StrongMe.Data.Models;

    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> applicationUserRepository;

        public ApplicationUserService(
            IDeletableEntityRepository<ApplicationUser> applicationUserRepository)
        {
            this.applicationUserRepository = applicationUserRepository;
        }

        public bool IsInstructorCodeValid(string code)
        {
            return this.applicationUserRepository.AllAsNoTracking().Any(a => a.Code == code);
        }
    }
}
