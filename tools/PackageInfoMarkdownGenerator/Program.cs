using Newtonsoft.Json;
using Shiny;

var relativePath = "../../../../../samples/docfx/articles";
var savePath = relativePath;
//var relativePath = args.Length == 0 ? "." : args[0];
//var savePath = args.Length == 2 ? "." : args[1];
var jsonFiles = Directory.GetFiles(relativePath, "*.json");

Console.WriteLine("Package Files to process: " + jsonFiles.Length);

foreach (var jsonFile in jsonFiles)
{
    var file = new FileInfo(jsonFile);
    var content = File.ReadAllText(file.FullName);

    try
    { 
        var package = JsonConvert.DeserializeObject<Package>(content);

        if (package.SaveLocation == null)
            package.SaveLocation = Path.ChangeExtension(file.FullName, ".md");

        var mdPath = Path.Combine(savePath, package.SaveLocation);
        if (File.Exists(mdPath))
            File.Delete(mdPath);

        Console.WriteLine("Generating markdown file for {0}", file.Name);
        var md = Generator.ToMarkdown(package);
        File.WriteAllText(mdPath, md);
        Console.WriteLine("Generated {0}", mdPath);
    }
    catch (Exception ex)
    {
        Console.WriteLine("Failed to write markdown file for " + file.FullName);
    }
}
Console.WriteLine("Press <ENTER> to quit");
Console.ReadLine();