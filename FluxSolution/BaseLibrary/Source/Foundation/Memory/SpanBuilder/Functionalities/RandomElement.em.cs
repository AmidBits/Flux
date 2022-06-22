namespace Flux
{
  public ref partial struct SpanBuilder<T>
  {
    /// <summary>Attempts to select a random element from the span in the output variable. Uses the specified random number generator (the .NET cryptographic if null).</summary>
    public bool TryRandomElement(out T result, System.Random random)
    {
      if (random is not null && m_bufferPosition is var sourceLength && sourceLength > 0)
      {
        result = m_buffer[random.Next(sourceLength)];
        return true;
      }

      result = default!;
      return false;
    }
    /// <summary>Attempts to select a random element from the span in the output variable. Uses the .NET cryptographic random number generator.</summary>
    public bool TryRandomElement(out T result)
      => TryRandomElement(out result, Randomization.NumberGenerator.Crypto);

    /// <summary>Returns a random element from the span. Uses the specified random number generator.</summary>
    public T RandomElement(System.Random random)
      => TryRandomElement(out var re, random) ? re : throw new System.InvalidOperationException();
    /// <summary>Returns a random element from the span. Uses the .NET cryptographic random number generator.</summary>
    public T RandomElement()
      => TryRandomElement(out var re, Randomization.NumberGenerator.Crypto) ? re : throw new System.InvalidOperationException();
  }
}
