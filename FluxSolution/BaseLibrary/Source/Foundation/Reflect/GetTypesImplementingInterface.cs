using System.Linq;

namespace Flux
{
  public static partial class Reflect
  {
    public static System.Collections.Generic.IEnumerable<System.Type> GetTypesImplementingInterface<TInterface>()
      => typeof(TInterface).IsInterface
      ? typeof(Reflect).Assembly.GetTypes().Where(t => typeof(TInterface).IsAssignableFrom(t) && ((t.IsClass && !t.IsAbstract) || t.IsValueType)).ToList()
      : throw new System.ArgumentException($"The type <{typeof(TInterface).FullName}> is not an interface.");
  }
}
