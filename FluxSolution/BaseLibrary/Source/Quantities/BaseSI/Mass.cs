namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitString(this Quantities.MassUnit unit, bool preferUnicode, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Quantities.MassUnit.Milligram => preferUnicode ? "\u338E" : "mg",
        Quantities.MassUnit.Gram => "g",
        Quantities.MassUnit.Ounce => "oz",
        Quantities.MassUnit.Pound => "lb",
        Quantities.MassUnit.Kilogram => preferUnicode ? "\u338F" : "kg",
        Quantities.MassUnit.Tonne => "t",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Quantities
  {
    public enum MassUnit
    {
      /// <summary>This is the default unit for mass.</summary>
      Kilogram,
      Milligram,
      Gram,
      Ounce,
      Pound,
      /// <summary>Metric ton.</summary>
      Tonne,
    }

    /// <summary>Mass. SI unit of kilogram. This is a base quantity.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Mass"/>
    public readonly record struct Mass
      : System.IComparable, System.IComparable<Mass>, System.IConvertible, System.IFormattable, IUnitQuantifiable<double, MassUnit>
    {
      public static readonly Mass Zero;

      public const MassUnit DefaultUnit = MassUnit.Kilogram;

      public static Mass ElectronMass
        => new(9.1093837015e-31);

      private readonly double m_value;

      public Mass(double value, MassUnit unit = DefaultUnit)
        => m_value = unit switch
        {
          MassUnit.Milligram => value / 1000000,
          MassUnit.Gram => value / 1000,
          MassUnit.Ounce => value / 35.27396195,
          MassUnit.Pound => value * 0.45359237,
          MassUnit.Kilogram => value,
          MassUnit.Tonne => value * 1000,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Static methods
      #endregion Static methods

      #region Overloaded operators
      public static explicit operator double(Mass v) => v.m_value;
      public static explicit operator Mass(double v) => new(v);

      public static bool operator <(Mass a, Mass b) => a.CompareTo(b) < 0;
      public static bool operator <=(Mass a, Mass b) => a.CompareTo(b) <= 0;
      public static bool operator >(Mass a, Mass b) => a.CompareTo(b) > 0;
      public static bool operator >=(Mass a, Mass b) => a.CompareTo(b) >= 0;

      public static Mass operator -(Mass v) => new(-v.m_value);
      public static Mass operator +(Mass a, double b) => new(a.m_value + b);
      public static Mass operator +(Mass a, Mass b) => a + b.m_value;
      public static Mass operator /(Mass a, double b) => new(a.m_value / b);
      public static Mass operator /(Mass a, Mass b) => a / b.m_value;
      public static Mass operator *(Mass a, double b) => new(a.m_value * b);
      public static Mass operator *(Mass a, Mass b) => a * b.m_value;
      public static Mass operator %(Mass a, double b) => new(a.m_value % b);
      public static Mass operator %(Mass a, Mass b) => a % b.m_value;
      public static Mass operator -(Mass a, double b) => new(a.m_value - b);
      public static Mass operator -(Mass a, Mass b) => a - b.m_value;
      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is Mass o ? CompareTo(o) : -1;

      // IComparable<T>
      public int CompareTo(Mass other) => m_value.CompareTo(other.m_value);

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
      public string ToUnitString(MassUnit unit, string? valueFormat = null, bool preferUnicode = false, bool useFullName = false)
        => $"{string.Format($"{{0{(valueFormat is null ? string.Empty : $":{valueFormat}")}}}", ToUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";
      public double ToUnitValue(MassUnit unit = DefaultUnit)
        => unit switch
        {
          MassUnit.Milligram => m_value * 1000000,
          MassUnit.Gram => m_value * 1000,
          MassUnit.Ounce => m_value * 35.27396195,
          MassUnit.Pound => m_value / 0.45359237,
          MassUnit.Kilogram => m_value,
          MassUnit.Tonne => m_value / 1000,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #endregion Implemented interfaces

      public override string ToString() => ToQuantityString();
    }
  }
}
