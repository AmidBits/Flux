namespace Flux
{
  public static partial class Em
  {
#pragma warning disable IDE0060 // Remove unused parameter
    public static string GetUnitString(this Units.RadiationExposureUnit unit, bool preferUnicode, bool useFullName = false)
#pragma warning restore IDE0060 // Remove unused parameter
      => useFullName ? unit.ToString() : unit switch
      {
        Units.RadiationExposureUnit.CoulombPerKilogram => "C/kg",
        Units.RadiationExposureUnit.Röntgen => "R",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum RadiationExposureUnit
    {
      CoulombPerKilogram,
      Röntgen
    }

    /// <summary>Force, unit of newton. This is an SI derived quantity.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Force"/>
    public readonly record struct RadiationExposure
      : System.IComparable, System.IComparable<RadiationExposure>, IUnitValueQuantifiable<double, RadiationExposureUnit>
    {
      public const RadiationExposureUnit DefaultUnit = RadiationExposureUnit.CoulombPerKilogram;

      private readonly double m_value;

      public RadiationExposure(double value, RadiationExposureUnit unit = DefaultUnit)
        => m_value = unit switch
        {
          RadiationExposureUnit.CoulombPerKilogram => value,
          RadiationExposureUnit.Röntgen => value / 3876,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Overloaded operators
      public static explicit operator double(RadiationExposure v) => v.m_value;
      public static explicit operator RadiationExposure(double v) => new(v);

      public static bool operator <(RadiationExposure a, RadiationExposure b) => a.CompareTo(b) < 0;
      public static bool operator <=(RadiationExposure a, RadiationExposure b) => a.CompareTo(b) <= 0;
      public static bool operator >(RadiationExposure a, RadiationExposure b) => a.CompareTo(b) > 0;
      public static bool operator >=(RadiationExposure a, RadiationExposure b) => a.CompareTo(b) >= 0;

      public static RadiationExposure operator -(RadiationExposure v) => new(-v.m_value);
      public static RadiationExposure operator +(RadiationExposure a, double b) => new(a.m_value + b);
      public static RadiationExposure operator +(RadiationExposure a, RadiationExposure b) => a + b.m_value;
      public static RadiationExposure operator /(RadiationExposure a, double b) => new(a.m_value / b);
      public static RadiationExposure operator /(RadiationExposure a, RadiationExposure b) => a / b.m_value;
      public static RadiationExposure operator *(RadiationExposure a, double b) => new(a.m_value * b);
      public static RadiationExposure operator *(RadiationExposure a, RadiationExposure b) => a * b.m_value;
      public static RadiationExposure operator %(RadiationExposure a, double b) => new(a.m_value % b);
      public static RadiationExposure operator %(RadiationExposure a, RadiationExposure b) => a % b.m_value;
      public static RadiationExposure operator -(RadiationExposure a, double b) => new(a.m_value - b);
      public static RadiationExposure operator -(RadiationExposure a, RadiationExposure b) => a - b.m_value;
      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is RadiationExposure o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(RadiationExposure other) => m_value.CompareTo(other.m_value);

      // IQuantifiable<>
      public string ToValueString(string? format = null, bool preferUnicode = false, bool useFullName = false, System.Globalization.CultureInfo? culture = null)
        => ToUnitValueString(DefaultUnit, format, preferUnicode, useFullName, culture);

      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(RadiationExposureUnit unit)
        => unit switch
        {
          RadiationExposureUnit.CoulombPerKilogram => m_value,
          RadiationExposureUnit.Röntgen => m_value * 3876,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(RadiationExposureUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false, System.Globalization.CultureInfo? culture = null)
        => $"{string.Format(culture, $"{{0{(format is null ? string.Empty : $":{format}")}}}", GetUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";

      #endregion Implemented interfaces

      public override string ToString() => ToValueString();
    }
  }
}
