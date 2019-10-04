using AirBench.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AirBench
{
    public class BaseController : Controller
    {
        protected Context context;
        public BaseController()
        {
            context = new Context();

            context.Database.Log = (message) => Debug.WriteLine(message);
        }

        private bool disposed = false;
        protected override void Dispose(bool disposing)
        {
            if (disposed == true)
            {
                return;
            }

            if (disposing)
            {
                context.Dispose();
            }

            disposed = true;

            base.Dispose(disposing);
        }
    }
}