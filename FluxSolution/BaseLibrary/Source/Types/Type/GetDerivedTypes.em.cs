namespace Flux
{
  public static partial class Reflection
  {
    /// <summary>Creates a new sequence with the derived types of the <paramref name="source"/> from types in the specified type collection.</summary>
    public static System.Collections.Generic.IEnumerable<System.Type> GetDerivedTypes(this System.Type source, System.Collections.Generic.IEnumerable<System.Type> types)
      => types.Where(type => IsSubtypeOf(type, source));

    /// <summary>Creates a new sequence with the derived types of <paramref name="source"/> selected from types within the assemblies referenced by the <paramref name="source"/> assembly.</summary>
    public static System.Collections.Generic.IEnumerable<System.Type> GetDerivedTypes(this System.Type source)
      => GetDerivedTypes(source, source.Assembly.DefinedTypes);
  }
}
