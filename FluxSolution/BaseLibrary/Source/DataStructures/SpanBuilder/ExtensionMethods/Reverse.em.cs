//namespace Flux
//{
//  public static partial class Fx
//  {
//    /// <summary>Reverse all ranged elements in-place.</summary>
//    public static Flux.SpanMaker<T> Reverse<T>(this Flux.SpanMaker<T> source, int startIndex, int endIndex)
//    {
//      if (startIndex < 0 || startIndex >= source.Count) throw new System.ArgumentOutOfRangeException(nameof(startIndex));
//      if (endIndex < startIndex || endIndex >= source.Count) throw new System.ArgumentOutOfRangeException(nameof(endIndex));

//      for (; startIndex < endIndex; startIndex++, endIndex--)
//        source.Swap(startIndex, endIndex);

//      return source;
//    }
//    /// <summary>Reverse all elements in-place.</summary>
//    public static Flux.SpanMaker<T> Reverse<T>(this Flux.SpanMaker<T> source)
//      => source.Reverse(0, source.Count - 1);
//  }
//}
