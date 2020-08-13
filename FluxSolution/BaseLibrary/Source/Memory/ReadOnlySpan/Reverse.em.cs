namespace Flux
{
  public static partial class XtensionsReadOnlySpan
  {
    /// <summary>Creates a new array with the elements in reverse order.</summary>
    public static T[] Reverse<T>(this System.ReadOnlySpan<T> source)
    {
      var target = source.ToArray();

      for (int indexL = 0, indexR = target.Length - 1; indexL < indexR; indexL++, indexR--)
      {
        var tmp = target[indexL];
        target[indexL] = target[indexR];
        target[indexR] = tmp;
      }

      return target;
    }
  }
}
