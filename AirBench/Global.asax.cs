using AirBench.Data;
using AirBench.Models;
using AirBench.Repository;
using AirBench.Security;
using System.Data.Entity;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace AirBench
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(new DatabaseInitializer());
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_PostAuthenticateRequest()
        {
            IPrincipal user = HttpContext.Current.User;
            if (user.Identity.IsAuthenticated && user.Identity.AuthenticationType == "Forms")
            {
                FormsIdentity formsIdentity = (FormsIdentity)user.Identity;
                FormsAuthenticationTicket ticket = formsIdentity.Ticket;

                CustomIdentity customIdentity = new CustomIdentity(ticket);
                string currentUserName = ticket.Name;

                using (var context = new Context())
                {
                    LoginRepository repository = new LoginRepository(context);
                    User currentUser = repository.GetByUserName(currentUserName);

                    CustomPrincipal customPrincipal = new CustomPrincipal(customIdentity, currentUser);
                    HttpContext.Current.User = customPrincipal;
                    Thread.CurrentPrincipal = customPrincipal;
                }
            }
        }
    }
}
