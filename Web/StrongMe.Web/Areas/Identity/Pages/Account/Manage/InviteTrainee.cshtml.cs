namespace StrongMe.Web.Areas.Identity.Pages.Account.Manage
{
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Configuration;
    using StrongMe.Data.Models;
    using StrongMe.Services.Messaging;

    public partial class InviteTraineeModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IEmailSender emailSender;
        private readonly IConfiguration configuration;

        public InviteTraineeModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.emailSender = emailSender;
            this.configuration = configuration;
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

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            this.Load(user);
            return this.Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            if (!this.ModelState.IsValid)
            {
                this.Load(user);
                return this.Page();
            }

            if (!string.IsNullOrEmpty(this.Input.TraineeEmail))
            {
                var html = new StringBuilder();
                html.AppendLine($"<h3>Follow the link to register in StrongMe!</h3>");
                html.AppendLine($"<a href=\"https://{this.HttpContext.Request.Host}/Identity/Account/Register?code={user.Code}\" target=\"_blank\">Click To Register</a>");
                html.AppendLine("<br/>");
                html.AppendLine("<h5>Regards, <br/> StrongMe Team!</h5>");

                await this.emailSender.SendEmailAsync(
                    this.configuration["SendGrid:SenderEmail"],
                    this.configuration["SendGrid:SenderName"],
                    this.Input.TraineeEmail,
                    "StrongMe Invitation",
                    html.ToString());
            }

            await this.signInManager.RefreshSignInAsync(user);
            this.StatusMessage = $"Trainee {this.Input.TraineeEmail} has been invited";
            return this.RedirectToPage();
        }

        private void Load(ApplicationUser user)
        {
            this.Input = new InputModel
            {
                TraineeEmail = string.Empty,
            };
        }
    }
}
