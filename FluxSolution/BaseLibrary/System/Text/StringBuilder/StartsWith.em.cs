namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Indicates whether <paramref name="source"/> starts with <paramref name="target"/>. Uses the specified <paramref name="equalityComparer"/>, or default if null.</summary>
    public static bool StartsWith(this System.Text.StringBuilder source, System.ReadOnlySpan<char> target, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      equalityComparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

      var targetIndex = target.Length;

      if (source.Length < targetIndex)
        return false;

      while (--targetIndex >= 0)
        if (!equalityComparer.Equals(source[targetIndex], target[targetIndex]))
          return false;

      return true;
    }
  }
}
