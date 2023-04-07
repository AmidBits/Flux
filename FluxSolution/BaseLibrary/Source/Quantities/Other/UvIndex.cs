namespace Flux
{
  namespace Quantities
  {
    /// <summary>UV index, unit of itself.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Ultraviolet_index"/>
    public readonly record struct UvIndex
      : System.IComparable, System.IComparable<UvIndex>, IQuantifiable<double>
    {
      private readonly double m_value;

      public UvIndex(double value) => m_value = value > 0 ? value : throw new System.ArgumentOutOfRangeException(nameof(value));

      #region Overloaded operators
      public static explicit operator double(UvIndex v) => v.m_value;
      public static explicit operator UvIndex(double v) => new(v);

      public static bool operator <(UvIndex a, UvIndex b) => a.CompareTo(b) < 0;
      public static bool operator <=(UvIndex a, UvIndex b) => a.CompareTo(b) <= 0;
      public static bool operator >(UvIndex a, UvIndex b) => a.CompareTo(b) > 0;
      public static bool operator >=(UvIndex a, UvIndex b) => a.CompareTo(b) >= 0;

      public static UvIndex operator -(UvIndex v) => new(-v.m_value);
      public static UvIndex operator +(UvIndex a, double b) => new(a.m_value + b);
      public static UvIndex operator +(UvIndex a, UvIndex b) => a + b.m_value;
      public static UvIndex operator /(UvIndex a, double b) => new(a.m_value / b);
      public static UvIndex operator /(UvIndex a, UvIndex b) => a / b.m_value;
      public static UvIndex operator *(UvIndex a, double b) => new(a.m_value * b);
      public static UvIndex operator *(UvIndex a, UvIndex b) => a * b.m_value;
      public static UvIndex operator %(UvIndex a, double b) => new(a.m_value % b);
      public static UvIndex operator %(UvIndex a, UvIndex b) => a % b.m_value;
      public static UvIndex operator -(UvIndex a, double b) => new(a.m_value - b);
      public static UvIndex operator -(UvIndex a, UvIndex b) => a - b.m_value;
      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is UvIndex o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(UvIndex other) => m_value.CompareTo(other.m_value);

      // IQuantifiable<>
      public string ToQuantityString(string? format = null, bool preferUnicode = false, bool useFullName = false) => string.Format($"UV Index {{0:{format ?? "N1"}}}", m_value);
      public double Value { get => m_value; init => m_value = value; }

      #endregion Implemented interfaces

      public override string ToString() => ToQuantityString();
    }
  }
}
