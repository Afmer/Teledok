using Microsoft.AspNetCore.Mvc;

namespace app.Controllers;

public class FounderController : Controller
{
    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }
}