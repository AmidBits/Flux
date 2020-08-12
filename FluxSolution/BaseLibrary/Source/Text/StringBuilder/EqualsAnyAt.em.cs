using System.Linq;

namespace Flux
{
  public static partial class XtensionsStringBuilder
  {
    /// <summary>Determines whether this instance has the same value as any in the specified params array of strings when compared using the ordial comparison. The comparison starts at a specified character position.</summary>
    public static bool EqualsAnyAt(this System.Text.StringBuilder source, int startAt, params string[] values)
      => EqualsAnyAt(source, startAt, -1, System.Collections.Generic.EqualityComparer<char>.Default, values.AsEnumerable());
    /// <summary>Determines whether this instance has the same value as any in the specified params array of strings when compared using the specified comparison option. The comparison starts at a specified character position.</summary>
    public static bool EqualsAnyAt(this System.Text.StringBuilder source, int startAt, System.Collections.Generic.IEqualityComparer<char> comparer, params string[] values)
      => EqualsAnyAt(source, startAt, -1, comparer, values.AsEnumerable());
    /// <summary>Determines whether this instance has the same value as any in the specified params array of strings when compared using the ordial comparison. The comparison starts at a specified character position and examines a specified number of characters.</summary>
    public static bool EqualsAnyAt(this System.Text.StringBuilder source, int startAt, int count, params string[] values)
      => EqualsAnyAt(source, startAt, count, System.Collections.Generic.EqualityComparer<char>.Default, values.AsEnumerable());
    /// <summary>Determines whether this instance has the same value as any in the specified params array of strings when compared using the specified comparison option. The comparison starts at a specified character position and examines a specified number of characters.</summary>
    public static bool EqualsAnyAt(this System.Text.StringBuilder source, int startAt, int count, System.Collections.Generic.IEqualityComparer<char> comparer, params string[] values)
      => EqualsAnyAt(source, startAt, count, comparer, values.AsEnumerable());
    /// <summary>Determines whether this string has the same value as any in the specified sequence when compared using the specified comparison option. The comparison starts at a specified character position and examines a specified number of characters.</summary>
		// public static bool EqualsAnyAt(this string source, int startAt, int count, System.StringComparison comparisonType, System.Collections.Generic.IEnumerable<string> values) => values.Any(value => source.IndexOf(value, startAt, count > -1 ? count : System.Math.Min(source.Length - startAt, value.Length), comparisonType) == startAt);
    public static bool EqualsAnyAt(this System.Text.StringBuilder source, int startAt, int count, System.Collections.Generic.IEqualityComparer<char> comparer, System.Collections.Generic.IEnumerable<string> values)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      comparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

      if (startAt < 0 || startAt >= source.Length - 1) throw new System.ArgumentOutOfRangeException(nameof(startAt));
      else if (count <= 0 || startAt + count >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(count));

      foreach (var value in values ?? throw new System.ArgumentNullException(nameof(values)))
      {
        if (value.Length >= count) continue;

        var sourceIndex = startAt + count;
        var valueIndex = count;

        while (--sourceIndex >= 0 && --valueIndex >= 0)
          if (!comparer.Equals(source[sourceIndex], value[valueIndex]))
            break;

        if (sourceIndex < 0 && valueIndex < 0) return true;
      }

      return false;
    }
  }
}
