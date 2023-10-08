using app.Architecture.Enums;
using app.Architecture.Interfaces;
using app.Models;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers;

public class ClientController : Controller
{
    private IClientDBHandler _dbHandler;
    public ClientController(IClientDBHandler dbHandler)
    {
        _dbHandler = dbHandler;
    }
    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }
    [HttpPost] 
    public async Task<IActionResult> Add(AddClientModel model)
    {
        if(ModelState.IsValid)
        {
            var result = await _dbHandler.AddClient(model);
            if(result.Status == AddClientStatus.Success)
                return View("SuccessAdd");
            else
                return Content(result.Exception != null ? result.Exception.Message : "");
        }
        else
            return Json(new {status = "invalid"});
    }
}