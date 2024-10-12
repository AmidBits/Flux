namespace Flux.Quantities
{
  public enum ImpulseUnit
  {
    /// <summary>This is the default unit for <see cref="Impulse"/>.</summary>
    NewtonSecond,
  }

  /// <summary>Impulse, unit of Newton second.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Impulse"/>
  public readonly record struct Impulse
    : System.IComparable, System.IComparable<Impulse>, System.IFormattable, ISiUnitValueQuantifiable<double, ImpulseUnit>
  {
    private readonly double m_value;

    public Impulse(double value, ImpulseUnit unit = ImpulseUnit.NewtonSecond) => m_value = ConvertToUnit(unit, value);

    public Impulse(MetricPrefix prefix, double newtonSecond) => m_value = prefix.ConvertTo(newtonSecond, MetricPrefix.Unprefixed);

    public Impulse(Force force, Time time) : this(force.Value / time.Value) { }

    #region Static methods

    #endregion // Static methods

    #region Overloaded operators

    public static bool operator <(Impulse a, Impulse b) => a.CompareTo(b) < 0;
    public static bool operator <=(Impulse a, Impulse b) => a.CompareTo(b) <= 0;
    public static bool operator >(Impulse a, Impulse b) => a.CompareTo(b) > 0;
    public static bool operator >=(Impulse a, Impulse b) => a.CompareTo(b) >= 0;

    public static Impulse operator -(Impulse v) => new(-v.m_value);
    public static Impulse operator +(Impulse a, double b) => new(a.m_value + b);
    public static Impulse operator +(Impulse a, Impulse b) => a + b.m_value;
    public static Impulse operator /(Impulse a, double b) => new(a.m_value / b);
    public static Impulse operator /(Impulse a, Impulse b) => a / b.m_value;
    public static Impulse operator *(Impulse a, double b) => new(a.m_value * b);
    public static Impulse operator *(Impulse a, Impulse b) => a * b.m_value;
    public static Impulse operator %(Impulse a, double b) => new(a.m_value % b);
    public static Impulse operator %(Impulse a, Impulse b) => a % b.m_value;
    public static Impulse operator -(Impulse a, double b) => new(a.m_value - b);
    public static Impulse operator -(Impulse a, Impulse b) => a - b.m_value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Impulse o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Impulse other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(ImpulseUnit.NewtonSecond, format, formatProvider);

    #region ISiUnitValueQuantifiable<>

    public string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetPrefixName() + GetUnitName(ImpulseUnit.NewtonSecond, preferPlural);

    public string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetPrefixSymbol(preferUnicode) + GetUnitSymbol(ImpulseUnit.NewtonSecond, preferUnicode);

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, bool fullName = false)
      => GetSiUnitValue(prefix).ToSiFormattedString() + UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiUnitName(prefix, true) : GetSiUnitSymbol(prefix, false));

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    private static double ConvertFromUnit(ImpulseUnit unit, double value)
      => unit switch
      {
        ImpulseUnit.NewtonSecond => value,

        _ => GetUnitFactor(unit) * value,
      };

    private static double ConvertToUnit(ImpulseUnit unit, double value)
      => unit switch
      {
        ImpulseUnit.NewtonSecond => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double ConvertUnit(double value, ImpulseUnit from, ImpulseUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static double GetUnitFactor(ImpulseUnit unit)
      => unit switch
      {
        ImpulseUnit.NewtonSecond => 1,

        _ => throw new System.NotImplementedException()
      };

    public string GetUnitName(ImpulseUnit unit, bool preferPlural)
      => unit.ToString().ConvertUnitNameToPlural(preferPlural && GetUnitValue(unit).IsConsideredPlural());

    public string GetUnitSymbol(ImpulseUnit unit, bool preferUnicode)
      => unit switch
      {
        ImpulseUnit.NewtonSecond => preferUnicode ? "N\u22C5s" : "N·s",

        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(ImpulseUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(ImpulseUnit unit = ImpulseUnit.NewtonSecond, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, true) : GetUnitSymbol(unit, false));

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="Impulse.Value"/> property is in <see cref="ImpulseUnit.NewtonSecond"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
