namespace Flux
{
  namespace Quantities
  {
    /// <summary>
    /// <para>Currency, unit of "decimal".</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Currency"/></para>
    /// </summary>
    /// <remarks>This is obviously a long shot, but I ideas.</remarks>
    public readonly record struct Currency
    : System.IComparable, System.IComparable<Currency>, System.IFormattable, IValueQuantifiable<decimal>
    {
      private readonly decimal m_value;

      public Currency(decimal value) => m_value = value >= 0 ? value : throw new System.ArgumentOutOfRangeException(nameof(value));

      #region Overloaded operators

      public static bool operator <(Currency a, Currency b) => a.CompareTo(b) < 0;
      public static bool operator <=(Currency a, Currency b) => a.CompareTo(b) <= 0;
      public static bool operator >(Currency a, Currency b) => a.CompareTo(b) > 0;
      public static bool operator >=(Currency a, Currency b) => a.CompareTo(b) >= 0;

      public static Currency operator -(Currency v) => new(-v.m_value);
      public static Currency operator +(Currency a, decimal b) => new(a.m_value + b);
      public static Currency operator +(Currency a, Currency b) => a + b.m_value;
      public static Currency operator /(Currency a, decimal b) => new(a.m_value / b);
      public static Currency operator /(Currency a, Currency b) => a / b.m_value;
      public static Currency operator *(Currency a, decimal b) => new(a.m_value * b);
      public static Currency operator *(Currency a, Currency b) => a * b.m_value;
      public static Currency operator %(Currency a, decimal b) => new(a.m_value % b);
      public static Currency operator %(Currency a, Currency b) => a % b.m_value;
      public static Currency operator -(Currency a, decimal b) => new(a.m_value - b);
      public static Currency operator -(Currency a, Currency b) => a - b.m_value;

      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is Currency o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(Currency other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? formatProvider) => GetType().Name + m_value.ToString(format ?? "N2", formatProvider);

      #region IQuantifiable<>

      /// <summary>
      /// <para>The <see cref="Currency.Value"/> property is the ultraviolet index.</para>
      /// </summary>
      public decimal Value => m_value;

      #endregion // IQuantifiable<>

      #endregion // Implemented interfaces

      public override string ToString() => ToString(null, null);
    }
  }
}
