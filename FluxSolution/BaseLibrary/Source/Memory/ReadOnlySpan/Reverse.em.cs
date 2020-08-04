namespace Flux
{
  public static partial class XtensionsReadOnlySpan
  {
    /// <summary>Creates a new span with the elements in reverse order.</summary>
    public static T[] Reverse<T>(this System.ReadOnlySpan<T> source)
    {
      var target = new T[source.Length];

      for (int targetIndex = 0, sourceIndex = source.Length - 1; sourceIndex >= 0; targetIndex++, sourceIndex--)
        target[targetIndex] = source[sourceIndex];

      return target;
    }
  }
}
