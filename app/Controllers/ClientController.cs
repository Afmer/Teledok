using app.Architecture.Interfaces;
using app.Models;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers;

public class ClientController : Controller
{
    public ClientController(IDBManager manager)
    {
        
    }
    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }
    [HttpPost] 
    public IActionResult Add(AddClientModel model)
    {
        if(ModelState.IsValid)
        {
            if(model.FoundersNum != 0)
                return RedirectToAction("EnterFounders", "Client", model);
            else
                return Json(new {status = "ok"});
        }
        else
            return Json(new {status = "invalid"});
    }
}