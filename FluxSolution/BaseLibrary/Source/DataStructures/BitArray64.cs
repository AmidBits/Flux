namespace Flux.DataStructures
{
  /// <summary>
  /// <para>BitArray64 maintains the same functionality as <see cref="System.Collections.BitArray"/>.</para>
  /// </summary>
  /// <remarks>In .NET there is currently a maximum index limit for an array: 2,146,435,071 (0X7FEFFFFF). That number times 64 (137,371,844,544) is the practical limit of <see cref="BitArray64"/>.</remarks>
  public sealed class BitArray64
    : System.ICloneable, System.Collections.ICollection, System.Collections.Generic.IEnumerable<bool>
  {
    private readonly ulong[] m_bitArray;
    private readonly long m_bitLength;

    private BitArray64(long bitLength)
    {
      if (bitLength < 0) throw new System.ArgumentOutOfRangeException(nameof(bitLength));

      m_bitArray = new ulong[((bitLength - 1L) / 64) + 1];
      m_bitLength = bitLength;
    }

    /// <summary>
    /// <para>Creates a new <see cref="BitArray64"/> with the same length and bit values as <paramref name="bitArray"/>.</para>
    /// </summary>
    /// <param name="bitArray"></param>
    /// <exception cref="System.ArgumentNullException"></exception>
    public BitArray64(BitArray64 bitArray)
      : this(bitArray?.m_bitLength ?? throw new System.ArgumentNullException(nameof(bitArray)))
      => System.Array.Copy(bitArray.m_bitArray, m_bitArray, m_bitArray.Length);

    /// <summary>
    /// <para>Creates a new <see cref="BitArray64"/> of <paramref name="bitLength"/> values. All of the values in the <see cref="BitArray64"/> are set to <paramref name="defaultValue"/>.</para>
    /// </summary>
    /// <param name="bitLength">The number of bits to allocate.</param>
    /// <param name="defaultValue">This is the value each bit is set to.</param>
    public BitArray64(long bitLength, bool defaultValue) : this(bitLength) => SetAll(defaultValue);

    /// <summary>
    /// <para>Creates a new <see cref="BitArray64"/> of <paramref name="bitLength"/> with the (64-bit) <paramref name="fillPattern"/> repeated over the entire range of the bit array until all bits have been set.</para>
    /// </summary>
    /// <param name="bitLength">The number of bits to allocate.</param>
    /// <param name="fillPattern">The 64-bit fill pattern. Repeated until all bits have been touched.</param>
    public BitArray64(long bitLength, long fillPattern) : this(bitLength) => Fill(fillPattern);

    /// <summary>
    /// <para>Creates a new <see cref="BitArray64"/> of <paramref name="bitLength"/> with the (64-bit) <paramref name="multiFillPattern"/> repeated over the entire range of the bit array until all bits have been set.</para>
    /// </summary>
    /// <param name="bitLength">The number of bits to allocate.</param>
    /// <param name="multiFillPattern">An array of 64-bit patterns. Repeated until all bits in the array have been set.</param>
    [System.CLSCompliant(false)]
    public BitArray64(long bitLength, ulong[] multiFillPattern) : this(bitLength) => Fill(multiFillPattern);

    public bool this[long bitIndex] { get => Get(bitIndex); set => Set(bitIndex, value); }

    public long Length => m_bitLength;

    /// <summary>
    /// <para>Performs the bitwise AND operation between the elements of the current BitArray object and the corresponding elements in the specified array. The current BitArray object will be modified to store the result of the bitwise AND operation.</para>
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public Flux.DataStructures.BitArray64 And(Flux.DataStructures.BitArray64 other)
    {
      for (var i = System.Math.Min(m_bitArray.Length, other.m_bitArray.Length) - 1; i >= 0; i--)
        m_bitArray[i] &= other.m_bitArray[i];

      return this;
    }

    public void CopyTo<T>(System.Array array, long offset, int bitCount, System.Func<long, T> extractionSelector)
    {
      for (var i = (m_bitArray.Length * (64 / bitCount)) - 1; i >= 0; i--)
        array.SetValue(extractionSelector(i), offset + i);
    }

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <param name="pattern"></param>
    public void Fill(long pattern)
    {
      System.Array.Fill(m_bitArray, unchecked((ulong)pattern));

      if ((1UL << (int)(m_bitLength % 64)) - 1 is var bitMask && bitMask != 0)
        m_bitArray[^1] &= bitMask;
    }

    /// <summary>
    /// <para>Fills the entire bit array from the <paramref name="pattern"/> array in a repeated manner until all bits in the bit array has been set.</para>
    /// </summary>
    /// <param name="pattern"></param>
    [System.CLSCompliant(false)]
    public void Fill(ulong[] pattern)
    {
      for (var index = 0; index < m_bitArray.Length; index++)
        m_bitArray[(int)index] = pattern[index % pattern.Length];

      if ((1UL << (int)(m_bitLength % 64)) - 1 is var bitMask && bitMask != 0)
        m_bitArray[^1] &= bitMask;
    }

    public bool Get(long bitIndex) => (m_bitArray[bitIndex >> 6] & (1UL << (int)(bitIndex % 64))) != 0;

    [System.CLSCompliant(false)] public byte GetByte(long index) => (byte)((m_bitArray[index >> 3] >> (int)((index % 8) << 3)) & 0xFF);
    public short GetInt16(long index) => unchecked((short)GetUInt16(index));
    public int GetInt32(long index) => unchecked((int)GetUInt32(index));
    public long GetInt64(long index) => unchecked((long)m_bitArray[index]);
    [System.CLSCompliant(false)] public sbyte GetSByte(long index) => unchecked((sbyte)GetByte(index));
    [System.CLSCompliant(false)] public ushort GetUInt16(long index) => (ushort)((m_bitArray[index >> 2] >> (int)((index % 4) << 4)) & 0xFFFF);
    [System.CLSCompliant(false)] public uint GetUInt32(long index) => (uint)((m_bitArray[index >> 1] >> (int)((index % 2) << 5)) & 0xFFFFFFFF);
    [System.CLSCompliant(false)] public ulong GetUInt64(long index) => m_bitArray[index];

    /// <summary>
    /// <para>Determines whether all bits in the <see cref="BitArray64"/> are set to <see langword="true"/>.</para>
    /// </summary>
    /// <returns></returns>
    public bool HasAllSet()
    {
      for (var index = m_bitArray.Length - 2; index >= 0; index--)
        if (ulong.PopCount(m_bitArray[index]) != 64)
          return false;

      return ulong.PopCount(m_bitArray[^1]) == (ulong)(m_bitLength % 64);
    }

    /// <summary>
    /// <para>Determines whether any bit in the <see cref="BitArray64"/> is set to <see langword="true"/>.</para>
    /// </summary>
    /// <returns></returns>
    public bool HasAnySet()
    {
      for (var index = 0; index < m_bitArray.Length; index++)
        if (ulong.PopCount(m_bitArray[index]) > 0)
          return true;

      return false;
    }

    public Flux.DataStructures.BitArray64 LeftShift(int count)
    {
      throw new System.NotImplementedException();

      //return this;
    }

    public void Not()
    {
      for (var index = 0; index < m_bitArray.Length; index++)
        m_bitArray[index] = ~m_bitArray[index];

      m_bitArray[^1] &= (1UL << (int)(m_bitLength % 64)) - 1; // Ensure most significant unused bits are set to 0.
    }

    /// <summary>
    /// <para>Performs the bitwise OR operation between the elements of the current BitArray object and the corresponding elements in the specified array. The current BitArray object will be modified to store the result of the bitwise OR operation.</para>
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public Flux.DataStructures.BitArray64 Or(Flux.DataStructures.BitArray64 other)
    {
      for (var i = System.Math.Min(m_bitArray.Length, other.m_bitArray.Length) - 1; i >= 0; i--)
        m_bitArray[i] |= other.m_bitArray[i];

      return this;
    }

    public long PopCount()
    {
      var count = 0UL;
      for (var index = m_bitArray.Length - 1; index >= 0; index--)
        count += ulong.PopCount(m_bitArray[index]);
      return (long)count;
    }

    public Flux.DataStructures.BitArray64 RightShift(int count)
    {
      throw new System.NotImplementedException();

      //return this;
    }

    /// <summary>
    /// <para>Sets the bit value at position <paramref name="bitIndex"/> to <paramref name="value"/>.</para>
    /// </summary>
    /// <param name="bitIndex"></param>
    /// <param name="value"></param>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public void Set(long bitIndex, bool value)
    {
      if (bitIndex < 0 || bitIndex >= m_bitLength) throw new System.ArgumentOutOfRangeException(nameof(bitIndex));

      if (value) m_bitArray[bitIndex >> 6] |= (1UL << (int)(bitIndex % 64)); // Set the bit to 1.
      else m_bitArray[bitIndex >> 6] &= ~(1UL << (int)(bitIndex % 64)); // Set the bit to 0.
    }

    /// <summary>
    /// <para>Sets all the bit values to <paramref name="value"/>.</para>
    /// </summary>
    /// <param name="value"></param>
    public void SetAll(bool value)
    {
      if (value) Fill(unchecked((long)0xFFFFFFFFFFFFFFFFUL)); // Set all bits to 1.
      else System.Array.Clear(m_bitArray, 0, m_bitArray.Length); // Set all bits to 0.
    }

    /// <summary>In-place shuffle (randomized) of the span. Uses the specified rng.</summary>
    public void Shuffle(System.Random? rng = null)
    {
      rng ??= System.Random.Shared;

      for (var index = 0; index < m_bitLength; index++)
        Swap(index, rng.Next(index + 1)); // Since 'rng.Next(max-value-excluded)' we add one.
    }

    /// <summary>In-place swap of two elements by the specified indices.</summary>
    public void Swap(int indexA, int indexB)
    {
      if (indexA != indexB) // No need to actually swap if the indices are the same.
        (this[indexB], this[indexA]) = (this[indexA], this[indexB]);
    }

    /// <summary>
    /// <para>Performs the bitwise XOR operation between the elements of the current BitArray object and the corresponding elements in the specified array. The current BitArray object will be modified to store the result of the bitwise XOR operation.</para>
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public Flux.DataStructures.BitArray64 Xor(Flux.DataStructures.BitArray64 other)
    {
      for (var i = System.Math.Min(m_bitArray.Length, other.m_bitArray.Length) - 1; i >= 0; i--)
        m_bitArray[i] ^= other.m_bitArray[i];

      return this;
    }

    #region Static methods

    public static long ComputeLength(long bitLength, long bitCount) => ((bitLength - 1L) / bitCount) + 1L;

    #endregion // Static methods

    #region Implemented interfaces

    // ICloneable
    public object Clone()
    {
      var clone = new BitArray64(m_bitLength);
      m_bitArray.CopyTo(clone.m_bitArray, 0);
      return clone;
    }

    // ICollection
    public int Count => checked((int)m_bitLength); // Throw if to big.
    public bool IsSynchronized => false;
    public object SyncRoot => this;
    public void CopyTo(System.Array array, int index)
    {
      var type = array.GetType().GetElementType() ?? throw new System.NotSupportedException();

      if (type == typeof(ulong))
        System.Array.Copy(m_bitArray, 0, array, index, m_bitArray.Length);
      else if (type == typeof(long))
        CopyTo(array, index, 64, GetInt64);
      else if (type == typeof(uint))
        CopyTo(array, index, 32, GetUInt32);
      else if (type == typeof(int))
        CopyTo(array, index, 32, GetInt32);
      else if (type == typeof(byte))
        CopyTo(array, index, 8, GetByte);
      else if (type == typeof(sbyte))
        CopyTo(array, index, 8, GetSByte);
      else if (type == typeof(short))
        CopyTo(array, index, 16, GetInt16);
      else if (type == typeof(ushort))
        CopyTo(array, index, 16, GetUInt16);
      else
        throw new ArgumentException("Unsupported type.", nameof(array));
    }

    // IEnumerable<T>
    public System.Collections.Generic.IEnumerator<bool> GetEnumerator()
    {
      for (var index = 0L; index < m_bitLength; index++)
        yield return Get(index);
    }
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();


    #endregion // Implemented interfaces

    public override string ToString() => $"{GetType().Name} {{ BitCount = {m_bitLength} }}";
  }
}
