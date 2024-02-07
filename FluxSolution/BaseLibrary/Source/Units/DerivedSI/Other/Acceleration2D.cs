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

      public Acceleration2D(System.Numerics.Vector2 value, AccelerationUnit unit = Acceleration.DefaultUnit)
        => m_value = unit switch
        {
          AccelerationUnit.MeterPerSecondSquared => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Overloaded operators
      public static Acceleration2D operator -(Acceleration2D v)
        => new(-v.m_value);
      public static Acceleration2D operator +(Acceleration2D a, float b)
        => new(a.m_value + new System.Numerics.Vector2(b));
      public static Acceleration2D operator +(Acceleration2D a, Acceleration2D b)
        => new(a.m_value + b.m_value);
      public static Acceleration2D operator /(Acceleration2D a, float b)
        => new(a.m_value / b);
      public static Acceleration2D operator /(Acceleration2D a, Acceleration2D b)
        => new(a.m_value / b.m_value);
      public static Acceleration2D operator *(Acceleration2D a, float b)
        => new(a.m_value * b);
      public static Acceleration2D operator *(Acceleration2D a, Acceleration2D b)
        => new(a.m_value * b.m_value);
      public static Acceleration2D operator %(Acceleration2D a, float b)
        => new(new System.Numerics.Vector2(a.m_value.X % b, a.m_value.Y % b));
      public static Acceleration2D operator %(Acceleration2D a, Acceleration2D b)
        => new(new System.Numerics.Vector2(a.m_value.X % b.m_value.X, a.m_value.Y % b.m_value.Y));
      public static Acceleration2D operator -(Acceleration2D a, float b)
        => new(a.m_value - new System.Numerics.Vector2(b));
      public static Acceleration2D operator -(Acceleration2D a, Acceleration2D b)
        => new(a.m_value - b.m_value);
      #endregion Overloaded operators

      #region Implemented interfaces

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? formatProvider) => ToValueString(QuantifiableValueStringOptions.Default with { Format = format, FormatProvider = formatProvider });

      // IQuantifiable<>
      public string ToValueString(QuantifiableValueStringOptions options) => ToUnitValueString(Acceleration.DefaultUnit, options);

      public System.Numerics.Vector2 Value => m_value;

      // IUnitQuantifiable<>
      public System.Numerics.Vector2 GetUnitValue(AccelerationUnit unit)
        => unit switch
        {
          AccelerationUnit.MeterPerSecondSquared => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(AccelerationUnit unit, QuantifiableValueStringOptions options)
        => $"{Value.ToString(options.Format, options.CultureInfo)} {unit.GetUnitString(options)}";

      #endregion Implemented interfaces

      public override string ToString() => ToValueString(QuantifiableValueStringOptions.Default);
    }
  }
}
#endif
