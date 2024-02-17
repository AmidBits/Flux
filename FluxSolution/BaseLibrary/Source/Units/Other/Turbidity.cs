namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.TurbidityUnit unit, Units.UnitValueStringOptions options = default)
      => options.UseFullName ? unit.ToString() : unit switch
      {
        Units.TurbidityUnit.NephelometricTurbidityUnits => "NTU",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum TurbidityUnit
    {
      /// <summary>This is the default unit for <see cref="Turbidity"/>.</summary>
      NephelometricTurbidityUnits,
    }

    /// <summary>Turbidity, unit of NTU (nephelometric turbidity units).</summary>
    /// <see href="https://en.wikipedia.org/wiki/Turbidity"/>
    public readonly record struct Turbidity
      : System.IComparable, System.IComparable<Turbidity>, System.IFormattable, IUnitValueQuantifiable<double, TurbidityUnit>
    {
      private readonly double m_value;

      public Turbidity(double value, TurbidityUnit unit = TurbidityUnit.NephelometricTurbidityUnits)
        => m_value = unit switch
        {
          TurbidityUnit.NephelometricTurbidityUnits => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Static methods

      public static Turbidity From(Force force, Time time)
        => new(force.Value / time.Value);
      #endregion Static methods

      #region Overloaded operators
      public static explicit operator double(Turbidity v) => v.m_value;
      public static explicit operator Turbidity(double v) => new(v);

      public static bool operator <(Turbidity a, Turbidity b) => a.CompareTo(b) < 0;
      public static bool operator <=(Turbidity a, Turbidity b) => a.CompareTo(b) <= 0;
      public static bool operator >(Turbidity a, Turbidity b) => a.CompareTo(b) > 0;
      public static bool operator >=(Turbidity a, Turbidity b) => a.CompareTo(b) >= 0;

      public static Turbidity operator -(Turbidity v) => new(-v.m_value);
      public static Turbidity operator +(Turbidity a, double b) => new(a.m_value + b);
      public static Turbidity operator +(Turbidity a, Turbidity b) => a + b.m_value;
      public static Turbidity operator /(Turbidity a, double b) => new(a.m_value / b);
      public static Turbidity operator /(Turbidity a, Turbidity b) => a / b.m_value;
      public static Turbidity operator *(Turbidity a, double b) => new(a.m_value * b);
      public static Turbidity operator *(Turbidity a, Turbidity b) => a * b.m_value;
      public static Turbidity operator %(Turbidity a, double b) => new(a.m_value % b);
      public static Turbidity operator %(Turbidity a, Turbidity b) => a % b.m_value;
      public static Turbidity operator -(Turbidity a, double b) => new(a.m_value - b);
      public static Turbidity operator -(Turbidity a, Turbidity b) => a - b.m_value;
      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is Turbidity o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(Turbidity other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, IFormatProvider? formatProvider)
        => ToUnitValueString(TurbidityUnit.NephelometricTurbidityUnits, UnitValueStringOptions.Default with { Format = format, FormatProvider = formatProvider });

      // IQuantifiable<>
      /// <summary>
      /// <para>The unit of the <see cref="Turbidity.Value"/> property is in <see cref="TurbidityUnit.TurbidityUnit"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(TurbidityUnit unit)
        => unit switch
        {
          TurbidityUnit.NephelometricTurbidityUnits => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(TurbidityUnit unit, UnitValueStringOptions options = default)
        => $"{string.Format(options.CultureInfo, $"{{0{(options.Format is null ? string.Empty : $":{options.Format}")}}}", GetUnitValue(unit))} {unit.GetUnitString(options)}";

      #endregion Implemented interfaces
    }
  }
}
