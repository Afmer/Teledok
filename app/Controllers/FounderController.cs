using app.Architecture.DataModel;
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
            var founder = new Founder()
            {
                AddDateTime = DateTime.UtcNow,
                UpdateDateTime = DateTime.UtcNow,
                Patronymic = model.Patronymic,
                Surname = model.Surname,
                Name = model.Name,
                INN = model.INN
            };
            await _dbHandler.AddFounder(founder);
            return View("SuccessAdd");
        }
        else
            return View(model);
    }
}