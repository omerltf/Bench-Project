using AirBench.Data;
using AirBench.FormModels;
using AirBench.Models;
using AirBench.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AirBench.Controllers
{
    [Authorize]
    public class LoginController : BaseController
    {
        [AllowAnonymous]
        public ActionResult Create ()
        {
            CreateUser user = new CreateUser();
            return View("Create", user);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Create (CreateUser user)
        {
            LoginRepository repository = new LoginRepository(context);
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
            try
            {
                User newUser = new User(0, user.Name, user.UserName, hashedPassword);
                repository.Insert(newUser);
                return RedirectToAction("Index");
            }
            catch (DbUpdateException ex)
            {
                HandleDbUpdateException(ex);
            }
            return View("Create", user);
        }

        [AllowAnonymous]
        public ActionResult Login ()
        {
            LoginView view = new LoginView();
            return View(view);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login (LoginView viewModel)
        {
            LoginRepository repository = new LoginRepository(context);
            if (ModelState.IsValidField("UserName") && ModelState.IsValidField("Password"))
            {
                User currentUser = repository.GetByUserName(viewModel.UserName);
                if (currentUser == null || !BCrypt.Net.BCrypt.Verify(viewModel.Password, currentUser.HashedPassword))
                {
                    ModelState.AddModelError("", "Login Failed. You Shall Not Pass!!!");
                }
            }
            if (ModelState.IsValid)
            {
                FormsAuthentication.SetAuthCookie(viewModel.UserName, false);
                return RedirectToAction("Index", "Bench");
            }
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        private void HandleDbUpdateException(DbUpdateException ex)
        {
            if (ex.InnerException != null && ex.InnerException.InnerException != null)
            {
                SqlException sqlException = ex.InnerException.InnerException as SqlException;
                if (sqlException != null && sqlException.Number == 2627)
                {
                    ModelState.AddModelError("Name", "That name is already taken.");
                }
            }
        }
    }
}