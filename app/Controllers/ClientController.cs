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
            if(model.ClientType == ClientType.LegalEntity)
            {
                if(model.FounderINNs != null)
                {
                    var isFounderINNsExists = _dbHandler.IsFounderINNsExists(model.FounderINNs);
                    bool isError = false;
                    string errorMessage = "Следующих учредителей нет в базе: ";
                    for(int i = 0; i < isFounderINNsExists.Length; i++)
                    {
                        if(!isFounderINNsExists[i])
                        {
                            string str = "";
                            if(isError)
                                str += "; ";
                            str += model.FounderINNs[i];
                            errorMessage += str;
                            isError = true;
                        }
                    }
                    if(isError)
                    {
                        ViewBag.FounderINNsNotExistsMessage = errorMessage;
                        return View(model);
                    }
                }
                else
                    return Content("Founder's INN not found");
            }
            var result = await _dbHandler.AddClient(model);
            if(result.Status == AddClientStatus.Success)
                return View("SuccessAdd");
            else if(result.Status == AddClientStatus.INNExistsError)
            {
                ModelState.AddModelError(nameof(model.ClientINN), "Такой ИНН уже существует");
                return View(model);
            }
            else
                return Content(result.Exception != null ? result.Exception.Message : "");
        }
        else
            return Json(new {status = "invalid"});
    }

    [HttpGet]
    public IActionResult ShowAll()
    {
        return View(_dbHandler.GetAllClients());
    }
    [HttpGet] 
    public IActionResult Show(string INN)
    {
        var client = _dbHandler.GetClient(INN);
        if(client != null)
            return View(client);
        else
            return View("ClientNotFound");
    }
}