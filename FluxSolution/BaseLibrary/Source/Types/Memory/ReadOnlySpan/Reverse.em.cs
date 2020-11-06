namespace Flux
{
  public static partial class Xtensions
  {

    /// <summary>Creates a new array with the range [leftIndex, rightIndex] (inclusive) of elements in reverse order.</summary>
    public static T[] Reverse<T>(this System.ReadOnlySpan<T> source, int minIndex, int maxIndex)
    {
      var target = source.ToArray();
      Reverse<T>(target, minIndex, maxIndex);
      return target;
    }
    /// <summary>Creates a new array with the elements in reverse order.</summary>
    public static T[] Reverse<T>(this System.ReadOnlySpan<T> source)
    {
      var target = source.ToArray();
      Reverse<T>(target, 0, source.Length - 1);
      return target;
    }
  }
}
