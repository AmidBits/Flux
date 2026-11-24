namespace Flux.Units
{
  /// <summary>
  /// <para>Currency, unit of "decimal".</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Currency"/></para>
  /// </summary>
  /// <remarks>This is obviously a long shot, but I ideas.</remarks>
  public readonly record struct Currency
  : System.IComparable, System.IComparable<Currency>, System.IFormattable, IValueQuantifiable<decimal>
  {
    private readonly decimal m_currency;

    public Currency(decimal value) => m_currency = value >= 0 ? value : throw new System.ArgumentOutOfRangeException(nameof(value));

    #region Overloaded operators

    public static explicit operator Currency(decimal value) => new(value);
    public static implicit operator decimal(Currency value) => value.m_currency;

    public static bool operator <(Currency a, Currency b) => a.CompareTo(b) < 0;
    public static bool operator <=(Currency a, Currency b) => a.CompareTo(b) <= 0;
    public static bool operator >(Currency a, Currency b) => a.CompareTo(b) > 0;
    public static bool operator >=(Currency a, Currency b) => a.CompareTo(b) >= 0;

    public static Currency operator -(Currency v) => new(-v.m_currency);
    public static Currency operator +(Currency a, decimal b) => new(a.m_currency + b);
    public static Currency operator +(Currency a, Currency b) => a + b.m_currency;
    public static Currency operator /(Currency a, decimal b) => new(a.m_currency / b);
    public static Currency operator /(Currency a, Currency b) => a / b.m_currency;
    public static Currency operator *(Currency a, decimal b) => new(a.m_currency * b);
    public static Currency operator *(Currency a, Currency b) => a * b.m_currency;
    public static Currency operator %(Currency a, decimal b) => new(a.m_currency % b);
    public static Currency operator %(Currency a, Currency b) => a % b.m_currency;
    public static Currency operator -(Currency a, decimal b) => new(a.m_currency - b);
    public static Currency operator -(Currency a, Currency b) => a - b.m_currency;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Currency o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Currency other) => m_currency.CompareTo(other.m_currency);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => m_currency.ToString(format ?? "C", formatProvider ?? System.Globalization.CultureInfo.CurrentCulture);

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The <see cref="Currency.Value"/> property is the ultraviolet index.</para>
    /// </summary>
    public decimal Value => m_currency;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
