using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace ShadowCore.API.Configuration.Extensions
{
    [SuppressMessage("", "CS1591:MissingXmlDocumentation")]
    public static class ApiDescriptionExtensions
    {
        /// <summary>
        /// Retrieves all attributes of the controller and it's parents
        /// </summary>
        /// <param name="apiDescription"></param>
        /// <returns></returns>
        public static IEnumerable<object> AllControllerAttributes(this ApiDescription apiDescription)
        {
            var controllerActionDescriptor = apiDescription.ControllerActionDescriptor();
            if (controllerActionDescriptor == null)
            {
                return Enumerable.Empty<object>();
            }

            // Using controllerActionDescriptor.type.GetCustomTypes(true) returns invalid attribute collection,
            // so we need to manually traverse the hierarchy tree
            return GetHierarchyControllerAttributes(controllerActionDescriptor.ControllerTypeInfo);
        }

        /// <summary>
        /// Recursively goes through type hierarchy tree and accumulates attributes of each type in the tree.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static IEnumerable<object> GetHierarchyControllerAttributes(TypeInfo type)
        {
            if (type.BaseType == null)
            {
                return type.GetCustomAttributes(false);
            }

            return type.GetCustomAttributes(false)
                       .ToList()
                       .Concat(GetHierarchyControllerAttributes(type.BaseType.GetTypeInfo()).ToList());
        }

        /// <summary>
        /// Gets Controller action descriptor object from apiDescription
        /// </summary>
        /// <param name="apiDescription"></param>
        /// <returns></returns>
        private static ControllerActionDescriptor ControllerActionDescriptor(this ApiDescription apiDescription)
        {
            var controllerActionDescriptor = apiDescription.GetProperty<ControllerActionDescriptor>();
            if (controllerActionDescriptor == null)
            {
                controllerActionDescriptor = apiDescription.ActionDescriptor as ControllerActionDescriptor;

                if (controllerActionDescriptor != null)
                {
                    apiDescription.SetProperty(controllerActionDescriptor);
                }
            }
            return controllerActionDescriptor;
        }
    }
}
