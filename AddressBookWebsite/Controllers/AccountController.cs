using AddressBookWebsite.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace AddressBookWebsite.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        private ContactsDbContext db = new ContactsDbContext();
        static readonly string securityCode = "mysaltkey";

        public AccountController()
        {

        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }

            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //Encrypt password using MD5 encryption and salt
        /*©Copyright S Ravindran */
        public static string CreateSHAHash(string pword)
        {
            SHA512Managed sha512 = new SHA512Managed();
            Byte[] EncryptedSHA512 = sha512.ComputeHash(Encoding.UTF8.GetBytes(string.Concat(pword, securityCode)));
            sha512.Clear();
            return Convert.ToBase64String(EncryptedSHA512);
        }

        [AllowAnonymous]
        public ActionResult RegisteredUsers()
        {
            if (Session["FullName"] != null)
            {
                return View(db.RoleModel.ToList());
            }

            else
            {
                return RedirectToAction("Manage", "Account");
            }
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, int ?id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (ModelState.IsValid)
            {
                using (ContactsDbContext db = new ContactsDbContext())
                {
                    var pword = model.Password;
                    var saltedPassword = CreateSHAHash(pword);

                    var user = db.RoleModel.FirstOrDefault(u => u.UserName == model.UserName && u.RoleName == model.RoleName && u.FullName == model.FullName && u.Email == model.Email && u.Password == saltedPassword);
                  
                    if (user != null)
                    {
                        Session["FullName"] = model.FullName;
                        Session["RoleName"] = model.RoleName;
                        db.UserLogin.Add(model);
                        ModelState.Clear();
                        return RedirectToAction("Index", "Contacts");
                    }

                    else
                    {
                        ModelState.AddModelError("", "Invalid login credentials");
                    }
                }
            }
            return View();
        }

        public ActionResult LoggedIn()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult CreateUserRole()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUserRole(CreateUserRoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var salt = CreateSHAHash(model.Password);
            model.Password = salt;
            model.ConfirmPassword = salt;

            if (ModelState.IsValid)
            {
                using (ContactsDbContext db = new ContactsDbContext())
                {
                    var user = db.RoleModel.FirstOrDefault(u => u.UserName == model.UserName && u.FullName == model.FullName && u.Email == model.Email);
                    if (user != null)
                    {
                        ModelState.AddModelError("", "Username already exists");
                    }

                    else
                    {
                        db.RoleModel.Add(model);
                        db.SaveChanges();
                        ModelState.Clear();
                    }
                }
            }

            return View();
        }

        [AllowAnonymous]
        public ActionResult RegisterAdmin()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterAdmin(CreateUserRoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var salt = CreateSHAHash(model.Password);
            model.Password = salt;
            model.ConfirmPassword = salt;

            if (ModelState.IsValid)
            {
                using (ContactsDbContext db = new ContactsDbContext())
                {
                    var user = db.RoleModel.FirstOrDefault(u => u.UserName == model.UserName || u.FullName == model.FullName || u.Email == model.Email);
                    if (user != null)
                    {
                        ModelState.AddModelError("", "Username or email already exists");
                    }

                    else
                    {
                        db.RoleModel.Add(model);
                        db.SaveChanges();
                        ModelState.Clear();
                    }
                }
            }

            return View();
        }

        [AllowAnonymous]
        public ActionResult Manage()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Manage(LoginViewModel model, int ?id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (ModelState.IsValid)
            {
                using (ContactsDbContext db = new ContactsDbContext())
                {
                    var saltedPassword = CreateSHAHash(model.Password);
                    
                    var user = db.RoleModel.FirstOrDefault(u => u.UserName == model.UserName && u.RoleName == model.RoleName && u.Email == model.Email && u.FullName == model.FullName && u.Password == saltedPassword);
                    if (user != null)
                    {
                        Session["FullName"] = model.FullName;
                        Session["RoleName"] = model.RoleName;
                        db.UserLogin.Add(model);
                        ModelState.Clear();
                        return RedirectToAction("RegisteredUsers", "Account");
                    }

                    else
                    {
                        ModelState.AddModelError("", "Administrator does not exist");
                    }

                    return View(model);
                }
            }
            return View();
        }

        [AllowAnonymous]
        // GET: Account/Delete/5
        public ActionResult Remove(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CreateUserRoleViewModel usersLogin = db.RoleModel.Find(id);
            if (usersLogin == null)
            {
                return HttpNotFound();
            }

            return View(usersLogin);
        }

        // POST: Account/Delete/5
        [HttpPost, ActionName("Remove")]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult RemoveConfirmed(int? id)
        {
            CreateUserRoleViewModel userLogin = db.RoleModel.Find(id);
            db.RoleModel.Remove(userLogin);
            db.SaveChanges();
            return RedirectToAction("RegisteredUsers", "Account");
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            Session["FullName"] = null;
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public List<string> Roles { get; set; }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}

