namespace Flux
{
  public static partial class Fx
  {
    public static int IndexOf(this System.Text.StringBuilder source, System.Func<char, bool> predicate)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var sourceLength = source.Length;

      for (var index = 0; index < sourceLength; index++)
        if (predicate(source[index]))
          return index;

      return -1;
    }

    /// <summary>Reports the first index of the specified char in the string builder, or -1 if not found. Uses the specified <paramref name="equalityComparer"/>.</summary>
    public static int IndexOf(this System.Text.StringBuilder source, char value, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      equalityComparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

      var sourceLength = source.Length;

      for (var index = 0; index < sourceLength; index++)
        if (equalityComparer.Equals(source[index], value))
          return index;

      return -1;
    }

    /// <summary>Returns the first index of the specified string in the string builder, or -1 if not found. Uses the specified <paramref name="equalityComparer"/>.</summary>
    public static int IndexOf(this System.Text.StringBuilder source, System.ReadOnlySpan<char> value, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      equalityComparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

      var maxIndex = source.Length - value.Length;

      for (var index = 0; index <= maxIndex; index++)
        if (EqualsAt(source, index, value, 0, value.Length, equalityComparer))
          return index;

      return -1;
    }
  }
}
