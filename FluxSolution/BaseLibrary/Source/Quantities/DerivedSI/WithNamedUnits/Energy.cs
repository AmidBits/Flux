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
    public readonly struct Energy
      : System.IComparable, System.IComparable<Energy>, System.IConvertible, System.IEquatable<Energy>, System.IFormattable, IUnitQuantifiable<double, EnergyUnit>
    {
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
      [System.Diagnostics.Contracts.Pure] public static explicit operator double(Energy v) => v.m_value;
      [System.Diagnostics.Contracts.Pure] public static explicit operator Energy(double v) => new(v);

      [System.Diagnostics.Contracts.Pure] public static bool operator <(Energy a, Energy b) => a.CompareTo(b) < 0;
      [System.Diagnostics.Contracts.Pure] public static bool operator <=(Energy a, Energy b) => a.CompareTo(b) <= 0;
      [System.Diagnostics.Contracts.Pure] public static bool operator >(Energy a, Energy b) => a.CompareTo(b) > 0;
      [System.Diagnostics.Contracts.Pure] public static bool operator >=(Energy a, Energy b) => a.CompareTo(b) >= 0;

      [System.Diagnostics.Contracts.Pure] public static bool operator ==(Energy a, Energy b) => a.Equals(b);
      [System.Diagnostics.Contracts.Pure] public static bool operator !=(Energy a, Energy b) => !a.Equals(b);

      [System.Diagnostics.Contracts.Pure] public static Energy operator -(Energy v) => new(-v.m_value);
      [System.Diagnostics.Contracts.Pure] public static Energy operator +(Energy a, double b) => new(a.m_value + b);
      [System.Diagnostics.Contracts.Pure] public static Energy operator +(Energy a, Energy b) => a + b.m_value;
      [System.Diagnostics.Contracts.Pure] public static Energy operator /(Energy a, double b) => new(a.m_value / b);
      [System.Diagnostics.Contracts.Pure] public static Energy operator /(Energy a, Energy b) => a / b.m_value;
      [System.Diagnostics.Contracts.Pure] public static Energy operator *(Energy a, double b) => new(a.m_value * b);
      [System.Diagnostics.Contracts.Pure] public static Energy operator *(Energy a, Energy b) => a * b.m_value;
      [System.Diagnostics.Contracts.Pure] public static Energy operator %(Energy a, double b) => new(a.m_value % b);
      [System.Diagnostics.Contracts.Pure] public static Energy operator %(Energy a, Energy b) => a % b.m_value;
      [System.Diagnostics.Contracts.Pure] public static Energy operator -(Energy a, double b) => new(a.m_value - b);
      [System.Diagnostics.Contracts.Pure] public static Energy operator -(Energy a, Energy b) => a - b.m_value;
      #endregion Overloaded operators

      #region Implemented interfaces
      // IComparable<>
      [System.Diagnostics.Contracts.Pure] public int CompareTo(Energy other) => m_value.CompareTo(other.m_value);
      // IComparable
      [System.Diagnostics.Contracts.Pure] public int CompareTo(object? other) => other is not null && other is Energy o ? CompareTo(o) : -1;

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
      [System.Diagnostics.Contracts.Pure] public bool Equals(Energy other) => m_value == other.m_value;

      // IFormattable
      [System.Diagnostics.Contracts.Pure] public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

      // IQuantifiable<>
      [System.Diagnostics.Contracts.Pure] public double Value { get => m_value; init => m_value = value; }
      // IUnitQuantifiable<>
      [System.Diagnostics.Contracts.Pure]
      public string ToUnitString(EnergyUnit unit = DefaultUnit, string? format = null, bool preferUnicode = false, bool useFullName = false)
        => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";
      [System.Diagnostics.Contracts.Pure]
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

      #region Object overrides
      [System.Diagnostics.Contracts.Pure] public override bool Equals(object? obj) => obj is Energy o && Equals(o);
      [System.Diagnostics.Contracts.Pure] public override int GetHashCode() => m_value.GetHashCode();
      [System.Diagnostics.Contracts.Pure] public override string ToString() => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
      #endregion Object overrides
    }
  }
}
