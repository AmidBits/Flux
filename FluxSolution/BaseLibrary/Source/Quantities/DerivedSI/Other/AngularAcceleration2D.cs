namespace Flux
{
  namespace Quantities
  {
    /// <summary>Angular, acceleration unit of radians per second square. This is an SI derived quantity.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Angular_acceleration"/>
    public readonly record struct AngularAcceleration2D
    : IUnitQuantifiable<Numerics.CartesianCoordinate2<double>, AngularAccelerationUnit>
    {
      private readonly Numerics.CartesianCoordinate2<double> m_value;

      public AngularAcceleration2D(Numerics.CartesianCoordinate2<double> value, AngularAccelerationUnit unit = AngularAcceleration.DefaultUnit)
        => m_value = unit switch
        {
          AngularAccelerationUnit.RadianPerSecondSquared => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Overloaded operators
      public static AngularAcceleration2D operator -(AngularAcceleration2D v)
        => new(-v.m_value);
      public static AngularAcceleration2D operator +(AngularAcceleration2D a, double b)
        => new(a.m_value + b);
      public static AngularAcceleration2D operator +(AngularAcceleration2D a, AngularAcceleration2D b)
        => new(a.m_value + b.m_value);
      public static AngularAcceleration2D operator /(AngularAcceleration2D a, double b)
        => new(a.m_value / b);
      public static AngularAcceleration2D operator /(AngularAcceleration2D a, AngularAcceleration2D b)
        => new(a.m_value / b.m_value);
      public static AngularAcceleration2D operator *(AngularAcceleration2D a, double b)
        => new(a.m_value * b);
      public static AngularAcceleration2D operator *(AngularAcceleration2D a, AngularAcceleration2D b)
        => new(a.m_value * b.m_value);
      public static AngularAcceleration2D operator %(AngularAcceleration2D a, double b)
        => new(a.m_value % b);
      public static AngularAcceleration2D operator %(AngularAcceleration2D a, AngularAcceleration2D b)
        => new(a.m_value % b.m_value);
      public static AngularAcceleration2D operator -(AngularAcceleration2D a, double b)
        => new(a.m_value - b);
      public static AngularAcceleration2D operator -(AngularAcceleration2D a, AngularAcceleration2D b)
        => new(a.m_value - b.m_value);
      #endregion Overloaded operators

      #region Implemented interfaces
      // IQuantifiable<>
      public string ToQuantityString(string? format = null, bool preferUnicode = false, bool useFullName = false)
        => ToUnitString(AngularAcceleration.DefaultUnit, format, preferUnicode, useFullName);

      public Numerics.CartesianCoordinate2<double> Value { get => m_value; init => m_value = value; }

      // IUnitQuantifiable<>
      public string ToUnitString(AngularAccelerationUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false)
        => $"{Value.ToString()} {unit.GetUnitString(preferUnicode, useFullName)}";

      public Numerics.CartesianCoordinate2<double> ToUnitValue(AngularAccelerationUnit unit = AngularAcceleration.DefaultUnit)
        => unit switch
        {
          AngularAccelerationUnit.RadianPerSecondSquared => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };
      #endregion Implemented interfaces

      #region Object overrides
      public override string ToString()
        => $"{GetType().Name} {{ {ToQuantityString()} }}";
      #endregion Object overrides
    }
  }
}
