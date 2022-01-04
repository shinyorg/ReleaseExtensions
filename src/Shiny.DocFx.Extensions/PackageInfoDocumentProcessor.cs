using Microsoft.DocAsCode.Common;
using Microsoft.DocAsCode.Plugins;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Composition;
using System.IO;


namespace Shiny.DocFx.Extensions
{
    [Export(typeof(IDocumentProcessor))]
    public class PackageInfoDocumentProcessor : IDocumentProcessor
    {
        public string Name => nameof(PackageInfoDocumentProcessor);


        //[ImportMany(nameof(PackageInfoDocumentProcessor))]
        [ImportMany(nameof(PackageInfoDocumentBuildStep))]
        public IEnumerable<IDocumentBuildStep> BuildSteps { get; set; }


        public ProcessingPriority GetProcessingPriority(FileAndType file)
        {
            if (file.Type == DocumentType.Article &&
                ".json".Equals(Path.GetExtension(file.File), StringComparison.OrdinalIgnoreCase))
            {
                return ProcessingPriority.Normal;
            }
            return ProcessingPriority.NotSupported;
        }


        public FileModel Load(FileAndType file, ImmutableDictionary<string, object> metadata)
        {
            var content = new Dictionary<string, object>
            {
                ["conceptual"] = File.ReadAllText(Path.Combine(file.BaseDir, file.File)),
                ["type"] = "Conceptual",
                ["path"] = file.File,
            };
            var localPathFromRoot = PathUtility.MakeRelativePath(
                EnvironmentContext.BaseDirectory, 
                EnvironmentContext.FileAbstractLayer.GetPhysicalPath(file.File)
            );

            return new FileModel(file, content)
            {
                LocalPathFromRoot = localPathFromRoot,
            };
        }


        public SaveResult Save(FileModel model) => new SaveResult
        {
            DocumentType = "Conceptual",
            FileWithoutExtension = Path.ChangeExtension(model.File, null),
        };


        public void UpdateHref(FileModel model, IDocumentBuildContext context)
        {
        }
    }
}
