#if NET7_0_OR_GREATER
namespace Flux
{
  namespace Units
  {
    /// <summary>Acceleration, unit of meters per second square. This is an SI derived quantity.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Acceleration"/>
    public readonly record struct Acceleration3D
      : System.IFormattable, IUnitValueQuantifiable<System.Numerics.Vector3, AccelerationUnit>
    {
      private readonly System.Numerics.Vector3 m_value;

      public Acceleration3D(System.Numerics.Vector3 value, AccelerationUnit unit = AccelerationUnit.MeterPerSecondSquared)
        => m_value = unit switch
        {
          AccelerationUnit.MeterPerSecondSquared => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Overloaded operators
      public static Acceleration3D operator -(Acceleration3D v)
        => new(-v.m_value);
      public static Acceleration3D operator +(Acceleration3D a, float b)
        => new(a.m_value + new System.Numerics.Vector3(b));
      public static Acceleration3D operator +(Acceleration3D a, Acceleration3D b)
        => new(a.m_value + b.m_value);
      public static Acceleration3D operator /(Acceleration3D a, float b)
        => new(a.m_value / b);
      public static Acceleration3D operator /(Acceleration3D a, Acceleration3D b)
        => new(a.m_value / b.m_value);
      public static Acceleration3D operator *(Acceleration3D a, float b)
        => new(a.m_value * b);
      public static Acceleration3D operator *(Acceleration3D a, Acceleration3D b)
        => new(a.m_value * b.m_value);
      public static Acceleration3D operator %(Acceleration3D a, float b)
        => new(new System.Numerics.Vector3(a.m_value.X % b, a.m_value.Y % b, a.m_value.Z % b));
      public static Acceleration3D operator %(Acceleration3D a, Acceleration3D b)
        => new(new System.Numerics.Vector3(a.m_value.X % b.m_value.X, a.m_value.Y % b.m_value.Y, a.m_value.Z % b.m_value.Z));
      public static Acceleration3D operator -(Acceleration3D a, float b)
        => new(a.m_value - new System.Numerics.Vector3(b));
      public static Acceleration3D operator -(Acceleration3D a, Acceleration3D b)
        => new(a.m_value - b.m_value);
      #endregion Overloaded operators

      #region Implemented interfaces

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => ToUnitValueString(AccelerationUnit.MeterPerSecondSquared, UnitValueStringOptions.Default with { Format = format, FormatProvider = formatProvider });

      // IQuantifiable<>
      public System.Numerics.Vector3 Value => m_value;

      // IUnitQuantifiable<>
      public System.Numerics.Vector3 GetUnitValue(AccelerationUnit unit)
        => unit switch
        {
          AccelerationUnit.MeterPerSecondSquared => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(AccelerationUnit unit, UnitValueStringOptions options = default)
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
