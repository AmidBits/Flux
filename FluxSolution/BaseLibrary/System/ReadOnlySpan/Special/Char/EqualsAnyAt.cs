namespace Flux
{
  public static partial class Reflection
  {
    /// <summary>Determines whether this instance has the same value as any in the specified params array of strings when compared using the ordial comparison. The comparison starts at a specified character position.</summary>
    public static bool EqualsAnyAt(this System.ReadOnlySpan<char> source, int startAt, params string[] targets)
      => EqualsAnyAt(source, startAt, -1, System.StringComparison.Ordinal, targets.AsEnumerable());
    /// <summary>Determines whether this instance has the same value as any in the specified params array of strings when compared using the specified comparison option. The comparison starts at a specified character position.</summary>
    public static bool EqualsAnyAt(this System.ReadOnlySpan<char> source, int startAt, System.StringComparison comparisonType, params string[] targets)
      => EqualsAnyAt(source, startAt, -1, comparisonType, targets.AsEnumerable());
    /// <summary>Determines whether this instance has the same value as any in the specified params array of strings when compared using the ordial comparison. The comparison starts at a specified character position and examines a specified number of characters.</summary>
    public static bool EqualsAnyAt(this System.ReadOnlySpan<char> source, int startAt, int count, params string[] targets)
      => EqualsAnyAt(source, startAt, count, System.StringComparison.Ordinal, targets.AsEnumerable());
    /// <summary>Determines whether this instance has the same value as any in the specified params array of strings when compared using the specified comparison option. The comparison starts at a specified character position and examines a specified number of characters.</summary>
    public static bool EqualsAnyAt(this System.ReadOnlySpan<char> source, int startAt, int count, System.StringComparison comparisonType, params string[] targets)
      => EqualsAnyAt(source, startAt, count, comparisonType, targets.AsEnumerable());
    /// <summary>Determines whether this string has the same value as any in the specified sequence when compared using the specified comparison option. The comparison starts at a specified character position and examines a specified number of characters.</summary>
		// public static bool EqualsAnyAt(this string source, int startAt, int count, System.StringComparison comparisonType, System.Collections.Generic.IEnumerable<string> values) => values.Any(value => source.IndexOf(value, startAt, count > -1 ? count : System.Math.Min(source.Length - startAt, value.Length), comparisonType) == startAt);
    public static bool EqualsAnyAt(this System.ReadOnlySpan<char> source, int startAt, int count, System.StringComparison comparisonType, System.Collections.Generic.IEnumerable<string> targets)
    {
      System.ArgumentNullException.ThrowIfNull(targets);

      if (startAt < 0 || startAt >= source.Length - 1) throw new System.ArgumentOutOfRangeException(nameof(startAt));
      if (count >= 0 && startAt + count >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(count));

      foreach (var target in targets)
      {
        //if (/*count >= 0 && */target.Length >= count)
        //  continue;

        var targetIndex = count < 0 ? target.Length : count; // Value index starts at specified positive count or value length if negative.
        var sourceIndex = startAt + targetIndex;

        var counter = 0;

        while (--sourceIndex >= 0 && --targetIndex >= 0)
        {
          if (source[sourceIndex].ToString().Equals(target[targetIndex].ToString(), comparisonType))
            counter++;

          if (counter >= count)
            return true;
        }

        //return true;
      }

      return false;
    }
  }
}
