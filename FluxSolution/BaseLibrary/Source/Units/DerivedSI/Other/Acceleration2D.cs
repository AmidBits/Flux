#if NET7_0_OR_GREATER
namespace Flux
{
  namespace Units
  {
    /// <summary>Acceleration, unit of meters per second square. This is an SI derived quantity.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Acceleration"/>
    public readonly record struct Acceleration2D
  : System.IFormattable, IUnitValueQuantifiable<System.Numerics.Vector2, AccelerationUnit>
    {
      private readonly System.Numerics.Vector2 m_value;

      public Acceleration2D(System.Numerics.Vector2 value, AccelerationUnit unit = AccelerationUnit.MeterPerSecondSquared)
        => m_value = unit switch
        {
          AccelerationUnit.MeterPerSecondSquared => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Overloaded operators

      public static Acceleration2D operator -(Acceleration2D v) => new(-v.m_value);
      public static Acceleration2D operator +(Acceleration2D a, float b) => new(a.m_value + new System.Numerics.Vector2(b));
      public static Acceleration2D operator +(Acceleration2D a, Acceleration2D b) => new(a.m_value + b.m_value);
      public static Acceleration2D operator /(Acceleration2D a, float b) => new(a.m_value / b);
      public static Acceleration2D operator /(Acceleration2D a, Acceleration2D b) => new(a.m_value / b.m_value);
      public static Acceleration2D operator *(Acceleration2D a, float b) => new(a.m_value * b);
      public static Acceleration2D operator *(Acceleration2D a, Acceleration2D b) => new(a.m_value * b.m_value);
      public static Acceleration2D operator %(Acceleration2D a, float b) => new(new System.Numerics.Vector2(a.m_value.X % b, a.m_value.Y % b));
      public static Acceleration2D operator %(Acceleration2D a, Acceleration2D b) => new(new System.Numerics.Vector2(a.m_value.X % b.m_value.X, a.m_value.Y % b.m_value.Y));
      public static Acceleration2D operator -(Acceleration2D a, float b) => new(a.m_value - new System.Numerics.Vector2(b));
      public static Acceleration2D operator -(Acceleration2D a, Acceleration2D b) => new(a.m_value - b.m_value);

      #endregion Overloaded operators

      #region Implemented interfaces

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => ToUnitValueString(AccelerationUnit.MeterPerSecondSquared, format, formatProvider);

      // IQuantifiable<>
      public System.Numerics.Vector2 Value => m_value;

      // IUnitQuantifiable<>
      public System.Numerics.Vector2 GetUnitValue(AccelerationUnit unit)
        => unit switch
        {
          AccelerationUnit.MeterPerSecondSquared => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(AccelerationUnit unit = AccelerationUnit.MeterPerSecondSquared, string? format = null, System.IFormatProvider? formatProvider = null, bool preferUnicode = false, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool useFullName = false)
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
#endif
