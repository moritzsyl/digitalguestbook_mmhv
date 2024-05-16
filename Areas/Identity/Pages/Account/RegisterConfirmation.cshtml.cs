using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using digitalguestbook.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace digitalguestbook.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterConfirmation : PageModel
    {
        private readonly UserManager<digitalguestbookUser> _userManager;

        public RegisterConfirmation(UserManager<digitalguestbookUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                return Page();
            }

            return NotFound($"Error confirming email for user with ID '{userId}'.");
        }
    }
}