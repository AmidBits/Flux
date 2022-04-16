namespace Flux
{
  public static partial class TypeEm
  {
    /// <summary>Determines whether the <paramref name="source"/> is a subtype of <paramref name="supertype"/>. Similar functionality as IsSubclassOf but can also handle generics. IsSupertypeOf can be performed by switching the two arguments.</summary>
    public static bool IsSubtypeOf(this System.Type source, System.Type supertype)
    {
      if (source is null || supertype is null || source.Equals(supertype))
        return false;

      foreach (var type in source.GetInterfaces())
        if (supertype.Equals(supertype.IsGenericTypeDefinition && type.IsGenericType ? type.GetGenericTypeDefinition() : type))
          return true;

      foreach (var type in GetBaseTypeChain(source))
        if (supertype.Equals(supertype.IsGenericTypeDefinition && type.IsGenericType ? type.GetGenericTypeDefinition() : type))
          return true;

      return false;
    }
  }
}
