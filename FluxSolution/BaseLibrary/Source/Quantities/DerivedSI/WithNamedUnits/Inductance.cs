namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitString(this Quantities.InductanceUnit unit, bool preferUnicode, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Quantities.InductanceUnit.Henry => "H",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Quantities
  {
    public enum InductanceUnit
    {
      /// <summary>Henry.</summary>
      Henry,
    }

    /// <summary>Electrical inductance unit of Henry.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inductance"/>
    public readonly struct Inductance
      : System.IComparable, System.IComparable<Inductance>, System.IConvertible, System.IEquatable<Inductance>, System.IFormattable, IUnitQuantifiable<double, InductanceUnit>
    {
      public const InductanceUnit DefaultUnit = InductanceUnit.Henry;

      private readonly double m_value;

      public Inductance(double value, InductanceUnit unit = DefaultUnit)
        => m_value = unit switch
        {
          InductanceUnit.Henry => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Overloaded operators
      public static explicit operator double(Inductance v) => v.m_value;
      public static explicit operator Inductance(double v) => new(v);

      public static bool operator <(Inductance a, Inductance b) => a.CompareTo(b) < 0;
      public static bool operator <=(Inductance a, Inductance b) => a.CompareTo(b) <= 0;
      public static bool operator >(Inductance a, Inductance b) => a.CompareTo(b) > 0;
      public static bool operator >=(Inductance a, Inductance b) => a.CompareTo(b) >= 0;

      public static bool operator ==(Inductance a, Inductance b) => a.Equals(b);
      public static bool operator !=(Inductance a, Inductance b) => !a.Equals(b);

      public static Inductance operator -(Inductance v) => new(-v.m_value);
      public static Inductance operator +(Inductance a, double b) => new(a.m_value + b);
      public static Inductance operator +(Inductance a, Inductance b) => a + b.m_value;
      public static Inductance operator /(Inductance a, double b) => new(a.m_value / b);
      public static Inductance operator /(Inductance a, Inductance b) => a / b.m_value;
      public static Inductance operator *(Inductance a, double b) => new(a.m_value * b);
      public static Inductance operator *(Inductance a, Inductance b) => a * b.m_value;
      public static Inductance operator %(Inductance a, double b) => new(a.m_value % b);
      public static Inductance operator %(Inductance a, Inductance b) => a % b.m_value;
      public static Inductance operator -(Inductance a, double b) => new(a.m_value - b);
      public static Inductance operator -(Inductance a, Inductance b) => a - b.m_value;
      #endregion Overloaded operators

      #region Implemented interfaces
      // IComparable<>
      public int CompareTo(Inductance other) => m_value.CompareTo(other.m_value);
      // IComparable
      public int CompareTo(object? other) => other is not null && other is Inductance o ? CompareTo(o) : -1;

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
      public bool Equals(Inductance other) => m_value == other.m_value;

      // IFormattable
      public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

      // IQuantifiable<>
      public string ToQuantityString(string? format = null, bool preferUnicode = false, bool useFullName = false)
        => ToUnitString(DefaultUnit, format, preferUnicode, useFullName);

      public double Value { get => m_value; init => m_value = value; }

      // IUnitQuantifiable<>

      public string ToUnitString(InductanceUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false)
        => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";

      public double ToUnitValue(InductanceUnit unit = DefaultUnit)
        => unit switch
        {
          InductanceUnit.Henry => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };
      #endregion Implemented interfaces

      #region Object overrides
      public override bool Equals(object? obj) => obj is Inductance o && Equals(o);
      public override int GetHashCode() => m_value.GetHashCode();
      public override string ToString() => $"{GetType().Name} {{ {ToQuantityString()} }}";
      #endregion Object overrides
    }
  }
}