namespace Flux
{
  public static partial class Em
  {
#pragma warning disable IDE0060 // Remove unused parameter
    public static string GetUnitString(this Units.SurfaceTensionUnit unit, bool preferUnicode, bool useFullName = false)
#pragma warning restore IDE0060 // Remove unused parameter
      => useFullName ? unit.ToString() : unit switch
      {
        Units.SurfaceTensionUnit.NewtonPerMeter => "N/m",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum SurfaceTensionUnit
    {
      NewtonPerMeter,
    }

    /// <summary>Surface tension, unit of Newton per meter.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Surface_tension"/>
    public readonly record struct SurfaceTension
      : System.IComparable, System.IComparable<SurfaceTension>, System.IFormattable, IUnitValueQuantifiable<double, SurfaceTensionUnit>
    {
      public const SurfaceTensionUnit DefaultUnit = SurfaceTensionUnit.NewtonPerMeter;

      private readonly double m_value;

      public SurfaceTension(double value, SurfaceTensionUnit unit = DefaultUnit)
        => m_value = unit switch
        {
          SurfaceTensionUnit.NewtonPerMeter => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Static methods

      public static SurfaceTension From(Force force, Length length)
        => new(force.Value / length.Value);

      public static SurfaceTension From(Energy energy, Area area)
        => new(energy.Value / area.Value);
      #endregion Static methods

      #region Overloaded operators
      public static explicit operator double(SurfaceTension v) => v.m_value;
      public static explicit operator SurfaceTension(double v) => new(v);

      public static bool operator <(SurfaceTension a, SurfaceTension b) => a.CompareTo(b) < 0;
      public static bool operator <=(SurfaceTension a, SurfaceTension b) => a.CompareTo(b) <= 0;
      public static bool operator >(SurfaceTension a, SurfaceTension b) => a.CompareTo(b) > 0;
      public static bool operator >=(SurfaceTension a, SurfaceTension b) => a.CompareTo(b) >= 0;

      public static SurfaceTension operator -(SurfaceTension v) => new(-v.m_value);
      public static SurfaceTension operator +(SurfaceTension a, double b) => new(a.m_value + b);
      public static SurfaceTension operator +(SurfaceTension a, SurfaceTension b) => a + b.m_value;
      public static SurfaceTension operator /(SurfaceTension a, double b) => new(a.m_value / b);
      public static SurfaceTension operator /(SurfaceTension a, SurfaceTension b) => a / b.m_value;
      public static SurfaceTension operator *(SurfaceTension a, double b) => new(a.m_value * b);
      public static SurfaceTension operator *(SurfaceTension a, SurfaceTension b) => a * b.m_value;
      public static SurfaceTension operator %(SurfaceTension a, double b) => new(a.m_value % b);
      public static SurfaceTension operator %(SurfaceTension a, SurfaceTension b) => a % b.m_value;
      public static SurfaceTension operator -(SurfaceTension a, double b) => new(a.m_value - b);
      public static SurfaceTension operator -(SurfaceTension a, SurfaceTension b) => a - b.m_value;
      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is SurfaceTension o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(SurfaceTension other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

      // IQuantifiable<>
      public string ToValueString(string? format = null, bool preferUnicode = false, bool useFullName = false, System.Globalization.CultureInfo? culture = null)
        => ToUnitValueString(DefaultUnit, format, preferUnicode, useFullName, culture);

      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(SurfaceTensionUnit unit)
        => unit switch
        {
          SurfaceTensionUnit.NewtonPerMeter => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(SurfaceTensionUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false, System.Globalization.CultureInfo? culture = null)
        => $"{string.Format(culture, $"{{0{(format is null ? string.Empty : $":{format}")}}}", GetUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";

      #endregion Implemented interfaces

      public override string ToString() => ToValueString();
    }
  }
}
