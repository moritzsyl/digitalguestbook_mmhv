using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using digitalguestbook.Areas.Identity.Data;
using digitalguestbook.Services;
using Microsoft.Extensions.Logging;

namespace digitalguestbook.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<digitalguestbookUser> _signInManager;
        private readonly UserManager<digitalguestbookUser> _userManager;
        private readonly IUserStore<digitalguestbookUser> _userStore;
        private readonly IUserEmailStore<digitalguestbookUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<digitalguestbookUser> userManager,
            IUserStore<digitalguestbookUser> userStore,
            SignInManager<digitalguestbookUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)

        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "First Name")]
            [DataType(DataType.Text)]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Last Name")]
            [DataType(DataType.Text)]
            public string LastName { get; set; }

            [Display(Name = "Company Name")]
            [DataType(DataType.Text)]
            public string CompanyName { get; set; }

            [Required]
            [Display(Name = "Phone Number")]
            [DataType(DataType.PhoneNumber)]
            public string PhoneNumber { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = CreateUser();

                user.EmailConfirmed = false;
                user.UserName = Input.Email;

                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    string emailsubject = "Test";
                    string username = "Vincent";
                    string emailmessage = "It works!";

                    EmailSender emailSender = new EmailSender();
                    emailSender.SendEmail(emailsubject,"vincent.baudisch@gmail.com", username, emailmessage);

                    return RedirectToPage("RegisterConfirmation",
                        new { email = Input.Email, returnUrl = returnUrl });
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Page();
        }

        private digitalguestbookUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<digitalguestbookUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(digitalguestbookUser)}'. " +
                                                    $"Ensure that '{nameof(digitalguestbookUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                                                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<digitalguestbookUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }

            return (IUserEmailStore<digitalguestbookUser>)_userStore;
        }
    }
}
