
namespace Flux
{
  public static partial class Fx
  {
    public static string GetUnitString(this Quantities.PowerUnit unit, bool preferUnicode, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Quantities.PowerUnit.Watt => "W",
        Quantities.PowerUnit.KiloWatt => preferUnicode ? "\u33BE" : "kW",
        Quantities.PowerUnit.MegaWatt => preferUnicode ? "\u33BF" : "MW",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Quantities
  {
    public enum PowerUnit
    {
      /// <summary>This is the default unit for <see cref="Power"/>.</summary>
      Watt,
      KiloWatt,
      MegaWatt,
    }

    /// <summary>Power unit of watt.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Power"/>
    public readonly record struct Power
      : System.IComparable, System.IComparable<Power>, System.IFormattable, ISiPrefixValueQuantifiable<double, PowerUnit>
    {
      private readonly double m_value;

      public Power(double value, PowerUnit unit = PowerUnit.Watt)
        => m_value = unit switch
        {
          PowerUnit.Watt => value,
          PowerUnit.KiloWatt => value * 1000,
          PowerUnit.MegaWatt => value * 1000000,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      /// <summary>Creates a new Power instance from the specified <paramref name="current"/> and <paramref name="voltage"/>.</summary>
      /// <param name="current"></param>
      /// <param name="voltage"></param>
      public Power(ElectricCurrent current, Voltage voltage) : this(current.Value * voltage.Value) { }

      #region Static methods

      #endregion // Static methods

      #region Overloaded operators

      public static bool operator <(Power a, Power b) => a.CompareTo(b) < 0;
      public static bool operator <=(Power a, Power b) => a.CompareTo(b) <= 0;
      public static bool operator >(Power a, Power b) => a.CompareTo(b) > 0;
      public static bool operator >=(Power a, Power b) => a.CompareTo(b) >= 0;

      public static Power operator -(Power v) => new(-v.m_value);
      public static Power operator +(Power a, double b) => new(a.m_value + b);
      public static Power operator +(Power a, Power b) => a + b.m_value;
      public static Power operator /(Power a, double b) => new(a.m_value / b);
      public static Power operator /(Power a, Power b) => a / b.m_value;
      public static Power operator *(Power a, double b) => new(a.m_value * b);
      public static Power operator *(Power a, Power b) => a * b.m_value;
      public static Power operator %(Power a, double b) => new(a.m_value % b);
      public static Power operator %(Power a, Power b) => a % b.m_value;
      public static Power operator -(Power a, double b) => new(a.m_value - b);
      public static Power operator -(Power a, Power b) => a - b.m_value;

      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is Power o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(Power other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => ToUnitValueString(PowerUnit.Watt, format, formatProvider);

      // ISiUnitValueQuantifiable<>
      public (MetricPrefix Prefix, PowerUnit Unit) GetSiPrefixUnit(MetricPrefix prefix) => (prefix, PowerUnit.Watt);

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
      /// <para>The unit of the <see cref="Power.Value"/> property is in <see cref="PowerUnit.Watt"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public string GetUnitSymbol(PowerUnit unit, bool preferUnicode, bool useFullName) => unit.GetUnitString(preferUnicode, useFullName);

      public double GetUnitValue(PowerUnit unit)
        => unit switch
        {
          PowerUnit.Watt => m_value,
          PowerUnit.KiloWatt => m_value / 1000,
          PowerUnit.MegaWatt => m_value / 1000000,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(PowerUnit unit = PowerUnit.Watt, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false, bool useFullName = false)
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
