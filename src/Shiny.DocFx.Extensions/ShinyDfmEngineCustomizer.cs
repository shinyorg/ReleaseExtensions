using Microsoft.DocAsCode.Dfm;
using System.Collections.Generic;
using System.ComponentModel.Composition;


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

//using System.Text.RegularExpressions;
//using Microsoft.DocAsCode.MarkdownLite;

//namespace LuceneDocsPlugins
//{

//    /// <summary>
//    /// The regex rule to parse out the custom Lucene tokens
//    /// </summary>
//    public class LuceneNoteBlockRule : IMarkdownRule
//    {
//        // TODO: I think there's an issue with this regex and multi-line or something, for example see: DrillDownQuery class
//        // since this isn't matching it's experimental tag (there's lots of others too)
//        public virtual Regex LabelRegex { get; } = new Regex("^@lucene\\.(?<notetype>(experimental|internal))", RegexOptions.Compiled);

//        public virtual IMarkdownToken TryMatch(IMarkdownParser parser, IMarkdownParsingContext context)
//        {

//            var match = LabelRegex.Match(context.CurrentMarkdown);
//            if (match.Length == 0)
//            {
//                return null;
//            }
//            var sourceInfo = context.Consume(match.Length);
//            return new LuceneNoteBlockToken(this, parser.Context, match.Groups[1].Value, sourceInfo);
//        }

//        public virtual string Name => "LuceneNote";
//    }
//}





//using Microsoft.DocAsCode.MarkdownLite;

//namespace LuceneDocsPlugins
//{
//    /// <summary>
//    /// A custom token class representing our custom Lucene tokens
//    /// </summary>
//    public class LuceneNoteBlockToken : IMarkdownToken
//    {
//        public LuceneNoteBlockToken(IMarkdownRule rule, IMarkdownContext context, string label, SourceInfo sourceInfo)
//        {
//            Rule = rule;
//            Context = context;
//            Label = label;
//            SourceInfo = sourceInfo;
//        }

//        public IMarkdownRule Rule { get; }

//        public IMarkdownContext Context { get; }

//        public string Label { get; }

//        public SourceInfo SourceInfo { get; }
//    }

//}