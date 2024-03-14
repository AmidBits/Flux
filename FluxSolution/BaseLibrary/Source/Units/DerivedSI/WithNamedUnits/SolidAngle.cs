namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.SolidAngleUnit unit, bool preferUnicode, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Units.SolidAngleUnit.Steradian => preferUnicode ? "\u33DB" : "sr",
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
        => ToUnitValueString(SolidAngleUnit.Steradian, format, formatProvider);

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

      public string ToUnitValueString(SolidAngleUnit unit = SolidAngleUnit.Steradian, string? format = null, System.IFormatProvider? formatProvider = null, bool preferUnicode = false, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool useFullName = false)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(GetUnitValue(unit).ToString(format, formatProvider));
        sb.Append(unitSpacing.ToSpacingString());
        sb.Append(unit.GetUnitString(preferUnicode, useFullName));
        return sb.ToString();
      }

      #endregion Implemented interfaces

      public override string ToString() => ToString(null, null);
    }
  }
}
