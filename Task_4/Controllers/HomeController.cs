using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Task_4.Interface;
using Task_4.Models;
using Task_4.Service;

namespace Task_4.Controllers;

public class HomeController : Controller
{
    private readonly IUserService _userService;
    private readonly IAuthService _authService;
    public HomeController(IUserService userService, IAuthService authService)
    {
        _userService = userService;
        _authService = authService;
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
 var LoggedInUser = await _authService.GetLoggedInUserAsync();

        if (LoggedInUser.Status == UserStatus.BLOCKED || LoggedInUser is null )
        {
            

            return RedirectToAction("signin", "Auth");
        }
        switch (action)
        {
            case "block":
                await _userService.BlockUser(SelectedUserIds);
               
                TempData["ToastMessage"] = "Users blocked successfully!";
                TempData["ToastType"] = "success";
                break;
            case "activate":
                await _userService.ActivateUser(SelectedUserIds);
                TempData["ToastMessage"] = "Users unblocked successfully!";
                TempData["ToastType"] = "success";
                break;
            case "delete":
                await _userService.DeleteUser(SelectedUserIds);
                TempData["ToastMessage"] = "Users deleted!";
                TempData["ToastType"] = "success";
                break;
        }


        return RedirectToAction("Index", "Home");
    }



}
