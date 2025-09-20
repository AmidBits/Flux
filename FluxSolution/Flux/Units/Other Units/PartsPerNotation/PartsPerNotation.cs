namespace Flux.Units
{
  /// <summary>
  /// <para>Parts per notation.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Parts-per_notation"/></para>
  /// </summary>
  /// <remarks>In science and engineering, the parts-per notation is a set of pseudo-units to describe small values of miscellaneous dimensionless quantities, e.g. mole fraction or mass fraction. Since these fractions are quantity-per-quantity measures, they are pure numbers with no associated units of measurement.</remarks>
  public readonly record struct PartsPerNotation
    : System.IComparable, System.IComparable<PartsPerNotation>, System.IFormattable, IUnitValueQuantifiable<double, PartsPerNotationUnit>
  {
    private readonly double m_parts;

    /// <summary>Creates a new instance of this type.</summary>
    /// <param name="parts">The parts in parts per notation.</param>
    /// <param name="unit">The notation in parts per notation.</param>
    public PartsPerNotation(double parts, PartsPerNotationUnit unit = PartsPerNotationUnit.Percent) => m_parts = ConvertToUnit(unit, parts);

    public PartsPerNotation(MetricPrefix prefix, double percent) => m_parts = prefix.ChangePrefix(percent, MetricPrefix.Unprefixed);

    #region Static methods
    #endregion Static methods

    #region Overloaded operators

    public static bool operator <(PartsPerNotation a, PartsPerNotation b) => a.CompareTo(b) < 0;
    public static bool operator <=(PartsPerNotation a, PartsPerNotation b) => a.CompareTo(b) <= 0;
    public static bool operator >(PartsPerNotation a, PartsPerNotation b) => a.CompareTo(b) > 0;
    public static bool operator >=(PartsPerNotation a, PartsPerNotation b) => a.CompareTo(b) >= 0;

    public static PartsPerNotation operator -(PartsPerNotation v) => new(-v.m_parts);
    public static PartsPerNotation operator +(PartsPerNotation a, double b) => new(a.m_parts + b);
    public static PartsPerNotation operator +(PartsPerNotation a, PartsPerNotation b) => a + b.m_parts;
    public static PartsPerNotation operator /(PartsPerNotation a, double b) => new(a.m_parts / b);
    public static PartsPerNotation operator /(PartsPerNotation a, PartsPerNotation b) => a / b.m_parts;
    public static PartsPerNotation operator *(PartsPerNotation a, double b) => new(a.m_parts * b);
    public static PartsPerNotation operator *(PartsPerNotation a, PartsPerNotation b) => a * b.m_parts;
    public static PartsPerNotation operator %(PartsPerNotation a, double b) => new(a.m_parts % b);
    public static PartsPerNotation operator %(PartsPerNotation a, PartsPerNotation b) => a % b.m_parts;
    public static PartsPerNotation operator -(PartsPerNotation a, double b) => new(a.m_parts - b);
    public static PartsPerNotation operator -(PartsPerNotation a, PartsPerNotation b) => a - b.m_parts;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is PartsPerNotation o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(PartsPerNotation other) => m_parts.CompareTo(other.m_parts);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(PartsPerNotationUnit.Percent, format, formatProvider);

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="PartsPerNotation.Value"/> property is in <see cref="PartsPerNotationUnit.Percent"/>.</para>
    /// </summary>
    public double Value => m_parts;

    #endregion // IValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(PartsPerNotationUnit unit, double value)
      => unit switch
      {
        PartsPerNotationUnit.One => value,

        _ => unit.GetUnitFactor() * value,
      };

    public static double ConvertToUnit(PartsPerNotationUnit unit, double value)
      => unit switch
      {
        PartsPerNotationUnit.One => value,

        _ => value / unit.GetUnitFactor(),
      };

    public static double ConvertUnit(double value, PartsPerNotationUnit from, PartsPerNotationUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public double GetUnitValue(PartsPerNotationUnit unit) => ConvertToUnit(unit, m_parts);

    public string ToUnitString(PartsPerNotationUnit unit = PartsPerNotationUnit.One, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing spacing = UnicodeSpacing.Space, bool fullName = false)
    {
      var value = GetUnitValue(unit);

      return value.ToString(format, formatProvider) + spacing.ToSpacingString() + (fullName ? unit.GetUnitName(value.IsConsideredPlural()) : unit.GetUnitSymbol(false));
    }

    #endregion // IUnitValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
