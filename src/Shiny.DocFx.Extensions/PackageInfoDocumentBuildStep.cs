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


        //private readonly TaskFactory _taskFactory = new TaskFactory(new StaTaskScheduler(1));
        public void Build(FileModel model, IHostService host)
        {
            var dict = (Dictionary<string, object>)model.Content;

            var content = (string)dict["conceptual"];
            var package = JsonConvert.DeserializeObject<Package>(content);
            // TODO: build content
            //content = _taskFactory.StartNew(() => RtfToHtmlConverter.ConvertRtfToHtml(content)).Result;
            //dict["conceptual"] = content;
        }


        public void Postbuild(ImmutableList<FileModel> models, IHostService host) {}
        public IEnumerable<FileModel> Prebuild(ImmutableList<FileModel> models, IHostService host) => models;
    }
}
