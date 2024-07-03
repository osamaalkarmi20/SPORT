using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SPORT.Models;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace SPORT.Controllers
{

    public class UserController : Controller
    {

        // Use DI to bring in the user service
        private readonly IUser userService;

        public UserController(IUser service)
        {
            userService = service;

        }

        public IActionResult Authenticate()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> SignUp(RegisterDTO registerDTO)
        {
           
            var user = await userService.Register(registerDTO, this.ModelState);

            if (ModelState.IsValid)
            {
                return Redirect("Authenticate");
            }

            return View();
        }
        [HttpPost]
        public async Task<ActionResult<UserDTO>> Authenticate(LoginDTO data)
        {
            if (data.UserName != null && data.Password != null)
            {
                var user = await userService.Authenticate(data.UserName, data.Password);

                if (ModelState.IsValid)
                {
                    return RedirectToAction("Index", "Home");
                }
                return View(user);
            }
            return View();
        }


    }
}
