namespace Flux.Quantities
{
  /// <summary>
  /// <para>Radix, unit of natural number, in the closed interval [<see cref="Radix.MinRadix"/> = 2, <see cref="Radix.MaxRadix"/> = 256].</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Radix"/></para>
  /// </summary>
  public readonly record struct Radix
    : System.IComparable, System.IComparable<Radix>, System.IFormattable, IValueQuantifiable<int>
  {
    public const string Base64 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz+/";

    public const int MaxValue = 256;
    public const int MinValue = 2;

    public static Radix Binary => new(2);
    public static Radix Octal => new(8);
    public static Radix Decimal => new(10);
    public static Radix Hexadecimal => new(16);

    private readonly int m_value;

    public Radix(int radix) => m_value = IntervalNotation.Closed.AssertValidMember(radix, MinValue, MaxValue, nameof(radix));

    #region Static methods

    /// <summary>
    /// <para>Asserts that the <paramref name="radix"/> with an <paramref name="alternativeMaxRadix"/> is valid, i.e. an integer in the range [<see cref="MinValue"/>, <paramref name="alternativeMaxRadix"/>], and throws an exception if it's not.</para>
    /// </summary>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TSelf AssertMember<TSelf>(TSelf radix, TSelf alternativeMaxRadix, string? paramName = null)
      where TSelf : System.Numerics.INumber<TSelf>
      => VerifyMember(radix, alternativeMaxRadix)
      ? radix
      : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(radix), $"The radix ({radix}) is out of range: {IntervalNotation.Closed.ToNotationString(TSelf.CreateChecked(MinValue), alternativeMaxRadix)}.");

    /// <summary>
    /// <para>Asserts that the <paramref name="radix"/> is valid, i.e. an integer in the range [<see cref="MinValue"/>, <see cref="MaxValue"/>], and throws an exception if it's not.</para>
    /// </summary>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TSelf AssertMember<TSelf>(TSelf radix, string? paramName = null)
      where TSelf : System.Numerics.INumber<TSelf>
      => VerifyMember(radix)
      ? radix
      : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(radix), $"The radix ({radix}) is out of range: {IntervalNotation.Closed.ToNotationString(TSelf.CreateChecked(MinValue), TSelf.CreateChecked(MaxValue))}.");

    /// <summary>
    /// <para>Determines whether the <paramref name="radix"/> is valid, i.e. an integer in the range [<see cref="MinValue"/>, <paramref name="alternativeMaxRadix"/>].</para>
    /// </summary>
    public static bool VerifyMember<TSelf>(TSelf radix, TSelf alternativeMaxRadix)
      where TSelf : System.Numerics.INumber<TSelf>
      => TSelf.IsInteger(radix) && TSelf.CreateChecked(MinValue) is var minRadix && alternativeMaxRadix >= minRadix && IntervalNotation.Closed.IsValidMember(radix, minRadix, alternativeMaxRadix);

    /// <summary>
    /// <para>Determines whether the <paramref name="radix"/> is valid, i.e. an integer in the range [<see cref="MinValue"/>, <see cref="MaxValue"/>].</para>
    /// </summary>
    public static bool VerifyMember<TSelf>(TSelf radix)
      where TSelf : System.Numerics.INumber<TSelf>
      => VerifyMember(radix, TSelf.CreateChecked(MaxValue));

    #endregion Static methods

    #region Overloaded operators

    public static bool operator <(Radix a, Radix b) => a.CompareTo(b) < 0;
    public static bool operator <=(Radix a, Radix b) => a.CompareTo(b) <= 0;
    public static bool operator >(Radix a, Radix b) => a.CompareTo(b) > 0;
    public static bool operator >=(Radix a, Radix b) => a.CompareTo(b) >= 0;

    public static Radix operator -(Radix v) => new(-v.m_value);
    public static Radix operator +(Radix a, int b) => new(a.m_value + b);
    public static Radix operator +(Radix a, Radix b) => a + b.m_value;
    public static Radix operator /(Radix a, int b) => new(a.m_value / b);
    public static Radix operator /(Radix a, Radix b) => a / b.m_value;
    public static Radix operator *(Radix a, int b) => new(a.m_value * b);
    public static Radix operator *(Radix a, Radix b) => a * b.m_value;
    public static Radix operator %(Radix a, int b) => new(a.m_value % b);
    public static Radix operator %(Radix a, Radix b) => a % b.m_value;
    public static Radix operator -(Radix a, int b) => new(a.m_value - b);
    public static Radix operator -(Radix a, Radix b) => a - b.m_value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable<>
    public int CompareTo(Radix other) => m_value.CompareTo(other.m_value);

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Radix o ? CompareTo(o) : -1;

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => string.Format(formatProvider, $"{{0{(format is null ? string.Empty : ':' + format)}}}", m_value);

    #region IQuantifiable<>

    /// <summary>
    /// <para>The <see cref="Radix.Value"/> property is a radix in the closed interval [<see cref="MinRadix"/>, <see cref="MaxRadix"/>].</para>
    /// </summary>
    public int Value => m_value;

    #endregion // IQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
