using System;

namespace ShadowTools.Mapper.Options
{
    public class AutomapperOptions
    {
        private int _maxDepth;
        public int MaxDepth {
            get  => _maxDepth; 
            set {
                if (_maxDepth >= 0 && _maxDepth < 1000)
                {
                    _maxDepth = value;
                }
                else
                {
                    throw new ArgumentException("Automapper depth should be between 0 and 1000"); 
                }
            }
        }
    }
}
