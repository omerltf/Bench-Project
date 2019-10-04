using System.Web.Optimization;

namespace AirBench
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/pure").Include(
                      "~/Content/pure.css",
                      "~/Content/site.css"));
        }
    }
}
