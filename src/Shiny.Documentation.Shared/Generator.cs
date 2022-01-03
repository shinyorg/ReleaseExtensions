using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Shiny
{
    public static class Generator
    {
        public static string DefaultSampleLinkUrl { get; set; } = "https://github.com/shinyorg/samples/tree/main/";
        public static string DefaultMinAndroidVersion { get; set; } = "8.0";
        public static string DefaultTargetAndroidVersion { get; set; } = "11 (API 30)";


        public static string ToMarkdown(Package package)
        {
            var sb = new StringBuilder()
                .AppendLine("# " + package.Component)
                .AppendLine()
                .AppendLineIf(package.Description)
                .AppendLine();

            // TODO: sponsor image
            AppendGeneral(sb, package);

            if (!package.Startup.IsEmpty())
            { 
                sb
                    .AppendLine("## Shiny Startup")
                    .AppendLine(Utils.GetStartup(package.Startup))
                    .AppendLine();
            }

            if (package.Features != null)
            {
                sb
                    .Append(Utils.ToList(
                        "Features",
                        package.Features
                    ))
                    .AppendLine();
            }

            if (package.SampleLinks != null)
            {
                var links = package
                    .SampleLinks
                    .Select(url =>
                    {
                        if (!url.StartsWith("https:"))
                            url = DefaultSampleLinkUrl + url;

                        return $"<{url}>";
                    })
                    .ToArray();

                sb
                    .Append(Utils.ToList(
                        "Samples",
                        links
                    ))
                    .AppendLine();
            }

            AppendPlatforms(sb, package);
            AppendFaq(sb, package.Faqs);
            return sb.ToString();
        }


        static void AppendGeneral(StringBuilder sb, Package package)
        {
            var data = new List<string[]>();
            if (!package.Nuget.IsEmpty())
                data.Add(new[] { "NuGet", $"[!NugetShield({package.Nuget})]" });

            if (!package.Service.IsEmpty())
            {
                data.Add(new [] { "Service", package.Service });
                data.Add(new [] { "Auto-Register", package.CanAutoRegister.ToText() });
            }

            if (!package.Delegate.IsEmpty())
                data.Add(new[] { "Delegate", package.Delegate });

            if (!package.StaticClass.IsEmpty())
                data.Add(new[] { "Static Class", package.StaticClass });

            var table = Utils.ToMarkdownTable(
                new [] { "Area", "Value" },
                data
            );
            sb.AppendLine(table);
        }


        static void AppendFaq(StringBuilder sb, Faq[] faqs)
        {
            if (faqs == null)
                return;

            sb
                .AppendLine("## Frequently Asked Questions")
                .AppendLine();

            foreach (var faq in faqs)
            {
                sb
                    .AppendLine($"> Q: {faq.Question}")
                    .AppendLine($">> A: {faq.Answer}")
                    .AppendLine();
            }
        }


        static void AppendPlatforms(StringBuilder sb, Package package)
        {
            if (package.iOS == null && package.Android == null && package.Uwp == null)
                return;

            AppendIos(sb, package.iOS);
            sb.AppendLine();

            AppendAndroid(sb, package.Android);
            sb.AppendLine();

            AppendUwp(sb, package.Uwp);
            sb.AppendLine();
        }


        static void AppendIos(StringBuilder sb, iOSConfig ios)
        {
            if (ios == null)
                return;

            WritePlatformHeader(sb, "iOS", ios.Info);
            if (ios.UsesPush || ios.UsesJobs || ios.UsesBackgroundTransfers)
            {
                sb.AppendLine("# [App Delegate](#iostab/appdelegate)").AppendLine();
                AppendIosAppDelegate(sb, ios);
                sb.AppendLine();
            }
            if (ios.UsesBackgroundTransfers || ios.UsesJobs || ios.UsesPush || ios.InfoPlistValues != null || ios.BackgroundModes != null)
            {
                sb.AppendLine("# [Info Plist](#iostab/info)").AppendLine();
                AppendIosInfoPlist(sb, ios);
                sb.AppendLine();
            }
            if (ios.Entitlements != null || ios.UsesPush)
            {
                sb.AppendLine("# [Entitlements.plist](#iostab/entitlements)").AppendLine();
                AppendIosEntitlementsPlist(sb, ios);
                sb.AppendLine();
            }
            sb
                .AppendLine()
                .AppendLine("***")
                .AppendLine();
        }


        static void AppendAndroid(StringBuilder sb, AndroidConfig android)
        {
            if (android == null)
                return;

            WritePlatformHeader(sb, "Android", android.Info);
            sb
                .AppendLine("Minimum Version: " + android.MinVersion ?? DefaultMinAndroidVersion)
                .AppendLine("Target Version: " + android.TargetVersion ?? DefaultTargetAndroidVersion);

            if (android.ManifestUsesFeatures != null || android.ManifestUsesPermissions != null)
            {
                sb
                    .AppendXmlCode()
                    .AppendLine("<manifest xmlns:android=\"http://schemas.android.com/apk/res/android\" android:versionCode=\"1\" android:versionName=\"1.0\" package=\"com.org.yourapp\" android:installLocation=\"preferExternal\">");

                if (android.ManifestUsesPermissions != null)
                    foreach (var permission in android.ManifestUsesPermissions)
                        sb.AppendLine($"    <uses-permission android:name=\"{permission}\" />");

                if (android.ManifestUsesFeatures != null)
                    foreach (var feature in android.ManifestUsesFeatures)
                        sb.AppendLine($"    <uses-feature android:name=\"{feature}\" />");

                sb
                    .AppendLine("</manifest>")
                    .AppendLine("```");
            }
        }


        static void AppendUwp(StringBuilder sb, UwpConfig uwp)
        {
            if (uwp == null)
                return;

            WritePlatformHeader(sb, "UWP", uwp.Info);
            if (uwp.DeviceCapabilities == null && uwp.Capabilities == null && uwp.BackgroundTasks == null)
            {
                sb.AppendLine("No Special Configuration Required");
                return;
            }
            sb
                .AppendXmlCode()
                .AppendLine("<Package>")
                .AppendLine("    <Applications>");

            if (uwp.BackgroundTasks != null)
            {
                sb.AppendLine("        <Extensions>");
                foreach (var task in uwp.BackgroundTasks)
                    sb.AppendLine($"            <Task Type=\"{task}\" />");

                sb.AppendLine("        </Extensions>");
            }

            sb.AppendLine("    </Applications>");

            if (uwp.DeviceCapabilities != null || uwp.Capabilities != null)
            {
                sb.AppendLine("    <Capabilities>");
                if (uwp.Capabilities != null)
                    foreach (var cap in uwp.Capabilities)
                        sb.AppendLine($"        <Capability Name=\"{cap}\" />");

                if (uwp.DeviceCapabilities != null)
                    foreach (var dc in uwp.DeviceCapabilities)
                        sb.AppendLine($"        <DeviceCapability Name=\"{dc}\" />");

                sb.AppendLine("    </Capabilities>");
            }

            sb
                .AppendLine("</Package>")
                .AppendLine("```");
        }


        static void WritePlatformHeader(StringBuilder sb, string title, string info) => sb
            .AppendLine("## " + title)
            .AppendLineIf(info)
            .AppendLine();


        static void AppendIosAppDelegate(StringBuilder sb, iOSConfig ios)
        {
            sb
                .AppendLine("```csharp")
                .AppendLine("using System;")
                .AppendLine("using Foundation;")
                .AppendLine("using Xamarin.Forms.Platform.iOS;")
                .AppendLine("using Shiny;")
                .AppendLine()
                .AppendLine("namespace YourIosApp")
                .AppendLine("{")
                .AppendLine("   [Register(\"AppDelegate\")]")
                .AppendLine("   public partial class AppDelegate : FormsApplicationDelegate")
                .AppendLine("   {")
                .AppendLine("       public override bool FinishedLaunching(UIApplication app, NSDictionary options)")
                .AppendLine("       {")
                .AppendLine("           this.ShinyFinishedLaunching(new Samples.SampleStartup());")
                .AppendLine("           global::Xamarin.Forms.Forms.Init();")
                .AppendLine("           this.LoadApplication(new Samples.App());")
                .AppendLine("       }")
                .AppendLine();

            if (ios.UsesJobs)
                sb.AppendLine("        public override void PerformFetch(UIApplication application, Action<UIBackgroundFetchResult> completionHandler) => this.ShinyPerformFetch(completionHandler);").AppendLine();

            if (ios.UsesPush)
            {
                sb
                    .AppendLine("        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken) => this.ShinyRegisteredForRemoteNotifications(deviceToken);")
                    .AppendLine()
                    .AppendLine("        public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error) => this.ShinyFailedToRegisterForRemoteNotifications(error);")
                    .AppendLine()
                    .AppendLine("        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler) => this.ShinyDidReceiveRemoteNotification(userInfo, completionHandler);")
                    .AppendLine();
            }

            if (ios.UsesBackgroundTransfers)
                sb.AppendLine("        public override void HandleEventsForBackgroundUrl(UIApplication application, string sessionIdentifier, Action completionHandler) => this.ShinyHandleEventsForBackgroundUrl(sessionIdentifier, completionHandler);").AppendLine();

            sb
                .AppendLine("\t}")
                .AppendLine("}")
                .AppendLine("```");
        }


        static void AppendIosInfoPlist(StringBuilder sb, iOSConfig ios)
        {
            sb
                .AppendXmlCode()
                .AppendLine("<!DOCTYPE plist PUBLIC \" -//Apple//DTD PLIST 1.0//EN\" \"http://www.apple.com/DTDs/PropertyList-1.0.dtd\">")
                .AppendLine("<plist version=\"1.0\">")
                .AppendLine("<dict>");

            if (ios.UsesJobs)
            {
                sb.Append(@"
            <key>BGTaskSchedulerPermittedIdentifiers</key>
            <array>
                <string>com.shiny.job</string>
                <string>com.shiny.jobpower</string>
                <string>com.shiny.jobnet</string>
                <string>com.shiny.jobpowernet</string>
            </array>
        ");
            }

            if (ios.InfoPlistValues != null)
            {
                foreach (var value in ios.InfoPlistValues)
                {
                    sb
                        .AppendLine($"    <key>{value}</key>")
                        .AppendLine("       <string>Say something useful here that your users will understand</string>");
                }
            }

            if (ios.BackgroundModes != null || ios.UsesJobs || ios.UsesPush)
            {
                sb
                    .AppendLine("    <key>UIBackgroundModes</key>")
                    .AppendLine("    <array>");

                if (ios.UsesJobs)
                {
                    sb
                        .AppendLine("       <string>processing</string>")
                        .AppendLine("       <string>fetch</string>");
                }
                if (ios.UsesPush)
                {
                    sb.AppendLine("     <string>remote-notification</string>");
                }
                if (ios.BackgroundModes != null)
                {
                    foreach (var bg in ios.BackgroundModes)
                    {
                        sb.AppendLine($"        <string>{bg}</string>");
                    }
                }
                sb.AppendLine("    </array>");
            }
            sb
                .AppendLine("</dict>")
                .AppendLine("</plist>")
                .AppendLine("```");
        }


        static void AppendIosEntitlementsPlist(StringBuilder sb, iOSConfig ios)
        {
            sb
                .AppendXmlCode()
                .AppendLine("<!DOCTYPE plist PUBLIC \" -//Apple//DTD PLIST 1.0//EN\" \"http://www.apple.com/DTDs/PropertyList-1.0.dtd\">")
                .AppendLine("<plist version=\"1.0\">")
                .AppendLine("<dict>");

            if (ios.Entitlements != null)
            {
                foreach (var pair in ios.Entitlements)
                {
                    sb.AppendLine($"    <key>{pair.Key}</key>");
                    if (pair.Value.StartsWith("<"))
                        sb.AppendLine($"    {pair.Value}");
                    else
                        sb.AppendLine($"    <string>{pair.Value}</string>");
                }
            }

            if (ios.UsesPush)
            {
                sb
                    .AppendLine("   <key>aps-environment</key>")
                    .AppendLine("   <string>development OR production</string>");
            }
            sb
                .AppendLine("</dict>")
                .AppendLine("</plist>")
                .AppendLine("```");
        }
    }
}
