namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static ElectricResistance Create(this ElectricResistanceUnit unit, double value)
      => new(value, unit);
    public static string GetUnitSymbol(this ElectricResistanceUnit unit)
      => unit switch
      {
        ElectricResistanceUnit.Ohm => " \u2126",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  public enum ElectricResistanceUnit
  {
    Ohm,
  }

  /// <summary>Electric resistance unit of Ohm.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Electric_resistance"/>
  public struct ElectricResistance
    : System.IComparable<ElectricResistance>, System.IConvertible, System.IEquatable<ElectricResistance>, IValueSiDerivedUnit<double>
  {
    public const ElectricResistanceUnit DefaultUnit = ElectricResistanceUnit.Ohm;

    public static ElectricResistance VonKlitzing
      => new(25812.80745); // 25812.80745;

    private readonly double m_value;

    public ElectricResistance(double value, ElectricResistanceUnit unit = DefaultUnit)
      => m_value = unit switch
      {
        ElectricResistanceUnit.Ohm => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double Value
      => m_value;

    public string ToUnitString(ElectricResistanceUnit unit = DefaultUnit, string? format = null)
      => $"{(format is null ? ToUnitValue(unit) : string.Format($"{{0:{format}}}", ToUnitValue(unit)))}{unit.GetUnitSymbol()}";
    public double ToUnitValue(ElectricResistanceUnit unit = DefaultUnit)
      => unit switch
      {
        ElectricResistanceUnit.Ohm => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Static methods
    /// <summary>Creates a new ElectricResistance instance from the specified voltage and current.</summary>
    /// <param name="voltage"></param>
    /// <param name="current"></param>
    public static ElectricResistance From(Voltage voltage, ElectricCurrent current)
      => new(voltage.Value / current.Value);
    /// <summary>Converts resistor values as if in parallel configuration.</summary>
    public static ElectricResistance FromParallelResistors(params double[] resistors)
    {
      var sum = 0.0;
      foreach (var resistor in resistors)
        sum += 1 / resistor;
      return (ElectricResistance)(1 / sum);
    }
    /// <summary>Converts resistor values as if in serial configuration.</summary>
    public static ElectricResistance FromSerialResistors(params double[] resistors)
    {
      var sum = 0.0;
      foreach (var resistor in resistors)
        sum += resistor;
      return (ElectricResistance)sum;
    }
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(ElectricResistance v)
      => v.m_value;
    public static explicit operator ElectricResistance(double v)
      => new(v);

    public static bool operator <(ElectricResistance a, ElectricResistance b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(ElectricResistance a, ElectricResistance b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(ElectricResistance a, ElectricResistance b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(ElectricResistance a, ElectricResistance b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(ElectricResistance a, ElectricResistance b)
      => a.Equals(b);
    public static bool operator !=(ElectricResistance a, ElectricResistance b)
      => !a.Equals(b);

    public static ElectricResistance operator -(ElectricResistance v)
      => new(-v.m_value);
    public static ElectricResistance operator +(ElectricResistance a, double b)
      => new(a.m_value + b);
    public static ElectricResistance operator +(ElectricResistance a, ElectricResistance b)
      => a + b.m_value;
    public static ElectricResistance operator /(ElectricResistance a, double b)
      => new(a.m_value / b);
    public static ElectricResistance operator /(ElectricResistance a, ElectricResistance b)
      => a / b.m_value;
    public static ElectricResistance operator *(ElectricResistance a, double b)
      => new(a.m_value * b);
    public static ElectricResistance operator *(ElectricResistance a, ElectricResistance b)
      => a * b.m_value;
    public static ElectricResistance operator %(ElectricResistance a, double b)
      => new(a.m_value % b);
    public static ElectricResistance operator %(ElectricResistance a, ElectricResistance b)
      => a % b.m_value;
    public static ElectricResistance operator -(ElectricResistance a, double b)
      => new(a.m_value - b);
    public static ElectricResistance operator -(ElectricResistance a, ElectricResistance b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(ElectricResistance other)
      => m_value.CompareTo(other.m_value);

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

    // IEquatable
    public bool Equals(ElectricResistance other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is ElectricResistance o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
    #endregion Object overrides
  }
}
