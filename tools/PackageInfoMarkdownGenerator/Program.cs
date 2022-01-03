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
    var package = JsonConvert.DeserializeObject<Package>(content);

    if (package.SaveLocation == null)
        package.SaveLocation = file.FullName.Replace(".json", ".md");

    var mdPath = Path.Combine(savePath, package.SaveLocation);
    if (File.Exists(mdPath))
        File.Delete(mdPath);

    Console.WriteLine("Generating {0} markdown file");
    var md = Generator.ToMarkdown(package);
    File.WriteAllText(mdPath, md);
    Console.WriteLine("Generated {0}", mdPath);
}