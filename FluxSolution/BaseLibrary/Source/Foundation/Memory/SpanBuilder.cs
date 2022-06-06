namespace Flux
{
  public ref partial struct SpanBuilder<T>
  {
    private int bufferPosition;
    private Span<T> buffer;

    public SpanBuilder(System.Span<T> initialBuffer)
    {
      bufferPosition = 0;
      buffer = initialBuffer;
    }
    public SpanBuilder()
      : this(new T[32])
    {
    }

    /// <summary>The length of the current content of the SpanBuilder.</summary>
    public int Length => bufferPosition;

    /// <summary>The current capacity of the SpanBuilder.</summary>
    public int Capacity => buffer.Length;

    public ref T this[int index] => ref buffer[index];

    public void Append(ReadOnlySpan<T> value)
    {
      if (bufferPosition + value.Length is var needed && needed > buffer.Length) Grow(needed * 2);

      value.CopyTo(buffer[bufferPosition..]);

      bufferPosition += value.Length;
    }

    public System.ReadOnlySpan<T> AsReadOnlySpan() => buffer[..bufferPosition];

    public void Clear() => bufferPosition = 0;

    public void EnsureCapacity(int newCapacity)
    {
      if (newCapacity < 0) throw new ArgumentOutOfRangeException(nameof(newCapacity));

      if (newCapacity > Capacity)
        Grow(newCapacity);
    }

    private void Grow(int capacity = 0)
    {
      var newCapacity = capacity > Capacity ? capacity : Capacity * 2;

      var rented = System.Buffers.ArrayPool<T>.Shared.Rent(newCapacity);
      buffer.CopyTo(rented);
      buffer = rented;
      System.Buffers.ArrayPool<T>.Shared.Return(rented);
    }

    public void Insert(int index, System.ReadOnlySpan<T> value)
    {
      if (index < 0 || index > bufferPosition) throw new ArgumentOutOfRangeException(nameof(index));

      if (bufferPosition + value.Length is var needed && needed > buffer.Length) Grow(needed);

      var endIndex = index + value.Length;

      buffer[index..bufferPosition].CopyTo(buffer[endIndex..]); // Move right side of old content.

      value.CopyTo(buffer[index..endIndex]); // Insert new content in buffer gap.

      bufferPosition += value.Length;
    }

    public void Remove(int startIndex, int length)
    {
      if (startIndex < 0 || startIndex >= bufferPosition) throw new System.ArgumentOutOfRangeException(nameof(startIndex));
      if (length < 0 || startIndex + length is var endIndex && endIndex >= bufferPosition) throw new System.ArgumentOutOfRangeException(nameof(length));

      buffer[(startIndex + length)..bufferPosition].CopyTo(buffer[startIndex..]);

      bufferPosition -= length;

      buffer.Slice(bufferPosition, length).Clear();
    }

    #region Object overrides
    public override string ToString()
      => AsReadOnlySpan().ToString();
    #endregion Object overrides
  }
}
