namespace Flux
{
  public static partial class Fx
  {
    public static string GetUnitString(this Quantities.CapacitanceUnit unit, bool preferUnicode, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Quantities.CapacitanceUnit.Farad => "F",
        Quantities.CapacitanceUnit.MicroFarad => preferUnicode ? "\u338C" : "\u00B5F",
        Quantities.CapacitanceUnit.NanoFarad => preferUnicode ? "\u338B" : "nF",
        Quantities.CapacitanceUnit.PicoFarad => preferUnicode ? "\u338A" : "pF",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Quantities
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
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => ToUnitValueString(CapacitanceUnit.Farad, format, formatProvider);

      // ISiUnitValueQuantifiable<>
      public CapacitanceUnit BaseUnit => CapacitanceUnit.Farad;

      public CapacitanceUnit UnprefixedUnit => CapacitanceUnit.Farad;

      public string GetSiPrefixSymbol(MetricPrefix prefix, bool preferUnicode, bool useFullName) => prefix.GetUnitString(preferUnicode, useFullName) + GetUnitSymbol(UnprefixedUnit, preferUnicode, useFullName);

      public double GetSiPrefixValue(MetricPrefix prefix) => MetricPrefix.NoPrefix.Convert(m_value, prefix);

      public string ToSiPrefixValueString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false, bool useFullName = false)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(GetSiPrefixValue(prefix).ToString(format, formatProvider));
        sb.Append(unitSpacing.ToSpacingString());
        sb.Append(prefix.GetUnitString(preferUnicode, useFullName));
        sb.Append(UnprefixedUnit.GetUnitString(preferUnicode, useFullName));
        return sb.ToString();
      }

      // IQuantifiable<>
      /// <summary>
      /// <para>The unit of the <see cref="Capacitance.Value"/> property is in <see cref="CapacitanceUnit.Farad"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public string GetUnitSymbol(CapacitanceUnit unit, bool preferUnicode, bool useFullName) => unit.GetUnitString(preferUnicode, useFullName);

      public double GetUnitValue(CapacitanceUnit unit)
        => unit switch
        {
          CapacitanceUnit.Farad => m_value,
          CapacitanceUnit.MicroFarad => m_value / 1000000,
          CapacitanceUnit.NanoFarad => m_value / 1000000000,
          CapacitanceUnit.PicoFarad => m_value / 1000000000000,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(CapacitanceUnit unit = CapacitanceUnit.Farad, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false, bool useFullName = false)
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