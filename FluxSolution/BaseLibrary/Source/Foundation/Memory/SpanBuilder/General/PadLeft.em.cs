//namespace Flux
//{
//  public ref partial struct SpanBuilder<T>
//  {
//    /// <summary>Pads this StringBuilder on the left with the specified padding character.</summary>
//    public void PadLeft(int totalWidth, T padding)
//      => Insert(0, padding, totalWidth - Length);
//    /// <summary>Pads this StringBuilder on the left with the specified padding string.</summary>
//    public void PadLeft(int totalWidth, System.ReadOnlySpan<T> padding)
//    {
//      while (Length < totalWidth)
//        Insert(0, padding);

//      Remove(0, Length - totalWidth);
//    }
//  }
//}
