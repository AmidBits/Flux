namespace Flux
{
  public static partial class SpanBuilderExtensionMethods
  {
    /// <summary>Pad evenly on both sides to the specified width by the specified <paramref name="paddingLeft"/> and <paramref name="paddingRight"/> respectively.</summary>
    public static SpanBuilder<T> PadEven<T>(this SpanBuilder<T> source, int totalWidth, T paddingLeft, T paddingRight, bool leftBias = true)
    {
      if (totalWidth > source.Count)
      {
        var quotient = System.Math.DivRem(totalWidth - source.Count, 2, out var remainder);

        PadLeft(source, source.Count + (leftBias && remainder > 0 ? quotient + 1 : quotient), paddingLeft);
        // The two lines below are the original right biased (always) which works great.
        //PadLeft(Length + (totalWidth - Length) / 2, paddingLeft);
        PadRight(source, totalWidth, paddingRight);
      }

      return source;
    }

    /// <summary>Pad evenly on both sides to the specified width by the specified <paramref name="paddingLeft"/> and <paramref name="paddingRight"/> respectively.</summary>
    public static SpanBuilder<T> PadEven<T>(this SpanBuilder<T> source, int totalWidth, System.ReadOnlySpan<T> paddingLeft, System.ReadOnlySpan<T> paddingRight, bool leftBias = true)
    {
      if (totalWidth > source.Count)
      {
        var quotient = System.Math.DivRem(totalWidth - source.Count, 2, out var remainder);

        PadLeft(source, source.Count + (leftBias && remainder > 0 ? quotient + 1 : quotient), paddingLeft);
        // The two lines below are the original right biased (always) which works great.
        //PadLeft(Length + (totalWidth - Length) / 2, paddingLeft);
        PadRight(source, totalWidth, paddingRight);
      }

      return source;
    }

    /// <summary>Pad on the left with the specified <paramref name="padding"/>.</summary>
    public static SpanBuilder<T> PadLeft<T>(this SpanBuilder<T> source, int totalWidth, T padding)
    {
      if (source.Count < totalWidth)
        source.Insert(0, padding, totalWidth - source.Count);

      return source;
    }

    /// <summary>Pad on the left with the specified <paramref name="padding"/>.</summary>
    public static SpanBuilder<T> PadLeft<T>(this SpanBuilder<T> source, int totalWidth, System.ReadOnlySpan<T> padding)
    {
      while (source.Count < totalWidth)
        source.Insert(0, padding, 1);

      return source.Remove(0, source.Count - totalWidth);
    }

    /// <summary>Pad on the right with the specified <paramref name="padding"/>.</summary>
    public static SpanBuilder<T> PadRight<T>(this SpanBuilder<T> source, int totalWidth, T padding)
      => source.Append(padding, totalWidth - source.Count);

    /// <summary>Pad on the right with the specified <paramref name="padding"/>.</summary>
    public static SpanBuilder<T> PadRight<T>(this SpanBuilder<T> source, int totalWidth, System.ReadOnlySpan<T> padding)
    {
      while (source.Count < totalWidth)
        source.Append(padding, 1);

      return source.Remove(totalWidth, source.Count - totalWidth);
    }
  }
}
