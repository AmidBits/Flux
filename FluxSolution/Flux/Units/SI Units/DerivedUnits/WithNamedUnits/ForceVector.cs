namespace Flux.Units
{
  /// <summary>
  /// <para>ForceVector, a vector quantity, unit of newton. This is an SI derived quantity.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Force"/></para>
  /// </summary>
  public readonly record struct ForceVector
    : System.IFormattable, ISiUnitValueQuantifiable<System.Runtime.Intrinsics.Vector256<double>, ForceUnit>
  {
    private readonly System.Runtime.Intrinsics.Vector256<double> m_value;

    public ForceVector(System.Runtime.Intrinsics.Vector256<double> value, ForceUnit unit = ForceUnit.Newton) => m_value = ConvertFromUnit(unit, value);

    public ForceVector(double valueX, double valueY, double valueZ, double valueW, ForceUnit unit = ForceUnit.Newton)
      : this(System.Runtime.Intrinsics.Vector256.Create(valueX, valueY, valueZ, valueW), unit) { }

    public ForceVector(MetricPrefix prefix, System.Runtime.Intrinsics.Vector256<double> NewtonSquare) => m_value = prefix.ConvertTo(NewtonSquare, MetricPrefix.Unprefixed);

    public ForceVector(MetricPrefix prefix, double NewtonSquareX, double NewtonSquareY, double NewtonSquareZ, double NewtonSquareW)
      : this(prefix, System.Runtime.Intrinsics.Vector256.Create(NewtonSquareX, NewtonSquareY, NewtonSquareZ, NewtonSquareW)) { }

    public double X => m_value[0];
    public double Y => m_value[1];
    public double Z => m_value[2];
    public double W => m_value[3];

    #region Overloaded operators

    public static ForceVector operator -(ForceVector v) => new(v.m_value.Negate());
    public static ForceVector operator +(ForceVector a, double b) => new(a.m_value.Add(b));
    public static ForceVector operator +(ForceVector a, ForceVector b) => new(a.m_value.Add(b.m_value));
    public static ForceVector operator /(ForceVector a, double b) => new(a.m_value.Divide(b));
    public static ForceVector operator /(ForceVector a, ForceVector b) => new(a.m_value.Divide(b.m_value));
    public static ForceVector operator *(ForceVector a, double b) => new(a.m_value.Multiply(b));
    public static ForceVector operator *(ForceVector a, ForceVector b) => new(a.m_value.Multiply(b.m_value));
    public static ForceVector operator %(ForceVector a, double b) => new(a.m_value.Remainder(b));
    public static ForceVector operator %(ForceVector a, ForceVector b) => new(a.m_value.Remainder(b.m_value));
    public static ForceVector operator -(ForceVector a, double b) => new(a.m_value.Subtract(b));
    public static ForceVector operator -(ForceVector a, ForceVector b) => new(a.m_value.Subtract(b.m_value));

    #endregion Overloaded operators

    #region Implemented interfaces

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(ForceUnit.Newton, format, formatProvider);

    #region ISiUnitValueQuantifiable<>

    public static string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetMetricPrefixName() + GetUnitName(ForceUnit.Newton, preferPlural);

    public static string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetMetricPrefixSymbol(preferUnicode) + GetUnitSymbol(ForceUnit.Newton, preferUnicode);

    public System.Runtime.Intrinsics.Vector256<double> GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, bool fullName = false)
      => GetSiUnitValue(prefix).ToString() + Unicode.UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiUnitName(prefix, true) : GetSiUnitSymbol(prefix, false));

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static System.Runtime.Intrinsics.Vector256<double> ConvertFromUnit(ForceUnit unit, System.Runtime.Intrinsics.Vector256<double> value)
      => unit switch
      {
        ForceUnit.Newton => value,

        _ => GetUnitFactor(unit) * value,
      };

    public static System.Runtime.Intrinsics.Vector256<double> ConvertToUnit(ForceUnit unit, System.Runtime.Intrinsics.Vector256<double> value)
      => unit switch
      {
        ForceUnit.Newton => value,

        _ => value / GetUnitFactor(unit),
      };

    public static System.Runtime.Intrinsics.Vector256<double> ConvertUnit(System.Runtime.Intrinsics.Vector256<double> value, ForceUnit from, ForceUnit to)
      => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static System.Runtime.Intrinsics.Vector256<double> GetUnitFactor(ForceUnit unit) => System.Runtime.Intrinsics.Vector256.Create(Force.GetUnitFactor(unit));

    public static string GetUnitName(ForceUnit unit, bool preferPlural) => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(ForceUnit unit, bool preferUnicode) => Force.GetUnitSymbol(unit, preferUnicode);

    public System.Runtime.Intrinsics.Vector256<double> GetUnitValue(ForceUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(ForceUnit unit = ForceUnit.Newton, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString() + Unicode.UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, true) : GetUnitSymbol(unit, false));

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="ForceVector.Value"/> property is in <see cref="ForceUnit.NewtonSquared"/>.</para>
    /// </summary>
    public System.Runtime.Intrinsics.Vector256<double> Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
