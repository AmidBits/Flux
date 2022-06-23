namespace Flux
{
  public ref partial struct SpanBuilder<T>
  {
    /// <summary>Swap two elements by the specified indices.</summary>
    internal void SwapImpl(int indexA, int indexB)
    {
      if (indexA != indexB)
        (m_buffer[indexB], m_buffer[indexA]) = (m_buffer[indexA], m_buffer[indexB]);
    }
    /// <summary>Swap two elements by the specified indices.</summary>
    public void Swap(int indexA, int indexB)
    {
      if (Length == 0)
        throw new System.ArgumentException(@"The span builder is empty.");
      else if (indexA < 0 || indexA >= Length)
        throw new System.ArgumentOutOfRangeException(nameof(indexA));
      else if (indexB < 0 || indexB >= Length)
        throw new System.ArgumentOutOfRangeException(nameof(indexB));
      else
        SwapImpl(indexA, indexB);
    }

    public void SwapFirstWith(int index)
      => Swap(0, index);

    public void SwapLastWith(int index)
      => Swap(index, Length - 1);
  }
}
