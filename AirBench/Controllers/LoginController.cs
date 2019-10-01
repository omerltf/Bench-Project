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

namespace AirBench.Controllers
{
    public class LoginController : BaseController
    {

        public ActionResult Create ()
        {
            CreateUser user = new CreateUser();
            return View("Create", user);
        }

        [HttpPost]
        public ActionResult Create (CreateUser user)
        {
            LoginRepository repository = new LoginRepository(context);
            try
            {
                User newUser = new User(0, user.Name, user.UserName, user.Password);
                repository.Insert(newUser);
                return RedirectToAction("Index");
            }
            catch (DbUpdateException ex)
            {
                HandleDbUpdateException(ex);
            }
            return View("Create", user);
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