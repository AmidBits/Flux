namespace Flux
{
  public static partial class Fx
  {
    public static string GetUnitString(this Quantities.EnergyUnit unit, bool preferUnicode, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Quantities.EnergyUnit.Joule => "J",
        Quantities.EnergyUnit.ElectronVolt => "eV",
        Quantities.EnergyUnit.Calorie => preferUnicode ? "\u3388" : "cal",
        Quantities.EnergyUnit.WattHour => "W\u22C5h",
        Quantities.EnergyUnit.KilowattHour => "kW\u22C5h",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Quantities
  {
    public enum EnergyUnit
    {
      /// <summary>This is the default unit for <see cref="Energy"/>.</summary>
      Joule,
      ElectronVolt,
      Calorie,
      WattHour,
      KilowattHour
    }

    /// <summary>Energy unit of Joule.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Energy"/>
    public readonly record struct Energy
      : System.IComparable, System.IComparable<Energy>, System.IFormattable, IUnitValueQuantifiable<double, EnergyUnit>
    {
      private readonly double m_value;

      public Energy(double value, EnergyUnit unit = EnergyUnit.Joule)
        => m_value = unit switch
        {
          EnergyUnit.Joule => value,
          EnergyUnit.ElectronVolt => value / 1.602176634e-19,
          EnergyUnit.Calorie => value / 4.184,
          EnergyUnit.WattHour => value / 3.6e3,
          EnergyUnit.KilowattHour => value / 3.6e6,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Static methods

      #endregion // Static methods

      #region Overloaded operators

      public static bool operator <(Energy a, Energy b) => a.CompareTo(b) < 0;
      public static bool operator <=(Energy a, Energy b) => a.CompareTo(b) <= 0;
      public static bool operator >(Energy a, Energy b) => a.CompareTo(b) > 0;
      public static bool operator >=(Energy a, Energy b) => a.CompareTo(b) >= 0;

      public static Energy operator -(Energy v) => new(-v.m_value);
      public static Energy operator +(Energy a, double b) => new(a.m_value + b);
      public static Energy operator +(Energy a, Energy b) => a + b.m_value;
      public static Energy operator /(Energy a, double b) => new(a.m_value / b);
      public static Energy operator /(Energy a, Energy b) => a / b.m_value;
      public static Energy operator *(Energy a, double b) => new(a.m_value * b);
      public static Energy operator *(Energy a, Energy b) => a * b.m_value;
      public static Energy operator %(Energy a, double b) => new(a.m_value % b);
      public static Energy operator %(Energy a, Energy b) => a % b.m_value;
      public static Energy operator -(Energy a, double b) => new(a.m_value - b);
      public static Energy operator -(Energy a, Energy b) => a - b.m_value;

      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is Energy o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(Energy other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => ToUnitValueString(EnergyUnit.Joule, format, formatProvider);

      // IMetricMultiplicable<>
      public double GetMetricValue(MetricPrefix prefix) => MetricPrefix.NoPrefix.Convert(m_value, prefix);

      public string ToMetricValueString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null, bool preferUnicode = false, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool useFullName = false)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(GetMetricValue(prefix).ToString(format, formatProvider));
        sb.Append(unitSpacing.ToSpacingString());
        sb.Append(prefix.GetUnitString(preferUnicode, useFullName));
        sb.Append(EnergyUnit.Joule.GetUnitString(preferUnicode, useFullName));
        return sb.ToString();
      }

      // IQuantifiable<>
      /// <summary>
      /// <para>The unit of the <see cref="Energy.Value"/> property is in <see cref="EnergyUnit.Joule"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public string GetUnitSymbol(EnergyUnit unit, bool preferUnicode, bool useFullName) => unit.GetUnitString(preferUnicode, useFullName);

      public double GetUnitValue(EnergyUnit unit)
        => unit switch
        {
          EnergyUnit.Joule => m_value,
          EnergyUnit.ElectronVolt => m_value * 1.602176634e-19,
          EnergyUnit.Calorie => m_value * 4.184,
          EnergyUnit.WattHour => m_value * 3.6e3,
          EnergyUnit.KilowattHour => m_value * 3.6e6,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(EnergyUnit unit = EnergyUnit.Joule, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false, bool useFullName = false)
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
