namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Reports the last index of the specified rune in the string builder, or -1 if not found. Uses the specified comparer.</summary>
    public static int LastIndexOf(this System.Text.StringBuilder source, System.Text.Rune target, System.Collections.Generic.IEqualityComparer<System.Text.Rune>? equalityComparer = null)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      equalityComparer ??= System.Collections.Generic.EqualityComparer<System.Text.Rune>.Default;

      var index = source.Length;

      foreach (var current in source.EnumerateRunesReverse())
      {
        index -= current.Utf16SequenceLength;

        if (equalityComparer.Equals(current, target))
          return index;
      }

      return -1;
    }

    /// <summary>Reports the last index of the specified char in the string builder. Or -1 if not found. Uses the specified comparer.</summary>
    public static int LastIndexOf(this System.Text.StringBuilder source, char target, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      equalityComparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

      var sourceIndex = source.Length;

      while (--sourceIndex >= 0)
        if (equalityComparer.Equals(source[sourceIndex], target))
          return sourceIndex;

      return -1;
    }

    /// <summary>Reports the last index of the specified string in the string builder. Or -1 if not found. Uses the specified comparer.</summary>
    public static int LastIndexOf(this System.Text.StringBuilder source, System.ReadOnlySpan<char> target, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      equalityComparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

      var sourceIndex = source.Length - target.Length + 1;

      while (--sourceIndex >= 0)
        if (EqualsAt(source, sourceIndex, target, equalityComparer))
          return sourceIndex;

      return -1;
    }
  }
}
