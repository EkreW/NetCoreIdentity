using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NetCoreIdentity_1.Models;
using NetCoreIdentity_1.Models.Entities;
using NetCoreIdentity_1.Models.ViewModels.AppUsers.PureVms;
using System.Diagnostics;

namespace NetCoreIdentity_1.Controllers
{
    //[AutoValidateAntiforgeryToken] //Get ile gelen sayfada verilen özel bir token sayesinde Post'un bu tokensiz yapýlamamasýný saglar...PostMan gibi third part software'lerinden gözlemlediginizde direkt Post tarafýna ulasamadýgýnýzý göreceksiniz...
    public class HomeController : Controller
    {
        //Identity kütüphanesi crud ve servis iþlemleri icin bir takým class'lara sahiptir... Bu Manager Class'larý sizin ilgili Identity yapýlarýnýzýn Crud iþlemlerine ve baska business logic iþlemlerine girmesini saglarlar...

        private readonly ILogger<HomeController> _logger;
        readonly UserManager<AppUser> _userManager;
        readonly RoleManager<AppRole> _roleManager;
        readonly SignInManager<AppUser> _signInManager;

        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterRequestModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new()
                {
                    UserName = model.UserName,
                    Email = model.Email
                };
                IdentityResult result = await _userManager.CreateAsync(appUser, model.Password);

                if (result.Succeeded)
                {
                    #region AdminEklemekIcinKodlar
                    AppRole role = await _roleManager.FindByNameAsync("Admin"); //Admin ismindeki rolu bulabilirse Role nesnesini appRole'e atacaktýr eðer bulamazsa appRole null olacaktýr.
                    if (role == null) await _roleManager.CreateAsync(new() { Name = "Admin" }); //Admin isminde bir rol yaratýldý.
                    await _userManager.AddToRoleAsync(appUser, "Admin"); //appUser degiskeninin tuttugu kullanýcý nesnesini Admin isimli Role'e ekledik.
                    #endregion
                    return RedirectToAction("Index");
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
    }
}
