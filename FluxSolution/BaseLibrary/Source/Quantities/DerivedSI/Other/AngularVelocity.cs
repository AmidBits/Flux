namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitString(this Quantities.AngularVelocityUnit unit, bool preferUnicode, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Quantities.AngularVelocityUnit.RadianPerSecond => preferUnicode ? "\u33AE" : "rad/s",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Quantities
  {
    public enum AngularVelocityUnit
    {
      RadianPerSecond,
    }

    /// <summary>Angular velocity, unit of radians per second. This is an SI derived quantity.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Angular_velocity"/>
    public readonly struct AngularVelocity
      : System.IComparable, System.IComparable<AngularVelocity>, System.IConvertible, System.IEquatable<AngularVelocity>, System.IFormattable, IUnitQuantifiable<double, AngularVelocityUnit>
    {
      public const AngularVelocityUnit DefaultUnit = AngularVelocityUnit.RadianPerSecond;

      private readonly double m_value;

      public AngularVelocity(double value, AngularVelocityUnit unit = DefaultUnit)
        => m_value = unit switch
        {
          AngularVelocityUnit.RadianPerSecond => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public Frequency ToFrequency()
        => new(m_value / GenericMath.PiX2);

      #region Static methods
      /// <see cref="https://en.wikipedia.org/wiki/Revolutions_per_minute"/>

      public static double ConvertAngularVelocityToRotationalSpeed(double radPerSecond)
        => radPerSecond / GenericMath.PiX2;

      /// <see cref="https://en.wikipedia.org/wiki/Revolutions_per_minute"/>

      public static double ConvertRotationalSpeedToAngularVelocity(double revolutionPerMinute)
        => revolutionPerMinute / 60;


      public static AngularVelocity From(Angle angle, Time time)
        => new(angle.Value / time.Value);

      /// <summary>Creates a new <see cref="AngularVelocity"/> instance from <see cref="LinearVelocity">tangential/linear speed</see> and <see cref="Length">radius</see></summary>
      /// <param name="tangentialSpeed"></param>
      /// <param name="radius"></param>

      public static AngularVelocity From(LinearVelocity tangentialSpeed, Length radius)
        => new(tangentialSpeed.Value / radius.Value);
      #endregion Static methods

      #region Overloaded operators
      public static explicit operator double(AngularVelocity v) => v.m_value;
      public static explicit operator AngularVelocity(double v) => new(v);

      public static bool operator <(AngularVelocity a, AngularVelocity b) => a.CompareTo(b) < 0;
      public static bool operator <=(AngularVelocity a, AngularVelocity b) => a.CompareTo(b) <= 0;
      public static bool operator >(AngularVelocity a, AngularVelocity b) => a.CompareTo(b) > 0;
      public static bool operator >=(AngularVelocity a, AngularVelocity b) => a.CompareTo(b) >= 0;

      public static bool operator ==(AngularVelocity a, AngularVelocity b) => a.Equals(b);
      public static bool operator !=(AngularVelocity a, AngularVelocity b) => !a.Equals(b);

      public static AngularVelocity operator -(AngularVelocity v) => new(-v.m_value);
      public static AngularVelocity operator +(AngularVelocity a, AngularVelocity b) => new(a.m_value + b.m_value);
      public static AngularVelocity operator /(AngularVelocity a, AngularVelocity b) => new(a.m_value / b.m_value);
      public static AngularVelocity operator *(AngularVelocity a, AngularVelocity b) => new(a.m_value * b.m_value);
      public static AngularVelocity operator %(AngularVelocity a, AngularVelocity b) => new(a.m_value % b.m_value);
      public static AngularVelocity operator -(AngularVelocity a, AngularVelocity b) => new(a.m_value - b.m_value);

      public static AngularVelocity operator +(AngularVelocity a, double b) => new(a.m_value + b);
      public static AngularVelocity operator /(AngularVelocity a, double b) => new(a.m_value / b);
      public static AngularVelocity operator *(AngularVelocity a, double b) => new(a.m_value * b);
      public static AngularVelocity operator %(AngularVelocity a, double b) => new(a.m_value % b);
      public static AngularVelocity operator -(AngularVelocity a, double b) => new(a.m_value - b);
      #endregion Overloaded operators

      #region Implemented interfaces
      // IComparable<>
      public int CompareTo(AngularVelocity other) => m_value.CompareTo(other.m_value);
      // IComparable
      public int CompareTo(object? other) => other is not null && other is AngularVelocity o ? CompareTo(o) : -1;

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

      // IEquatable<>
      public bool Equals(AngularVelocity other) => m_value == other.m_value;

      // IFormattable
      public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

      // IQuantifiable<>
      public string ToQuantityString(string? format = null, bool preferUnicode = false, bool useFullName = false)
        => ToUnitString(DefaultUnit, format, preferUnicode, useFullName);

      public double Value { get => m_value; init => m_value = value; }

      // IUnitQuantifiable<>

      public string ToUnitString(AngularVelocityUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false)
        => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";

      public double ToUnitValue(AngularVelocityUnit unit = DefaultUnit)
        => unit switch
        {
          AngularVelocityUnit.RadianPerSecond => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };
      #endregion Implemented interfaces

      #region Object overrides
      public override bool Equals(object? obj) => obj is AngularVelocity o && Equals(o);
      public override int GetHashCode() => m_value.GetHashCode();
      public override string ToString() => $"{GetType().Name} {{ {ToQuantityString()} }}";
      #endregion Object overrides
    }
  }
}
