//namespace Flux
//{
//  public ref partial struct SpanBuilder<T>
//  {
//    /// <summary>Pads the StringBuilder evenly on both sides to the specified width by the specified padding characters for left and right respectively.</summary>
//    public void PadEven(int totalWidth, T paddingLeft, T paddingRight, bool leftBias = true)
//    {
//      if (totalWidth > Length)
//      {
//        var quotient = System.Math.DivRem(totalWidth - Length, 2, out var remainder);

//        PadLeft(Length + (leftBias && remainder > 0 ? quotient + 1 : quotient), paddingLeft);
//        // The two lines below are the original right biased (always) which works great.
//        //PadLeft(Length + (totalWidth - Length) / 2, paddingLeft);
//        PadRight(totalWidth, paddingRight);
//      }
//    }
//    /// <summary>Pads the StringBuilder evenly on both sides to the specified width by the specified padding strings for left and right respectively.</summary>
//    public void PadEven(int totalWidth, System.ReadOnlySpan<T> paddingLeft, System.ReadOnlySpan<T> paddingRight, bool leftBias = true)
//    {
//      if (totalWidth > Length)
//      {
//        var quotient = System.Math.DivRem(totalWidth - Length, 2, out var remainder);

//        PadLeft(Length + (leftBias && remainder > 0 ? quotient + 1 : quotient), paddingLeft);
//        // The two lines below are the original right biased (always) which works great.
//        //PadLeft(Length + (totalWidth - Length) / 2, paddingLeft);
//        PadRight(totalWidth, paddingRight);
//      }
//    }
//  }
//}
