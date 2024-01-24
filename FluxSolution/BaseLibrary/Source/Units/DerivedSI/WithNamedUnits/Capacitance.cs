namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.CapacitanceUnit unit, bool preferUnicode, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Units.CapacitanceUnit.Farad => "F",
        Units.CapacitanceUnit.MicroFarad => preferUnicode ? "\u338C" : "\u00B5F",
        Units.CapacitanceUnit.NanoFarad => preferUnicode ? "\u338B" : "nF",
        Units.CapacitanceUnit.PicoFarad => preferUnicode ? "\u338A" : "pF",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum CapacitanceUnit
    {
      /// <summary>Farad.</summary>
      Farad,
      MicroFarad,
      NanoFarad,
      PicoFarad,
    }

    /// <summary>Electrical capacitance unit of Farad.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Capacitance"/>
    public readonly record struct Capacitance
      : System.IComparable, System.IComparable<Capacitance>, IUnitValueQuantifiable<double, CapacitanceUnit>
    {
      public const CapacitanceUnit DefaultUnit = CapacitanceUnit.Farad;

      private readonly double m_value;

      public Capacitance(double value, CapacitanceUnit unit = DefaultUnit)
        => m_value = unit switch
        {
          CapacitanceUnit.Farad => value,
          CapacitanceUnit.MicroFarad => value * 1000000,
          CapacitanceUnit.NanoFarad => value * 1000000000,
          CapacitanceUnit.PicoFarad => value * 1000000000000,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Overloaded operators
      public static explicit operator double(Capacitance v) => v.m_value;
      public static explicit operator Capacitance(double v) => new(v);

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

      // IQuantifiable<>
      public string ToValueString(string? format = null, bool preferUnicode = false, bool useFullName = false, System.Globalization.CultureInfo? culture = null)
        => ToUnitValueString(DefaultUnit, format, preferUnicode, useFullName, culture);

      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(CapacitanceUnit unit)
        => unit switch
        {
          CapacitanceUnit.Farad => m_value,
          CapacitanceUnit.MicroFarad => m_value / 1000000,
          CapacitanceUnit.NanoFarad => m_value / 1000000000,
          CapacitanceUnit.PicoFarad => m_value / 1000000000000,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(CapacitanceUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false, System.Globalization.CultureInfo? culture = null)
        => $"{string.Format(culture, $"{{0{(format is null ? string.Empty : $":{format}")}}}", GetUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";

      #endregion Implemented interfaces

      public override string ToString() => ToValueString();
    }
  }
}
