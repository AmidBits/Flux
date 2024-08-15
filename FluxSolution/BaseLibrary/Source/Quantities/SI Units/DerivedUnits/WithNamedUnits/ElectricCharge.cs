namespace Flux.Quantities
{
  public enum ElectricChargeUnit
  {
    /// <summary>This is the default unit for <see cref="ElectricCharge"/>.</summary>
    Coulomb,
  }

  /// <summary>Electric charge, unit of Coulomb.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Electric_charge"/>
  public readonly record struct ElectricCharge
    : System.IComparable, System.IComparable<ElectricCharge>, System.IFormattable, ISiPrefixValueQuantifiable<double, ElectricChargeUnit>
  {
    public static ElectricCharge ElementaryCharge => new(1.602176634e-19);

    private readonly double m_value;

    public ElectricCharge(double value, ElectricChargeUnit unit = ElectricChargeUnit.Coulomb)
      => m_value = unit switch
      {
        ElectricChargeUnit.Coulomb => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Overloaded operators

    public static bool operator <(ElectricCharge a, ElectricCharge b) => a.CompareTo(b) < 0;
    public static bool operator <=(ElectricCharge a, ElectricCharge b) => a.CompareTo(b) <= 0;
    public static bool operator >(ElectricCharge a, ElectricCharge b) => a.CompareTo(b) > 0;
    public static bool operator >=(ElectricCharge a, ElectricCharge b) => a.CompareTo(b) >= 0;

    public static ElectricCharge operator -(ElectricCharge v) => new(-v.m_value);
    public static ElectricCharge operator +(ElectricCharge a, double b) => new(a.m_value + b);
    public static ElectricCharge operator +(ElectricCharge a, ElectricCharge b) => a + b.m_value;
    public static ElectricCharge operator /(ElectricCharge a, double b) => new(a.m_value / b);
    public static ElectricCharge operator /(ElectricCharge a, ElectricCharge b) => a / b.m_value;
    public static ElectricCharge operator *(ElectricCharge a, double b) => new(a.m_value * b);
    public static ElectricCharge operator *(ElectricCharge a, ElectricCharge b) => a * b.m_value;
    public static ElectricCharge operator %(ElectricCharge a, double b) => new(a.m_value % b);
    public static ElectricCharge operator %(ElectricCharge a, ElectricCharge b) => a % b.m_value;
    public static ElectricCharge operator -(ElectricCharge a, double b) => new(a.m_value - b);
    public static ElectricCharge operator -(ElectricCharge a, ElectricCharge b) => a - b.m_value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is ElectricCharge o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(ElectricCharge other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => ToUnitValueString(ElectricChargeUnit.Coulomb, format, formatProvider);

    // ISiUnitValueQuantifiable<>
    public (MetricPrefix Prefix, ElectricChargeUnit Unit) GetSiPrefixUnit(MetricPrefix prefix) => (prefix, ElectricChargeUnit.Coulomb);

    public string GetSiPrefixSymbol(MetricPrefix prefix, bool preferUnicode, bool useFullName) => prefix.GetUnitString(preferUnicode, useFullName) + GetUnitSymbol(GetSiPrefixUnit(prefix).Unit, preferUnicode, useFullName);

    public double GetSiPrefixValue(MetricPrefix prefix) => MetricPrefix.NoPrefix.Convert(m_value, prefix);

    public string ToSiPrefixValueString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false, bool useFullName = false)
    {
      var sb = new System.Text.StringBuilder();
      sb.Append(GetSiPrefixValue(prefix).ToString(format, formatProvider));
      sb.Append(unitSpacing.ToSpacingString());
      sb.Append(GetSiPrefixSymbol(prefix, preferUnicode, useFullName));
      return sb.ToString();
    }

    // IQuantifiable<>
    /// <summary>
    /// <para>The unit of the <see cref="ElectricCharge.Value"/> property is in <see cref="ElectricChargeUnit.Ohm"/>.</para>
    /// </summary>
    public double Value => m_value;

    // IUnitQuantifiable<>
    public string GetUnitSymbol(ElectricChargeUnit unit, bool preferUnicode, bool useFullName)
      => useFullName ? unit.ToString() : unit switch
      {
        Quantities.ElectricChargeUnit.Coulomb => "C",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(ElectricChargeUnit unit)
        => unit switch
        {
          ElectricChargeUnit.Coulomb => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

    public string ToUnitValueString(ElectricChargeUnit unit = ElectricChargeUnit.Coulomb, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false, bool useFullName = false)
    {
      var sb = new System.Text.StringBuilder();
      sb.Append(GetUnitValue(unit).ToString(format, formatProvider));
      sb.Append(unitSpacing.ToSpacingString());
      sb.Append(GetUnitSymbol(unit, preferUnicode, useFullName));
      return sb.ToString();
    }

    #endregion Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
