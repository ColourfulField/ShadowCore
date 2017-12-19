using System.Collections.Generic;

namespace ShadowBox.Utilities.Localization
{
    public class ClassLibraryLocalizationOptions
    {
        public IReadOnlyDictionary<string, string> ResourcePaths { get; set; }
        public bool UseSeparateFolderForEachResource { get; set; } = false;
    }
}
