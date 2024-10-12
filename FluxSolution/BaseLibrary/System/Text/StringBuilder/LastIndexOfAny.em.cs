namespace Flux
{
  public static partial class Fx
  {
    public static int LastIndexOfAny(this System.Text.StringBuilder source, params System.Func<char, bool>[] predicates)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var predicatesLength = predicates.Length;

      for (var sourceIndex = source.Length - 1; sourceIndex >= 0; sourceIndex--)
      {
        var sourceChar = source[sourceIndex];

        for (var predicatesIndex = 0; predicatesIndex < predicatesLength; predicatesIndex++) // Favor targets in order.
          if (predicates[predicatesIndex](sourceChar))
            return sourceIndex;
      }

      return -1;
    }

    /// <summary>Returns the last index of any of the specified characters. Or -1 if none were found.</summary>
    public static int LastIndexOfAny(this System.Text.StringBuilder source, System.Collections.Generic.IEqualityComparer<char>? equalityComparer, params char[] values)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      equalityComparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

      var valuesLength = values.Length;

      for (var sourceIndex = source.Length - 1; sourceIndex >= 0; sourceIndex--)
      {
        var sourceChar = source[sourceIndex];

        for (var valuesIndex = 0; valuesIndex < valuesLength; valuesIndex++) // Favor targets in order.
          if (equalityComparer.Equals(values[valuesIndex], sourceChar))
            return sourceIndex;
      }

      return -1;
    }

    /// <summary>Returns the last index of any of the specified values. or -1 if none is found.</summary>
    public static int LastIndexOfAny(this System.Text.StringBuilder source, System.Collections.Generic.IEqualityComparer<char>? equalityComparer, params string[] values)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      equalityComparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

      var valuesLength = values.Length;

      for (var sourceIndex = source.Length - 1; sourceIndex >= 0; sourceIndex--)
        for (var valuesIndex = 0; valuesIndex < valuesLength; valuesIndex++) // Favor targets in sorder.
        {
          var value = values[valuesIndex];

          if (source.EqualsAt(sourceIndex, value, 0, value.Length, equalityComparer))
            return sourceIndex;
        }

      return -1;
    }
  }
}
