namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitString(this Quantities.EnergyUnit unit, bool preferUnicode, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Quantities.EnergyUnit.Joule => "J",
        Quantities.EnergyUnit.ElectronVolt => "eV",
        Quantities.EnergyUnit.Calorie => preferUnicode ? "\u3388" : "cal",
        Quantities.EnergyUnit.WattHour => "W\u22C5h",
        Quantities.EnergyUnit.KilowattHour => "kW\u22C5h",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Quantities
  {
    public enum EnergyUnit
    {
      /// <summary>Joule.</summary>
      Joule,
      ElectronVolt,
      Calorie,
      WattHour,
      KilowattHour
    }

    /// <summary>Energy unit of Joule.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Energy"/>
    public readonly record struct Energy
      : System.IComparable, System.IComparable<Energy>, System.IConvertible, IUnitQuantifiable<double, EnergyUnit>
    {
      public static readonly Energy Zero;

      public const EnergyUnit DefaultUnit = EnergyUnit.Joule;

      private readonly double m_value;

      public Energy(double value, EnergyUnit unit = DefaultUnit)
        => m_value = unit switch
        {
          EnergyUnit.Joule => value,
          EnergyUnit.ElectronVolt => value / 1.602176634e-19,
          EnergyUnit.Calorie => value / 4.184,
          EnergyUnit.WattHour => value / 3.6e3,
          EnergyUnit.KilowattHour => value / 3.6e6,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Static methods
      #endregion Static methods

      #region Overloaded operators
      public static explicit operator double(Energy v) => v.m_value;
      public static explicit operator Energy(double v) => new(v);

      public static bool operator <(Energy a, Energy b) => a.CompareTo(b) < 0;
      public static bool operator <=(Energy a, Energy b) => a.CompareTo(b) <= 0;
      public static bool operator >(Energy a, Energy b) => a.CompareTo(b) > 0;
      public static bool operator >=(Energy a, Energy b) => a.CompareTo(b) >= 0;

      public static Energy operator -(Energy v) => new(-v.m_value);
      public static Energy operator +(Energy a, double b) => new(a.m_value + b);
      public static Energy operator +(Energy a, Energy b) => a + b.m_value;
      public static Energy operator /(Energy a, double b) => new(a.m_value / b);
      public static Energy operator /(Energy a, Energy b) => a / b.m_value;
      public static Energy operator *(Energy a, double b) => new(a.m_value * b);
      public static Energy operator *(Energy a, Energy b) => a * b.m_value;
      public static Energy operator %(Energy a, double b) => new(a.m_value % b);
      public static Energy operator %(Energy a, Energy b) => a % b.m_value;
      public static Energy operator -(Energy a, double b) => new(a.m_value - b);
      public static Energy operator -(Energy a, Energy b) => a - b.m_value;
      #endregion Overloaded operators

      #region Implemented interfaces
      // IComparable<>
      public int CompareTo(Energy other) => m_value.CompareTo(other.m_value);
      // IComparable
      public int CompareTo(object? other) => other is not null && other is Energy o ? CompareTo(o) : -1;

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
      public string ToUnitString(EnergyUnit unit = DefaultUnit, string? format = null, bool preferUnicode = false, bool useFullName = false)
        => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";

      public double ToUnitValue(EnergyUnit unit = DefaultUnit)
        => unit switch
        {
          EnergyUnit.Joule => m_value,
          EnergyUnit.ElectronVolt => m_value * 1.602176634e-19,
          EnergyUnit.Calorie => m_value * 4.184,
          EnergyUnit.WattHour => m_value * 3.6e3,
          EnergyUnit.KilowattHour => m_value * 3.6e6,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };
      #endregion Implemented interfaces

      public override string ToString() => ToQuantityString();
    }
  }
}
