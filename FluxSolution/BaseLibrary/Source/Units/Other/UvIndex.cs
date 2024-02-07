namespace Flux
{
  namespace Units
  {
    /// <summary>UV index, unit of itself.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Ultraviolet_index"/>
    public readonly record struct UvIndex
    : System.IComparable, System.IComparable<UvIndex>, System.IFormattable, IValueQuantifiable<double>
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

      // IFormattable
      public string ToString(string? format, IFormatProvider? formatProvider) => ToValueString(QuantifiableValueStringOptions.Default with { Format = format, FormatProvider = formatProvider });

      // IQuantifiable<>
      public string ToValueString(QuantifiableValueStringOptions options)
        => string.Format(options.CultureInfo, $"UV Index {{0:{options.Format ?? "N1"}}}", m_value);

      /// <summary>
      /// <para>The <see cref="UvIndex.Value"/> property is the ultraviolet index.</para>
      /// </summary>
      public double Value => m_value;

      #endregion Implemented interfaces

      public override string ToString() => ToValueString(QuantifiableValueStringOptions.Default);
    }
  }
}
