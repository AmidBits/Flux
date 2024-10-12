namespace Flux
{
  public static partial class Fx
  {
    public static int LastIndexOf(this System.Text.StringBuilder source, System.Func<char, bool> predicate)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      for (var index = source.Length - 1; index >= 0; index--)
        if (predicate(source[index]))
          return index;

      return -1;
    }

    /// <summary>Reports the last index of the specified char in the string builder. Or -1 if not found. Uses the specified comparer.</summary>
    public static int LastIndexOf(this System.Text.StringBuilder source, char target, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      equalityComparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

      for (var index = source.Length - 1; index >= 0; index--)
        if (equalityComparer.Equals(source[index], target))
          return index;

      return -1;
    }

    /// <summary>Reports the last index of the specified string in the string builder. Or -1 if not found. Uses the specified comparer.</summary>
    public static int LastIndexOf(this System.Text.StringBuilder source, System.ReadOnlySpan<char> target, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      equalityComparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

      for (var index = source.Length - 1 - target.Length; index >= 0; index--)
        if (EqualsAt(source, index, target, equalityComparer))
          return index;

      return -1;
    }
  }
}
