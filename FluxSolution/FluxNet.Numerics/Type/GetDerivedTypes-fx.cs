namespace FluxNet.Numerics
{
  public static partial class Types
  {
    /// <summary>Creates a new sequence with the derived types of the <paramref name="source"/> from the specified <paramref name="typeCollection"/>.</summary>
    public static System.Collections.Generic.IEnumerable<System.Type> GetDerivedTypes(this System.Type source, System.Collections.Generic.IEnumerable<System.Type> typeCollection)
      => typeCollection.Where(type => type.IsSubtypeOf(source));

    /// <summary>Creates a new sequence with the derived types of <paramref name="source"/> selected from types within the <paramref name="source"/> assembly.</summary>
    public static System.Collections.Generic.IEnumerable<System.Type> GetDerivedTypes(this System.Type source)
      => source.GetDerivedTypes(source.Assembly.DefinedTypes);
  }
}
