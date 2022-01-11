using Newtonsoft.Json;
using Shiny;

var relativePath = "./transform";
//var relativePath = args.Length == 0 ? "." : args[0];
var jsonFiles = Directory.GetFiles(relativePath, "*.json");

Console.WriteLine("Package Files to process: " + jsonFiles.Length);


foreach (var jsonFile in jsonFiles)
{
    var file = new FileInfo(jsonFile);
    var content = File.ReadAllText(file.FullName);

    try
    { 
        var package = JsonConvert.DeserializeObject<Package>(content);
        var mdPath = Path.ChangeExtension(file.FullName, ".md");
        //var mdPath = Path.Combine(savePath, package.SaveLocation);
        if (File.Exists(mdPath))
            File.Delete(mdPath);

        Console.WriteLine("Generating markdown file for {0}", file.Name);
        var md = Generator.ToMarkdown(package);
        File.WriteAllText(mdPath, md);
        Console.WriteLine("Generated {0}", mdPath);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Failed to write markdown file for {file.FullName} - {ex}");
    }
}
Console.WriteLine("Press <ENTER> to quit");
Console.ReadLine();