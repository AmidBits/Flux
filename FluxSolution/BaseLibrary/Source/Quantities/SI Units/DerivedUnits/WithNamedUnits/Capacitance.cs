namespace Flux.Quantities
{
  public enum CapacitanceUnit
  {
    /// <summary>This is the default unit for <see cref="Capacitance"/>.</summary>
    Farad,
    MicroFarad,
    NanoFarad,
    PicoFarad,
  }

  /// <summary>Electrical capacitance unit of Farad.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Capacitance"/>
  public readonly record struct Capacitance
    : System.IComparable, System.IComparable<Capacitance>, System.IFormattable, ISiPrefixValueQuantifiable<double, CapacitanceUnit>
  {
    private readonly double m_value;

    public Capacitance(double value, CapacitanceUnit unit = CapacitanceUnit.Farad)
      => m_value = unit switch
      {
        CapacitanceUnit.Farad => value,
        CapacitanceUnit.MicroFarad => value * 1000000,
        CapacitanceUnit.NanoFarad => value * 1000000000,
        CapacitanceUnit.PicoFarad => value * 1000000000000,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Overloaded operators

    public static bool operator <(Capacitance a, Capacitance b) => a.CompareTo(b) < 0;
    public static bool operator <=(Capacitance a, Capacitance b) => a.CompareTo(b) <= 0;
    public static bool operator >(Capacitance a, Capacitance b) => a.CompareTo(b) > 0;
    public static bool operator >=(Capacitance a, Capacitance b) => a.CompareTo(b) >= 0;

    public static Capacitance operator -(Capacitance v) => new(-v.m_value);
    public static Capacitance operator +(Capacitance a, double b) => new(a.m_value + b);
    public static Capacitance operator +(Capacitance a, Capacitance b) => a + b.m_value;
    public static Capacitance operator /(Capacitance a, double b) => new(a.m_value / b);
    public static Capacitance operator /(Capacitance a, Capacitance b) => a / b.m_value;
    public static Capacitance operator *(Capacitance a, double b) => new(a.m_value * b);
    public static Capacitance operator *(Capacitance a, Capacitance b) => a * b.m_value;
    public static Capacitance operator %(Capacitance a, double b) => new(a.m_value % b);
    public static Capacitance operator %(Capacitance a, Capacitance b) => a % b.m_value;
    public static Capacitance operator -(Capacitance a, double b) => new(a.m_value - b);
    public static Capacitance operator -(Capacitance a, Capacitance b) => a - b.m_value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Capacitance o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Capacitance other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToSiPrefixString(MetricPrefix.Unprefixed);

    // ISiUnitValueQuantifiable<>
    public string GetSiPrefixName(MetricPrefix prefix, bool preferPlural) => prefix.GetPrefixName() + GetUnitName(CapacitanceUnit.Farad, preferPlural);

    public string GetSiPrefixSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetPrefixSymbol(preferUnicode) + GetUnitSymbol(CapacitanceUnit.Farad, preferUnicode);

    public double GetSiPrefixValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiPrefixString(MetricPrefix prefix, bool fullName = false)
      => GetSiPrefixValue(prefix).ToSiFormattedString() + UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiPrefixName(prefix, true) : GetSiPrefixSymbol(prefix, false));

    //public string ToSiPrefixValueNameString(MetricPrefix prefix, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferPlural = true)
    //  => GetSiPrefixValue(prefix).ToSiFormattedString() + unitSpacing.ToSpacingString() + GetSiPrefixName(prefix, preferPlural);

    //public string ToSiPrefixValueSymbolString(MetricPrefix prefix, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false)
    //  => GetSiPrefixValue(prefix).ToSiFormattedString() + unitSpacing.ToSpacingString() + GetSiPrefixSymbol(prefix, preferUnicode);

    // IQuantifiable<>
    /// <summary>
    /// <para>The unit of the <see cref="Capacitance.Value"/> property is in <see cref="CapacitanceUnit.Farad"/>.</para>
    /// </summary>
    public double Value => m_value;

    // IUnitQuantifiable<>
    public string GetUnitName(CapacitanceUnit unit, bool preferPlural) => unit.ToString().ConvertUnitNameToPlural(preferPlural && GetUnitValue(unit).IsConsideredPlural());

    public string GetUnitSymbol(CapacitanceUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.CapacitanceUnit.Farad => "F",
        Quantities.CapacitanceUnit.MicroFarad => preferUnicode ? "\u338C" : "\u00B5F",
        Quantities.CapacitanceUnit.NanoFarad => preferUnicode ? "\u338B" : "nF",
        Quantities.CapacitanceUnit.PicoFarad => preferUnicode ? "\u338A" : "pF",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(CapacitanceUnit unit)
      => unit switch
      {
        CapacitanceUnit.Farad => m_value,
        CapacitanceUnit.MicroFarad => m_value / 1000000,
        CapacitanceUnit.NanoFarad => m_value / 1000000000,
        CapacitanceUnit.PicoFarad => m_value / 1000000000000,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public string ToUnitString(CapacitanceUnit unit = CapacitanceUnit.Farad, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, true) : GetUnitSymbol(unit, false));

    //public string ToUnitValueNameString(CapacitanceUnit unit = CapacitanceUnit.Farad, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferPlural = false)
    //  => GetUnitValue(unit).ToString(format, formatProvider) + unitSpacing.ToSpacingString() + GetUnitName(unit, preferPlural);

    //public string ToUnitValueSymbolString(CapacitanceUnit unit = CapacitanceUnit.Farad, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false)
    //  => GetUnitValue(unit).ToString(format, formatProvider) + unitSpacing.ToSpacingString() + GetUnitSymbol(unit, preferUnicode);

    #endregion Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
