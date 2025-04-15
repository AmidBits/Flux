namespace Flux
{
  public static partial class Enums
  {
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

  }
}
