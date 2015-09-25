using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BLL.Services;
using DAO.Model;
using DAO.Repository;
using EntityFrameworkDAO.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PaymentSystem.Models;

namespace PaymentSystem.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private readonly IRepositoryFactory _factory;

        public ManageController(IRepositoryFactory factory)
        {
            _factory = factory;
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, IRepositoryFactory factory)
            : this(factory)
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

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = UserManager.ChangePassword(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = UserManager.FindById(User.Identity.GetUserId());
                if (user != null)
                {
                    SignInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                }
                TempData["SuccessMessage"] = "Пароль успешно сменен.";
                return RedirectToAction("Details");
            }
            AddErrors(result);
            return View(model);
        }

        public ActionResult Details(string id)
        {
            if (string.IsNullOrEmpty(id) || !(User.IsInRole("Admin") || User.IsInRole("Support")))
                id = User.Identity.GetUserId();
            var user = _factory.GetUserRepository(UserManager).FindById(id);
            if (user == null)
                return new HttpStatusCodeResult(404);
            return View(user);
        }

        public ActionResult Edit()
        {
            string userId = User.Identity.GetUserId();
            return View(_factory.GetUserRepository(UserManager).FindById(userId));
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(User user, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentType.StartsWith("image"))
                {
                    user.ImgMimeType = file.ContentType;
                    user.ImageBytes = new byte[file.ContentLength];
                    file.InputStream.Read(user.ImageBytes, 0, file.ContentLength);
                }
                _factory.GetUserRepository(UserManager).Edit(user, User.Identity.GetUserId());
                TempData["SuccessMessage"] = "Данные успешно сохранены";
                return RedirectToAction("Details");
            }
            return View(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

        #region Вспомогательные приложения
        // Используется для защиты от XSRF-атак при добавлении внешних имен входа
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        #endregion

    }
}