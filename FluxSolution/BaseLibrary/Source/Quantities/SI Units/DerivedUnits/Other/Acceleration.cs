namespace Flux
{
  public static partial class Fx
  {
    public static string GetUnitString(this Quantities.AccelerationUnit unit, bool preferUnicode = true, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Quantities.AccelerationUnit.MeterPerSecondSquared => preferUnicode ? "\u33A8" : "m/s²",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Quantities
  {
    public enum AccelerationUnit
    {
      /// <summary>This is the default unit for <see cref="Acceleration"/>.</summary>
      MeterPerSecondSquared,
    }

    /// <summary>Acceleration, a scalar quantity, unit of meters per second square. This is an SI derived quantity.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Acceleration"/>
    public readonly record struct Acceleration
      : System.IComparable, System.IComparable<Acceleration>, System.IFormattable, IUnitValueQuantifiable<double, AccelerationUnit>
    {
      /// <summary>
      /// <para>The approximate acceleration due to gravity on the surface of the Moon.</para>
      /// </summary>
      public static Acceleration MoonGravity => new(1.625);

      /// <summary>
      /// <para>The nominal gravitational acceleration of an object in a vacuum near the surface of the Earth.</para>
      /// </summary>
      public static Acceleration StandardGravity => new(9.80665);

      private readonly double m_value;

      public Acceleration(double value, AccelerationUnit unit = AccelerationUnit.MeterPerSecondSquared)
        => m_value = unit switch
        {
          AccelerationUnit.MeterPerSecondSquared => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      /// <summary>
      /// <para>Creates a new acceleration from the length (magnitude) of <paramref name="vector"/> and <paramref name="unit"/>.</para>
      /// </summary>
      /// <param name="vector"></param>
      /// <param name="unit"></param>
      public Acceleration(System.Numerics.Vector2 vector, AccelerationUnit unit = AccelerationUnit.MeterPerSecondSquared) : this(vector.Length(), unit) { }

      /// <summary>
      /// <para>Creates a new acceleration from the length (magnitude) of <paramref name="vector"/> and <paramref name="unit"/>.</para>
      /// </summary>
      /// <param name="vector"></param>
      /// <param name="unit"></param>
      public Acceleration(System.Numerics.Vector3 vector, AccelerationUnit unit = AccelerationUnit.MeterPerSecondSquared) : this(vector.Length(), unit) { }

      #region Overloaded operators

      public static bool operator <(Acceleration a, Acceleration b) => a.CompareTo(b) < 0;
      public static bool operator <=(Acceleration a, Acceleration b) => a.CompareTo(b) <= 0;
      public static bool operator >(Acceleration a, Acceleration b) => a.CompareTo(b) > 0;
      public static bool operator >=(Acceleration a, Acceleration b) => a.CompareTo(b) >= 0;

      public static Acceleration operator -(Acceleration v) => new(-v.m_value);
      public static Acceleration operator +(Acceleration a, double b) => new(a.m_value + b);
      public static Acceleration operator +(Acceleration a, Acceleration b) => a + b.m_value;
      public static Acceleration operator /(Acceleration a, double b) => new(a.m_value / b);
      public static Acceleration operator /(Acceleration a, Acceleration b) => a / b.m_value;
      public static Acceleration operator *(Acceleration a, double b) => new(a.m_value * b);
      public static Acceleration operator *(Acceleration a, Acceleration b) => a * b.m_value;
      public static Acceleration operator %(Acceleration a, double b) => new(a.m_value % b);
      public static Acceleration operator %(Acceleration a, Acceleration b) => a % b.m_value;
      public static Acceleration operator -(Acceleration a, double b) => new(a.m_value - b);
      public static Acceleration operator -(Acceleration a, Acceleration b) => a - b.m_value;

      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is Acceleration o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(Acceleration other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => ToUnitValueString(AccelerationUnit.MeterPerSecondSquared, format, formatProvider);

      // IQuantifiable<>
      /// <summary>
      /// <para>The unit of the <see cref="Acceleration.Value"/> property is in <see cref="AccelerationUnit.MeterPerSecondSquared"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public string GetUnitSymbol(AccelerationUnit unit, bool preferUnicode, bool useFullName) => unit.GetUnitString(preferUnicode, useFullName);

      public double GetUnitValue(AccelerationUnit unit)
        => unit switch
        {
          AccelerationUnit.MeterPerSecondSquared => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(AccelerationUnit unit = AccelerationUnit.MeterPerSecondSquared, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false, bool useFullName = false)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(GetUnitValue(unit).ToString(format, formatProvider));
        sb.Append(unitSpacing.ToSpacingString());
        sb.Append(GetUnitSymbol(unit, preferUnicode, useFullName));
        return sb.ToString();
      }

      #endregion Implemented interfaces

      public override string ToString() => ToString(null, null);
    }
  }
}
