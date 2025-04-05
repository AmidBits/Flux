namespace Flux
{
  public static partial class FxType
  {
    /// <summary>Creates a new sequence with the inheritance "chain" of base types of the <paramref name="source"/> type, excluding the <paramref name="source"/> itself. This does not include interfaces.</summary>
    public static System.Collections.Generic.List<System.Type> GetInheritedBaseTypes(this System.Type source, bool includeSource)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var list = new System.Collections.Generic.List<System.Type>();

      for (var baseType = includeSource ? source : source.BaseType; baseType != null; baseType = baseType.BaseType)
        list.Add(baseType);

      return list;
    }
  }
}
