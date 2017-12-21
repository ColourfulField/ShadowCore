using System;

namespace ShadowTools.AutomaticDI
{
    public class FeatureAttribute: Attribute
    {
        public string Name { get; set; }

        public FeatureAttribute(string name)
        {
            Name = name;
        }
    }
}
