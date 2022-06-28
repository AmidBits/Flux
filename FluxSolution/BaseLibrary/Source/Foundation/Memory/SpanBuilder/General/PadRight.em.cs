//namespace Flux
//{
//  public ref partial struct SpanBuilder<T>
//  {
//    /// <summary>Pads this StringBuilder on the right with the specified padding character.</summary>
//    public void PadRight(int totalWidth, T padding)
//      => Append(padding, totalWidth - Length);
//    /// <summary>Pads this StringBuilder on the right with the specified padding string.</summary>
//    public void PadRight(int totalWidth, System.ReadOnlySpan<T> padding)
//    {
//      while (Length < totalWidth)
//        Append(padding);

//      Remove(totalWidth, Length - totalWidth);
//    }
//  }
//}
