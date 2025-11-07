//namespace Flux
//{
//  public static partial class IEnumerables
//  {
//    #region GetInfimumAndSupremum

//    /// <summary>
//    /// <para>Gets the nearest ("less-than" and "greater-than", optionally with "-or-equal") elements and indices to the singleton set {<paramref name="referenceValue"/>}, as evaluated by the <paramref name="valueSelector"/>, in <paramref name="source"/>. Uses the specified <paramref name="comparer"/> (default if null).</para>
//    /// <para>The infimum of a (singleton in this case) subset <paramref name="referenceValue"/> of a set <paramref name="source"/> is the greatest element in <paramref name="source"/> that is less-than-or-equal <paramref name="referenceValue"/>. If <paramref name="proper"/> is <see langword="true"/> then infimum will never be equal.</para>
//    /// <para>The supremum of a (singleton in this case) subset <paramref name="referenceValue"/> of a set <paramref name="source"/> is the least element in <paramref name="source"/> that is greater-than-or-equal <paramref name="referenceValue"/>. If <paramref name="proper"/> is <see langword="true"/> then supremum will never be equal.</para>
//    /// <para><see href="https://en.wikipedia.org/wiki/Infimum_and_supremum"/></para>
//    /// </summary>
//    /// <typeparam name="TSource"></typeparam>
//    /// <typeparam name="TValue"></typeparam>
//    /// <param name="source">This is the set P.</param>
//    /// <param name="valueSelector"></param>
//    /// <param name="referenceValue">This is the subset S.</param>
//    /// <param name="proper">If <paramref name="proper"/> is <see langword="true"/> then infimum and supremum will never be equal, otherwise it may be equal.</param>
//    /// <param name="comparer">Uses the specified comparer, or default if null.</param>
//    /// <returns></returns>
//    /// <exception cref="System.ArgumentNullException"/>
//    public static (int InfimumIndex, TSource? InfimumItem, TValue? InfimumValue, int SupremumIndex, TSource? SupremumItem, TValue? SupremumValue) GetInfimumAndSupremum<TSource, TValue>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, TValue> valueSelector, TValue referenceValue, bool proper, System.Collections.Generic.IComparer<TValue>? comparer = null)
//    {
//      System.ArgumentNullException.ThrowIfNull(source);
//      System.ArgumentNullException.ThrowIfNull(valueSelector);

//      comparer ??= System.Collections.Generic.Comparer<TValue>.Default;

//      var infIndex = -1;
//      var infItem = default(TSource);
//      var infValue = referenceValue;

//      var supIndex = -1;
//      var supItem = default(TSource);
//      var supValue = referenceValue;

//      var index = 0;

//      foreach (var item in source)
//      {
//        var value = valueSelector(item);

//        var cmp = comparer.Compare(value, referenceValue);

//        if ((!proper ? cmp <= 0 : cmp < 0) && (infIndex < 0 || comparer.Compare(value, infValue) > 0))
//        {
//          infIndex = index;
//          infItem = item;
//          infValue = value;
//        }

//        if ((!proper ? cmp >= 0 : cmp > 0) && (supIndex < 0 || comparer.Compare(value, supValue) < 0))
//        {
//          supIndex = index;
//          supItem = item;
//          supValue = value;
//        }

//        index++;
//      }

//      return (infIndex, infItem, infValue, supIndex, supItem, supValue);
//    }

//    #endregion
//  }
//}
