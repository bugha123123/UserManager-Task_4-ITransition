using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Task_4.Models;

namespace Task_4.Controllers;

public class HomeController : Controller
{

 
    public IActionResult Index()
    {
        return View();
    }

 
}
