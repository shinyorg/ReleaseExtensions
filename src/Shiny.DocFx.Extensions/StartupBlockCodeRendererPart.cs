using Microsoft.DocAsCode.Dfm;
using Microsoft.DocAsCode.MarkdownLite;


namespace Shiny.DocFx.Extensions
{
    public class StartupBlockCodeRendererPart : DfmCustomizedRendererPartBase<IMarkdownRenderer, MarkdownCodeBlockToken, MarkdownBlockContext>
    {
        public override string Name => nameof(StartupBlockCodeRendererPart);


        public override bool Match(IMarkdownRenderer renderer, MarkdownCodeBlockToken token, MarkdownBlockContext context)
            => token.Lang == "shinystartup";


        public override StringBuffer Render(IMarkdownRenderer renderer, MarkdownCodeBlockToken token, MarkdownBlockContext context)
        {
            return TEMPLATE.Replace("{{BODY}}", token.Code);
        }


        const string TEMPLATE = @"
<pre>
<code class=""lang-csharp"">
using Microsoft.Extensions.DependencyInjection;
using Shiny;

namespace YourNamespace
{
    public class YourShinyStartup : ShinyStartup
    {
        public override void ConfigureServices(IServiceCollection services, IPlatform platform)
        {
            {{BODY}}
        }
    }
}
</code>
</pre>
";
    }
}
