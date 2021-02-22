using System.Linq;

namespace Flux
{
  namespace Reflection
  {
    public static class ApplicationDomain
    {
      public static System.Collections.Generic.IEnumerable<System.Type> GetTypesImplementingInterface<TInterface>()
        => typeof(TInterface).IsInterface
        ? typeof(ApplicationDomain).Assembly.GetTypes().Where(t => typeof(TInterface).IsAssignableFrom(t) && ((t.IsClass && !t.IsAbstract) || t.IsValueType)).ToList()
        : throw new System.ArgumentException($"The type <{typeof(TInterface).FullName}> is not an interface.");
    }
  }
}
