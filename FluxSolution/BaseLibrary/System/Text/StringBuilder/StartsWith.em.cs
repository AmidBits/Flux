namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Indicates whether <paramref name="source"/> starts with <paramref name="target"/>. Uses the specified <paramref name="equalityComparer"/>, or default if null.</summary>
    public static bool StartsWith(this System.Text.StringBuilder source, System.ReadOnlySpan<char> target, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      equalityComparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

      var sourceLength = source.Length;
      var targetLength = target.Length;

      if (sourceLength < targetLength)
        return false;

      for (var index = targetLength - 1; index >= 0; index--)
        if (!equalityComparer.Equals(source[index], target[index]))
          return false;

      return true;
    }
  }
}
