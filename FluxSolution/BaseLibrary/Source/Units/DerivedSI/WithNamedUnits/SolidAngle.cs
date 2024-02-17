namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.SolidAngleUnit unit, Units.UnitValueStringOptions options)
      => options.UseFullName ? unit.ToString() : unit switch
      {
        Units.SolidAngleUnit.Steradian => options.PreferUnicode ? "\u33DB" : "sr",
        Units.SolidAngleUnit.Spat => "sp",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum SolidAngleUnit
    {
      /// <summary>This is the default unit for <see cref="SolidAngle"/>.</summary>
      Steradian,
      Spat,
    }

    /// <summary>Solid angle. Unit of steradian.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Solid_angle"/>
    public readonly record struct SolidAngle
      : System.IComparable, System.IComparable<SolidAngle>, System.IFormattable, IUnitValueQuantifiable<double, SolidAngleUnit>
    {
      private readonly double m_value;

      public SolidAngle(double value, SolidAngleUnit unit = SolidAngleUnit.Steradian)
        => m_value = unit switch
        {
          SolidAngleUnit.Spat => value / (System.Math.Tau + System.Math.Tau),
          SolidAngleUnit.Steradian => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Overloaded operators
      public static explicit operator double(SolidAngle v) => v.m_value;
      public static explicit operator SolidAngle(double v) => new(v);

      public static bool operator <(SolidAngle a, SolidAngle b) => a.CompareTo(b) < 0;
      public static bool operator <=(SolidAngle a, SolidAngle b) => a.CompareTo(b) <= 0;
      public static bool operator >(SolidAngle a, SolidAngle b) => a.CompareTo(b) > 0;
      public static bool operator >=(SolidAngle a, SolidAngle b) => a.CompareTo(b) >= 0;

      public static SolidAngle operator -(SolidAngle v) => new(-v.m_value);
      public static SolidAngle operator +(SolidAngle a, double b) => new(a.m_value + b);
      public static SolidAngle operator +(SolidAngle a, SolidAngle b) => a + b.m_value;
      public static SolidAngle operator /(SolidAngle a, double b) => new(a.m_value / b);
      public static SolidAngle operator /(SolidAngle a, SolidAngle b) => a / b.m_value;
      public static SolidAngle operator *(SolidAngle a, double b) => new(a.m_value * b);
      public static SolidAngle operator *(SolidAngle a, SolidAngle b) => a * b.m_value;
      public static SolidAngle operator %(SolidAngle a, double b) => new(a.m_value % b);
      public static SolidAngle operator %(SolidAngle a, SolidAngle b) => a % b.m_value;
      public static SolidAngle operator -(SolidAngle a, double b) => new(a.m_value - b);
      public static SolidAngle operator -(SolidAngle a, SolidAngle b) => a - b.m_value;
      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is SolidAngle o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(SolidAngle other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => ToUnitValueString(SolidAngleUnit.Steradian, UnitValueStringOptions.Default with { Format = format, FormatProvider = formatProvider });

      // IQuantifiable<>
      /// <summary>
      /// <para>The unit of the <see cref="SolidAngle.Value"/> property is in <see cref="SolidAngleUnit.Steradian"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(SolidAngleUnit unit)
        => unit switch
        {
          SolidAngleUnit.Spat => m_value * (System.Math.Tau + System.Math.Tau),
          SolidAngleUnit.Steradian => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(SolidAngleUnit unit, UnitValueStringOptions options = default)
        => $"{string.Format(options.CultureInfo, $"{{0{(options.Format is null ? string.Empty : $":{options.Format}")}}}", GetUnitValue(unit))} {unit.GetUnitString(options)}";

      #endregion Implemented interfaces
    }
  }
}
