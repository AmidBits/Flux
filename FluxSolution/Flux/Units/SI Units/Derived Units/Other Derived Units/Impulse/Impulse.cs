namespace Flux.Units
{
  /// <summary>
  /// <para>Impulse, unit of Newton second.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Impulse"/></para>
  /// </summary>
  public readonly record struct Impulse
    : System.IComparable, System.IComparable<Impulse>, System.IFormattable, ISiUnitValueQuantifiable<double, ImpulseUnit>
  {
    private readonly double m_value;

    public Impulse(double value, ImpulseUnit unit = ImpulseUnit.NewtonSecond) => m_value = ConvertToUnit(unit, value);

    public Impulse(MetricPrefix prefix, double newtonSecond) => m_value = prefix.ConvertPrefix(newtonSecond, MetricPrefix.Unprefixed);

    public Impulse(Force force, Time time) : this(force.Value / time.Value) { }

    #region Static methods

    #endregion // Static methods

    #region Overloaded operators

    public static bool operator <(Impulse a, Impulse b) => a.CompareTo(b) < 0;
    public static bool operator >(Impulse a, Impulse b) => a.CompareTo(b) > 0;
    public static bool operator <=(Impulse a, Impulse b) => a.CompareTo(b) <= 0;
    public static bool operator >=(Impulse a, Impulse b) => a.CompareTo(b) >= 0;

    public static Impulse operator -(Impulse v) => new(-v.m_value);
    public static Impulse operator *(Impulse a, Impulse b) => new(a.m_value * b.m_value);
    public static Impulse operator /(Impulse a, Impulse b) => new(a.m_value / b.m_value);
    public static Impulse operator %(Impulse a, Impulse b) => new(a.m_value % b.m_value);
    public static Impulse operator +(Impulse a, Impulse b) => new(a.m_value + b.m_value);
    public static Impulse operator -(Impulse a, Impulse b) => new(a.m_value - b.m_value);
    public static Impulse operator *(Impulse a, double b) => new(a.m_value * b);
    public static Impulse operator /(Impulse a, double b) => new(a.m_value / b);
    public static Impulse operator %(Impulse a, double b) => new(a.m_value % b);
    public static Impulse operator +(Impulse a, double b) => new(a.m_value + b);
    public static Impulse operator -(Impulse a, double b) => new(a.m_value - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Impulse o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Impulse other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(ImpulseUnit.NewtonSecond, format, formatProvider);

    #region ISiUnitValueQuantifiable<>

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertPrefix(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null)
      => GetSiUnitValue(prefix).ToSiFormattedString(format, formatProvider) + UnicodeSpacing.ThinSpace.ToSpacingString() + prefix.GetMetricPrefixSymbol() + ImpulseUnit.NewtonSecond.GetUnitSymbol();

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(ImpulseUnit unit, double value)
      => unit switch
      {
        ImpulseUnit.NewtonSecond => value,

        _ => unit.GetUnitFactor() * value,
      };

    public static double ConvertToUnit(ImpulseUnit unit, double value)
      => unit switch
      {
        ImpulseUnit.NewtonSecond => value,

        _ => value / unit.GetUnitFactor(),
      };

    public static double ConvertUnit(double value, ImpulseUnit from, ImpulseUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public double GetUnitValue(ImpulseUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(ImpulseUnit unit = ImpulseUnit.NewtonSecond, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing spacing = UnicodeSpacing.Space, bool fullName = false)
    {
      var value = GetUnitValue(unit);

      return value.ToString(format, formatProvider) + spacing.ToSpacingString() + (fullName ? unit.GetUnitName(INumber.IsConsideredPlural(value)) : unit.GetUnitSymbol(false));
    }

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
