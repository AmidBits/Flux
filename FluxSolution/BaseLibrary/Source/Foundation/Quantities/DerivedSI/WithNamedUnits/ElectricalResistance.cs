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
    : System.IComparable, System.IComparable<ElectricalResistance>, System.IConvertible, System.IEquatable<ElectricalResistance>, System.IFormattable, IUnitQuantifiable<double, ElectricalResistanceUnit>
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

    [System.Diagnostics.Contracts.Pure]
    public ElectricalConductance ToElectricalConductance()
      => new(1 / m_value);

    #region Static methods
    /// <summary>Creates a new ElectricResistance instance from the specified voltage and current.</summary>
    /// <param name="voltage"></param>
    /// <param name="current"></param>
    [System.Diagnostics.Contracts.Pure]
    public static ElectricalResistance From(Voltage voltage, ElectricCurrent current)
      => new(voltage.Value / current.Value);
    /// <summary>Converts resistor values as if in parallel configuration.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static ElectricalResistance FromParallelResistors(params double[] resistors)
    {
      var sum = 0.0;
      foreach (var resistor in resistors)
        sum += 1 / resistor;
      return (ElectricalResistance)(1 / sum);
    }
    /// <summary>Converts resistor values as if in serial configuration.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static ElectricalResistance FromSerialResistors(params double[] resistors)
    {
      var sum = 0.0;
      foreach (var resistor in resistors)
        sum += resistor;
      return (ElectricalResistance)sum;
    }
    #endregion Static methods

    #region Overloaded operators
    [System.Diagnostics.Contracts.Pure] public static explicit operator double(ElectricalResistance v) => v.m_value;
    [System.Diagnostics.Contracts.Pure] public static explicit operator ElectricalResistance(double v) => new(v);

    [System.Diagnostics.Contracts.Pure] public static bool operator <(ElectricalResistance a, ElectricalResistance b) => a.CompareTo(b) < 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator <=(ElectricalResistance a, ElectricalResistance b) => a.CompareTo(b) <= 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator >(ElectricalResistance a, ElectricalResistance b) => a.CompareTo(b) > 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator >=(ElectricalResistance a, ElectricalResistance b) => a.CompareTo(b) >= 0;

    [System.Diagnostics.Contracts.Pure] public static bool operator ==(ElectricalResistance a, ElectricalResistance b) => a.Equals(b);
    [System.Diagnostics.Contracts.Pure] public static bool operator !=(ElectricalResistance a, ElectricalResistance b) => !a.Equals(b);

    [System.Diagnostics.Contracts.Pure] public static ElectricalResistance operator -(ElectricalResistance v) => new(-v.m_value);
    [System.Diagnostics.Contracts.Pure] public static ElectricalResistance operator +(ElectricalResistance a, double b) => new(a.m_value + b);
    [System.Diagnostics.Contracts.Pure] public static ElectricalResistance operator +(ElectricalResistance a, ElectricalResistance b) => a + b.m_value;
    [System.Diagnostics.Contracts.Pure] public static ElectricalResistance operator /(ElectricalResistance a, double b) => new(a.m_value / b);
    [System.Diagnostics.Contracts.Pure] public static ElectricalResistance operator /(ElectricalResistance a, ElectricalResistance b) => a / b.m_value;
    [System.Diagnostics.Contracts.Pure] public static ElectricalResistance operator *(ElectricalResistance a, double b) => new(a.m_value * b);
    [System.Diagnostics.Contracts.Pure] public static ElectricalResistance operator *(ElectricalResistance a, ElectricalResistance b) => a * b.m_value;
    [System.Diagnostics.Contracts.Pure] public static ElectricalResistance operator %(ElectricalResistance a, double b) => new(a.m_value % b);
    [System.Diagnostics.Contracts.Pure] public static ElectricalResistance operator %(ElectricalResistance a, ElectricalResistance b) => a % b.m_value;
    [System.Diagnostics.Contracts.Pure] public static ElectricalResistance operator -(ElectricalResistance a, double b) => new(a.m_value - b);
    [System.Diagnostics.Contracts.Pure] public static ElectricalResistance operator -(ElectricalResistance a, ElectricalResistance b) => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable<>
    [System.Diagnostics.Contracts.Pure] public int CompareTo(ElectricalResistance other) => m_value.CompareTo(other.m_value);
    // IComparable
    [System.Diagnostics.Contracts.Pure] public int CompareTo(object? other) => other is not null && other is ElectricalResistance o ? CompareTo(o) : -1;

    #region IConvertible
    [System.Diagnostics.Contracts.Pure] public System.TypeCode GetTypeCode() => System.TypeCode.Object;
    [System.Diagnostics.Contracts.Pure] public bool ToBoolean(System.IFormatProvider? provider) => m_value != 0;
    [System.Diagnostics.Contracts.Pure] public byte ToByte(System.IFormatProvider? provider) => System.Convert.ToByte(m_value);
    [System.Diagnostics.Contracts.Pure] public char ToChar(System.IFormatProvider? provider) => System.Convert.ToChar(m_value);
    [System.Diagnostics.Contracts.Pure] public System.DateTime ToDateTime(System.IFormatProvider? provider) => System.Convert.ToDateTime(m_value);
    [System.Diagnostics.Contracts.Pure] public decimal ToDecimal(System.IFormatProvider? provider) => System.Convert.ToDecimal(m_value);
    [System.Diagnostics.Contracts.Pure] public double ToDouble(System.IFormatProvider? provider) => System.Convert.ToDouble(m_value);
    [System.Diagnostics.Contracts.Pure] public short ToInt16(System.IFormatProvider? provider) => System.Convert.ToInt16(m_value);
    [System.Diagnostics.Contracts.Pure] public int ToInt32(System.IFormatProvider? provider) => System.Convert.ToInt32(m_value);
    [System.Diagnostics.Contracts.Pure] public long ToInt64(System.IFormatProvider? provider) => System.Convert.ToInt64(m_value);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public sbyte ToSByte(System.IFormatProvider? provider) => System.Convert.ToSByte(m_value);
    [System.Diagnostics.Contracts.Pure] public float ToSingle(System.IFormatProvider? provider) => System.Convert.ToSingle(m_value);
    [System.Diagnostics.Contracts.Pure] public string ToString(System.IFormatProvider? provider) => string.Format(provider, "{0}", m_value);
    [System.Diagnostics.Contracts.Pure] public object ToType(System.Type conversionType, System.IFormatProvider? provider) => System.Convert.ChangeType(m_value, conversionType, provider);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public ushort ToUInt16(System.IFormatProvider? provider) => System.Convert.ToUInt16(m_value);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public uint ToUInt32(System.IFormatProvider? provider) => System.Convert.ToUInt32(m_value);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public ulong ToUInt64(System.IFormatProvider? provider) => System.Convert.ToUInt64(m_value);
    #endregion IConvertible

    // IEquatable<>
    [System.Diagnostics.Contracts.Pure] public bool Equals(ElectricalResistance other) => m_value == other.m_value;

    // IFormattable
    [System.Diagnostics.Contracts.Pure] public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

    // ISiDerivedUnitQuantifiable<>
    [System.Diagnostics.Contracts.Pure]
    public double Value
      => m_value;
    [System.Diagnostics.Contracts.Pure]
    public string ToUnitString(ElectricalResistanceUnit unit = DefaultUnit, string? format = null, bool useFullName = false, bool preferUnicode = false)
      => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString()}";
    [System.Diagnostics.Contracts.Pure]
    public double ToUnitValue(ElectricalResistanceUnit unit = DefaultUnit)
      => unit switch
      {
        ElectricalResistanceUnit.Ohm => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
    #endregion Implemented interfaces

    #region Object overrides
    [System.Diagnostics.Contracts.Pure] public override bool Equals(object? obj) => obj is ElectricalResistance o && Equals(o);
    [System.Diagnostics.Contracts.Pure] public override int GetHashCode() => m_value.GetHashCode();
    [System.Diagnostics.Contracts.Pure] public override string ToString() => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
    #endregion Object overrides
  }
}
