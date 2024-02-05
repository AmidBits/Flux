namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.CurrentDensityUnit unit, QuantifiableValueStringOptions options)
      => options.UseFullName ? unit.ToString() : unit switch
      {
        Units.CurrentDensityUnit.AmperePerSquareMeter => "A/m²",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum CurrentDensityUnit
    {
      /// <summary>This is the default unit for <see cref="CurrentDensity"/>.</summary>
      AmperePerSquareMeter,
    }

    /// <summary>Current density, unit of ampere per square meter.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Current_density"/>
    public readonly record struct CurrentDensity
      : System.IComparable, System.IComparable<CurrentDensity>, System.IFormattable, IUnitValueQuantifiable<double, CurrentDensityUnit>
    {
      private readonly double m_value;

      public CurrentDensity(double value, CurrentDensityUnit unit = CurrentDensityUnit.AmperePerSquareMeter)
        => m_value = unit switch
        {
          CurrentDensityUnit.AmperePerSquareMeter => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };


      public MetricMultiplicative ToMetricMultiplicative() => new(GetUnitValue(CurrentDensityUnit.AmperePerSquareMeter), MetricMultiplicativePrefix.One);

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
      public string ToValueString(QuantifiableValueStringOptions options) => ToUnitValueString(CurrentDensityUnit.AmperePerSquareMeter, options);

      /// <summary>
      /// <para>The unit of the <see cref="CurrentDensity.Value"/> property is in <see cref="CurrentDensityUnit.AmperePerSquareMeter"/>.</para>
      /// </summary>
      public double Value => m_value;

      //IUnitQuantifiable<>
      public double GetUnitValue(CurrentDensityUnit unit)
        => unit switch
        {
          CurrentDensityUnit.AmperePerSquareMeter => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(CurrentDensityUnit unit, QuantifiableValueStringOptions options)
        => $"{string.Format(options.CultureInfo, $"{{0{(options.Format is null ? string.Empty : $":{options.Format}")}}}", GetUnitValue(unit))} {unit.GetUnitString(options)}";

      #endregion Implemented interfaces

      public override string ToString() => ToValueString(QuantifiableValueStringOptions.Default);
    }
  }
}
