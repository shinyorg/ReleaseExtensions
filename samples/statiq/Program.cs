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
                .AddShinyExtensions()
                .RunAsync();
    }
}