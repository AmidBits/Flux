namespace Flux
{
  public static partial class Em
  {
#pragma warning disable IDE0060 // Remove unused parameter
    public static string GetUnitString(this Units.TurbidityUnit unit, bool preferUnicode, bool useFullName = false)
#pragma warning restore IDE0060 // Remove unused parameter
      => useFullName ? unit.ToString() : unit switch
      {
        Units.TurbidityUnit.NephelometricTurbidityUnits => "NTU",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum TurbidityUnit
    {
      NephelometricTurbidityUnits,
    }

    /// <summary>Turbidity, unit of NTU (nephelometric turbidity units).</summary>
    /// <see href="https://en.wikipedia.org/wiki/Turbidity"/>
    public readonly record struct Turbidity
      : System.IComparable, System.IComparable<Turbidity>, System.IFormattable, IUnitValueQuantifiable<double, TurbidityUnit>
    {
      public const TurbidityUnit DefaultUnit = TurbidityUnit.NephelometricTurbidityUnits;

      private readonly double m_value;

      public Turbidity(double value, TurbidityUnit unit = DefaultUnit)
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
      public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

      // IQuantifiable<>
      public string ToValueString(string? format = null, bool preferUnicode = false, bool useFullName = false, System.Globalization.CultureInfo? culture = null)
        => ToUnitValueString(DefaultUnit, format, preferUnicode, useFullName, culture);

      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(TurbidityUnit unit)
        => unit switch
        {
          TurbidityUnit.NephelometricTurbidityUnits => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(TurbidityUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false, System.Globalization.CultureInfo? culture = null)
        => $"{string.Format(culture, $"{{0{(format is null ? string.Empty : $":{format}")}}}", GetUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";

      #endregion Implemented interfaces

      public override string ToString() => ToValueString();
    }
  }
}
