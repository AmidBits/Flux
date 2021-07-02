namespace Flux
{
  public sealed class BitArray
    : System.Collections.Generic.IEnumerable<bool>
  {
    private readonly long[] m_bitArray;
    private readonly long m_bitLength;

    public bool this[long index] { get => Get(index); set => Set(index, value); }

    public BitArray(long length, bool defaultValue)
    {
      if (length < 0) throw new System.ArgumentOutOfRangeException(nameof(length));

      m_bitArray = new long[((length - 1) / 64) + 1];
      m_bitLength = length;

      if (defaultValue)
      {
        SetAll(defaultValue);
      }
      else
      {
        System.Array.Clear(m_bitArray, 0, m_bitArray.Length);
      }
    }
    public BitArray(int length)
      : this(length, false)
    {
    }

    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public bool Get(long index) => (index >= 0 && (ulong)index < (ulong)m_bitLength) ? (m_bitArray[index >> 6] & (1L << (int)(index % 64))) != 0 : throw new System.ArgumentOutOfRangeException(nameof(index));

    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public void Set(long index, bool value)
    {
      if (index < 0 || (ulong)index >= (ulong)m_bitLength) throw new System.ArgumentOutOfRangeException(nameof(index));

      if (value)
      {
        m_bitArray[index >> 6] |= (1L << (int)(index % 64));
      }
      else
      {
        m_bitArray[index >> 6] &= ~(1L << (int)(index % 64));
      }
    }

    public void SetAll(bool value)
    {
      var fillValue = value ? -1L : 0L;

      for (int i = 0; i < m_bitLength; i++)
      {
        m_bitArray[i] = fillValue;
      }
    }

    public long Length
      => m_bitLength;

    public System.Collections.IEnumerator GetEnumerator()
      => new BitArrayEnumerator(this);
    System.Collections.Generic.IEnumerator<bool> System.Collections.Generic.IEnumerable<bool>.GetEnumerator()
      => new BitArrayEnumerator(this);

    private class BitArrayEnumerator
      : Disposable, System.ICloneable, System.Collections.Generic.IEnumerator<bool>
    {
      private readonly BitArray m_bitArray;

      private long m_index;

      private bool m_current;

      internal BitArrayEnumerator(BitArray bitArray)
      {
        m_bitArray = bitArray;

        m_index = -1;
      }

      protected override void DisposeManaged()
        => base.DisposeManaged();

      // IClone
      public object Clone()
        => MemberwiseClone();

      // IEnumerator
      public virtual object Current
        => Current;
      public virtual bool MoveNext()
      {
        if (m_index < (m_bitArray.m_bitLength - 1))
        {
          m_current = m_bitArray[++m_index];

          return true;
        }
        else
        {
          m_index = m_bitArray.m_bitLength;

          return false;
        }
      }
      public void Reset()
        => m_index = -1;

      // IEnumerator, IEnumerator<bool>
      bool System.Collections.Generic.IEnumerator<bool>.Current
        => m_current;
    }
  }
}
