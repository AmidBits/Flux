namespace Flux
{
  public static partial class XtensionsStringBuilder
  {
    /// <summary>Yields the number of characters that the source and the target have in common at the end.</summary>
    public static int CountEqualAtEnd<T>(this System.Text.StringBuilder source, string target)
    {
      var sourceIndex = source.Length;
      var targetIndex = target.Length;

      var minLength = sourceIndex < targetIndex ? sourceIndex : targetIndex;

      for (var atEnd = 0; --sourceIndex >= 0 && --targetIndex >= 0; atEnd++)
        if (source[sourceIndex] != target[targetIndex])
          return atEnd;

      return minLength;
    }
  }
}
