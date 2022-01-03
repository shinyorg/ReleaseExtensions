using System;
using System.Collections.Generic;
using Statiq.Common;


namespace Shiny.Statiq.Extensions
{
    public class StartupShortcode : SyncShortcode
    {
        public override ShortcodeResult Execute(KeyValuePair<string, string>[] args, string content, IDocument document, IExecutionContext context)
        {
            var full = Utils.GetStartup(content);
            return new ShortcodeResult(full);
        }
    }
}