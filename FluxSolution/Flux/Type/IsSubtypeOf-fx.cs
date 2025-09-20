//namespace Flux
//{
//  public static partial class Types
//  {
//    /// <summary>
//    /// <para>Determines whether the <paramref name="source"/> is a subtype of <paramref name="supertype"/>.</para>
//    /// </summary>
//    /// <remarks>Similar functionality as the built-in <see cref="System.Type.IsSubclassOf(Type)"/> but can also handle generics. This is also the same as switching the two arguments for <see cref="IsSupertypeOf(Type, Type)"/>.</remarks>
//    public static bool IsSubtypeOf(this System.Type source, System.Type supertype)
//    {
//      if (source is null || supertype is null || source.Equals(supertype))
//        return false;

//      var sourceInterfaces = source.GetInterfaces();

//      for (var index = sourceInterfaces.Length - 1; index >= 0; index--)
//      {
//        var type = sourceInterfaces[index];

//        if (supertype.IsGenericTypeDefinition && type.IsGenericType)
//          type = type.GetGenericTypeDefinition();

//        if (supertype.Equals(type))
//          return true;
//      }

//      var sourceInherited = GetInheritedBaseTypes(source, false);

//      for (var index = sourceInherited.Count - 1; index >= 0; index--)
//      {
//        var type = sourceInherited[index];

//        if (supertype.IsGenericTypeDefinition && type.IsGenericType)
//          type = type.GetGenericTypeDefinition();

//        if (supertype.Equals(type))
//          return true;
//      }

//      return false;
//    }

//    /// <summary>
//    /// <para>Determines whether the <paramref name="source"/> is a supertype of <paramref name="subtype"/>.</para>
//    /// </summary>
//    /// <remarks>This is (literally) the same as switching the two arguments for <see cref="IsSubtypeOf(Type, Type)"/>.</remarks>
//    public static bool IsSupertypeOf(this System.Type source, System.Type subtype)
//      => subtype.IsSubtypeOf(source);
//  }
//}
