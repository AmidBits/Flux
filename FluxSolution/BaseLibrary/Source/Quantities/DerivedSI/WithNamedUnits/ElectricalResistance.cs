namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitString(this Quantities.ElectricalResistanceUnit unit, bool preferUnicode = false, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Quantities.ElectricalResistanceUnit.Ohm => preferUnicode ? "\u2126" : "ohm",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Quantities
  {
    public enum ElectricalResistanceUnit
    {
      /// <summary>Ohm.</summary>
      Ohm,
    }

    /// <summary>Electric resistance, unit of Ohm.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Electrical_resistance_and_conductance"/>
    public readonly record struct ElectricalResistance
      : System.IComparable, System.IComparable<ElectricalResistance>, System.IConvertible, IUnitQuantifiable<double, ElectricalResistanceUnit>
    {
      public static readonly ElectricalResistance Zero;

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
      public static explicit operator double(ElectricalResistance v) => v.m_value;
      public static explicit operator ElectricalResistance(double v) => new(v);

      public static bool operator <(ElectricalResistance a, ElectricalResistance b) => a.CompareTo(b) < 0;
      public static bool operator <=(ElectricalResistance a, ElectricalResistance b) => a.CompareTo(b) <= 0;
      public static bool operator >(ElectricalResistance a, ElectricalResistance b) => a.CompareTo(b) > 0;
      public static bool operator >=(ElectricalResistance a, ElectricalResistance b) => a.CompareTo(b) >= 0;

      public static ElectricalResistance operator -(ElectricalResistance v) => new(-v.m_value);
      public static ElectricalResistance operator +(ElectricalResistance a, double b) => new(a.m_value + b);
      public static ElectricalResistance operator +(ElectricalResistance a, ElectricalResistance b) => a + b.m_value;
      public static ElectricalResistance operator /(ElectricalResistance a, double b) => new(a.m_value / b);
      public static ElectricalResistance operator /(ElectricalResistance a, ElectricalResistance b) => a / b.m_value;
      public static ElectricalResistance operator *(ElectricalResistance a, double b) => new(a.m_value * b);
      public static ElectricalResistance operator *(ElectricalResistance a, ElectricalResistance b) => a * b.m_value;
      public static ElectricalResistance operator %(ElectricalResistance a, double b) => new(a.m_value % b);
      public static ElectricalResistance operator %(ElectricalResistance a, ElectricalResistance b) => a % b.m_value;
      public static ElectricalResistance operator -(ElectricalResistance a, double b) => new(a.m_value - b);
      public static ElectricalResistance operator -(ElectricalResistance a, ElectricalResistance b) => a - b.m_value;
      #endregion Overloaded operators

      #region Implemented interfaces
      // IComparable<>
      public int CompareTo(ElectricalResistance other) => m_value.CompareTo(other.m_value);
      // IComparable
      public int CompareTo(object? other) => other is not null && other is ElectricalResistance o ? CompareTo(o) : -1;

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

      // IQuantifiable<>
      public string ToQuantityString(string? format = null, bool preferUnicode = false, bool useFullName = false)
        => ToUnitString(DefaultUnit, format, preferUnicode, useFullName);

      public double Value { get => m_value; init => m_value = value; }

      // IUnitQuantifiable<>
      public string ToUnitString(ElectricalResistanceUnit unit = DefaultUnit, string? format = null, bool preferUnicode = false, bool useFullName = false)
        => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";

      public double ToUnitValue(ElectricalResistanceUnit unit = DefaultUnit)
        => unit switch
        {
          ElectricalResistanceUnit.Ohm => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };
      #endregion Implemented interfaces

      public override string ToString() => ToQuantityString();
    }
  }
}
