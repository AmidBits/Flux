namespace Flux
{
  public static partial class Em
  {
#pragma warning disable IDE0060 // Remove unused parameter
    public static string GetUnitString(this Units.CurrentDensityUnit unit, bool preferUnicode, bool useFullName = false)
#pragma warning restore IDE0060 // Remove unused parameter
      => useFullName ? unit.ToString() : unit switch
      {
        Units.CurrentDensityUnit.AmperePerSquareMeter => "A/m²",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum CurrentDensityUnit
    {
      AmperePerSquareMeter,
    }

    /// <summary>Current density, unit of ampere per square meter.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Current_density"/>
    public readonly record struct CurrentDensity
      : System.IComparable, System.IComparable<CurrentDensity>, System.IFormattable, IUnitValueQuantifiable<double, CurrentDensityUnit>
    {
      public const CurrentDensityUnit DefaultUnit = CurrentDensityUnit.AmperePerSquareMeter;

      private readonly double m_value;

      public CurrentDensity(double value, CurrentDensityUnit unit = DefaultUnit)
        => m_value = unit switch
        {
          CurrentDensityUnit.AmperePerSquareMeter => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };


      public MetricMultiplicative ToMetricMultiplicative()
        => new(GetUnitValue(DefaultUnit), MetricMultiplicativePrefix.One);

      #region Overloaded operators
      public static explicit operator double(CurrentDensity v) => v.m_value;
      public static explicit operator CurrentDensity(double v) => new(v);

      public static bool operator <(CurrentDensity a, CurrentDensity b) => a.CompareTo(b) < 0;
      public static bool operator <=(CurrentDensity a, CurrentDensity b) => a.CompareTo(b) <= 0;
      public static bool operator >(CurrentDensity a, CurrentDensity b) => a.CompareTo(b) > 0;
      public static bool operator >=(CurrentDensity a, CurrentDensity b) => a.CompareTo(b) >= 0;

      public static CurrentDensity operator -(CurrentDensity v) => new(-v.m_value);
      public static CurrentDensity operator +(CurrentDensity a, double b) => new(a.m_value + b);
      public static CurrentDensity operator +(CurrentDensity a, CurrentDensity b) => a + b.m_value;
      public static CurrentDensity operator /(CurrentDensity a, double b) => new(a.m_value / b);
      public static CurrentDensity operator /(CurrentDensity a, CurrentDensity b) => a / b.m_value;
      public static CurrentDensity operator *(CurrentDensity a, double b) => new(a.m_value * b);
      public static CurrentDensity operator *(CurrentDensity a, CurrentDensity b) => a * b.m_value;
      public static CurrentDensity operator %(CurrentDensity a, double b) => new(a.m_value % b);
      public static CurrentDensity operator %(CurrentDensity a, CurrentDensity b) => a % b.m_value;
      public static CurrentDensity operator -(CurrentDensity a, double b) => new(a.m_value - b);
      public static CurrentDensity operator -(CurrentDensity a, CurrentDensity b) => a - b.m_value;
      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is CurrentDensity o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(CurrentDensity other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

      // IQuantifiable<>
      public string ToValueString(string? format = null, bool preferUnicode = false, bool useFullName = false, System.Globalization.CultureInfo? culture = null)
        => ToUnitValueString(DefaultUnit, format, preferUnicode, useFullName, culture);

      public double Value => m_value;

      //IUnitQuantifiable<>
      public double GetUnitValue(CurrentDensityUnit unit)
        => unit switch
        {
          CurrentDensityUnit.AmperePerSquareMeter => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(CurrentDensityUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false, System.Globalization.CultureInfo? culture = null)
        => $"{string.Format(culture, $"{{0{(format is null ? string.Empty : $":{format}")}}}", GetUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";

      #endregion Implemented interfaces

      public override string ToString() => ToValueString();
    }
  }
}
