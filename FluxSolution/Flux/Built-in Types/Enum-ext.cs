namespace Flux
{
  public static partial class EnumExtensions
  {
    #region Extremum

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static (T ExtremumMinItem, int ExtremumMinIndex, int ExtremumMinValue, T ExtremumMaxEnumItem, int ExtremumMaxIndex, int ExtremumMaxValue) Extremum<T>()
      where T : struct, System.Enum
    {
      var (ExtremumMinItem, ExtremumMinIndex, ExtremumMinValue, ExtremumMaxItem, ExtremumMaxIndex, ExtremumMaxValue) = IEnumerableExtensions.Extremum(System.Enum.GetValues<T>(), e => (int)(object)e);

      return ((T)(object)ExtremumMinItem, ExtremumMinIndex, ExtremumMinValue, (T)(object)ExtremumMaxItem, ExtremumMaxIndex, ExtremumMaxValue);
    }

    #endregion

    #region GetEnumAttribute

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

    #endregion

    #region InfimumSupremum

    /// <summary>
    /// <para>Gets the infimum and supremum of a singleton set <paramref name="source"/> of a set <typeparamref name="T"/>. I.e. the nearest ("less-than" and "greater-than") elements and indices to <paramref name="source"/> in <typeparamref name="T"/>.</para>
    /// <para>The infimum of a subset <paramref name="source"/> of a set <typeparamref name="T"/> is the greatest element in <typeparamref name="T"/> that is less than <paramref name="source"/>.</para>
    /// <para>The supremum of a subset <paramref name="source"/> of a set <typeparamref name="T"/> is the least element in <typeparamref name="T"/> that is greater than <paramref name="source"/>.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static (T InfimumItem, int InfimumIndex, int InfimumValue, T SupremumItem, int SupremumIndex, int SupremumValue) InfimumSupremum<T>(this T source)
      where T : struct, System.Enum
    {
      var (InfimumItem, InfimumIndex, InfimumValue, SupremumItem, SupremumIndex, SupremumValue) = System.Enum.GetValues<T>().InfimumSupremum(e => (int)(object)e, (int)(object)source, true);

      return ((T)(object)InfimumItem, InfimumIndex, InfimumValue, (T)(object)SupremumItem, SupremumIndex, SupremumValue);
    }

    #endregion
  }
}
