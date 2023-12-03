namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Reports the first index of the specified rune in the string builder, or -1 if not found. Uses the specified comparer.</summary>
    public static int IndexOf(this System.Text.StringBuilder source, System.Text.Rune target, System.Collections.Generic.IEqualityComparer<System.Text.Rune>? equalityComparer = null)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      equalityComparer ??= System.Collections.Generic.EqualityComparer<System.Text.Rune>.Default;

      var index = 0;

      foreach (var current in source.EnumerateRunes())
        if (equalityComparer.Equals(current, target))
          return index;
        else
          index += current.Utf16SequenceLength;

      return -1;
    }

    /// <summary>Reports the first index of the specified char in the string builder, or -1 if not found. Uses the specified comparer.</summary>
    public static int IndexOf(this System.Text.StringBuilder source, char target, System.Collections.Generic.IEqualityComparer<char>? comparer = null)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      comparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

      var sourceLength = source.Length;

      for (var index = 0; index < sourceLength; index++)
        if (comparer.Equals(source[index], target))
          return index;

      return -1;
    }

    /// <summary>Returns the first index of the specified string in the string builder, or -1 if not found. Uses the specified comparer.</summary>
    public static int IndexOf(this System.Text.StringBuilder source, System.ReadOnlySpan<char> target, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      equalityComparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

      var targetLength = target.Length;

      for (int index = 0, lastIndex = source.Length - targetLength; index <= lastIndex; index++)
        if (EqualsAt(source, index, target, 0, targetLength, equalityComparer))
          return index;

      return -1;
    }
  }
}
