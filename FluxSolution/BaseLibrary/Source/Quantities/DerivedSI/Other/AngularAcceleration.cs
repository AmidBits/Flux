namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitString(this Quantities.AngularAccelerationUnit unit, bool preferUnicode, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Quantities.AngularAccelerationUnit.RadianPerSecondSquared => preferUnicode ? "\u33AF" : "rad/s²",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Quantities
  {
    public enum AngularAccelerationUnit
    {
      RadianPerSecondSquared,
    }

    /// <summary>Angular, acceleration unit of radians per second square. This is an SI derived quantity.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Angular_acceleration"/>
    public readonly record struct AngularAcceleration
      : System.IComparable, System.IComparable<AngularAcceleration>, System.IConvertible, System.IFormattable, IUnitQuantifiable<double, AngularAccelerationUnit>
    {
      public static readonly AngularAcceleration Zero;

      public const AngularAccelerationUnit DefaultUnit = AngularAccelerationUnit.RadianPerSecondSquared;

      private readonly double m_value;

      public AngularAcceleration(double value, AngularAccelerationUnit unit = DefaultUnit)
        => m_value = unit switch
        {
          AngularAccelerationUnit.RadianPerSecondSquared => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Overloaded operators
      public static explicit operator double(AngularAcceleration v) => v.m_value;
      public static explicit operator AngularAcceleration(double v) => new(v);

      public static bool operator <(AngularAcceleration a, AngularAcceleration b) => a.CompareTo(b) < 0;
      public static bool operator <=(AngularAcceleration a, AngularAcceleration b) => a.CompareTo(b) <= 0;
      public static bool operator >(AngularAcceleration a, AngularAcceleration b) => a.CompareTo(b) > 0;
      public static bool operator >=(AngularAcceleration a, AngularAcceleration b) => a.CompareTo(b) >= 0;

      public static AngularAcceleration operator -(AngularAcceleration v) => new(-v.m_value);
      public static AngularAcceleration operator +(AngularAcceleration a, double b) => new(a.m_value + b);
      public static AngularAcceleration operator +(AngularAcceleration a, AngularAcceleration b) => a + b.m_value;
      public static AngularAcceleration operator /(AngularAcceleration a, double b) => new(a.m_value / b);
      public static AngularAcceleration operator /(AngularAcceleration a, AngularAcceleration b) => a / b.m_value;
      public static AngularAcceleration operator *(AngularAcceleration a, double b) => new(a.m_value * b);
      public static AngularAcceleration operator *(AngularAcceleration a, AngularAcceleration b) => a * b.m_value;
      public static AngularAcceleration operator %(AngularAcceleration a, double b) => new(a.m_value % b);
      public static AngularAcceleration operator %(AngularAcceleration a, AngularAcceleration b) => a % b.m_value;
      public static AngularAcceleration operator -(AngularAcceleration a, double b) => new(a.m_value - b);
      public static AngularAcceleration operator -(AngularAcceleration a, AngularAcceleration b) => a - b.m_value;
      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is AngularAcceleration o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(AngularAcceleration other) => m_value.CompareTo(other.m_value);

      #region IConvertible
      public System.TypeCode GetTypeCode() => System.TypeCode.Object;
      public bool ToBoolean(System.IFormatProvider? provider) => m_value != 0;
      public byte ToByte(System.IFormatProvider? provider) => System.Convert.ToByte(m_value);
      public char ToChar(System.IFormatProvider? provider) => System.Convert.ToChar(m_value);
      public System.DateTime ToDateTime(System.IFormatProvider? provider) => System.Convert.ToDateTime(m_value);
      public decimal ToDecimal(System.IFormatProvider? provider) => System.Convert.ToDecimal(m_value);
      public double ToDouble(System.IFormatProvider? provider) => System.Convert.ToDouble(m_value);
      public short ToInt16(System.IFormatProvider? provider) => System.Convert.ToInt16(m_value);
      public int ToInt32(System.IFormatProvider? provider) => System.Convert.ToInt32(m_value);
      public long ToInt64(System.IFormatProvider? provider) => System.Convert.ToInt64(m_value);
      [System.CLSCompliant(false)] public sbyte ToSByte(System.IFormatProvider? provider) => System.Convert.ToSByte(m_value);
      public float ToSingle(System.IFormatProvider? provider) => System.Convert.ToSingle(m_value);
      public string ToString(System.IFormatProvider? provider) => string.Format(provider, "{0}", m_value);
      public object ToType(System.Type conversionType, System.IFormatProvider? provider) => System.Convert.ChangeType(m_value, conversionType, provider);
      [System.CLSCompliant(false)] public ushort ToUInt16(System.IFormatProvider? provider) => System.Convert.ToUInt16(m_value);
      [System.CLSCompliant(false)] public uint ToUInt32(System.IFormatProvider? provider) => System.Convert.ToUInt32(m_value);
      [System.CLSCompliant(false)] public ulong ToUInt64(System.IFormatProvider? provider) => System.Convert.ToUInt64(m_value);
      #endregion IConvertible

      // IFormattable
      public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

      // IQuantifiable<>
      public string ToQuantityString(string? format = null, bool preferUnicode = false, bool useFullName = false) => ToUnitString(DefaultUnit, format, preferUnicode, useFullName);
      public double Value { get => m_value; init => m_value = value; }

      // IUnitQuantifiable<>
      public string ToUnitString(AngularAccelerationUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false)
        => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";
      public double ToUnitValue(AngularAccelerationUnit unit = DefaultUnit)
        => unit switch
        {
          AngularAccelerationUnit.RadianPerSecondSquared => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #endregion Implemented interfaces

      public override string ToString() => ToQuantityString();
    }
  }
}
