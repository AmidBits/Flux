namespace Flux
{
  public static partial class Em
  {
    /// <summary>
    /// <para></para>
    /// <para><see href="https://stackoverflow.com/a/37803935/3178666"/></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="self"></param>
    /// <returns></returns>
    public static bool HasAttribute<T>(this System.Reflection.ICustomAttributeProvider self)
      where T : System.Attribute
      => self.GetCustomAttributes(true).Any(o => o is T);

    /// <summary>
    /// <para></para>
    /// <para><see href="https://stackoverflow.com/a/37803935/3178666"/></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="self"></param>
    /// <returns></returns>
    public static bool HasAttribute<T>(this object self)
      where T : System.Attribute
      => self != null && self.GetType().HasAttribute<T>();
  }
}
