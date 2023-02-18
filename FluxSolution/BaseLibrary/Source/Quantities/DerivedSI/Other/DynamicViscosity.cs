namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitString(this Quantities.DynamicViscosityUnit unit, bool preferUnicode, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Quantities.DynamicViscosityUnit.PascalSecond => "Pa\u22C5s",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Quantities
  {
    public enum DynamicViscosityUnit
    {
      PascalSecond,
    }

    /// <summary>Dynamic viscosity, unit of Pascal second.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Dynamic_viscosity"/>
    public readonly record struct DynamicViscosity
      : System.IComparable, System.IComparable<DynamicViscosity>, System.IConvertible, System.IFormattable, IUnitQuantifiable<double, DynamicViscosityUnit>
    {
      public const DynamicViscosityUnit DefaultUnit = DynamicViscosityUnit.PascalSecond;

      private readonly double m_value;

      public DynamicViscosity(double value, DynamicViscosityUnit unit = DefaultUnit)
        => m_value = unit switch
        {
          DynamicViscosityUnit.PascalSecond => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Static methods

      public static DynamicViscosity From(Pressure pressure, Time time)
        => new(pressure.Value * time.Value);
      #endregion Static methods

      #region Overloaded operators
      public static explicit operator double(DynamicViscosity v) => v.m_value;
      public static explicit operator DynamicViscosity(double v) => new(v);

      public static bool operator <(DynamicViscosity a, DynamicViscosity b) => a.CompareTo(b) < 0;
      public static bool operator <=(DynamicViscosity a, DynamicViscosity b) => a.CompareTo(b) <= 0;
      public static bool operator >(DynamicViscosity a, DynamicViscosity b) => a.CompareTo(b) > 0;
      public static bool operator >=(DynamicViscosity a, DynamicViscosity b) => a.CompareTo(b) >= 0;

      public static DynamicViscosity operator -(DynamicViscosity v) => new(-v.m_value);
      public static DynamicViscosity operator +(DynamicViscosity a, double b) => new(a.m_value + b);
      public static DynamicViscosity operator +(DynamicViscosity a, DynamicViscosity b) => a + b.m_value;
      public static DynamicViscosity operator /(DynamicViscosity a, double b) => new(a.m_value / b);
      public static DynamicViscosity operator /(DynamicViscosity a, DynamicViscosity b) => a / b.m_value;
      public static DynamicViscosity operator *(DynamicViscosity a, double b) => new(a.m_value * b);
      public static DynamicViscosity operator *(DynamicViscosity a, DynamicViscosity b) => a * b.m_value;
      public static DynamicViscosity operator %(DynamicViscosity a, double b) => new(a.m_value % b);
      public static DynamicViscosity operator %(DynamicViscosity a, DynamicViscosity b) => a % b.m_value;
      public static DynamicViscosity operator -(DynamicViscosity a, double b) => new(a.m_value - b);
      public static DynamicViscosity operator -(DynamicViscosity a, DynamicViscosity b) => a - b.m_value;
      #endregion Overloaded operators

      #region Implemented interfaces
      // IComparable<>
      public int CompareTo(DynamicViscosity other) => m_value.CompareTo(other.m_value);
      // IComparable
      public int CompareTo(object? other) => other is not null && other is DynamicViscosity o ? CompareTo(o) : -1;

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
      public string ToQuantityString(string? format = null, bool preferUnicode = false, bool useFullName = false)
        => ToUnitString(DefaultUnit, format, preferUnicode, useFullName);

      public double Value { get => m_value; init => m_value = value; }

      // IUnitQuantifiable<>
      public string ToUnitString(DynamicViscosityUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false)
        => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";

      public double ToUnitValue(DynamicViscosityUnit unit = DefaultUnit)
        => unit switch
        {
          DynamicViscosityUnit.PascalSecond => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };
      #endregion Implemented interfaces

      public override string ToString() => ToQuantityString();
    }
  }
}
