using ElbruzWebPj.Models.MVVM;
using ElbruzWebPj.Models;
using Microsoft.AspNetCore.Mvc;

namespace ElbruzWebPj.Controllers
{
    public class AccountController : Controller
    {

        private readonly Cls_User _Cls_User;

        public AccountController(Cls_User cls_User)
        {
            _Cls_User = cls_User;
        }


        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(User user)
        {
            
            string answer = _Cls_User.LoginControl(user);

            if (answer == "Admin")
            {
                HttpContext.Session.SetString("Admin", answer);
                return RedirectToAction("Index", "Admin");
            }
            else if  (answer == user.Email)
            {
                HttpContext.Session.SetString("Email", answer);
                return RedirectToAction("Index", "Home");

            }
            else
            {
                TempData["Message"] = "Email / Şifre yanlış girildi";
                return View();
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                bool isExists = _Cls_User.IsExists(user.Email, user.Telephone);
                bool isInvalid = _Cls_User.IsPhoneNumberInvalid(user.Telephone);

                if (isExists || isInvalid)
                {
                    TempData["Message"] = "Bu Email veya Telefon Numarası Zaten Kayıtlı! ya da Telefon Numarısını Düzgün Girdiğinizden Emin Olunuz ❌";
                    return View(user);
                }
                else
                {
                    bool IsSucces = _Cls_User.RegisterCreate(user);
                    if (IsSucces)
                    {
                        TempData["Message"] = " Kayıt Yapıldı !";
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["Message"] = " Tüm Alanları Doldurunuz! ❌";
                        return View(user);
                    }
                }
            }
            TempData["Message"] = " Tüm Alanları Doldurunuz! ❌";
            return View(user);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Email");
            //  HttpContext.Session.Remove("Admin");
            return RedirectToAction("Index","Home");

        }


    }
}
