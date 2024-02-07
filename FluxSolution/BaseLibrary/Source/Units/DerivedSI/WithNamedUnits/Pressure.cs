namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.PressureUnit unit, QuantifiableValueStringOptions options)
      => options.UseFullName ? unit.ToString() : unit switch
      {
        Units.PressureUnit.Millibar => "mbar",
        Units.PressureUnit.Bar => options.PreferUnicode ? "\u3374" : "bar",
        Units.PressureUnit.HectoPascal => options.PreferUnicode ? "\u3371" : "hPa",
        Units.PressureUnit.KiloPascal => options.PreferUnicode ? "\u33AA" : "kPa",
        Units.PressureUnit.Pascal => options.PreferUnicode ? "\u33A9" : "Pa",
        Units.PressureUnit.Psi => "psi",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
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
      : System.IComparable, System.IComparable<Pressure>, System.IFormattable, IUnitValueQuantifiable<double, PressureUnit>
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
      public static explicit operator double(Pressure v) => v.m_value;
      public static explicit operator Pressure(double v) => new(v);

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
      public string ToString(string? format, System.IFormatProvider? formatProvider) => ToValueString(QuantifiableValueStringOptions.Default with { Format = format, FormatProvider = formatProvider });

      // IQuantifiable<>
      public string ToValueString(QuantifiableValueStringOptions options) => ToUnitValueString(PressureUnit.Pascal, options);

      /// <summary>
      /// <para>The unit of the <see cref="Pressure.Value"/> property is in <see cref="PressureUnit.Pascal"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
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

      public string ToUnitValueString(PressureUnit unit, QuantifiableValueStringOptions options)
        => $"{string.Format(options.CultureInfo, $"{{0{(options.Format is null ? string.Empty : $":{options.Format}")}}}", GetUnitValue(unit))} {unit.GetUnitString(options)}";

      #endregion Implemented interfaces

      public override string ToString() => ToValueString(QuantifiableValueStringOptions.Default);
    }
  }
}
