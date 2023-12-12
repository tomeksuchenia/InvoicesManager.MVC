using InvoicesManagerWebApp.Data;
using InvoicesManagerWebApp.Extensions;
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;

        public AccountController(UserManager<User> userManager, SignInManager<User> signManager, IInvoiceService invoiceService, ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signManager;
            _invoiceService = invoiceService;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
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

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Settings()
        {
            var userId = _httpContextAccessor.HttpContext.User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            return View(user);  
        }


        public async Task<IActionResult> Edit()
        {
            var userId = _httpContextAccessor.HttpContext.User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            var userVM = new EditUserViewModel
            {
                UserId = user.Id,
                AddressId = user.AddressId,
                EmailAddress = user.Email,
                Address = user.Address,
                CompanyName = user.CompanyName,
                TaxId = user.TaxId,
                TelephoneNumber = user.TelephoneNumber,
            };
            return View(userVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel userVM)
        {
            if(!ModelState.IsValid) return View();
            var user = new User
            {
                Id = userVM.UserId,
                CompanyName = userVM.CompanyName,
                Address = userVM.Address,
                TaxId = userVM.TaxId,
                TelephoneNumber = userVM.TelephoneNumber,
            };

            await _userService.Update(user);

            return RedirectToAction("Settings");
            
        }
    }
}
