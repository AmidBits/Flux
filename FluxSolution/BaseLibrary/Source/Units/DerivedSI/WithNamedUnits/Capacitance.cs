namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.CapacitanceUnit unit, QuantifiableValueStringOptions options)
      => options.UseFullName ? unit.ToString() : unit switch
      {
        Units.CapacitanceUnit.Farad => "F",
        Units.CapacitanceUnit.MicroFarad => options.PreferUnicode ? "\u338C" : "\u00B5F",
        Units.CapacitanceUnit.NanoFarad => options.PreferUnicode ? "\u338B" : "nF",
        Units.CapacitanceUnit.PicoFarad => options.PreferUnicode ? "\u338A" : "pF",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
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
      : System.IComparable, System.IComparable<Capacitance>, System.IFormattable, IUnitValueQuantifiable<double, CapacitanceUnit>
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

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? formatProvider) => ToValueString(QuantifiableValueStringOptions.Default with { Format = format, FormatProvider = formatProvider });

      // IQuantifiable<>
      public string ToValueString(QuantifiableValueStringOptions options) => ToUnitValueString(CapacitanceUnit.Farad, options);

      /// <summary>
      /// <para>The unit of the <see cref="Capacitance.Value"/> property is in <see cref="CapacitanceUnit.Farad"/>.</para>
      /// </summary>
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

      public string ToUnitValueString(CapacitanceUnit unit, QuantifiableValueStringOptions options)
        => $"{string.Format(options.CultureInfo, $"{{0{(options.Format is null ? string.Empty : $":{options.Format}")}}}", GetUnitValue(unit))} {unit.GetUnitString(options)}";

      #endregion Implemented interfaces

      public override string ToString() => ToValueString(QuantifiableValueStringOptions.Default);
    }
  }
}
