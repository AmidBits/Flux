namespace Flux
{
  public static partial class TypeEm
  {
    /// <summary>Creates a new sequence with the derived types of the <paramref name="source"/> from types in the specified type collection.</summary>
    public static System.Collections.Generic.IEnumerable<System.Type> GetDerivedTypes(this System.Type source, params System.Type[] types)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      foreach (var type in types)
        if (IsSubtypeOf(type, source))
          yield return type;
    }
    /// <summary>Creates a new sequence with the derived types of the <paramref name="source"/> from types within the Flux type library.</summary>
    public static System.Collections.Generic.IEnumerable<System.Type> GetDerivedTypes(this System.Type source)
      => GetDerivedTypes(source, typeof(TypeEm).Assembly.GetTypes());
  }
}
