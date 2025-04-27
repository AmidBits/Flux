namespace Flux
{
  public static partial class Types
  {
    /// <summary>
    /// <para>Gets all <typeparamref name="TAttribute"/> attributes of <paramref name="source"/>.</para>
    /// </summary>
    /// <typeparam name="TAttribute"></typeparam>
    /// <param name="source"></param>
    /// <param name="inherit"></param>
    /// <returns></returns>
    public static System.Collections.Generic.List<TAttribute> GetAttribute<TAttribute>(this System.Type source, bool inherit = false)
      where TAttribute : System.Attribute
      => [.. source.GetCustomAttributes(typeof(TAttribute), inherit).OfType<TAttribute>()];

    /// <summary>
    /// <para>Attempts to get all <typeparamref name="TAttribute"/> attributes of <paramref name="source"/> and indicates whether successful.</para>
    /// <para><see href="https://stackoverflow.com/a/37803935/3178666"/></para>
    /// </summary>
    /// <typeparam name="TAttribute"></typeparam>
    /// <param name="source"></param>
    /// <param name="attributes"></param>
    /// <param name="inherit"></param>
    /// <returns></returns>
    public static bool TryGetAttribute<TAttribute>(this System.Type source, out System.Collections.Generic.List<TAttribute> attributes, bool inherit = false)
      where TAttribute : System.Attribute
    {
      try { attributes = source.GetAttribute<TAttribute>(inherit); }
      catch { attributes = []; }

      return attributes is not null && attributes.Count > 0;
    }
  }
}
