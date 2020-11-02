namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Returns whether the specified part of the target is found at the specified index in the string, using the specified comparer.</summary>
    public static bool Equals(this System.Text.StringBuilder source, int sourceIndex, string target, int targetIndex, int length, System.Collections.Generic.IEqualityComparer<char> comparer)
    {
      if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

      if (source is null || target is null || sourceIndex < 0 || targetIndex < 0 || length <= 0 || sourceIndex + length > source.Length || targetIndex + length > target.Length) return ReferenceEquals(source, target);

      while (length-- > 0)
        if (!comparer.Equals(source[sourceIndex++], target[targetIndex++]))
          return false;

      return true;
    }
    /// <summary>Returns whether the specified part of the target is found at the specified index in the string, using the default comparer.</summary>
    public static bool Equals(this System.Text.StringBuilder source, int sourceIndex, string target, int targetIndex, int length)
      => Equals(source, sourceIndex, target, targetIndex, length, System.Collections.Generic.EqualityComparer<char>.Default);

    /// <summary>Returns whether the specified target is found at the specified index in the string, using the specified comparer.</summary>
    public static bool Equals(this System.Text.StringBuilder source, int sourceIndex, string target, System.Collections.Generic.IEqualityComparer<char> comparer)
      => Equals(source, sourceIndex, target, 0, target?.Length ?? 0, comparer);

    /// <summary>Returns whether the specified target is found at the specified index in the string, using the default comparer.</summary>
    public static bool Equals(this System.Text.StringBuilder source, int sourceIndex, string target)
      => Equals(source, sourceIndex, target, 0, target?.Length ?? 0, System.Collections.Generic.EqualityComparer<char>.Default);
  }
}
