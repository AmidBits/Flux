namespace Flux
{
  public static partial class EnumExtensions
  {
    extension(System.Enum)
    {
      #region Extremum

      /// <summary>
      /// <para></para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <returns></returns>
      public static (T ExtremumMinElement, int ExtremumMinIndex, int ExtremumMinValue, T ExtremumMaxElement, int ExtremumMaxIndex, int ExtremumMaxValue) Extremum<T>()
        where T : struct, System.Enum
        => System.Enum.GetValues<T>().Extremum(e => (int)(object)e);

      #endregion

      #region HasAllFlags

      //public static bool HasAllFlags<T>(T value, params System.ReadOnlySpan<T> flags)
      //  where T : struct, System.Enum
      //{
      //  var required = 0UL;

      //  foreach (var flag in flags)
      //    required |= System.Convert.ToUInt64(flag);

      //  var bits = System.Convert.ToUInt64(value);

      //  return (bits & required) == required;
      //}

      public static bool HasAllFlags<T>(T value, T flags)
        where T : struct, System.Enum
        => ((ulong)(long)(object)value & (ulong)(long)(object)flags) == (ulong)(long)(object)flags;

      #endregion

      #region HasAnyFlags

      //public static bool HasAnyFlags<T>(T value, params System.ReadOnlySpan<T> flags)
      //  where T : struct, System.Enum
      //{
      //  var optional = 0UL;

      //  foreach (var flag in flags)
      //    optional |= System.Convert.ToUInt64(flag);

      //  var bits = System.Convert.ToUInt64(value);

      //  return (bits & optional) != 0UL;
      //}

      public static bool HasAnyFlags<T>(T value, T flags)
        where T : struct, System.Enum
        => ((ulong)(long)(object)value & (ulong)(long)(object)flags) != 0UL;

      #endregion

      #region InfimumSupremum

      /// <summary>
      /// <para>Gets the infimum and supremum of a singleton set <paramref name="source"/> of a set <typeparamref name="TEnum"/>. I.e. the nearest ("less-than" and "greater-than") elements and indices to <paramref name="source"/> in <typeparamref name="TEnum"/>.</para>
      /// <para>The infimum of a subset <paramref name="source"/> of a set <typeparamref name="TEnum"/> is the greatest element in <typeparamref name="TEnum"/> that is less than <paramref name="source"/>.</para>
      /// <para>The supremum of a subset <paramref name="source"/> of a set <typeparamref name="TEnum"/> is the least element in <typeparamref name="TEnum"/> that is greater than <paramref name="source"/>.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <returns></returns>
      public static (TEnum InfimumItem, int InfimumIndex, int InfimumValue, TEnum SupremumItem, int SupremumIndex, int SupremumValue) InfimumSupremum<TEnum>(TEnum source)
        where TEnum : struct, System.Enum
        => System.Enum.GetValues<TEnum>().InfimumSupremum((int)(object)source, e => (int)(object)e, true);

      #endregion
    }

    extension(System.Enum source)
    {
      #region ..GetEnumAttribute

      /// <summary>
      /// <para>Gets all <see cref="System.Enum"/> <typeparamref name="TAttribute"/> attributes of <paramref name="source"/>.</para>
      /// </summary>
      /// <typeparam name="TAttribute"></typeparam>
      /// <param name="source"></param>
      /// <returns></returns>
      public System.Collections.Generic.List<TAttribute> GetEnumAttribute<TAttribute>()
        where TAttribute : Attribute
      => [.. source.GetType().GetMember(source.ToString()).SelectMany(mi => mi.GetCustomAttributes(typeof(TAttribute), false).OfType<TAttribute>())];

      /// <summary>
      /// <para>Attempts to get all <see cref="System.Enum"/> <typeparamref name="TAttribute"/> attributes of <paramref name="source"/> and indicates whether successful.</para>
      /// </summary>
      /// <typeparam name="TAttribute"></typeparam>
      /// <param name="source"></param>
      /// <param name="attributes"></param>
      /// <returns></returns>
      public bool TryGetEnumAttribute<TAttribute>(out System.Collections.Generic.List<TAttribute> attributes)
        where TAttribute : Attribute
      {
        try { attributes = source.GetEnumAttribute<TAttribute>(); }
        catch { attributes = []; }

        return attributes is not null && attributes.Count > 0;
      }

      #endregion
    }
  }
}
