namespace Flux
{
  public static partial class Enums
  {
    /// <summary>
    /// <para>Gets all <see cref="System.Enum"/> <typeparamref name="TAttribute"/> attributes of <paramref name="source"/>.</para>
    /// </summary>
    /// <typeparam name="TAttribute"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static System.Collections.Generic.List<TAttribute> GetEnumAttribute<TAttribute>(this System.Enum source)
      where TAttribute : Attribute
      => [.. source.GetType().GetMember(source.ToString()).SelectMany(mi => mi.GetCustomAttributes(typeof(TAttribute), false).OfType<TAttribute>())];

    /// <summary>
    /// <para>Attempts to get all <see cref="System.Enum"/> <typeparamref name="TAttribute"/> attributes of <paramref name="source"/> and indicates whether successful.</para>
    /// </summary>
    /// <typeparam name="TAttribute"></typeparam>
    /// <param name="source"></param>
    /// <param name="attributes"></param>
    /// <returns></returns>
    public static bool TryGetEnumAttribute<TAttribute>(this System.Enum source, out System.Collections.Generic.List<TAttribute> attributes)
      where TAttribute : Attribute
    {
      try { attributes = source.GetEnumAttribute<TAttribute>(); }
      catch { attributes = []; }

      return attributes is not null && attributes.Count > 0;
    }
  }
}
