namespace Flux
{
  public static partial class XtensionsString
  {
    public static int CountEqualAtEnd<T>(this string source, string target)
    {
      var sourceIndex = source.Length;
      var targetIndex = target.Length;

      var minLength = sourceIndex < targetIndex ? sourceIndex : targetIndex;

      for (var atEnd = 0; --sourceIndex >= 0 && --targetIndex >= 0; atEnd++)
        if (!source[sourceIndex].Equals(target[targetIndex]))
          return atEnd;

      return minLength;
    }
  }
}
