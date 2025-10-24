namespace Flux
{
  public static partial class XtensionEnum
  {
    /// <summary>
    /// <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static (int ExtremumMinEnumIndex, int ExtremumMinEnumItem, T ExtremumMinEnumValue, int ExtremumMaxEnumIndex, int ExtremumMaxEnumItem, T ExtremumMaxEnumValue) GetExtremum<T>()
      where T : struct, System.Enum
    {
      var (ExtremumMinIndex, ExtremumMinItem, ExtremumMinValue, ExtremumMaxIndex, ExtremumMaxItem, ExtremumMaxValue) = System.Enum.GetValues<T>().Select(t => (int)(object)t).GetExtremum(e => e);

      return (ExtremumMinIndex, ExtremumMinItem, (T)(object)ExtremumMinValue, ExtremumMaxIndex, ExtremumMaxItem, (T)(object)ExtremumMaxValue);
    }

    /// <summary>
    /// <para>Gets the infimum and supremum of a singleton set <paramref name="source"/> of a set <typeparamref name="T"/>. I.e. the nearest ("less-than" and "greater-than") elements and indices to <paramref name="source"/> in <typeparamref name="T"/>.</para>
    /// <para>The infimum of a subset <paramref name="source"/> of a set <typeparamref name="T"/> is the greatest element in <typeparamref name="T"/> that is less than <paramref name="source"/>.</para>
    /// <para>The supremum of a subset <paramref name="source"/> of a set <typeparamref name="T"/> is the least element in <typeparamref name="T"/> that is greater than <paramref name="source"/>.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static (int InfimumEnumIndex, int InfimumEnumItem, T InfimumEnumValue, int SupremumEnumIndex, int SupremumEnumItem, T SupremumEnumValue) GetInfimumAndSupremum<T>(this T source)
      where T : struct, System.Enum
    {
      var (InfimumIndex, InfimumItem, InfimumValue, SupremumIndex, SupremumItem, SupremumValue) = System.Enum.GetValues<T>().Select(t => (int)(object)t).GetInfimumAndSupremum(e => e, (int)(object)source, true);

      return (InfimumIndex, InfimumItem, (T)(object)InfimumValue, SupremumIndex, SupremumItem, (T)(object)SupremumValue);
    }

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
