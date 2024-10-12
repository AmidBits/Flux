namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Indicates whether the <paramref name="source"/> starts with <paramref name="count"/> elements satisfying the <paramref name="predicate"/>.</summary>
    public static bool StartsWith(this System.Text.StringBuilder source, int count, System.Func<char, bool> predicate)
      => source.StartMatchLength(predicate) == count;

    /// <summary>Indicates whether the <paramref name="source"/> starts with <paramref name="count"/> elements equal to <paramref name="target"/>. Uses the specified <paramref name="equalityComparer"/>, or default if null.</summary>
    public static bool StartsWith(this System.Text.StringBuilder source, int count, char target, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
      => source.StartMatchLength(target, equalityComparer) == count;

    /// <summary>Indicates whether the <paramref name="source"/> starts with <paramref name="target"/>. Uses the specified <paramref name="equalityComparer"/>, or default if null.</summary>
    public static bool StartsWith(this System.Text.StringBuilder source, System.ReadOnlySpan<char> target, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
      => source.StartMatchLength(target, equalityComparer) == target.Length;
  }
}
