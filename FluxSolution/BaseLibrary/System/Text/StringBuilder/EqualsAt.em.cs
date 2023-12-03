namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Returns whether the specified part of the target is found at the specified index in the string, using the specified comparer.</summary>
    public static bool EqualsAt(this System.Text.StringBuilder source, int sourceIndex, System.ReadOnlySpan<char> target, int targetIndex, int length, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      equalityComparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

      if (sourceIndex < 0 || targetIndex < 0 || length <= 0 || sourceIndex + length > source.Length || targetIndex + length > target.Length)
        return false;

      while (length-- > 0)
        if (!equalityComparer.Equals(source[sourceIndex++], target[targetIndex++]))
          return false;

      return true;
    }

    /// <summary>Returns whether the specified target is found at the specified index in the string, using the specified comparer.</summary>
    public static bool EqualsAt(this System.Text.StringBuilder source, int sourceIndex, System.ReadOnlySpan<char> target, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
      => EqualsAt(source, sourceIndex, target, 0, target.Length, equalityComparer);
  }
}
