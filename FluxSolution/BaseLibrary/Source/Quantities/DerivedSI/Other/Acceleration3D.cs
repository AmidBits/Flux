namespace Flux
{
  namespace Quantities
  {
    /// <summary>Acceleration, unit of meters per second square. This is an SI derived quantity.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Acceleration"/>
    public readonly record struct Acceleration3D
    : IUnitQuantifiable<Numerics.CartesianCoordinate3<double>, AccelerationUnit>
    {
      public static readonly Acceleration3D Zero;

      private readonly Numerics.CartesianCoordinate3<double> m_value;

      public Acceleration3D(Numerics.CartesianCoordinate3<double> value, AccelerationUnit unit = Acceleration.DefaultUnit)
        => m_value = unit switch
        {
          AccelerationUnit.MeterPerSecondSquared => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Overloaded operators
      public static Acceleration3D operator -(Acceleration3D v)
        => new(-v.m_value);
      public static Acceleration3D operator +(Acceleration3D a, double b)
        => new(a.m_value + b);
      public static Acceleration3D operator +(Acceleration3D a, Acceleration3D b)
        => new(a.m_value + b.m_value);
      public static Acceleration3D operator /(Acceleration3D a, double b)
        => new(a.m_value / b);
      public static Acceleration3D operator /(Acceleration3D a, Acceleration3D b)
        => new(a.m_value / b.m_value);
      public static Acceleration3D operator *(Acceleration3D a, double b)
        => new(a.m_value * b);
      public static Acceleration3D operator *(Acceleration3D a, Acceleration3D b)
        => new(a.m_value * b.m_value);
      public static Acceleration3D operator %(Acceleration3D a, double b)
        => new(a.m_value % b);
      public static Acceleration3D operator %(Acceleration3D a, Acceleration3D b)
        => new(a.m_value % b.m_value);
      public static Acceleration3D operator -(Acceleration3D a, double b)
        => new(a.m_value - b);
      public static Acceleration3D operator -(Acceleration3D a, Acceleration3D b)
        => new(a.m_value - b.m_value);
      #endregion Overloaded operators

      #region Implemented interfaces

      // IQuantifiable<>
      public string ToQuantityString(string? format = null, bool preferUnicode = false, bool useFullName = false) => ToUnitString(Acceleration.DefaultUnit, format, preferUnicode, useFullName);
      public Numerics.CartesianCoordinate3<double> Value { get => m_value; init => m_value = value; }

      // IUnitQuantifiable<>
      public string ToUnitString(AccelerationUnit unit = Acceleration.DefaultUnit, string? format = null, bool preferUnicode = false, bool useFullName = false) => $"{Value.ToString()} {unit.GetUnitString(preferUnicode, useFullName)}";
      public Numerics.CartesianCoordinate3<double> ToUnitValue(AccelerationUnit unit = Acceleration.DefaultUnit)
        => unit switch
        {
          AccelerationUnit.MeterPerSecondSquared => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #endregion Implemented interfaces

      public override string ToString() => ToQuantityString();
    }
  }
}
