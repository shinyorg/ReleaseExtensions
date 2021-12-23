using Microsoft.DocAsCode.Dfm;
using System.Collections.Generic;
using System.Composition;


namespace Shiny.DocFx.Extensions
{
    [Export(typeof(IDfmCustomizedRendererPartProvider))]
    public class StartupBlockCodeRendererPartProvider : IDfmCustomizedRendererPartProvider
    {
        public IEnumerable<IDfmCustomizedRendererPart> CreateParts(IReadOnlyDictionary<string, object> parameters)
        {
            yield return new StartupBlockCodeRendererPart();
        }
    }
}
