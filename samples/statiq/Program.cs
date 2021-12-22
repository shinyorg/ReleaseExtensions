using Shiny.Statiq.Extensions;
using Statiq.App;
using Statiq.Web;


namespace MySite
{
    public class Program
    {
        public static async Task<int> Main(string[] args) =>
            await Bootstrapper
                .Factory
                .CreateWeb(args)
                .AddShortcode<StartupShortcode>("Startup")
                //.AddShortcode<PackageInfoShortcode>("PackageInfo")
                .AddShortcode<NugetShieldShortcode>("NugetShield")
                //.AddShortcode<StaticClassesShortcode>("StaticClasses")
                .RunAsync();
    }
}