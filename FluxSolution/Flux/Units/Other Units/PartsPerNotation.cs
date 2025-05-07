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

        _ => GetUnitFactor(unit) * value,
      };

    public static double ConvertToUnit(PartsPerNotationUnit unit, double value)
      => unit switch
      {
        PartsPerNotationUnit.One => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double ConvertUnit(double value, PartsPerNotationUnit from, PartsPerNotationUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static double GetUnitFactor(PartsPerNotationUnit unit)
      => unit switch
      {
        PartsPerNotationUnit.One => 1,

        PartsPerNotationUnit.Percent => 1e2,
        PartsPerNotationUnit.PerMille => 1e3,
        PartsPerNotationUnit.PerMyriad => 1e4,
        PartsPerNotationUnit.PerCentMille => 1e5,
        PartsPerNotationUnit.PartPerMillion => 1e6,
        PartsPerNotationUnit.PartPerBillion => 1e9,
        PartsPerNotationUnit.PartPerTrillion => 1e12,
        PartsPerNotationUnit.PartPerQuadrillion => 1e15,

        _ => throw new System.NotImplementedException()
      };

    public static string GetUnitName(PartsPerNotationUnit unit, bool preferPlural) => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(PartsPerNotationUnit unit, bool preferUnicode)
      => unit switch
      {
        Units.PartsPerNotationUnit.PartPerQuadrillion => "ppq",
        Units.PartsPerNotationUnit.PartPerTrillion => "ppt",
        Units.PartsPerNotationUnit.PartPerBillion => "ppb",
        Units.PartsPerNotationUnit.PartPerMillion => preferUnicode ? "\u33D9" : "ppm",
        Units.PartsPerNotationUnit.PerCentMille => "pcm",
        Units.PartsPerNotationUnit.PerMyriad => "\u2031",
        Units.PartsPerNotationUnit.PerMille => "\u2030",
        Units.PartsPerNotationUnit.Percent => "\u0025",
        Units.PartsPerNotationUnit.One => "pp1",
        _ => string.Empty,
      };

    public double GetUnitValue(PartsPerNotationUnit unit) => ConvertToUnit(unit, m_parts);

    public string ToUnitString(PartsPerNotationUnit unit = PartsPerNotationUnit.One, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + Unicode.UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, GetUnitValue(unit).IsConsideredPlural()) : GetUnitSymbol(unit, false));

    #endregion // IUnitValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
