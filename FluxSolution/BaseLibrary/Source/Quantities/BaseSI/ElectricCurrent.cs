namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitString(this Quantities.ElectricCurrentUnit unit, bool preferUnicode, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Quantities.ElectricCurrentUnit.Ampere => "A",
        Quantities.ElectricCurrentUnit.Milliampere => preferUnicode ? "\u3383" : "mA",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Quantities
  {
    public enum ElectricCurrentUnit
    {
      /// <summary>This is the default unit for mass.</summary>
      Ampere,
      Milliampere,
    }

    /// <summary>Electric current. SI unit of ampere. This is a base quantity.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Electric_current"/>
    public readonly record struct ElectricCurrent
      : System.IComparable, System.IComparable<ElectricCurrent>, System.IConvertible, System.IFormattable, IUnitQuantifiable<double, ElectricCurrentUnit>
    {
      public static readonly ElectricCurrent Zero;

      public const ElectricCurrentUnit DefaultUnit = ElectricCurrentUnit.Ampere;

      private readonly double m_value;

      public ElectricCurrent(double value, ElectricCurrentUnit unit = DefaultUnit)
        => m_value = unit switch
        {
          ElectricCurrentUnit.Ampere => value,
          ElectricCurrentUnit.Milliampere => value / 1000,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Static methods
      /// <summary>Creates a new ElectricCurrent instance from power and voltage.</summary>
      /// <param name="power"></param>
      /// <param name="voltage"></param>
      public static ElectricCurrent From(Power power, Voltage voltage) => new(power.Value / voltage.Value);
      /// <summary>Creates a new ElectricCurrent instance from voltage and resistance.</summary>
      /// <param name="voltage"></param>
      /// <param name="resistance"></param>
      public static ElectricCurrent From(Voltage voltage, ElectricalResistance resistance) => new(voltage.Value / resistance.Value);
      #endregion Static methods

      #region Overloaded operators
      public static explicit operator double(ElectricCurrent v) => v.m_value;
      public static explicit operator ElectricCurrent(double v) => new(v);

      public static bool operator <(ElectricCurrent a, ElectricCurrent b) => a.CompareTo(b) < 0;
      public static bool operator <=(ElectricCurrent a, ElectricCurrent b) => a.CompareTo(b) <= 0;
      public static bool operator >(ElectricCurrent a, ElectricCurrent b) => a.CompareTo(b) > 0;
      public static bool operator >=(ElectricCurrent a, ElectricCurrent b) => a.CompareTo(b) >= 0;

      public static ElectricCurrent operator -(ElectricCurrent v) => new(-v.m_value);
      public static ElectricCurrent operator +(ElectricCurrent a, double b) => new(a.m_value + b);
      public static ElectricCurrent operator +(ElectricCurrent a, ElectricCurrent b) => a + b.m_value;
      public static ElectricCurrent operator /(ElectricCurrent a, double b) => new(a.m_value / b);
      public static ElectricCurrent operator /(ElectricCurrent a, ElectricCurrent b) => a / b.m_value;
      public static ElectricCurrent operator *(ElectricCurrent a, double b) => new(a.m_value * b);
      public static ElectricCurrent operator *(ElectricCurrent a, ElectricCurrent b) => a * b.m_value;
      public static ElectricCurrent operator %(ElectricCurrent a, double b) => new(a.m_value % b);
      public static ElectricCurrent operator %(ElectricCurrent a, ElectricCurrent b) => a % b.m_value;
      public static ElectricCurrent operator -(ElectricCurrent a, double b) => new(a.m_value - b);
      public static ElectricCurrent operator -(ElectricCurrent a, ElectricCurrent b) => a - b.m_value;
      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is ElectricCurrent o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(ElectricCurrent other) => m_value.CompareTo(other.m_value);

      #region IConvertible
      public System.TypeCode GetTypeCode() => System.TypeCode.Object;
      public bool ToBoolean(System.IFormatProvider? provider) => Value != 0;
      public byte ToByte(System.IFormatProvider? provider) => System.Convert.ToByte(Value);
      public char ToChar(System.IFormatProvider? provider) => System.Convert.ToChar(Value);
      public System.DateTime ToDateTime(System.IFormatProvider? provider) => System.Convert.ToDateTime(Value);
      public decimal ToDecimal(System.IFormatProvider? provider) => System.Convert.ToDecimal(Value);
      public double ToDouble(System.IFormatProvider? provider) => System.Convert.ToDouble(Value);
      public short ToInt16(System.IFormatProvider? provider) => System.Convert.ToInt16(Value);
      public int ToInt32(System.IFormatProvider? provider) => System.Convert.ToInt32(Value);
      public long ToInt64(System.IFormatProvider? provider) => System.Convert.ToInt64(Value);
      [System.CLSCompliant(false)] public sbyte ToSByte(System.IFormatProvider? provider) => System.Convert.ToSByte(Value);
      public float ToSingle(System.IFormatProvider? provider) => System.Convert.ToSingle(Value);
      public string ToString(System.IFormatProvider? provider) => string.Format(provider, "{0}", Value);
      public object ToType(System.Type conversionType, System.IFormatProvider? provider) => System.Convert.ChangeType(Value, conversionType, provider);
      [System.CLSCompliant(false)] public ushort ToUInt16(System.IFormatProvider? provider) => System.Convert.ToUInt16(Value);
      [System.CLSCompliant(false)] public uint ToUInt32(System.IFormatProvider? provider) => System.Convert.ToUInt32(Value);
      [System.CLSCompliant(false)] public ulong ToUInt64(System.IFormatProvider? provider) => System.Convert.ToUInt64(Value);
      #endregion IConvertible

      // IFormattable
      public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

      // IQuantifiable<>
      public string ToQuantityString(string? format = null, bool preferUnicode = false, bool useFullName = false) => ToUnitString(DefaultUnit, format, preferUnicode, useFullName);
      public double Value { get => m_value; init => m_value = value; }

      // IUnitQuantifiable<>
      public string ToUnitString(ElectricCurrentUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false)
        => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";
      public double ToUnitValue(ElectricCurrentUnit unit = DefaultUnit)
        => unit switch
        {
          ElectricCurrentUnit.Milliampere => m_value * 1000,
          ElectricCurrentUnit.Ampere => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #endregion Implemented interfaces

      public override string ToString() => ToQuantityString();
    }
  }
}