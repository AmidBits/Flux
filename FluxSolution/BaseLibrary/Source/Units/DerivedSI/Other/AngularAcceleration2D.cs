#if NET7_0_OR_GREATER
namespace Flux
{
  namespace Units
  {
    /// <summary>Angular, acceleration unit of radians per second square. This is an SI derived quantity.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Angular_acceleration"/>
    public readonly record struct AngularAcceleration2D
      : IUnitValueQuantifiable<System.Numerics.Vector2, AngularAccelerationUnit>
    {
      private readonly System.Numerics.Vector2 m_value;

      public AngularAcceleration2D(System.Numerics.Vector2 value, AngularAccelerationUnit unit = AngularAccelerationUnit.RadianPerSecondSquared)
        => m_value = unit switch
        {
          AngularAccelerationUnit.RadianPerSecondSquared => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Overloaded operators

      public static AngularAcceleration2D operator -(AngularAcceleration2D v) => new(-v.m_value);
      public static AngularAcceleration2D operator +(AngularAcceleration2D a, float b) => new(a.m_value + new System.Numerics.Vector2(b));
      public static AngularAcceleration2D operator +(AngularAcceleration2D a, AngularAcceleration2D b) => new(a.m_value + b.m_value);
      public static AngularAcceleration2D operator /(AngularAcceleration2D a, float b) => new(a.m_value / b);
      public static AngularAcceleration2D operator /(AngularAcceleration2D a, AngularAcceleration2D b) => new(a.m_value / b.m_value);
      public static AngularAcceleration2D operator *(AngularAcceleration2D a, float b) => new(a.m_value * b);
      public static AngularAcceleration2D operator *(AngularAcceleration2D a, AngularAcceleration2D b) => new(a.m_value * b.m_value);
      public static AngularAcceleration2D operator %(AngularAcceleration2D a, float b) => new(new System.Numerics.Vector2(a.m_value.X % b, a.m_value.Y % b));
      public static AngularAcceleration2D operator %(AngularAcceleration2D a, AngularAcceleration2D b) => new(new System.Numerics.Vector2(a.m_value.X % b.m_value.X, a.m_value.Y % b.m_value.Y));
      public static AngularAcceleration2D operator -(AngularAcceleration2D a, float b) => new(a.m_value - new System.Numerics.Vector2(b));
      public static AngularAcceleration2D operator -(AngularAcceleration2D a, AngularAcceleration2D b) => new(a.m_value - b.m_value);

      #endregion Overloaded operators

      #region Implemented interfaces

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => ToUnitValueString(AngularAccelerationUnit.RadianPerSecondSquared, UnitValueStringOptions.Default with { Format = format, FormatProvider = formatProvider });

      // IQuantifiable<>
      public System.Numerics.Vector2 Value { get => m_value; }

      // IUnitQuantifiable<>
      public System.Numerics.Vector2 GetUnitValue(AngularAccelerationUnit unit = AngularAccelerationUnit.RadianPerSecondSquared)
        => unit switch
        {
          AngularAccelerationUnit.RadianPerSecondSquared => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(AngularAccelerationUnit unit, string? format, System.IFormatProvider? formatProvider, bool preferUnicode, UnicodeSpacing unicodeSpacing, bool useFullName)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(GetUnitValue(unit).ToString(format, formatProvider));
        sb.Append(unicodeSpacing.ToSpacingString());
        sb.Append(unit.GetUnitString(preferUnicode, useFullName));
        return sb.ToString();
      }

      public string ToUnitValueString(AngularAccelerationUnit unit, UnitValueStringOptions options = default)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(GetUnitValue(unit).ToString(options.Format, options.FormatProvider));
        sb.Append(options.UnitSpacing.ToSpacingString());
        sb.Append(unit.GetUnitString(options.PreferUnicode, options.UseFullName));
        return sb.ToString();
      }

      #endregion Implemented interfaces

      public override string ToString() => ToString(null, null);
    }
  }
}
#endif
