namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Attempts to get all (<typeparamref name="T"/>) attributes of <paramref name="source"/> and indicates whether successful.</para>
    /// <para><see href="https://stackoverflow.com/a/37803935/3178666"/></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="attribute"></param>
    /// <returns></returns>
    public static bool TryGetAttribute<T>(this System.Reflection.ICustomAttributeProvider source, out T[] attribute)
      where T : System.Attribute
    {
      attribute = source.GetCustomAttributes(true).Where(o => o is T).Cast<T>().ToArray();

      return attribute.Length > 0;
    }

    /// <summary>
    /// <para>Attempts to get all (<typeparamref name="T"/>) attributes of <paramref name="source"/> and indicates whether successful.</para>
    /// <para><see href="https://stackoverflow.com/a/37803935/3178666"/></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="attribute"></param>
    /// <returns></returns>
    public static bool TryGetAttribute<T>(this object source, out T[] attribute)
      where T : System.Attribute
    {
      if (source is null)
      {
        attribute = [];
        return false;
      }

      return source.GetType().TryGetAttribute<T>(out attribute);
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
