namespace Flux
{
  public static partial class BitArrayExtensionMethods
  {
    public static System.Collections.Generic.IEnumerable<bool> Enumerate(this BitArray source)
    {
      for (var index = 0; index < source.Length; index++)
        yield return source[index];
    }
  }

  public sealed class BitArray
    : System.Collections.Generic.IEnumerable<bool>
  {
    private readonly ulong[] m_bitArray;
    private readonly long m_bitLength;

    public bool this[long index] { get => GetBit(index); set => SetBit(index, value); }

    public BitArray(long bitLength, bool defaultValue)
    {
      if (bitLength < 0) throw new System.ArgumentOutOfRangeException(nameof(bitLength));

      m_bitArray = new ulong[((bitLength - 1) / 64) + 1];
      m_bitLength = bitLength;

      if (defaultValue)
        SetAll(defaultValue);
      else
        System.Array.Clear(m_bitArray, 0, m_bitArray.Length);
    }
    public BitArray(long bitLength) : this(bitLength, false) { }

    public long Length => m_bitLength;

    public bool GetBit(long bitIndex) => (m_bitArray[bitIndex >> 6] & (1UL << (int)(bitIndex % 64))) != 0;

    [System.CLSCompliant(false)] public byte GetByte(long index) => (byte)((m_bitArray[index >> 4] >> (int)((index % 8) << 3)) & 0xFF);

    public short GetInt16(long index) => unchecked((short)GetUInt16(index));
    public int GetInt32(long index) => unchecked((int)GetUInt32(index));
    public long GetInt64(long index) => unchecked((long)GetUInt64(index));

    [System.CLSCompliant(false)] public ushort GetUInt16(long index) => (ushort)((m_bitArray[index >> 2] >> (int)((index % 4) << 4)) & 0xFFFFU);
    [System.CLSCompliant(false)] public uint GetUInt32(long index) => (uint)((m_bitArray[index >> 1] >> (int)((index % 2) << 5)) & 0xFFFFFFFFU);
    [System.CLSCompliant(false)] public ulong GetUInt64(long index) => m_bitArray[index];

    public System.Collections.Generic.IEnumerable<long> GetIndicesEqualToFalse() => System.Linq.Enumerable.Select(System.Linq.Enumerable.Cast<bool>(this).GetElementsAndIndicesInt64((e, i) => !e), e => e.index);
    public System.Collections.Generic.IEnumerable<long> GetIndicesEqualToTrue() => System.Linq.Enumerable.Select(System.Linq.Enumerable.Cast<bool>(this).GetElementsAndIndicesInt64((e, i) => e), e => e.index);

    [System.CLSCompliant(false)] public void SetAll(ulong value) => System.Array.Fill(m_bitArray, value);
    public void SetAll(bool value) => SetAll(value ? 0xFFFFFFFFFFFFFFFF : 0UL);

    public void SetBit(long bitIndex, bool value)
    {
      if (value)
        m_bitArray[bitIndex >> 6] |= (1UL << (int)(bitIndex % 64));
      else
        m_bitArray[bitIndex >> 6] &= ~(1UL << (int)(bitIndex % 64));
    }

    [System.CLSCompliant(false)]
    public void SetByte(long index, byte value)
    {
      var shiftCount = (int)((index % 8) << 3);
      var arrayIndex = index >> 4;

      m_bitArray[arrayIndex] = (m_bitArray[arrayIndex] & ~(0xFFUL << shiftCount)) | ((ulong)value << shiftCount);
    }

    public void SetInt16(long index, short value) => SetUInt16(index, unchecked((ushort)value));
    public void SetInt32(long index, int value) => SetUInt32(index, unchecked((uint)value));
    public void SetInt64(long index, long value) => SetUInt64(index, unchecked((ulong)value));

    [System.CLSCompliant(false)]
    public void SetUInt16(long index, ushort value)
    {
      var shiftCount = (int)((index % 4) << 4);
      var arrayIndex = index >> 2;

      m_bitArray[arrayIndex] = (m_bitArray[arrayIndex] & ~(0xFFFFUL << shiftCount)) | ((ulong)value << shiftCount);
    }
    [System.CLSCompliant(false)]
    public void SetUInt32(long index, uint value)
    {
      var shiftCount = (int)((index % 2) << 5);
      var arrayIndex = index >> 1;

      m_bitArray[arrayIndex] = (m_bitArray[arrayIndex] & ~(0xFFFFFFFFUL << shiftCount)) | ((ulong)value << shiftCount);
    }
    [System.CLSCompliant(false)] public void SetUInt64(long index, ulong value) => m_bitArray[index] = value;

    public System.Numerics.BigInteger ToBigInteger() => new(ToByteArray());
    public string ToBinaryString() => string.Concat(System.Linq.Enumerable.Select(System.Linq.Enumerable.Cast<bool>(this), b => b ? '1' : '0'));
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

    public System.Collections.Generic.IEnumerator<bool> GetEnumerator()
    {
      for (var index = 0L; index < m_bitLength; index++)
        yield return GetBit(index);
    }
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

    public override string ToString() => $"{GetType().Name} {{ Length = {Length} }}";
  }
}
