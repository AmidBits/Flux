namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Quantities.ElectricCurrentUnit unit, bool preferUnicode = false, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Quantities.ElectricCurrentUnit.Ampere => "A",
        Quantities.ElectricCurrentUnit.Milliampere => preferUnicode ? "\u3383" : "mA",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Quantities
  {
    public enum ElectricCurrentUnit
    {
      /// <summary>This is the default unit for <see cref="ElectricCurrent"/>.</summary>
      Ampere,
      Milliampere,
    }

    /// <summary>
    /// <para>Electric current. SI unit of ampere. This is a base quantity.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Electric_current"/></para>
    /// </summary>
    public readonly record struct ElectricCurrent
      : System.IComparable, System.IComparable<ElectricCurrent>, System.IFormattable, ISiPrefixValueQuantifiable<double, ElectricCurrentUnit>
    {
      private readonly double m_value;

      public ElectricCurrent(double value, ElectricCurrentUnit unit = ElectricCurrentUnit.Ampere)
        => m_value = unit switch
        {
          ElectricCurrentUnit.Ampere => value,
          ElectricCurrentUnit.Milliampere => value / 1000,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      /// <summary>
      /// <para>Creates a new instance from the specified <see cref="MetricPrefix"/> (metric multiple) of <see cref="ElectricCurrentUnit.Ampere"/>, e.g. <see cref="MetricPrefix.Milli"/> for milliamperes.</para>
      /// </summary>
      /// <param name="amperes"></param>
      /// <param name="prefix"></param>
      public ElectricCurrent(double amperes, MetricPrefix prefix) => m_value = prefix.Convert(amperes, MetricPrefix.NoPrefix);

      #region Static methods

      /// <summary>Creates a new ElectricCurrent instance from power and voltage.</summary>
      /// <param name="power"></param>
      /// <param name="voltage"></param>
      public static ElectricCurrent From(Power power, Voltage voltage) => new(power.Value / voltage.Value);

      /// <summary>Creates a new ElectricCurrent instance from voltage and resistance.</summary>
      /// <param name="voltage"></param>
      /// <param name="resistance"></param>
      public static ElectricCurrent From(Voltage voltage, ElectricalResistance resistance) => new(voltage.Value / resistance.Value);

      #endregion Static methods

      #region Overloaded operators

      public static bool operator <(ElectricCurrent a, ElectricCurrent b) => a.CompareTo(b) < 0;
      public static bool operator <=(ElectricCurrent a, ElectricCurrent b) => a.CompareTo(b) <= 0;
      public static bool operator >(ElectricCurrent a, ElectricCurrent b) => a.CompareTo(b) > 0;
      public static bool operator >=(ElectricCurrent a, ElectricCurrent b) => a.CompareTo(b) >= 0;

      public static ElectricCurrent operator -(ElectricCurrent v) => new(-v.m_value);
      public static ElectricCurrent operator +(ElectricCurrent a, double b) => new(a.m_value + b);
      public static ElectricCurrent operator +(ElectricCurrent a, ElectricCurrent b) => a + b.m_value;
      public static ElectricCurrent operator /(ElectricCurrent a, double b) => new(a.m_value / b);
      public static ElectricCurrent operator /(ElectricCurrent a, ElectricCurrent b) => a / b.m_value;
      public static ElectricCurrent operator *(ElectricCurrent a, double b) => new(a.m_value * b);
      public static ElectricCurrent operator *(ElectricCurrent a, ElectricCurrent b) => a * b.m_value;
      public static ElectricCurrent operator %(ElectricCurrent a, double b) => new(a.m_value % b);
      public static ElectricCurrent operator %(ElectricCurrent a, ElectricCurrent b) => a % b.m_value;
      public static ElectricCurrent operator -(ElectricCurrent a, double b) => new(a.m_value - b);
      public static ElectricCurrent operator -(ElectricCurrent a, ElectricCurrent b) => a - b.m_value;

      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is ElectricCurrent o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(ElectricCurrent other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => ToUnitValueString(ElectricCurrentUnit.Ampere, format, formatProvider);

      //IMetricMultiplicable<>
      public ElectricCurrentUnit BaseUnit => ElectricCurrentUnit.Ampere;

      public ElectricCurrentUnit UnprefixedUnit => ElectricCurrentUnit.Ampere;

      public string GetSiPrefixSymbol(MetricPrefix prefix, bool preferUnicode, bool useFullName) => prefix.GetUnitString(preferUnicode, useFullName) + GetUnitSymbol(UnprefixedUnit, preferUnicode, useFullName);

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
      /// <para>The unit of the <see cref="ElectricCurrent.Value"/> property is in <see cref="ElectricCurrentUnit.Ampere"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public string GetUnitSymbol(ElectricCurrentUnit unit, bool preferUnicode, bool useFullName) => unit.GetUnitString(preferUnicode, useFullName);

      public double GetUnitValue(ElectricCurrentUnit unit)
        => unit switch
        {
          ElectricCurrentUnit.Milliampere => m_value * 1000,
          ElectricCurrentUnit.Ampere => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(ElectricCurrentUnit unit = ElectricCurrentUnit.Ampere, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false, bool useFullName = false)
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
}