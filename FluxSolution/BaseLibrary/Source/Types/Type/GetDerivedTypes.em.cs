namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Creates a new sequence with the derived types of the <paramref name="source"/> from the specified type collection.</summary>
    public static System.Collections.Generic.IEnumerable<System.Type> GetDerivedTypes(this System.Type source, params System.Type[] types)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      if (source.IsGenericType)
        source = source.GetGenericTypeDefinition();

      foreach (var type in types)
        if ((type.IsGenericType ? type.GetGenericTypeDefinition() : type).IsSubtypeOf(source))
          yield return type;
    }
    /// <summary>Creates a new sequence with the derived types of the <paramref name="source"/> from the types from within the Flux library only.</summary>
    public static System.Collections.Generic.IEnumerable<System.Type> GetDerivedTypes(this System.Type source)
      => GetDerivedTypes(source, typeof(ExtensionMethods).Assembly.GetTypes());
  }
}
