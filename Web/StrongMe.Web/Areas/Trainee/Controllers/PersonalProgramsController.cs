namespace StrongMe.Web.Areas.Trainee.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using StrongMe.Common;
    using StrongMe.Data.Models;
    using StrongMe.Services.Data;
    using StrongMe.Web.Controllers;
    using StrongMe.Web.ViewModels.Trainee.PersonalPrograms;

    [Area("Trainee")]
    [Authorize(Roles = GlobalConstants.TraineeRoleName)]
    public class PersonalProgramsController : BaseController
    {
        private readonly IPersonalProgramsService personalProgramService;
        private readonly UserManager<ApplicationUser> userManager;

        public PersonalProgramsController(IPersonalProgramsService personalProgramService, UserManager<ApplicationUser> userManager)
        {
            this.personalProgramService = personalProgramService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> All()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var viewModel = new PersonalProgramListViewModel
            {
                PersonalPrograms = this.personalProgramService.GetAll<PersonalProgramViewModel>(user.Id),
            };

            return this.View(viewModel);
        }

        public IActionResult ById(int id)
        {
            var personalProgram = this.personalProgramService.GetById<SinglePersonalProgramViewModel>(id);
            return this.View(personalProgram);
        }
    }
}
