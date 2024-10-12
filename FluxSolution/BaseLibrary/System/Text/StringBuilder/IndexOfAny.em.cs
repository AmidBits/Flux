namespace Flux
{
  public static partial class Fx
  {
    public static int IndexOfAny(this System.Text.StringBuilder source, params System.Func<char, bool>[] predicates)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var sourceLength = source.Length;
      var predicatesLength = predicates.Length;

      for (var sourceIndex = 0; sourceIndex < sourceLength; sourceIndex++)
      {
        var sourceChar = source[sourceIndex];

        for (var predicatesIndex = 0; predicatesIndex < predicatesLength; predicatesIndex++) // Favor targets in order.
          if (predicates[predicatesIndex](sourceChar))
            return sourceIndex;
      }

      return -1;
    }

    /// <summary>Returns the first index of any of the specified characters within the string builder, or -1 if none were found. Uses the specified comparer.</summary>
    public static int IndexOfAny(this System.Text.StringBuilder source, System.Collections.Generic.IEqualityComparer<char>? equalityComparer, params char[] values)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      equalityComparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

      var sourceLength = source.Length;
      var valuesLength = values.Length;

      for (var sourceIndex = 0; sourceIndex < sourceLength; sourceIndex++)
      {
        var sourceChar = source[sourceIndex];

        for (var targetsIndex = 0; targetsIndex < valuesLength; targetsIndex++) // Favor targets in order.
          if (equalityComparer.Equals(values[targetsIndex], sourceChar))
            return sourceIndex;
      }

      return -1;
    }

    /// <summary>Reports the first index of any of the specified strings within the string builder, or -1 if none were found. Uses the specified comparer.</summary>
    public static int IndexOfAny(this System.Text.StringBuilder source, System.Collections.Generic.IEqualityComparer<char>? equalityComparer, params string[] values)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      equalityComparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

      var sourceLength = source.Length;
      var valuesLength = values.Length;

      for (var sourceIndex = 0; sourceIndex < sourceLength; sourceIndex++)
        for (var valuesIndex = 0; valuesIndex < valuesLength; valuesIndex++) // Favor targets in order.
        {
          var value = values[valuesIndex];

          if (EqualsAt(source, sourceIndex, value, 0, value.Length, equalityComparer))
            return sourceIndex;
        }

      return -1;
    }
  }
}
