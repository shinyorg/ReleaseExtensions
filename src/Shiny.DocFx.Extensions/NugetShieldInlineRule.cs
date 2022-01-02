using Microsoft.DocAsCode.MarkdownLite;
using System.Text.RegularExpressions;


namespace Shiny.DocFx.Extensions
{
    public class NugetShieldInlineRule : IMarkdownRule
    {
        static readonly Regex regex = new Regex(@"^\[!NugetShield\((.*)\)]", RegexOptions.Compiled);
        static readonly Regex regexWithLabel = new Regex(@"^\[!NugetShield\((.*)\,(.*)\)]", RegexOptions.Compiled);


        public string Name => "NugetShieldToken";

        public IMarkdownToken? TryMatch(IMarkdownParser parser, IMarkdownParsingContext context)
        {
            var values = TryGet(context.Markdown);
            if (values == null)
                return null;

            var text = Utils.ToNugetShield(values.Value.PackageName, values.Value.Label);

            // 'eat' the characters of the current markdown token so they aren't re-processed
            var sourceInfo = context.Consume(values.Value.Length);

            // return a docfx token that just returns the text passed to it
            return new MarkdownTextToken(this, parser.Context, text, sourceInfo);
        }


        static (int Length, string PackageName, string? Label)? TryGet(string markdown)
        {
            var matches = regex.Match(markdown);
            if (matches.Length == 0)
                matches = regexWithLabel.Match(markdown);

            switch (matches.Length)
            {
                case 1: return (matches.Length, matches.Groups[1].Value, null);
                case 2: return (matches.Length, matches.Groups[1].Value, matches.Groups[2].Value);

                default: return null;
            }
        }
    }
}
