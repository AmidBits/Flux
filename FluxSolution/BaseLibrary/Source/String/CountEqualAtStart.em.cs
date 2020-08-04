namespace Flux
{
  public static partial class XtensionsString
  {
    /// <summary>Determines whether the beginning of the first sequence is equivalent to the second sequence, using the specified element equality comparer.</summary>
    public static int CountEqualAtStart<T>(this string source, string target)
    {
      var index = 0;

      var minIndex = source.Length < target.Length ? source.Length : target.Length;

      while (index < minIndex && source[index] == target[index]) index++;

      return index;
    }
  }
}
