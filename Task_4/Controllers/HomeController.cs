using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Task_4.Interface;
using Task_4.Models;

namespace Task_4.Controllers;

public class HomeController : Controller
{
    private readonly IUserService _userService;

    public HomeController(IUserService userService)
    {
        _userService = userService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> BulkDelete(string[] SelectedUserIds, string action)
    {
        if (SelectedUserIds == null || SelectedUserIds.Length == 0)
            return RedirectToAction("Index", "Home");

        switch (action)
        {
            case "block":
                await _userService.BlockUser(SelectedUserIds);
                break;
            case "activate":
                await _userService.ActivateUser(SelectedUserIds);
                break;
            case "delete":
                await _userService.DeleteUser(SelectedUserIds);
                break;
        }

        return RedirectToAction("Index", "Home");
    }



}
