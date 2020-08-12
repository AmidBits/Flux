using System.Linq;

namespace Flux
{
  public static partial class XtensionsString
  {
    /// <summary>Determines whether this instance has the same value as any in the specified params array of strings when compared using the ordial comparison. The comparison starts at a specified character position.</summary>
    public static bool EqualsAnyAt(this string source, int startAt, params string[] values)
      => EqualsAnyAt(source, startAt, -1, System.StringComparison.Ordinal, values.AsEnumerable());
    /// <summary>Determines whether this instance has the same value as any in the specified params array of strings when compared using the specified comparison option. The comparison starts at a specified character position.</summary>
    public static bool EqualsAnyAt(this string source, int startAt, System.StringComparison comparisonType, params string[] values)
      => EqualsAnyAt(source, startAt, -1, comparisonType, values.AsEnumerable());
    /// <summary>Determines whether this instance has the same value as any in the specified params array of strings when compared using the ordial comparison. The comparison starts at a specified character position and examines a specified number of characters.</summary>
    public static bool EqualsAnyAt(this string source, int startAt, int count, params string[] values)
      => EqualsAnyAt(source, startAt, count, System.StringComparison.Ordinal, values.AsEnumerable());
    /// <summary>Determines whether this instance has the same value as any in the specified params array of strings when compared using the specified comparison option. The comparison starts at a specified character position and examines a specified number of characters.</summary>
    public static bool EqualsAnyAt(this string source, int startAt, int count, System.StringComparison comparisonType, params string[] values)
      => EqualsAnyAt(source, startAt, count, comparisonType, values.AsEnumerable());
    /// <summary>Determines whether this string has the same value as any in the specified sequence when compared using the specified comparison option. The comparison starts at a specified character position and examines a specified number of characters.</summary>
		// public static bool EqualsAnyAt(this string source, int startAt, int count, System.StringComparison comparisonType, System.Collections.Generic.IEnumerable<string> values) => values.Any(value => source.IndexOf(value, startAt, count > -1 ? count : System.Math.Min(source.Length - startAt, value.Length), comparisonType) == startAt);
    public static bool EqualsAnyAt(this string source, int startAt, int count, System.StringComparison comparisonType, System.Collections.Generic.IEnumerable<string> values)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      else if (startAt < 0 || startAt >= source.Length - 1) throw new System.ArgumentOutOfRangeException(nameof(startAt));
      else if (count >= 0 && startAt + count >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(count));

      foreach (var value in values)
      {
        if (count >= 0 && value.Length >= count) continue;

        var valueIndex = count < 0 ? value.Length : count; // Value index starts at specified positive count or value length if negative.
        var sourceIndex = startAt + valueIndex;

        while (--sourceIndex >= 0 && --valueIndex >= 0)
          if (!source[sourceIndex].ToString().Equals(value[valueIndex].ToString(), comparisonType))
            continue;

        return true;
      }

      return false;
    }
  }
}
