namespace Flux
{
  public static partial class ExtensionMethodsStringBuilder
  {
    /// <summary>Reports the last index of the specified rune in the string builder, or -1 if not found. Uses the specified comparer.</summary>
    public static int LastIndexOf(this System.Text.StringBuilder source, System.Text.Rune target, System.Collections.Generic.IEqualityComparer<System.Text.Rune> equalityComparer)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (equalityComparer is null) throw new System.ArgumentNullException(nameof(equalityComparer));

      var index = source.Length;

      foreach (var current in source.EnumerateRunesReverse())
      {
        index -= current.Utf16SequenceLength;

        if (equalityComparer.Equals(current, target))
          return index;
      }

      return -1;
    }
    /// <summary>Reports the last index of the specified rune in the string builder, or -1 if not found. Uses the default comparer.</summary>
    public static int LastIndexOf(this System.Text.StringBuilder source, System.Text.Rune target)
      => LastIndexOf(source, target, System.Collections.Generic.EqualityComparer<System.Text.Rune>.Default);

    /// <summary>Reports the last index of the specified char in the string builder. Or -1 if not found. Uses the specified comparer.</summary>
    public static int LastIndexOf(this System.Text.StringBuilder source, char target, System.Collections.Generic.IEqualityComparer<char> comparer)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

      for (var index = source.Length - 1; index >= 0; index--)
        if (comparer.Equals(source[index], target))
          return index;

      return -1;
    }
    /// <summary>Reports the last index of the specified char in the string builder. Or -1 if not found. Uses the default comparer.</summary>
    public static int LastIndexOf(this System.Text.StringBuilder source, char target)
      => LastIndexOf(source, target, System.Collections.Generic.EqualityComparer<char>.Default);

    /// <summary>Reports the last index of the specified string in the string builder. Or -1 if not found. Uses the specified comparer.</summary>
    public static int LastIndexOf(this System.Text.StringBuilder source, System.ReadOnlySpan<char> target, System.Collections.Generic.IEqualityComparer<char> equalityComparer)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (equalityComparer is null) throw new System.ArgumentNullException(nameof(equalityComparer));

      for (var index = source.Length - target.Length; index >= 0; index--)
        if (EqualsAt(source, index, target, equalityComparer))
          return index;

      return -1;
    }
    /// <summary>Reports the last index of the specified string in the string builder. Or -1 if not found. Uses the default comparer.</summary>
    public static int LastIndexOf(this System.Text.StringBuilder source, System.ReadOnlySpan<char> target)
      => LastIndexOf(source, target, System.Collections.Generic.EqualityComparer<char>.Default);
  }
}