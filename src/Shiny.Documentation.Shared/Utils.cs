using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Shiny
{
    public static class Utils
    {
        public static string ToText(this bool value) => value ? "YES" : "NO";
        public static bool IsEmpty(this string s) => String.IsNullOrWhiteSpace(s);


        public static string ToList(string title, string[] values)
        {
            var list = $"## {title}{Environment.NewLine}";
            foreach(var value in values)
                list += $"* {value}{Environment.NewLine}";

            return list;
        }


        public static string ToMarkdownTable(string[] headers, List<string[]> data)
        {
            var sb = new StringBuilder();
            foreach (var header in headers)
                sb.Append("|" + header);

            sb.AppendLine("|");
            foreach (var header in headers)
                sb.Append("|" + String.Concat(Enumerable.Repeat("-", header.Length)));

            sb.AppendLine("|");
            foreach (var dataRow in data)
            {
                foreach (var item in dataRow)
                    sb.Append("|" + item);
                
                sb.AppendLine("|");
            }
            return sb
                .AppendLine()
                .ToString();
        }


        public static StringBuilder AppendXmlCode(this StringBuilder sb) => sb
            .AppendLine("```xml")
            .AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");


        public static StringBuilder AppendLineIf(this StringBuilder sb, string text)
        {
            if (!text.IsEmpty())
                sb.AppendLine(text);
            
            return sb;
        } 


        public static string ToNugetShield(string packageName, string? label = null)
        {
            var imageUrl = $"https://img.shields.io/nuget/v/{packageName}.svg?style=for-the-badge";
            if (label != null)
                imageUrl += $"&label={label}";

            var hrefUrl = $"https://www.nuget.org/packages/{packageName}/";

            return $"<a href=\"{hrefUrl}\" target=\"{packageName}\"><img src=\"{imageUrl}\" /></a>";
        }


        const string StartupLayout = @"
```csharp
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
```
";
        public static string GetStartup(string content) => StartupLayout.Replace("{{BODY}}", content);
    }
}
