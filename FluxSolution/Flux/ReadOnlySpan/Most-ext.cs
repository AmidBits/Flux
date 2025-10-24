namespace Flux
{
  public static partial class XtensionReadOnlySpan
  {
    extension<T>(System.ReadOnlySpan<T> source)
    {
      /// <summary>
      /// <para>Slice a span, keeping the left-most <paramref name="count"/> of elements if available, otherwise as many as there are.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="count"></param>
      /// <returns></returns>
      public System.ReadOnlySpan<T> LeftMost(int count)
        => source[..int.Min(source.Length, count)];

      /// <summary>
      /// <para>Slice a span, keeping the right-most <paramref name="count"/> of elements if available, otherwise as many as there are.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="count"></param>
      /// <returns></returns>
      public System.ReadOnlySpan<T> RightMost(int count)
        => source[int.Max(0, source.Length - count)..];
    }
  }
}
