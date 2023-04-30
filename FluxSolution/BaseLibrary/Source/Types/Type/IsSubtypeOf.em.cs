namespace Flux
{
  public static partial class Reflection
  {
    /// <summary>Determines whether the <paramref name="source"/> is a subtype of <paramref name="supertype"/>. Similar functionality as IsSubclassOf but can also handle generics. IsSupertypeOf can be performed by switching the two arguments.</summary>
    public static bool IsSubtypeOf(this System.Type source, System.Type supertype)
    {
      if (source is null || supertype is null || source.Equals(supertype))
        return false;

      var sourceInterfaces = source.GetInterfaces();

      for (var index = sourceInterfaces.Length - 1; index >= 0; index--)
      {
        var type = sourceInterfaces[index];

        if (supertype.IsGenericTypeDefinition && type.IsGenericType)
          type = type.GetGenericTypeDefinition();

        if (supertype.Equals(type))
          return true;
      }

      var sourceInherited = GetInheritance(source);

      for (var index = sourceInherited.Count - 1; index >= 0; index--)
      {
        var type = sourceInherited[index];

        if (supertype.IsGenericTypeDefinition && type.IsGenericType)
          type = type.GetGenericTypeDefinition();

        if (supertype.Equals(type))
          return true;
      }

      return false;
    }

    /// <summary>Determines whether the <paramref name="source"/> is a supertype of <paramref name="subtype"/>. Basically switching the two arguments for IsSubtypeOf.</summary>
    public static bool IsSupertypeOf(this System.Type source, System.Type subtype)
      => subtype.IsSubtypeOf(source);
  }
}
