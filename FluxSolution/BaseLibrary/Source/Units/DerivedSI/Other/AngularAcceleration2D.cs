#if NET7_0_OR_GREATER
namespace Flux
{
  namespace Units
  {
    /// <summary>Angular, acceleration unit of radians per second square. This is an SI derived quantity.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Angular_acceleration"/>
    public readonly record struct AngularAcceleration2D
  : IUnitValueQuantifiable<System.Numerics.Vector2, AngularAccelerationUnit>
    {
      private readonly System.Numerics.Vector2 m_value;

      public AngularAcceleration2D(System.Numerics.Vector2 value, AngularAccelerationUnit unit = AngularAcceleration.DefaultUnit)
        => m_value = unit switch
        {
          AngularAccelerationUnit.RadianPerSecondSquared => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Overloaded operators
      public static AngularAcceleration2D operator -(AngularAcceleration2D v)
        => new(-v.m_value);
      public static AngularAcceleration2D operator +(AngularAcceleration2D a, float b)
        => new(a.m_value + new System.Numerics.Vector2(b));
      public static AngularAcceleration2D operator +(AngularAcceleration2D a, AngularAcceleration2D b)
        => new(a.m_value + b.m_value);
      public static AngularAcceleration2D operator /(AngularAcceleration2D a, float b)
        => new(a.m_value / b);
      public static AngularAcceleration2D operator /(AngularAcceleration2D a, AngularAcceleration2D b)
        => new(a.m_value / b.m_value);
      public static AngularAcceleration2D operator *(AngularAcceleration2D a, float b)
        => new(a.m_value * b);
      public static AngularAcceleration2D operator *(AngularAcceleration2D a, AngularAcceleration2D b)
        => new(a.m_value * b.m_value);
      public static AngularAcceleration2D operator %(AngularAcceleration2D a, float b)
        => new(new System.Numerics.Vector2(a.m_value.X % b, a.m_value.Y % b));
      public static AngularAcceleration2D operator %(AngularAcceleration2D a, AngularAcceleration2D b)
        => new(new System.Numerics.Vector2(a.m_value.X % b.m_value.X, a.m_value.Y % b.m_value.Y));
      public static AngularAcceleration2D operator -(AngularAcceleration2D a, float b)
        => new(a.m_value - new System.Numerics.Vector2(b));
      public static AngularAcceleration2D operator -(AngularAcceleration2D a, AngularAcceleration2D b)
        => new(a.m_value - b.m_value);
      #endregion Overloaded operators

      #region Implemented interfaces

      // IQuantifiable<>
      public string ToValueString(string? format = null, bool preferUnicode = false, bool useFullName = false, System.Globalization.CultureInfo? culture = null)
        => ToUnitValueString(AngularAcceleration.DefaultUnit, format, preferUnicode, useFullName, culture);

      public System.Numerics.Vector2 Value { get => m_value; }

      // IUnitQuantifiable<>
      public System.Numerics.Vector2 GetUnitValue(AngularAccelerationUnit unit = AngularAcceleration.DefaultUnit)
        => unit switch
        {
          AngularAccelerationUnit.RadianPerSecondSquared => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(AngularAccelerationUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false, System.Globalization.CultureInfo? culture = null)
        => $"{Value.ToString(format, culture)} {unit.GetUnitString(preferUnicode, useFullName)}";

      #endregion Implemented interfaces

      public override string ToString() => ToValueString();
    }
  }
}
#endif
