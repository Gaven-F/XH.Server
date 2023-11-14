using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XH_Server.Application.Services.System;

namespace Core.Controllers;

public class DemoController : ControllerBase
{
    private readonly IDBService _db;

    public DemoController(ISystemService db) => _db = db;

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Index()
    {
        _db.InitDataBase();
        return Ok();
    }
}
