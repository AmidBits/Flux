namespace Flux
{
  public readonly record struct Slice
    : System.IComparable, System.IComparable<Slice>, System.IFormattable
  {
    private readonly int m_index;
    private readonly int m_length;

    public Slice(int index, int length)
    {
      m_index = index;
      m_length = length;
    }
    public Slice(System.Range range, int length)
      => (m_index, m_length) = range.GetOffsetAndLength(length);

    public readonly void Deconstruct(out int index, out int length) { index = m_index; length = m_length; }

    public int Index => m_index;
    public int Length => m_length;

    public int GetMaxIndex() => m_index + m_length - 1;
    public int GetFollowingIndex() => m_index + m_length;

    public Interval<T> ToInterval<T>()
      where T : System.Numerics.IBinaryInteger<T>
      => new(T.CreateChecked(m_index), T.CreateChecked(m_index + m_length - 1));

    public Interval<int> ToInterval() => ToInterval<int>();

    public System.Range ToRange()
      => new(m_index, m_index + m_length);

    #region Static methods

    public static Slice Create<T>(Interval<T> slice)
      where T : System.Numerics.IBinaryInteger<T>
    {
      var (index, length) = slice.GetOffsetAndLength();

      return new(int.CreateChecked(index), int.CreateChecked(length));
    }

    #endregion // Static methods

    #region Implemented interfaces

    public int CompareTo(object? obj)
      => obj is Slice o ? CompareTo(o) : -1;

    public int CompareTo(Slice other)
      => m_index < other.m_index ? -1 : m_index > other.m_index ? 1 : m_length < other.m_length ? -1 : m_length > other.m_length ? 1 : 0;

    public string ToString(string? format, IFormatProvider? formatProvider)
      => $"{{{m_index.ToString(format, formatProvider)}, {m_length.ToString(format, formatProvider)}}}";

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
