namespace Flux
{
  public static partial class FxReflection
  {
    /// <summary>
    /// <para>Attempts to get all (<typeparamref name="TAttribute"/>) attributes of <paramref name="source"/> and indicates whether successful.</para>
    /// <para><see href="https://stackoverflow.com/a/37803935/3178666"/></para>
    /// </summary>
    /// <typeparam name="TAttribute"></typeparam>
    /// <param name="source"></param>
    /// <param name="attribute"></param>
    /// <returns></returns>
    public static bool TryGetAttribute<TAttribute>(this System.Reflection.ICustomAttributeProvider source, out TAttribute[] attribute)
      where TAttribute : System.Attribute
    {
      attribute = source.GetCustomAttributes(true).Where(o => o is TAttribute).Cast<TAttribute>().ToArray();

      return attribute.Length > 0;
    }

    /// <summary>
    /// <para>Attempts to get all (<typeparamref name="TAttribute"/>) attributes of <paramref name="source"/> and indicates whether successful.</para>
    /// <para><see href="https://stackoverflow.com/a/37803935/3178666"/></para>
    /// </summary>
    /// <typeparam name="TAttribute"></typeparam>
    /// <param name="source"></param>
    /// <param name="attribute"></param>
    /// <returns></returns>
    public static bool TryGetAttribute<TSource, TAttribute>(TSource source, out TAttribute[] attribute)
      where TAttribute : System.Attribute
    {
      if (source is null)
      {
        attribute = [];
        return false;
      }

      return source.GetType().TryGetAttribute<TAttribute>(out attribute);
    }

    ///// <summary>
    ///// <para>Indicates whether an object is marked with a certain attribute.</para>
    ///// <para><see href="https://stackoverflow.com/a/37803935/3178666"/></para>
    ///// <example><code>if (o.HasAttribute&lt;SomeAssignableAttribute&gt;()) { }</code></example>
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    ///// <param name="source"></param>
    ///// <returns></returns>
    //public static bool HasAttribute<T>(this System.Reflection.ICustomAttributeProvider source)
    //  where T : System.Attribute
    //  => source.GetCustomAttributes(true).Any(o => o is T a);

    ///// <summary>
    ///// <para>Indicates whether an object is marked with a certain attribute.</para>
    ///// <para><see href="https://stackoverflow.com/a/37803935/3178666"/></para>
    ///// <example><code>if (o.HasAttribute&lt;SomeAssignableAttribute&gt;()) { }</code></example>
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    ///// <param name="source"></param>
    ///// <returns></returns>
    //public static bool HasAttribute<T>(this object source)
    //  where T : System.Attribute
    //  => source != null && source.GetType().HasAttribute<T>();
  }
}
