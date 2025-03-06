//namespace Flux
//{
//  public static partial class Fx
//  {
//    /// <summary>
//    /// <para>Returns whether <paramref name="maxLength"/> (or the actual length if less) of any <paramref name="values"/> are found at the <paramref name="sourceIndex"/> in the <paramref name="source"/>.</para>
//    /// <para>Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
//    /// </summary>
//    /// <param name="source"></param>
//    /// <param name="sourceIndex"></param>
//    /// <param name="maxLength"></param>
//    /// <param name="equalityComparer"></param>
//    /// <param name="values"></param>
//    /// <returns></returns>
//    public static bool EqualsAnyAt(this System.ReadOnlySpan<char> source, int sourceIndex, int maxLength, System.Collections.Generic.IEqualityComparer<char>? equalityComparer, params string[] values)
//    {
//      for (var valuesIndex = 0; valuesIndex < values.Length; valuesIndex++)
//        if (values[valuesIndex] is var value && source[sourceIndex..].IsCommonPrefix(value, maxLength, equalityComparer))
//          return true;

//      return false;
//    }

//    /// <summary>
//    /// <para>Returns whether any <paramref name="values"/> are found at the <paramref name="sourceIndex"/> in the <paramref name="source"/>.</para>
//    /// <para>Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
//    /// </summary>
//    /// <param name="source"></param>
//    /// <param name="sourceIndex"></param>
//    /// <param name="equalityComparer"></param>
//    /// <param name="values"></param>
//    /// <returns></returns>
//    public static bool EqualsAnyAt(this System.ReadOnlySpan<char> source, int sourceIndex, System.Collections.Generic.IEqualityComparer<char>? equalityComparer, params string[] values)
//      => source.EqualsAnyAt(sourceIndex, int.MaxValue, equalityComparer, values);


//    /// <summary>Determines whether this instance has the same value as any in the specified params array of strings when compared using the ordial comparison. The comparison starts at a specified character position.</summary>
//    public static bool EqualsAnyAt(this System.ReadOnlySpan<char> source, int startAt, params string[] targets)
//      => EqualsAnyAt(source, startAt, -1, System.StringComparison.Ordinal, targets.AsEnumerable());
//    /// <summary>Determines whether this instance has the same value as any in the specified params array of strings when compared using the specified comparison option. The comparison starts at a specified character position.</summary>
//    public static bool EqualsAnyAt(this System.ReadOnlySpan<char> source, int startAt, System.StringComparison comparisonType, params string[] targets)
//      => EqualsAnyAt(source, startAt, -1, comparisonType, targets.AsEnumerable());
//    /// <summary>Determines whether this instance has the same value as any in the specified params array of strings when compared using the ordial comparison. The comparison starts at a specified character position and examines a specified number of characters.</summary>
//    public static bool EqualsAnyAt(this System.ReadOnlySpan<char> source, int startAt, int count, params string[] targets)
//      => EqualsAnyAt(source, startAt, count, System.StringComparison.Ordinal, targets.AsEnumerable());
//    /// <summary>Determines whether this instance has the same value as any in the specified params array of strings when compared using the specified comparison option. The comparison starts at a specified character position and examines a specified number of characters.</summary>
//    public static bool EqualsAnyAt(this System.ReadOnlySpan<char> source, int startAt, int count, System.StringComparison comparisonType, params string[] targets)
//      => EqualsAnyAt(source, startAt, count, comparisonType, targets.AsEnumerable());
//    /// <summary>Determines whether this string has the same value as any in the specified sequence when compared using the specified comparison option. The comparison starts at a specified character position and examines a specified number of characters.</summary>
//		// public static bool EqualsAnyAt(this string source, int startAt, int count, System.StringComparison comparisonType, System.Collections.Generic.IEnumerable<string> values) => values.Any(value => source.IndexOf(value, startAt, count > -1 ? count : System.Math.Min(source.Length - startAt, value.Length), comparisonType) == startAt);
//    public static bool EqualsAnyAt(this System.ReadOnlySpan<char> source, int startAt, int count, System.StringComparison comparisonType, System.Collections.Generic.IEnumerable<string> targets)
//    {
//      if (startAt < 0 || startAt >= source.Length - 1) throw new System.ArgumentOutOfRangeException(nameof(startAt));
//      if (count >= 0 && startAt + count >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(count));

//      System.ArgumentNullException.ThrowIfNull(targets);

//      foreach (var target in targets)
//      {
//        //if (/*count >= 0 && */target.Length >= count)
//        //  continue;

//        var targetIndex = count < 0 ? target.Length : count; // Value index starts at specified positive count or value length if negative.
//        var sourceIndex = startAt + targetIndex;

//        var counter = 0;

//        while (--sourceIndex >= 0 && --targetIndex >= 0)
//        {
//          if (source[sourceIndex].ToString().Equals(target[targetIndex].ToString(), comparisonType))
//            counter++;

//          if (counter >= count)
//            return true;
//        }

//        //return true;
//      }

//      return false;
//    }
//  }
//}
