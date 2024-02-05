namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.RadiationExposureUnit unit, QuantifiableValueStringOptions options)
      => options.UseFullName ? unit.ToString() : unit switch
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
      /// <summary>This is the default unit for <see cref="RadiationExposure"/>.</summary>
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
      public string ToValueString(QuantifiableValueStringOptions options) => ToUnitValueString(DefaultUnit, options);

      /// <summary>
      /// <para>The unit of the <see cref="RadiationExposure.Value"/> property is in <see cref="RadiationExposureUnit.CoulombPerKilogram"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(RadiationExposureUnit unit)
        => unit switch
        {
          RadiationExposureUnit.CoulombPerKilogram => m_value,
          RadiationExposureUnit.Röntgen => m_value * 3876,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(RadiationExposureUnit unit, QuantifiableValueStringOptions options)
        => $"{string.Format(options.CultureInfo, $"{{0{(options.Format is null ? string.Empty : $":{options.Format}")}}}", GetUnitValue(unit))} {unit.GetUnitString(options)}";

      #endregion Implemented interfaces

      public override string ToString() => ToValueString(QuantifiableValueStringOptions.Default);
    }
  }
}
