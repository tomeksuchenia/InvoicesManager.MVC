using InvoicesManagerWebApp.Data;
using InvoicesManagerWebApp.Interface;
using InvoicesManagerWebApp.Models;
using InvoicesManagerWebApp.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;

namespace InvoicesManagerWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IInvoiceService _invoiceService;
        private readonly ApplicationDbContext _context;

        public AccountController(UserManager<User> userManager, SignInManager<User> signManager, IInvoiceService invoiceService, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signManager;
            _invoiceService = invoiceService;
            _context = context;
        }

        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) return View();

            var user = await _userManager.FindByEmailAsync(loginViewModel.EmailAddress);

            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Invoices");
                    }
                }
                TempData["Error"] = "Wrong credentials. Please, try again";
                return View(loginViewModel);
            }
            TempData["Error"] = "Wrong credentials. Please, try again";
            return View();
        }

        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid) return View();

            var user = await _userManager.FindByEmailAsync(registerViewModel.EmailAddress);

            if (user != null)
            {
                TempData["Error"] = "This email is already in use";
                return View(registerViewModel);
            }

            var newUser = new User
            {
                Email = registerViewModel.EmailAddress,
                UserName = registerViewModel.EmailAddress,
                //AddressId = 1
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.Password);

            return View();
        }
    }
}
