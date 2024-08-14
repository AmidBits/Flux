namespace Flux
{
  public static partial class Fx
  {
    public static string GetUnitString(this Quantities.PressureUnit unit, bool preferUnicode, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Quantities.PressureUnit.Millibar => "mbar",
        Quantities.PressureUnit.Bar => preferUnicode ? "\u3374" : "bar",
        Quantities.PressureUnit.HectoPascal => preferUnicode ? "\u3371" : "hPa",
        Quantities.PressureUnit.KiloPascal => preferUnicode ? "\u33AA" : "kPa",
        Quantities.PressureUnit.Pascal => preferUnicode ? "\u33A9" : "Pa",
        Quantities.PressureUnit.Psi => "psi",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Quantities
  {
    public enum PressureUnit
    {
      /// <summary>This is the default unit for <see cref="Pressure"/>.</summary>
      Pascal,
      Millibar,
      Bar,
      HectoPascal,
      KiloPascal,
      Psi,
    }

    /// <summary>Pressure, unit of Pascal. This is an SI derived quantity.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Pressure"/>
    public readonly record struct Pressure
      : System.IComparable, System.IComparable<Pressure>, System.IFormattable, ISiPrefixValueQuantifiable<double, PressureUnit>
    {
      private readonly double m_value;

      public Pressure(double value, PressureUnit unit = PressureUnit.Pascal)
        => m_value = unit switch
        {
          PressureUnit.Millibar => value * 100,
          PressureUnit.Bar => value / 100000,
          PressureUnit.HectoPascal => value * 100,
          PressureUnit.KiloPascal => value * 1000,
          PressureUnit.Pascal => value,
          PressureUnit.Psi => value * (8896443230521.0 / 1290320000.0),
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Static methods
      #endregion Static methods

      #region Overloaded operators

      public static bool operator <(Pressure a, Pressure b) => a.CompareTo(b) < 0;
      public static bool operator <=(Pressure a, Pressure b) => a.CompareTo(b) <= 0;
      public static bool operator >(Pressure a, Pressure b) => a.CompareTo(b) > 0;
      public static bool operator >=(Pressure a, Pressure b) => a.CompareTo(b) >= 0;

      public static Pressure operator -(Pressure v) => new(-v.m_value);
      public static Pressure operator +(Pressure a, double b) => new(a.m_value + b);
      public static Pressure operator +(Pressure a, Pressure b) => a + b.m_value;
      public static Pressure operator /(Pressure a, double b) => new(a.m_value / b);
      public static Pressure operator /(Pressure a, Pressure b) => a / b.m_value;
      public static Pressure operator *(Pressure a, double b) => new(a.m_value * b);
      public static Pressure operator *(Pressure a, Pressure b) => a * b.m_value;
      public static Pressure operator %(Pressure a, double b) => new(a.m_value % b);
      public static Pressure operator %(Pressure a, Pressure b) => a % b.m_value;
      public static Pressure operator -(Pressure a, double b) => new(a.m_value - b);
      public static Pressure operator -(Pressure a, Pressure b) => a - b.m_value;

      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is Pressure o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(Pressure other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => ToUnitValueString(PressureUnit.Pascal, format, formatProvider);

      // ISiUnitValueQuantifiable<>
      public (MetricPrefix Prefix, PressureUnit Unit) GetSiPrefixUnit(MetricPrefix prefix) => (prefix, PressureUnit.Pascal);

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
      /// <para>The unit of the <see cref="Pressure.Value"/> property is in <see cref="PressureUnit.Pascal"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public string GetUnitSymbol(PressureUnit unit, bool preferUnicode, bool useFullName) => unit.GetUnitString(preferUnicode, useFullName);

      public double GetUnitValue(PressureUnit unit)
        => unit switch
        {
          PressureUnit.Millibar => m_value / 100,
          PressureUnit.Bar => m_value / 100000,
          PressureUnit.HectoPascal => m_value / 100,
          PressureUnit.KiloPascal => m_value / 1000,
          PressureUnit.Pascal => m_value,
          PressureUnit.Psi => m_value * (1290320000.0 / 8896443230521.0),
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(PressureUnit unit = PressureUnit.Pascal, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false, bool useFullName = false)
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
