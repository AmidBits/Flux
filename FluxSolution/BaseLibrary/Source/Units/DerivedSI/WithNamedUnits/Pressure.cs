namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.PressureUnit unit, bool preferUnicode, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Units.PressureUnit.Millibar => "mbar",
        Units.PressureUnit.Bar => preferUnicode ? "\u3374" : "bar",
        Units.PressureUnit.HectoPascal => preferUnicode ? "\u3371" : "hPa",
        Units.PressureUnit.KiloPascal => preferUnicode ? "\u33AA" : "kPa",
        Units.PressureUnit.Pascal => preferUnicode ? "\u33A9" : "Pa",
        Units.PressureUnit.Psi => "psi",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum PressureUnit
    {
      Pascal, // DefaultUnit first for actual instatiation defaults.
      Millibar,
      Bar,
      HectoPascal,
      KiloPascal,
      Psi,
    }

    /// <summary>Pressure, unit of Pascal. This is an SI derived quantity.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Pressure"/>
    public readonly record struct Pressure
      : System.IComparable, System.IComparable<Pressure>, System.IFormattable, IUnitQuantifiable<double, PressureUnit>
    {
      public const PressureUnit DefaultUnit = PressureUnit.Pascal;

      private readonly double m_value;

      public Pressure(double value, PressureUnit unit = DefaultUnit)
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
      public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

      // IQuantifiable<>
      public string ToQuantityString(string? format = null, bool preferUnicode = false, bool useFullName = false) => ToUnitString(DefaultUnit, format, preferUnicode, useFullName);
      public double Value { get => m_value; init => m_value = value; }

      // IUnitQuantifiable<>
      public string ToUnitString(PressureUnit unit = DefaultUnit, string? format = null, bool preferUnicode = false, bool useFullName = false)
        => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";
      public double ToUnitValue(PressureUnit unit = DefaultUnit)
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

      #endregion Implemented interfaces

      public override string ToString() => ToQuantityString();
    }
  }
}
