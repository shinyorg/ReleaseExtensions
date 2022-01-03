//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using Devlead.Statiq.Tabs;
//using Statiq.Common;
//using Statiq.Markdown;


//namespace Docs.Shortcodes
//{
//    public class PackageInfoShortcode : SyncShortcode
//    {
//        public override ShortcodeResult Execute(KeyValuePair<string, string>[] args, string content, IDocument document, IExecutionContext context)
//        {
//            var packageName = args.FirstOrDefault().Value;
//            var package = Utils.GetPackage(packageName);
//            var scount = package.Services?.Length ?? 0;
//            var pcontent = "";

//            switch (scount)
//            {
//                case 0:
//                    throw new ArgumentException($"{packageName} has no services defined");

//                case 1:
//                    pcontent = RenderService(document, package, package.Services!.First());
//                    break;

//                default:
//                    if (args.Length != 2)
//                        throw new ArgumentException($"Package '{packageName}' has multiple services and thus requires a secondary argument of the service you want to render");

//                    var sn = args[1].Value;
//                    var service = package
//                        .Services!
//                        .FirstOrDefault(x => x.Name.Equals(sn));

//                    if (service == null)
//                        throw new ArgumentException("No service found called " + sn);

//                    pcontent = RenderService(document, package, service);
//                    break;
//            }
//            return new ShortcodeResult(pcontent);
//        }


//        //static void AppendIf(StringBuilder sb, string category, string? value)
//        //{
//        //    if (!value.IsNullOrEmpty())
//        //        sb.AppendLine($"|**{category}**|{value}|");
//        //}

//        // TODO: generate samples
//        // TODO: show sponsor only tag
//        static string RenderService(IDocument document, Package package, PackageService service)
//        {
//            var tabGroup = BuildTabGroup(package, service);
//            var contentBuilder = new StringBuilder();

//            contentBuilder.AppendLine("<div class=\"tab-wrap\">");

//            var first = true;
//            foreach (var tab in tabGroup.Tabs)
//            {
//                contentBuilder.AppendLine($"<input type=\"radio\" id=\"{tabGroup.Id}-{tab.Id}\" name=\"{tabGroup.Id}\" class=\"tab\" {(first ? "checked" : string.Empty)}><label for=\"{tabGroup.Id}-{tab.Id}\" >{tab.Name}</label>");
//                first = false;
//            }

//            foreach (var tab in tabGroup.Tabs)
//            {
//                contentBuilder.AppendLine("<div class=\"tab__content\">");
//                contentBuilder.AppendLine();

//                using (var writer = new StringWriter())
//                {
//                    MarkdownHelper.RenderMarkdown(
//                        null, // TODO: IExecutionState
//                        document,
//                        tab.Content,
//                        writer,
//                        false, //prependLinkRoot
//                        "common" //configuration
//                    );
//                    contentBuilder.AppendLine(writer.ToString());
//                }

//                contentBuilder.AppendLine();
//                contentBuilder.AppendLine("</div>");
//            }

//            contentBuilder.AppendLine("</div>");

//            return contentBuilder.ToString();
//        }


//        static TabGroup BuildTabGroup(Package package, PackageService service)
//        {
//            var tabGroup = new TabGroup();

//            var tabs = new List<TabGroupTab>();
//            tabs.Add(new TabGroupTab
//            {
//                Name = "General",
//                Content = RenderGeneralTab(package, service)
//            });
//            tabs.Add(new TabGroupTab
//            {
//                Name = "Startup",
//                Content = RenderStartup(service)
//            });

//            if (service.Android != null)
//            {
//                tabs.Add(new TabGroupTab
//                {
//                    Name = "Android",
//                    Content = RenderAndroid(service.Android)
//                });
//            }

//            if (service.iOS != null)
//            {
//                tabs.Add(new TabGroupTab
//                {
//                    Name = "iOS",
//                    Content = RenderIosTab(service.iOS)
//                });
//            }
//            if (service.Uwp != null)
//            {
//                tabs.Add(new TabGroupTab
//                {
//                    Name = "Windows",
//                    Content = RenderUwp(service.Uwp)
//                });
//            }
//            tabGroup.Tabs = tabs.ToArray();
//            return tabGroup;
//        }


//        static string RenderGeneralTab(Package package, PackageService service)
//        {
//            var sb = new StringBuilder()
//                .AppendLine("|Area|Info|")
//                .AppendLine("|---|---|")
//                .AppendLine($"|Description|{service.Description}|")
//                .AppendLine($"|Service|{service.Name}|")
//                .AppendLine($"|NuGet|{Utils.ToNugetShield(package.Name, package.Name)}|")
//                .AppendLine($"|Static Generated Class|{service.Static}|");

//            // samples on the service or package?  currently on package
//            //if (package != null)
//            //{
//            //    foreach (var sample )
//            //}

//            return sb.ToString();
//        }


//        static string RenderStartup(PackageService service)
//        {
//            // startup tab (can auto register, service reg),
//            var sb = new StringBuilder();
//            //new StartupShortcode().Execute(null, "", null, null).ContentProvider
//            sb.AppendLine("<?! Startup ?>");

//            var reg = $"services.{service.Startup}";
//            if (service.BgDelegate != null)
//            {
//                sb.AppendLine($"{reg}<{service.BgDelegate}>({service.StartupArgs});");

//                if (!service.BgDelegateRequired)
//                {
//                    sb.AppendLine();
//                    sb.AppendLine($"{reg}({service.StartupArgs});");
//                }
//            }
//            sb.Append($"services.{service.Startup}");

//            sb.AppendLine("<?!/ Startup ?>");
//            return sb.ToString();
//        }


//        static string RenderIosTab(iOSConfig ios)
//        {
//            var sb = new StringBuilder();
//            sb.AppendLine("## Minimum Version: " + ios.MinVersion);

//            RenderAppDelegate(ios, sb);
//            RenderInfoPlist(ios, sb);
//            RenderEntitlementsPlist(ios, sb);
//            return sb.ToString();
//        }


