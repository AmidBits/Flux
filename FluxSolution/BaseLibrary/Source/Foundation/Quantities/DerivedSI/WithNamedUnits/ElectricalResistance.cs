namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitString(this ElectricalResistanceUnit unit, bool useNameInsteadOfSymbol = false, bool useUnicodeIfAvailable = false)
      => useNameInsteadOfSymbol ? unit.ToString() : unit switch
      {
        ElectricalResistanceUnit.Ohm => useUnicodeIfAvailable ? "\u2126" : "ohm",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  public enum ElectricalResistanceUnit
  {
    /// <summary>Ohm.</summary>
    Ohm,
  }

  /// <summary>Electric resistance, unit of Ohm.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Electrical_resistance_and_conductance"/>
  public struct ElectricalResistance
    : System.IComparable<ElectricalResistance>, System.IConvertible, System.IEquatable<ElectricalResistance>, ISiDerivedUnitQuantifiable<double, ElectricalResistanceUnit>
  {
    public const ElectricalResistanceUnit DefaultUnit = ElectricalResistanceUnit.Ohm;

    public static ElectricalResistance VonKlitzing
      => new(25812.80745); // 25812.80745;

    private readonly double m_value;

    public ElectricalResistance(double value, ElectricalResistanceUnit unit = DefaultUnit)
      => m_value = unit switch
      {
        ElectricalResistanceUnit.Ohm => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public ElectricalConductance ToElectricalConductance()
      => new(1 / m_value);

    public double Value
      => m_value;

    public string ToUnitString(ElectricalResistanceUnit unit = DefaultUnit, string? format = null, bool useFullName = false, bool preferUnicode = false)
      => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString()}";
    public double ToUnitValue(ElectricalResistanceUnit unit = DefaultUnit)
      => unit switch
      {
        ElectricalResistanceUnit.Ohm => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Static methods
    /// <summary>Creates a new ElectricResistance instance from the specified voltage and current.</summary>
    /// <param name="voltage"></param>
    /// <param name="current"></param>
    public static ElectricalResistance From(Voltage voltage, ElectricCurrent current)
      => new(voltage.Value / current.Value);
    /// <summary>Converts resistor values as if in parallel configuration.</summary>
    public static ElectricalResistance FromParallelResistors(params double[] resistors)
    {
      var sum = 0.0;
      foreach (var resistor in resistors)
        sum += 1 / resistor;
      return (ElectricalResistance)(1 / sum);
    }
    /// <summary>Converts resistor values as if in serial configuration.</summary>
    public static ElectricalResistance FromSerialResistors(params double[] resistors)
    {
      var sum = 0.0;
      foreach (var resistor in resistors)
        sum += resistor;
      return (ElectricalResistance)sum;
    }
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(ElectricalResistance v)
      => v.m_value;
    public static explicit operator ElectricalResistance(double v)
      => new(v);

    public static bool operator <(ElectricalResistance a, ElectricalResistance b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(ElectricalResistance a, ElectricalResistance b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(ElectricalResistance a, ElectricalResistance b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(ElectricalResistance a, ElectricalResistance b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(ElectricalResistance a, ElectricalResistance b)
      => a.Equals(b);
    public static bool operator !=(ElectricalResistance a, ElectricalResistance b)
      => !a.Equals(b);

    public static ElectricalResistance operator -(ElectricalResistance v)
      => new(-v.m_value);
    public static ElectricalResistance operator +(ElectricalResistance a, double b)
      => new(a.m_value + b);
    public static ElectricalResistance operator +(ElectricalResistance a, ElectricalResistance b)
      => a + b.m_value;
    public static ElectricalResistance operator /(ElectricalResistance a, double b)
      => new(a.m_value / b);
    public static ElectricalResistance operator /(ElectricalResistance a, ElectricalResistance b)
      => a / b.m_value;
    public static ElectricalResistance operator *(ElectricalResistance a, double b)
      => new(a.m_value * b);
    public static ElectricalResistance operator *(ElectricalResistance a, ElectricalResistance b)
      => a * b.m_value;
    public static ElectricalResistance operator %(ElectricalResistance a, double b)
      => new(a.m_value % b);
    public static ElectricalResistance operator %(ElectricalResistance a, ElectricalResistance b)
      => a % b.m_value;
    public static ElectricalResistance operator -(ElectricalResistance a, double b)
      => new(a.m_value - b);
    public static ElectricalResistance operator -(ElectricalResistance a, ElectricalResistance b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(ElectricalResistance other)
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
    public bool Equals(ElectricalResistance other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is ElectricalResistance o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
    #endregion Object overrides
  }
}
