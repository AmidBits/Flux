namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Creates a new sequence with the derived types of the <paramref name="source"/> from the specified <paramref name="types"/>.</summary>
    public static System.Collections.Generic.IEnumerable<System.Type> GetDerivedTypes(this System.Type source, System.Collections.Generic.IEnumerable<System.Type> types)
      => System.Linq.Enumerable.Where(types, type => type.IsSubtypeOf(source));

    /// <summary>Creates a new sequence with the derived types of <paramref name="source"/> selected from types within the <paramref name="source"/> assembly.</summary>
    public static System.Collections.Generic.IEnumerable<System.Type> GetDerivedTypes(this System.Type source)
      => source.GetDerivedTypes(source.Assembly.DefinedTypes);
  }
}
