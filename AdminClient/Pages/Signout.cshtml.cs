using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdminClient.Pages;

public class SignOutModel : PageModel
{
    public IActionResult OnGet()
    {
        return SignOut("Cookies", "oidc");
    }
}
