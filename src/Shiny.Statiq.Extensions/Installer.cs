using Statiq.App;


namespace Shiny.Statiq.Extensions
{
    public static class Installer
    {
        public static Bootstrapper AddShinyExtensions(this Bootstrapper bootstrapper) => bootstrapper
            .AddShortcode<StartupShortcode>("Startup")
            .AddShortcode<NugetShieldShortcode>("NugetShield");
    }
}
