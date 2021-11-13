using System.Linq;

namespace Flux
{
  public static partial class Reflect
  {
    public static System.Collections.Generic.IEnumerable<System.Type> GetTypesImplementingInterface(System.Type type)
      => type.IsInterface
      ? typeof(Reflect).Assembly.GetTypes().Where(t => t != type && (t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == type) || (t.IsGenericType ? (t.GetGenericTypeDefinition() == type) : (type.IsAssignableFrom(t) && ((t.IsClass && !t.IsAbstract) || t.IsValueType)))))
      : throw new System.ArgumentException($"The type <{type.FullName}> is not an interface.");
  }
}
