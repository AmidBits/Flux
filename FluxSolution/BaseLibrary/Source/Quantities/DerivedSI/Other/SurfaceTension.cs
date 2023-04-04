namespace Flux
{
  public static partial class QuantitiesExtensionMethods
  {
#pragma warning disable IDE0060 // Remove unused parameter
    public static string GetUnitString(this Quantities.SurfaceTensionUnit unit, bool preferUnicode, bool useFullName = false)
#pragma warning restore IDE0060 // Remove unused parameter
      => useFullName ? unit.ToString() : unit switch
      {
        Quantities.SurfaceTensionUnit.NewtonPerMeter => "N/m",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Quantities
  {
    public enum SurfaceTensionUnit
    {
      NewtonPerMeter,
    }

    /// <summary>Surface tension, unit of Newton per meter.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Surface_tension"/>
    public readonly record struct SurfaceTension
      : System.IComparable, System.IComparable<SurfaceTension>, System.IFormattable, IUnitQuantifiable<double, SurfaceTensionUnit>
    {
      public static readonly SurfaceTension Zero;

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
      public string ToQuantityString(string? format = null, bool preferUnicode = false, bool useFullName = false) => ToUnitString(DefaultUnit, format, preferUnicode, useFullName);
      public double Value { get => m_value; init => m_value = value; }

      // IUnitQuantifiable<>
      public string ToUnitString(SurfaceTensionUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false)
        => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";
      public double ToUnitValue(SurfaceTensionUnit unit = DefaultUnit)
        => unit switch
        {
          SurfaceTensionUnit.NewtonPerMeter => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #endregion Implemented interfaces

      public override string ToString() => ToQuantityString();
    }
  }
}
