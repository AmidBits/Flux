namespace Flux
{
  public ref partial struct SpanBuilder<T>
  {
    /// <summary>Pads the StringBuilder evenly on both sides to the specified width by the specified padding characters for left and right respectively.</summary>
    public void PadEven(int totalWidth, T paddingLeft, T paddingRight)
    {
      if (totalWidth > Length)
      {
        PadLeft(Length + (totalWidth - Length) / 2, paddingLeft);
        PadRight(totalWidth, paddingRight);
      }
    }
    /// <summary>Pads the StringBuilder evenly on both sides to the specified width by the specified padding strings for left and right respectively.</summary>
    public void PadEven(int totalWidth, System.ReadOnlySpan<T> paddingLeft, System.ReadOnlySpan<T> paddingRight)
    {
      if (totalWidth > Length)
      {
        PadLeft(Length + (totalWidth - Length) / 2, paddingLeft);
        PadRight(totalWidth, paddingRight);
      }
    }
  }
}
