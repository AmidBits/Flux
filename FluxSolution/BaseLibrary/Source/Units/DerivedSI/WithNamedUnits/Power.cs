namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.PowerUnit unit, bool preferUnicode, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Units.PowerUnit.Watt => "W",
        Units.PowerUnit.KiloWatt => preferUnicode ? "\u33BE" : "kW",
        Units.PowerUnit.MegaWatt => preferUnicode ? "\u33BF" : "MW",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
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
      : System.IComparable, System.IComparable<Power>, System.IEquatable<Power>, System.IFormattable, IUnitValueQuantifiable<double, PowerUnit>
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

      #region Static methods

      /// <summary>Creates a new Power instance from the specified <paramref name="current"/> and <paramref name="voltage"/>.</summary>
      /// <param name="current"></param>
      /// <param name="voltage"></param>
      public static Power From(ElectricCurrent current, Voltage voltage)
        => new(current.Value * voltage.Value);

      #endregion Static methods

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

      // IQuantifiable<>
      /// <summary>
      /// <para>The unit of the <see cref="Power.Value"/> property is in <see cref="PowerUnit.Watt"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(PowerUnit unit)
        => unit switch
        {
          PowerUnit.Watt => m_value,
          PowerUnit.KiloWatt => m_value / 1000,
          PowerUnit.MegaWatt => m_value / 1000000,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(PowerUnit unit = PowerUnit.Watt, string? format = null, System.IFormatProvider? formatProvider = null, bool preferUnicode = false, UnicodeSpacing unicodeSpacing = UnicodeSpacing.None, bool useFullName = false)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(GetUnitValue(unit).ToString(format, formatProvider));
        sb.Append(unicodeSpacing.ToSpacingString());
        sb.Append(unit.GetUnitString(preferUnicode, useFullName));
        return sb.ToString();
      }

      #endregion Implemented interfaces

      public override string ToString() => ToString(null, null);
    }
  }
}
