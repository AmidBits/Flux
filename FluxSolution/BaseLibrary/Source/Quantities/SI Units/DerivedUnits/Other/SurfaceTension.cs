namespace Flux.Quantities
{
  public enum SurfaceTensionUnit
  {
    /// <summary>This is the default unit for <see cref="SurfaceTension"/>.</summary>
    NewtonPerMeter,
  }

  /// <summary>Surface tension, unit of Newton per meter.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Surface_tension"/>
  public readonly record struct SurfaceTension
    : System.IComparable, System.IComparable<SurfaceTension>, System.IFormattable, IUnitValueQuantifiable<double, SurfaceTensionUnit>
  {
    private readonly double m_value;

    public SurfaceTension(double value, SurfaceTensionUnit unit = SurfaceTensionUnit.NewtonPerMeter)
      => m_value = unit switch
      {
        SurfaceTensionUnit.NewtonPerMeter => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public SurfaceTension(Force force, Length length) : this(force.Value / length.Value) { }

    public SurfaceTension(Energy energy, Area area) : this(energy.Value / area.Value) { }

    #region Static methods

    #endregion // Static methods

    #region Overloaded operators

    public static bool operator <(SurfaceTension a, SurfaceTension b) => a.CompareTo(b) < 0;
    public static bool operator <=(SurfaceTension a, SurfaceTension b) => a.CompareTo(b) <= 0;
    public static bool operator >(SurfaceTension a, SurfaceTension b) => a.CompareTo(b) > 0;
    public static bool operator >=(SurfaceTension a, SurfaceTension b) => a.CompareTo(b) >= 0;

    public static SurfaceTension operator -(SurfaceTension v) => new(-v.m_value);
    public static SurfaceTension operator +(SurfaceTension a, double b) => new(a.m_value + b);
    public static SurfaceTension operator +(SurfaceTension a, SurfaceTension b) => a + b.m_value;
    public static SurfaceTension operator /(SurfaceTension a, double b) => new(a.m_value / b);
    public static SurfaceTension operator /(SurfaceTension a, SurfaceTension b) => a / b.m_value;
    public static SurfaceTension operator *(SurfaceTension a, double b) => new(a.m_value * b);
    public static SurfaceTension operator *(SurfaceTension a, SurfaceTension b) => a * b.m_value;
    public static SurfaceTension operator %(SurfaceTension a, double b) => new(a.m_value % b);
    public static SurfaceTension operator %(SurfaceTension a, SurfaceTension b) => a % b.m_value;
    public static SurfaceTension operator -(SurfaceTension a, double b) => new(a.m_value - b);
    public static SurfaceTension operator -(SurfaceTension a, SurfaceTension b) => a - b.m_value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is SurfaceTension o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(SurfaceTension other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(SurfaceTensionUnit.NewtonPerMeter, format, formatProvider);

    // IQuantifiable<>
    /// <summary>
    /// <para>The unit of the <see cref="SurfaceTension.Value"/> property is in <see cref="SurfaceTensionUnit.NewtonPerMeter"/>.</para>
    /// </summary>
    public double Value => m_value;

    // IUnitQuantifiable<>
    public string GetUnitName(SurfaceTensionUnit unit, bool preferPlural)
      => unit.ToString().ConvertUnitNameToPlural(preferPlural && GetUnitValue(unit).IsConsideredPlural());

    public string GetUnitSymbol(SurfaceTensionUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.SurfaceTensionUnit.NewtonPerMeter => "N/m",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(SurfaceTensionUnit unit)
      => unit switch
      {
        SurfaceTensionUnit.NewtonPerMeter => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public string ToUnitString(SurfaceTensionUnit unit = SurfaceTensionUnit.NewtonPerMeter, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, true) : GetUnitSymbol(unit, false));

    //public string ToUnitValueNameString(SurfaceTensionUnit unit = SurfaceTensionUnit.NewtonPerMeter, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferPlural = false)
    //  => GetUnitValue(unit).ToString(format, formatProvider) + unitSpacing.ToSpacingString() + GetUnitName(unit, preferPlural);

    //public string ToUnitValueSymbolString(SurfaceTensionUnit unit = SurfaceTensionUnit.NewtonPerMeter, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false)
    //  => GetUnitValue(unit).ToString(format, formatProvider) + unitSpacing.ToSpacingString() + GetUnitSymbol(unit, preferUnicode);

    #endregion Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
