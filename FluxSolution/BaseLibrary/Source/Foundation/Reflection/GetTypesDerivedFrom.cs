namespace Flux
{
  public static partial class Reflection
  {
    /// <summary>Creates a new sequence of types, from a pool of specified <paramref name="poolOfTypes"/>, which derives from the <paramref name="type"/>.</summary>
    public static System.Collections.Generic.IEnumerable<System.Type> GetTypesDerivedFrom(System.Type type, params System.Type[] poolOfTypes)
      => System.Linq.Enumerable.Where(poolOfTypes, t => t.IsSubtypeOf(type));
    /// <summary>Creates a new sequence of types, from all types in Flux, which derives from the <paramref name="type"/>.</summary>
    public static System.Collections.Generic.IEnumerable<System.Type> GetTypesDerivedFrom(System.Type type)
      => GetTypesDerivedFrom(type, typeof(Reflection).Assembly.GetTypes());
  }
}
