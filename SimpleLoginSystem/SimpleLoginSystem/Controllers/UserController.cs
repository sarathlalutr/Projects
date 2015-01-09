using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SimpleLoginSystem.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(SimpleLoginSystem.Models.UserModel user)
        {
            if (ModelState.IsValid)
            {
                if (IsValid(user.Email, user.Password))
                {
                    var objRole = new MyRoleProvider();
                    FormsAuthentication.SetAuthCookie(user.Email,false);
                    var roles = objRole.GetRolesForUser(user.Email);

                    if (roles.Contains("Teacher"))
                    {
                        return RedirectToAction("Home", "Teacher", new {area = "Teacher"});
                    }
                    else
                    {
                         return RedirectToAction("Index", "Home");
                    }
                   
                }
                else
                {
                    ModelState.AddModelError("","Login Data is Incorrect");
                }
            }
            return View(user);
        }
        [HttpGet]
        public ActionResult Registration()

        {
            return View();
        }
         [HttpPost]
        public ActionResult Registration(SimpleLoginSystem.Models.UserModel user)

        {
             try
             {
                 if (ModelState.IsValid)
                 {
                     using (var db = new MainDBContext())
                     {
                         var crypto = new SimpleCrypto.PBKDF2();
                         var encrPass = crypto.Compute(user.Password);
                         var sysUser = db.SystemUsers.Create();
                         sysUser.Email = user.Email;
                         sysUser.Password = encrPass;
                         sysUser.PasswordSalt = crypto.Salt;
                         sysUser.UserId = Guid.NewGuid();
                         db.SystemUsers.Add(sysUser);
                         db.SaveChanges();
                         return RedirectToAction("Index", "Home");
                     }
                 }
             }
             catch (Exception)
             {
                 
                 throw;
             }
             
            return View();
        }

        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(SimpleLoginSystem.Models.ChangePasswordModel objUser)
        {
            //http://www.entityframeworktutorial.net/update-entity-in-entity-framework.aspx
            if (ModelState.IsValid)
            {
                if (IsValid(objUser.Email, objUser.OldPassword))
                {
                    using (var ctx = new MainDBContext())
                    {
                        var users = (from s in ctx.SystemUsers
                            where s.Email == objUser.Email
                            select s).FirstOrDefault();

                        var crypto = new SimpleCrypto.PBKDF2();
                         var encrPass = crypto.Compute(objUser.NewPassword);

                        users.Password = encrPass;
                        users.PasswordSalt = crypto.Salt;
                        ctx.SaveChanges();
                    }
                    
                }
                else
                {
                    ModelState.AddModelError("", "Email or Password is incorrect");
                }
            }


            return View();
        }
        public ActionResult RedirectToDefault()
        {

            String[] roles = Roles.GetRolesForUser();
            if (roles.Contains("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }
            else if (roles.Contains("Student"))
            {
                return RedirectToAction("Index", "User");
            }
            else if (roles.Contains("Teacher"))
            {
                return RedirectToAction("Index", "Teacher");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index","Home");
        }
        private bool IsValid(string email, string password)
        {
            var crypto = new SimpleCrypto.PBKDF2();
            bool IsValid = false;
            using (var db=new MainDBContext())
            {
                var user = db.SystemUsers.FirstOrDefault(u => u.Email == email);
                if (user!=null)
                {
                    if (user.Password == crypto.Compute(password, user.PasswordSalt))
                    {
                        IsValid = true;
                    }
                }
            }


            return IsValid;
        }


      
        public ActionResult CreateAdmin()
        {
            return View();
        }
        
    }
}
