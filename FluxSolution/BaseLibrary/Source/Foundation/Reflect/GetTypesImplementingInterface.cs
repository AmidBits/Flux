using System.Linq;

namespace Flux
{
  public static partial class Reflect
  {
    public static System.Collections.Generic.IEnumerable<System.Type> GetTypesImplementingGenericInterface(System.Type type)
      => type.IsInterface
      ? typeof(Reflect).Assembly.GetTypes().Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == type))
      : throw new System.ArgumentException($"The type <{type.FullName}> is not an interface.");

    public static System.Collections.Generic.IEnumerable<System.Type> GetTypesImplementingInterface(System.Type type)
      => type.IsInterface
      //? typeof(Reflect).Assembly.GetTypes() is var types && type.IsGenericType
      //? types.Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == type))
      //: types.Where(t => type.IsAssignableFrom(t) && ((t.IsClass && !t.IsAbstract) || t.IsValueType))
      ? typeof(Reflect).Assembly.GetTypes().Where(t => t != type).Where(t =>
      t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == type)
      ||
      (type.IsAssignableFrom(t) && ((t.IsClass && !t.IsAbstract) || t.IsValueType))
      )
      : throw new System.ArgumentException($"The type <{type.FullName}> is not an interface.");
  }
}
