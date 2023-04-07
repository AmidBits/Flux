namespace Flux
{
  public static partial class QuantitiesExtensionMethods
  {
#pragma warning disable IDE0060 // Remove unused parameter
    public static string GetUnitString(this Quantities.IrradianceUnit unit, bool preferUnicode, bool useFullName = false)
#pragma warning restore IDE0060 // Remove unused parameter
      => useFullName ? unit.ToString() : unit switch
      {
        Quantities.IrradianceUnit.WattPerSquareMeter => "W/m²",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Quantities
  {
    public enum IrradianceUnit
    {
      WattPerSquareMeter,
    }

    /// <summary>irradiance, unit of watt per square meter.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Irradiance"/>
    public readonly record struct Irradiance
      : System.IComparable, System.IComparable<Irradiance>, System.IFormattable, IUnitQuantifiable<double, IrradianceUnit>
    {
      public const IrradianceUnit DefaultUnit = IrradianceUnit.WattPerSquareMeter;

      private readonly double m_value;

      public Irradiance(double value, IrradianceUnit unit = DefaultUnit)
        => m_value = unit switch
        {
          IrradianceUnit.WattPerSquareMeter => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Static methods

      public static Irradiance From(Power power, Area area)
        => new(power.Value / area.Value);
      #endregion Static methods

      #region Overloaded operators
      public static explicit operator double(Irradiance v) => v.m_value;
      public static explicit operator Irradiance(double v) => new(v);

      public static bool operator <(Irradiance a, Irradiance b) => a.CompareTo(b) < 0;
      public static bool operator <=(Irradiance a, Irradiance b) => a.CompareTo(b) <= 0;
      public static bool operator >(Irradiance a, Irradiance b) => a.CompareTo(b) > 0;
      public static bool operator >=(Irradiance a, Irradiance b) => a.CompareTo(b) >= 0;

      public static Irradiance operator -(Irradiance v) => new(-v.m_value);
      public static Irradiance operator +(Irradiance a, double b) => new(a.m_value + b);
      public static Irradiance operator +(Irradiance a, Irradiance b) => a + b.m_value;
      public static Irradiance operator /(Irradiance a, double b) => new(a.m_value / b);
      public static Irradiance operator /(Irradiance a, Irradiance b) => a / b.m_value;
      public static Irradiance operator *(Irradiance a, double b) => new(a.m_value * b);
      public static Irradiance operator *(Irradiance a, Irradiance b) => a * b.m_value;
      public static Irradiance operator %(Irradiance a, double b) => new(a.m_value % b);
      public static Irradiance operator %(Irradiance a, Irradiance b) => a % b.m_value;
      public static Irradiance operator -(Irradiance a, double b) => new(a.m_value - b);
      public static Irradiance operator -(Irradiance a, Irradiance b) => a - b.m_value;
      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is Irradiance o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(Irradiance other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

      // IQuantifiable<>
      public string ToQuantityString(string? format = null, bool preferUnicode = false, bool useFullName = false) => ToUnitString(DefaultUnit, format, preferUnicode, useFullName);
      public double Value { get => m_value; init => m_value = value; }

      // IUnitQuantifiable<>
      public string ToUnitString(IrradianceUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false)
        => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";
      public double ToUnitValue(IrradianceUnit unit = DefaultUnit)
        => unit switch
        {
          IrradianceUnit.WattPerSquareMeter => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #endregion Implemented interfaces

      public override string ToString() => ToQuantityString();
    }
  }
}
