//namespace Flux
//{
//  public static partial class Types
//  {
//    /// <summary>
//    /// <para>Determines whether <paramref name="source"/> is assignable to the <paramref name="genericType"/>.</para>
//    /// <para><see href="https://stackoverflow.com/a/1075059/3178666"/></para>
//    /// <example>var isUnsignedNumber = typeof(int).IsAssignableToGenericType(typeof(System.Numerics.IUnsignedNumber&lt;>));</example>
//    /// </summary>
//    public static bool IsAssignableToGenericType(this System.Type source, System.Type genericType)
//    {
//      if (source.IsGenericType && source.GetGenericTypeDefinition() == genericType)
//        return true;

//      foreach (var it in source.GetInterfaces())
//        if (it.IsGenericType && it.GetGenericTypeDefinition() == genericType)
//          return true;

//      return source.BaseType?.IsAssignableToGenericType(genericType) ?? false;
//    }

//    //public static bool IsAssignableToGenericType<T>(this T source, System.Type genericType) => typeof(T).IsAssignableToGenericType(genericType);
//  }
//}
