namespace Flux
{
  public static partial class XtensionsStringBuilder
  {
    /// <summary>Yields the number of characters that the source and the target have in common from the start.</summary>
    public static int CountEqualAtStart<T>(this System.Text.StringBuilder source, string target)
    {
      var index = 0;

      var minIndex = source.Length < target.Length ? source.Length : target.Length;

      while (index < minIndex && source[index] == target[index]) index++;

      return index;
    }
  }
}
