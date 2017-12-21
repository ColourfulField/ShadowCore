using System;

namespace ShadowTools.Utilities.Extensions.Reflection
{
    public static class ObjectExtensions
    {
        public static T Clone<T>(this T source, int depth = 0)
        {
            if (depth < 0)
            {
                return default(T);
            }

            var destination = Activator.CreateInstance<T>();

            var sourceProperties = source.GetType().GetProperties();
            foreach (var sourceProperty in sourceProperties)
            {
                if (sourceProperty.PropertyType.IsSimple())
                {
                    sourceProperty.SetValue(destination, sourceProperty.GetValue(source));
                }
                else
                {
                    if (sourceProperty.GetValue(source) != null)
                    {
                        var childInstance = Clone<T>((T) sourceProperty.GetValue(source), depth - 1);
                        sourceProperty.SetValue(destination, childInstance);
                    }
                }
            }

            return destination;
        }
    }
}
