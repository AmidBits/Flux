using System.Linq;

namespace Flux
{
  public static partial class StringEm
  {
    /// <summary>Reports the zero-based index of the last occurrence in this instance of any substring in a specified array of strings.</summary>
    public static int LastIndexOfAny(this string source, params string[] values)
      => LastIndexOfAny(source, 0, -1, System.StringComparison.Ordinal, values.AsEnumerable());
    /// <summary>Reports the zero-based index of the last occurrence in this instance of any substring in a specified array of strings when compared using the specified comparison option.</summary>
    public static int LastIndexOfAny(this string source, System.StringComparison comparisonType, params string[] values)
      => LastIndexOfAny(source, 0, -1, comparisonType, values.AsEnumerable());
    /// <summary>Reports the zero-based index of the last occurrence in this instance of any substring in a specified array of strings. The search starts at a specified character position.</summary>
    public static int LastIndexOfAny(this string source, int startIndex, params string[] values)
      => LastIndexOfAny(source, startIndex, -1, System.StringComparison.Ordinal, values.AsEnumerable());
    /// <summary>Reports the zero-based index of the last occurrence in this instance of any substring in a specified array of strings when compared using the specified comparison option. The search starts at a specified character position.</summary>
    public static int LastIndexOfAny(this string source, int startIndex, System.StringComparison comparisonType, params string[] values)
      => LastIndexOfAny(source, startIndex, -1, comparisonType, values.AsEnumerable());
    /// <summary>Reports the zero-based index of the last occurrence in this instance of any substring in a specified array of strings. The search starts at a specified character position and examines a specified number of character positions.</summary>
    public static int LastIndexOfAny(this string source, int startIndex, int count, params string[] values)
      => LastIndexOfAny(source, startIndex, count, System.StringComparison.Ordinal, values.AsEnumerable());
    /// <summary>Reports the zero-based index of the last occurrence in this instance of any substring in a specified array of strings when compared using the specified comparison option. The search starts at a specified character position and examines a specified number of character positions.</summary>
    public static int LastIndexOfAny(this string source, int startIndex, int count, System.StringComparison comparisonType, params string[] values)
      => LastIndexOfAny(source, startIndex, count, comparisonType, values.AsEnumerable());
    /// <summary>Reports the zero-based index of the last occurrence in this instance of any substring in a specified sequence when compared using the specified comparison option. The search starts at a specified character position and examines a specified number of character positions.</summary>
    public static int LastIndexOfAny(this string source, int startIndex, int count, System.StringComparison comparisonType, System.Collections.Generic.IEnumerable<string> values)
    {
      var sourceLengthAdjusted = (source ?? throw new System.ArgumentNullException(nameof(source))).Length - startIndex; // Adjusted with offset from startIndex, in case startIndex is too close to the end of the string.

      foreach (var value in values ?? throw new System.ArgumentNullException(nameof(values)))
        if (source.LastIndexOf(value, startIndex, count > -1 ? count : System.Math.Min(sourceLengthAdjusted, value.Length), comparisonType) is var index && index > -1)
          return index;

      return -1;
    }
  }
}
