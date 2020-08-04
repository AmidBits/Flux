namespace Flux
{
  public static partial class XtensionsStringBuilder
  {
    /// <summary>Returns whether the specified part of the target is found at the specified index in the string.</summary>
    public static bool Equals(this System.Text.StringBuilder source, int sourceIndex, string target, int targetIndex, int length, Flux.StringComparer? comparer = null)
    {
      if (ReferenceEquals(source, target)) return true;
      else if (source is null || target is null || sourceIndex < 0 || targetIndex < 0 || length <= 0 || sourceIndex + length > source.Length || targetIndex + length > target.Length) return false;

      comparer ??= Flux.StringComparer.Ordinal;

      while (length-- > 0)
      {
        if (!comparer.Equals(source[sourceIndex++], target[targetIndex++]))
        {
          return false;
        }
      }

      return true;
    }
    /// <summary>Returns whether the specified target is found at the specified index in the string.</summary>
    public static bool Equals(this System.Text.StringBuilder source, int sourceIndex, string target, Flux.StringComparer? comparer = null) 
      => source.Equals(sourceIndex, target, 0, target.Length, comparer);
  }
}
