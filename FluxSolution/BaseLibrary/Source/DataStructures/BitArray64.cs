namespace Flux.DataStructures
{
  /// <summary>
  /// 
  /// </summary>
  /// <remarks>In .NET there is currently a maximum index limit for an array: 2,146,435,071 (0X7FEFFFFF). That number times 64 (137,371,844,544) is the practical limit of <see cref="BitArray64"/>.</remarks>
  public sealed class BitArray64
    : System.ICloneable, System.Collections.Generic.IEnumerable<bool>
  {
    private readonly ulong[] m_bitArray;
    private readonly long m_bitCount;

    private BitArray64(long bitCount)
    {
      if (bitCount < 0) throw new System.ArgumentOutOfRangeException(nameof(bitCount));

      m_bitArray = new ulong[((bitCount - 1) / 64) + 1];
      m_bitCount = bitCount;
    }

    /// <summary>
    /// <para>Create a bit array of <paramref name="bitCount"/> with all bits set to <paramref name="defaultValue"/>.</para>
    /// </summary>
    /// <param name="bitCount">The number of bits to allocate.</param>
    /// <param name="defaultValue">This is the value each bit is set to.</param>
    public BitArray64(long bitCount, bool defaultValue) : this(bitCount) => SetAll(defaultValue);

    /// <summary>
    /// <para>Create a bit array of <paramref name="bitCount"/> with the (64-bit) <paramref name="fillPattern"/> repeated over the entire range of the bit array until all bits have been set.</para>
    /// </summary>
    /// <param name="bitCount">The number of bits to allocate.</param>
    /// <param name="fillPattern">The 64-bit fill pattern. Repeated until all bits have been touched.</param>
    public BitArray64(long bitCount, long fillPattern) : this(bitCount) => Fill(fillPattern);

    /// <summary>
    /// <para>Create a bit array of <paramref name="bitCount"/> with the (64-bit) <paramref name="multiFillPattern"/> repeated over the entire range of the bit array until all bits have been set.</para>
    /// </summary>
    /// <param name="bitCount">The number of bits to allocate.</param>
    /// <param name="multiFillPattern">An array of 64-bit patterns. Repeated until all bits in the array have been set.</param>
    [System.CLSCompliant(false)]
    public BitArray64(long bitCount, ulong[] multiFillPattern) : this(bitCount) => Fill(multiFillPattern);

    public bool this[long bitIndex] { get => Get(bitIndex); set => Set(bitIndex, value); }

    public long BitCount => m_bitCount;

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <param name="pattern"></param>
    public void Fill(long pattern)
    {
      System.Array.Fill(m_bitArray, unchecked((ulong)pattern));

      if ((1UL << (int)(m_bitCount % 64)) - 1 is var bitMask && bitMask != 0)
        m_bitArray[m_bitArray.Length - 1] &= bitMask;
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
    }

    public bool Get(long bitIndex) => (m_bitArray[bitIndex >> 6] & (1UL << (int)(bitIndex % 64))) != 0;

    public bool HasAllSet()
    {
      for (var index = m_bitArray.Length - 2; index >= 0; index--)
        if (ulong.PopCount(m_bitArray[index]) != 64)
          return false;

      return ulong.PopCount(m_bitArray[m_bitArray.Length - 1]) == (ulong)(m_bitCount % 64);
    }

    public bool HasAnySet()
    {
      for (var index = 0; index < m_bitArray.Length; index++)
        if (ulong.PopCount(m_bitArray[index]) > 0)
          return true;

      return false;
    }

    public void Not()
    {
      for (var index = 0; index < m_bitArray.Length; index++)
        m_bitArray[index] = ~m_bitArray[index];

      m_bitArray[m_bitArray.Length - 1] &= (1UL << (int)(m_bitCount % 64)) - 1; // Ensure most significant unused bits are set to 0.
    }

    public long PopCount()
    {
      var count = 0UL;
      for (var index = m_bitArray.Length - 1; index >= 0; index--)
        count += ulong.PopCount(m_bitArray[index]);
      return (long)count;
    }

    public void Set(long bitIndex, bool value)
    {
      if (bitIndex < 0 || bitIndex >= m_bitCount) throw new System.ArgumentOutOfRangeException(nameof(bitIndex));

      if (value) m_bitArray[bitIndex >> 6] |= (1UL << (int)(bitIndex % 64)); // Set the bit to 1.
      else m_bitArray[bitIndex >> 6] &= ~(1UL << (int)(bitIndex % 64)); // Set the bit to 0.
    }

    public void SetAll(bool value)
    {
      if (value) Fill(unchecked((long)0xFFFFFFFFFFFFFFFFUL)); // Set all bits to 1.
      else System.Array.Clear(m_bitArray, 0, m_bitArray.Length); // Set all bits to 0.
    }

    public System.Collections.Generic.IEnumerator<bool> GetEnumerator()
    {
      for (var index = 0L; index < m_bitCount; index++)
        yield return Get(index);
    }
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

    #region IClone

    public object Clone()
    {
      var clone = new BitArray64(m_bitCount);
      m_bitArray.CopyTo(clone.m_bitArray, 0);
      return clone;
    }

    #endregion // IClone

    #region ICollection // Because of possibly more bits than an int can hold, ICollection is not implemented.

    public void CopyTo(Array array, int index)
    {
      if (array is ulong[])
        System.Array.Copy(m_bitArray, 0, array, index, m_bitArray.Length);
      else
        throw new System.ArgumentException("Unsupported type.", nameof(array));
    }

    #endregion // ICollection

    public override string ToString() => $"{GetType().Name} {{ BitCount = {m_bitCount} }}";
  }
}
