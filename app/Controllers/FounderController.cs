using app.Architecture.DataModel;
using app.Architecture.Enums;
using app.Architecture.Interfaces;
using app.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace app.Controllers;

public class FounderController : Controller
{
    private readonly IFounderDBHandler _dbHandler;
    public FounderController(IFounderDBHandler dbHandler)
    {
        _dbHandler = dbHandler;
    }
    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Add(AddFounderModel model)
    {
        if(ModelState.IsValid)
        {
            var result = await _dbHandler.AddFounder(model);
            if(result.Status == AddFounderStatus.INNExistsError)
            {
                ModelState.AddModelError(nameof(model.INN), "Учредитель с таким ИНН уже существует");
                return View(model);
            }
            else if(result.Status == AddFounderStatus.UnknownError)
                return Content("unknown error");
            return View("SuccessAdd");
        }
        else
            return View(model);
    }
}