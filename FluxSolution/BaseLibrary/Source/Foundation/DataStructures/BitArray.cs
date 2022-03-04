namespace Flux
{
  public sealed class BitArray
    : System.Collections.Generic.IEnumerable<bool>
  {
    private readonly ulong[] m_bitArray;
    private readonly long m_bitLength;

    //private readonly long m_byteLength;
    //private readonly long m_uint16Length;
    //private readonly long m_uint32Length;
    //private readonly long m_uint64Length;

    public bool this[long index] { get => GetBit(index); set => SetBit(index, value); }

    public BitArray(long bitLength, bool defaultValue)
    {
      if (bitLength < 0) throw new System.ArgumentOutOfRangeException(nameof(bitLength));

      m_bitArray = new ulong[((bitLength - 1) / 64) + 1];
      m_bitLength = bitLength;

      //m_byteLength = (m_bitLength / 8) + ((m_bitLength % 8) > 0 ? 1 : 0);
      //m_uint16Length = (m_bitLength / 16) + ((m_bitLength % 16) > 0 ? 1 : 0);
      //m_uint32Length = (m_bitLength / 32) + ((m_bitLength % 32) > 0 ? 1 : 0);
      //m_uint64Length = (m_bitLength / 64) + ((m_bitLength % 64) > 0 ? 1 : 0);

      if (defaultValue)
        SetAll(defaultValue);
      else
        System.Array.Clear(m_bitArray, 0, m_bitArray.Length);
    }
    public BitArray(long bitLength)
      : this(bitLength, false)
    { }

    public long Length
      => m_bitLength;

    public bool GetBit(long bitIndex)
      => (bool)((m_bitArray[(bitIndex >> 6)] & (1UL << (int)(bitIndex % 64))) != 0);

    [System.CLSCompliant(false)]
    public byte GetByte(long index)
      => (byte)((m_bitArray[(index >> 4)] >> (int)((index % 8) << 3)) & 0xFF);

    public short GetInt16(long index)
      => unchecked((short)GetUInt16(index));
    public int GetInt32(long index)
      => unchecked((int)GetUInt32(index));
    public long GetInt64(long index)
      => unchecked((long)GetUInt64(index));

    [System.CLSCompliant(false)]
    public ushort GetUInt16(long index)
      => (ushort)((m_bitArray[(index >> 2)] >> (int)((index % 4) << 4)) & 0xFFFFU);
    [System.CLSCompliant(false)]
    public uint GetUInt32(long index)
      => (uint)((m_bitArray[(index >> 1)] >> (int)((index % 2) << 5)) & 0xFFFFFFFFU);
    [System.CLSCompliant(false)]
    public ulong GetUInt64(long index)
      => m_bitArray[index];

    public System.Collections.Generic.IEnumerable<long> GetIndicesEqualToFalse()
      => System.Linq.Enumerable.Cast<bool>(this).GetIndicesInt64(b => !b);
    public System.Collections.Generic.IEnumerable<long> GetIndicesEqualToTrue()
      => System.Linq.Enumerable.Cast<bool>(this).GetIndicesInt64(b => b);

    [System.CLSCompliant(false)]
    public void SetAll(ulong value)
      => System.Array.Fill(m_bitArray, value);
    public void SetAll(bool value)
      => SetAll(value ? 0xFFFFFFFFFFFFFFFF : 0UL);

    public void SetBit(long bitIndex, bool value)
    {
      if (value)
        m_bitArray[(bitIndex >> 6)] |= (1UL << (int)(bitIndex % 64));
      else
        m_bitArray[(bitIndex >> 6)] &= ~(1UL << (int)(bitIndex % 64));
    }

    [System.CLSCompliant(false)]
    public void SetByte(long index, byte value)
    {
      var shiftCount = (int)((index % 8) << 3);
      var arrayIndex = (index >> 4);

      m_bitArray[arrayIndex] = (m_bitArray[arrayIndex] & ~(0xFFUL << shiftCount)) | ((ulong)value << shiftCount);
    }

    public void SetInt16(long index, short value)
      => SetUInt16(index, unchecked((ushort)value));
    public void SetInt32(long index, int value)
      => SetUInt32(index, unchecked((uint)value));
    public void SetInt64(long index, long value)
      => SetUInt64(index, unchecked((ulong)value));

    [System.CLSCompliant(false)]
    public void SetUInt16(long index, ushort value)
    {
      var shiftCount = (int)((index % 4) << 4);
      var arrayIndex = (index >> 2);

      m_bitArray[arrayIndex] = (m_bitArray[arrayIndex] & ~(0xFFFFUL << shiftCount)) | ((ulong)value << shiftCount);
    }
    [System.CLSCompliant(false)]
    public void SetUInt32(long index, uint value)
    {
      var shiftCount = (int)((index % 2) << 5);
      var arrayIndex = (index >> 1);

      m_bitArray[arrayIndex] = (m_bitArray[arrayIndex] & ~(0xFFFFFFFFUL << shiftCount)) | ((ulong)value << shiftCount);
    }
    [System.CLSCompliant(false)]
    public void SetUInt64(long index, ulong value)
      => m_bitArray[index] = value;

    public System.Numerics.BigInteger ToBigInteger()
      => new(ToByteArray());
    public byte[] ToByteArray()
    {
      var bytes = new byte[System.Math.DivRem(m_bitLength, 8, out var remainder) + (remainder == 0 ? 0 : 1)];

      var bytesLength = bytes.Length;
      var arrayLength = m_bitArray.Length;

      var byteIndex = 0;

      for (var index = 0; index < arrayLength; index++)
        for (var longIndex = 0; longIndex < 64 && byteIndex < bytesLength; longIndex += 8)
          bytes[byteIndex++] = (byte)(m_bitArray[index] >> longIndex);

      return bytes;
    }

    public System.Collections.IEnumerator GetEnumerator()
      => new BitArrayExEnumerator(this);
    System.Collections.Generic.IEnumerator<bool> System.Collections.Generic.IEnumerable<bool>.GetEnumerator()
      => new BitArrayExEnumerator(this);

    public override string ToString()
      => $"{GetType().Name} {{ Length = {Length} }}";

    private sealed class BitArrayExEnumerator
      : Disposable, System.ICloneable, System.Collections.Generic.IEnumerator<bool>
    {
      private readonly BitArray m_bitArray;

      private long m_index;

      private bool m_current;

      internal BitArrayExEnumerator(BitArray bitArray)
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
      public object Current
        => Current;
      public bool MoveNext()
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
