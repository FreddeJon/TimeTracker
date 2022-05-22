namespace AdminClient.Controllers;

[Authorize(Roles = "Admin")]
public class AccountController : Controller
{
    [AllowAnonymous]
    public IActionResult AccessDenied()
    {
        return View();
    }
}
