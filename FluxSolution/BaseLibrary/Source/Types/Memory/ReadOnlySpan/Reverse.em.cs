namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Creates a new array with the range [leftIndex, rightIndex] (inclusive) of elements in reverse order.</summary>
    public static T[] Reverse<T>(this System.ReadOnlySpan<T> source, int leftIndex, int rightIndex)
    {
      if (leftIndex < 0 || leftIndex >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(leftIndex));
      if (rightIndex < 0 || rightIndex >= source.Length || rightIndex < leftIndex) throw new System.ArgumentOutOfRangeException(nameof(rightIndex));

      var target = source.ToArray();

      for (; leftIndex < rightIndex; leftIndex++, rightIndex--)
      {
        var tmp = target[leftIndex];
        target[leftIndex] = target[rightIndex];
        target[rightIndex] = tmp;
      }

      return target;
    }
    /// <summary>Creates a new array with the elements in reverse order.</summary>
    public static T[] Reverse<T>(this System.ReadOnlySpan<T> source)
      => Reverse(source, 0, source.Length - 1);
  }
}
