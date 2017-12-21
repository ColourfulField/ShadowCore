using System.Collections.Generic;

namespace ShadowTools.Utilities.Localization
{
    public class ClassLibraryLocalizationOptions
    {
        public IReadOnlyDictionary<string, string> ResourcePaths { get; set; }
        public bool UseSeparateFolderForEachResource { get; set; } = false;
    }
}
