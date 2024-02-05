#if NET7_0_OR_GREATER
namespace Flux
{
  namespace Units
  {
    /// <summary>Angular, acceleration unit of radians per second square. This is an SI derived quantity.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Angular_acceleration"/>
    public readonly record struct AngularAcceleration3D
  : IUnitValueQuantifiable<System.Numerics.Vector3, AngularAccelerationUnit>
    {
      private readonly System.Numerics.Vector3 m_value;

      public AngularAcceleration3D(System.Numerics.Vector3 value, AngularAccelerationUnit unit = AngularAcceleration.DefaultUnit)
        => m_value = unit switch
        {
          AngularAccelerationUnit.RadianPerSecondSquared => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Overloaded operators
      public static AngularAcceleration3D operator -(AngularAcceleration3D v)
        => new(-v.m_value);
      public static AngularAcceleration3D operator +(AngularAcceleration3D a, float b)
        => new(a.m_value + new System.Numerics.Vector3(b));
      public static AngularAcceleration3D operator +(AngularAcceleration3D a, AngularAcceleration3D b)
        => new(a.m_value + b.m_value);
      public static AngularAcceleration3D operator /(AngularAcceleration3D a, float b)
        => new(a.m_value / b);
      public static AngularAcceleration3D operator /(AngularAcceleration3D a, AngularAcceleration3D b)
        => new(a.m_value / b.m_value);
      public static AngularAcceleration3D operator *(AngularAcceleration3D a, float b)
        => new(a.m_value * b);
      public static AngularAcceleration3D operator *(AngularAcceleration3D a, AngularAcceleration3D b)
        => new(a.m_value * b.m_value);
      public static AngularAcceleration3D operator %(AngularAcceleration3D a, float b)
        => new(new System.Numerics.Vector3(a.m_value.X % b, a.m_value.Y % b, a.m_value.Z % b));
      public static AngularAcceleration3D operator %(AngularAcceleration3D a, AngularAcceleration3D b)
        => new(new System.Numerics.Vector3(a.m_value.X % b.m_value.X, a.m_value.Y % b.m_value.Y, a.m_value.Z % b.m_value.Z));
      public static AngularAcceleration3D operator -(AngularAcceleration3D a, float b)
        => new(a.m_value - new System.Numerics.Vector3(b));
      public static AngularAcceleration3D operator -(AngularAcceleration3D a, AngularAcceleration3D b)
        => new(a.m_value - b.m_value);
      #endregion Overloaded operators

      #region Implemented interfaces
      // IQuantifiable<>
      public string ToValueString(QuantifiableValueStringOptions options) => ToUnitValueString(AngularAcceleration.DefaultUnit, options);

      public System.Numerics.Vector3 Value { get => m_value; }

      // IUnitQuantifiable<>
      public System.Numerics.Vector3 GetUnitValue(AngularAccelerationUnit unit = AngularAcceleration.DefaultUnit)
        => unit switch
        {
          AngularAccelerationUnit.RadianPerSecondSquared => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(AngularAccelerationUnit unit, QuantifiableValueStringOptions options)
        => $"{Value.ToString(options.Format, options.CultureInfo)} {unit.GetUnitString(options)}";

      #endregion Implemented interfaces

      public override string ToString() => ToValueString(QuantifiableValueStringOptions.Default);
    }
  }
}
#endif
