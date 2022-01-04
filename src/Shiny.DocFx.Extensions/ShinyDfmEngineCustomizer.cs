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

            // insert block rule above header rule. Why? I dunno, that's what the docs say:
            // https://dotnet.github.io/docfx/tutorial/intro_markdown_lite.html#select-token-kind
            //var blockIndex = builder.BlockRules.FindIndex(r => r is MarkdownHeadingBlockRule);
            //builder.BlockRules = builder.BlockRules.Insert(blockIndex, new LuceneNoteBlockRule());
        }
    }
}