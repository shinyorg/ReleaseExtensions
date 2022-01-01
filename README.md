# Shiny Documentation and Build Extensions

[![Build](https://github.com/shinyorg/ReleaseExtensions/actions/workflows/build.yml/badge.svg?branch=master)](https://github.com/shinyorg/ReleaseExtensions/actions/workflows/build.yml)

## DOCFX

### SETUP
1. Copy Shiny.DocFx.Extensions to shinytemplate/plugins
2. Add "shinytemplate" to "template": [] array 
3. Ensure docfx.json has setting: "markdownEngineName": "dfm",

### USAGE

[!NugetShield(Your.Nuget.Package)]

```markdown
'''shinystartup
Enter startup code here
'''
```

## STATIQ

### SETUP

1. Install Nuget Package Shiny.Statiq.Extensions
2. Add the following to your statiq bootstrap
```csharp
public static async Task<int> Main(string[] args) =>
    await Bootstrapper
        .Factory
        .CreateWeb(args)
        .AddShortcode<Shiny.Statiq.Extensions.StartupShortcode>("Startup")
        .AddShortcode<Shiny.Statiq.Extensions.NugetShieldShortcode>("NugetShield")
        // OR .AddShinyExtensions()  // make sure to have namespace Shiny.Statiq.Extensions
        .RunAsync();
}
```

### USAGE

```xml
<?# Startup ?>
THIS IS A TEST
<?#/ Startup ?>

<?! NugetShield "Shiny.Push" /?>
```