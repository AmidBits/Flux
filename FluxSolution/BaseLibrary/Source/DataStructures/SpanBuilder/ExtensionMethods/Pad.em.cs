namespace Flux
{
  public static partial class ExtensionMethodsSpanBuilder
  {
    /// <summary>Pad evenly on both sides to the specified width by the specified <paramref name="paddingLeft"/> and <paramref name="paddingRight"/> respectively.</summary>
    public static SpanBuilder<T> PadEven<T>(this SpanBuilder<T> source, int totalWidth, T paddingLeft, T paddingRight, bool leftBias = true)
    {
      if (totalWidth > source.Length)
      {
        var quotient = System.Math.DivRem(totalWidth - source.Length, 2, out var remainder);

        source.PadLeft(source.Length + (leftBias && remainder > 0 ? quotient + 1 : quotient), paddingLeft);
        // The two lines below are the original right biased (always) which works great.
        //PadLeft(Length + (totalWidth - Length) / 2, paddingLeft);
        source.PadRight(totalWidth, paddingRight);
      }

      return source;
    }

    /// <summary>Pad evenly on both sides to the specified width by the specified <paramref name="paddingLeft"/> and <paramref name="paddingRight"/> respectively.</summary>
    public static SpanBuilder<T> PadEven<T>(this SpanBuilder<T> source, int totalWidth, System.ReadOnlySpan<T> paddingLeft, System.ReadOnlySpan<T> paddingRight, bool leftBias = true)
    {
      if (totalWidth > source.Length)
      {
        var quotient = System.Math.DivRem(totalWidth - source.Length, 2, out var remainder);

        source.PadLeft(source.Length + (leftBias && remainder > 0 ? quotient + 1 : quotient), paddingLeft);
        // The two lines below are the original right biased (always) which works great.
        //PadLeft(Length + (totalWidth - Length) / 2, paddingLeft);
        source.PadRight(totalWidth, paddingRight);
      }

      return source;
    }

    /// <summary>Pad on the left with the specified <paramref name="padding"/>.</summary>
    public static SpanBuilder<T> PadLeft<T>(this SpanBuilder<T> source, int totalWidth, T padding)
    {
      if (source.Length < totalWidth)
        source.Insert(0, padding, totalWidth - source.Length);

      return source;
    }

    /// <summary>Pad on the left with the specified <paramref name="padding"/>.</summary>
    public static SpanBuilder<T> PadLeft<T>(this SpanBuilder<T> source, int totalWidth, System.ReadOnlySpan<T> padding)
    {
      while (source.Length < totalWidth)
        source.Insert(0, padding);

      return source.Remove(0, source.Length - totalWidth);
    }

    /// <summary>Pad on the right with the specified <paramref name="padding"/>.</summary>
    public static SpanBuilder<T> PadRight<T>(this SpanBuilder<T> source, int totalWidth, T padding)
      => source.Append(padding, totalWidth - source.Length);

    /// <summary>Pad on the right with the specified <paramref name="padding"/>.</summary>
    public static SpanBuilder<T> PadRight<T>(this SpanBuilder<T> source, int totalWidth, System.ReadOnlySpan<T> padding)
    {
      while (source.Length < totalWidth)
        source.Append(padding);

      return source.Remove(totalWidth, source.Length - totalWidth);
    }
  }
}
