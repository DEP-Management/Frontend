using DEP_Blazor_WASM.Services.Interfaces;
using System.Reflection;

namespace DEP_Blazor_WASM.Services
{
    public class ObjectComparisonService : IObjectComparisonService
    {
        public bool AreSimplePropertiesDifferent<T>(T obj1, T obj2)
        {
            if (obj1 is null && obj2 is null) return false;
            
            if (obj1 == null || obj2 == null) return true; // Consider null as different

            return typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(prop => IsPrimitiveOrNullablePrimitive(prop.PropertyType)) // Only check primitive properties
                .Any(prop => !Equals(prop.GetValue(obj1), prop.GetValue(obj2)));
        }

        private static bool IsPrimitiveOrNullablePrimitive(Type type)
        {
            return type.IsPrimitive || type.IsEnum || type == typeof(string) ||
                   type == typeof(decimal) || type == typeof(DateTime) ||
                   Nullable.GetUnderlyingType(type)?.IsPrimitive == true ||
                   Nullable.GetUnderlyingType(type)?.IsEnum == true;
        }

    }
}
