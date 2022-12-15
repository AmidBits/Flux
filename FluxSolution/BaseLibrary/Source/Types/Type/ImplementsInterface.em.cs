namespace Flux
{
  public static partial class Reflection
  {
    /// <summary>Creates a new sequence with interfaces that the <paramref name="source"/> type implements, excluding <paramref name="source"/> itself.</summary>
    public static bool ImplementsInterface(this System.Type source, System.Type targetInterface)
      => !targetInterface.IsInterface
      ? throw new System.ArgumentException("Not an interface.", nameof(targetInterface))
      : (targetInterface.IsGenericType ? targetInterface.GetGenericTypeDefinition() : targetInterface) is var target
      ? source.GetInterfaces().Any(i => (i.IsGenericType ? i.GetGenericTypeDefinition() : i) == target)
      : false;
  }
}
