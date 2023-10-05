using app.Architecture.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers;

public class ClientController : Controller
{
    public ClientController(IDBManager manager)
    {
        
    }
    public IActionResult Add()
    {
        return View();
    }
}