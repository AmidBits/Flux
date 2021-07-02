namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Determines whether the <paramref name="source"/> is a subtype of <paramref name="supertype"/>. Similar functionality as IsSubclassOf but can also handle generics.</summary>
    public static bool IsSubtypeOf(this System.Type source, System.Type supertype)
    {
      if (source is null || supertype is null || source.Equals(supertype))
        return false;

      if (supertype.IsGenericType)
        supertype = supertype.GetGenericTypeDefinition();

      foreach (var type in source.GetInterfaces())
        if (supertype.Equals(type.IsGenericType ? type.GetGenericTypeDefinition() : type))
          return true;

      foreach (var type in GetBaseTypeChain(source))
        if (supertype.Equals(type.IsGenericType ? type.GetGenericTypeDefinition() : type))
          return true;

      return false;
    }
  }
}
