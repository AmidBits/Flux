namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.IrradianceUnit unit, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Units.IrradianceUnit.WattPerSquareMeter => "W/m²",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum IrradianceUnit
    {
      /// <summary>This is the default unit for <see cref="Irradiance"/>.</summary>
      WattPerSquareMeter,
    }

    /// <summary>irradiance, unit of watt per square meter.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Irradiance"/>
    public readonly record struct Irradiance
      : System.IComparable, System.IComparable<Irradiance>, System.IFormattable, IUnitValueQuantifiable<double, IrradianceUnit>
    {
      private readonly double m_value;

      public Irradiance(double value, IrradianceUnit unit = IrradianceUnit.WattPerSquareMeter)
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
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => ToUnitValueString(IrradianceUnit.WattPerSquareMeter, format, formatProvider);

      // IQuantifiable<>
      /// <summary>
      /// <para>The unit of the <see cref="Irradiance.Value"/> property is in <see cref="IrradianceUnit.WattPerSquareMeter"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(IrradianceUnit unit)
        => unit switch
        {
          IrradianceUnit.WattPerSquareMeter => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(IrradianceUnit unit = IrradianceUnit.WattPerSquareMeter, string? format = null, System.IFormatProvider? formatProvider = null, bool preferUnicode = false, UnicodeSpacing unicodeSpacing = UnicodeSpacing.None, bool useFullName = false)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(GetUnitValue(unit).ToString(format, formatProvider));
        sb.Append(unicodeSpacing.ToSpacingString());
        sb.Append(unit.GetUnitString(useFullName));
        return sb.ToString();
      }

      #endregion Implemented interfaces

      public override string ToString() => ToString(null, null);
    }
  }
}
