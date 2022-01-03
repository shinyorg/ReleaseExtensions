using Microsoft.DocAsCode.Plugins;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Composition;


namespace Shiny.DocFx.Extensions
{
    [Export(nameof(PackageInfoDocumentBuildStep), typeof(IDocumentBuildStep))]
    public class PackageInfoDocumentBuildStep : IDocumentBuildStep
    {
        public string Name => nameof(PackageInfoDocumentBuildStep);
        public int BuildOrder => 0;


        public void Build(FileModel model, IHostService host)
        {
            var dict = (Dictionary<string, object>)model.Content;

            var content = (string)dict["conceptual"];
            var package = JsonConvert.DeserializeObject<Package>(content);
            dict["conceptual"] = Generator.ToMarkdown(package);
        }


        public void Postbuild(ImmutableList<FileModel> models, IHostService host) {}
        public IEnumerable<FileModel> Prebuild(ImmutableList<FileModel> models, IHostService host) => models;
    }
}
