using Microsoft.DocAsCode.Dfm;
using Microsoft.DocAsCode.MarkdownLite;

using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace Shiny.DocFx.Extensions
{
    //https://dotnet.github.io/docfx/tutorial/intro_markdown_lite.html
    //public class MyEngineBuilder : GfmEngineBuilder
    //{
    //    public MyEngineBuilder(Options options) : base(options)
    //    {
    //        //this.BlockRules = this.BlockRules.Insert(0,mbox, )
    //    }
    //}


    public class NugetShieldInlineRule : IMarkdownRule
    {
        static readonly Regex regex = new Regex(@"^\[!NugetShield\((.*)\)]", RegexOptions.Compiled);
        //static readonly Regex regex = new Regex(@"^\<NugetShield:(\w+?)\>", RegexOptions.Compiled);


        public string Name => "NugetShieldToken";

        public IMarkdownToken? TryMatch(IMarkdownParser parser, IMarkdownParsingContext context)
        {
            var match = regex.Match(context.CurrentMarkdown);
            if (match.Length == 0)
                return null;

            var packageName = match.Groups[1].Value;
            if (packageName == null)
                return null;

            var text = Utils.ToNugetShield(packageName);

            // 'eat' the characters of the current markdown token so they aren't re-processed
            var sourceInfo = context.Consume(match.Length);

            // return a docfx token that just returns the text passed to it
            return new MarkdownTextToken(this, parser.Context, text, sourceInfo);
        }
    }
}
