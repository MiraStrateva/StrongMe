namespace StrongMe.Web.Areas.Identity.Pages.Account.Manage
{
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using StrongMe.Data.Models;

    public partial class InviteTraineeModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;

        public InviteTraineeModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [EmailAddress]
            [Display(Name = "Trainee Email")]
            public string TraineeEmail { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            Input = new InputModel
            {
                TraineeEmail = string.Empty
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            if (!string.IsNullOrEmpty(Input.TraineeEmail))
            {
                var html = new StringBuilder();
                html.AppendLine($"<h1>Follow the link to register in StrongMe!</h1>");
                html.AppendLine($"<a href=\"{this.HttpContext.Request.Host}/Identity/Account/Register?code={user.Code}\" />");
                await this._emailSender.SendEmailAsync(Input.TraineeEmail, "StrongMe Invitation", html.ToString());
            } 

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = $"Trainee {Input.TraineeEmail} has been invited";
            return RedirectToPage();
        }
    }
}
