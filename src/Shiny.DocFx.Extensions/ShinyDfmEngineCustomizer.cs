using Microsoft.DocAsCode.Dfm;
using System.Collections.Generic;
using System.Composition;


namespace Shiny.DocFx.Extensions
{
    [Export(typeof(IDfmEngineCustomizer))]
    public class ShinyDfmEngineCustomizer : IDfmEngineCustomizer
    {
        public void Customize(DfmEngineBuilder builder, IReadOnlyDictionary<string, object> parameters)
        {
            builder.InlineRules = builder.InlineRules.Insert(0, new NugetShieldInlineRule());
        }
    }
}
