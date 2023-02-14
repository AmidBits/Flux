namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitString(this Quantities.ImpulseUnit unit, bool preferUnicode, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Quantities.ImpulseUnit.NewtonSecond => "N\u22C5s",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Quantities
  {
    public enum ImpulseUnit
    {
      NewtonSecond,
    }

    /// <summary>Impulse, unit of Newton second.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Impulse"/>
    public readonly record struct Impulse
      : System.IComparable, System.IComparable<Impulse>, System.IConvertible, System.IFormattable, IUnitQuantifiable<double, ImpulseUnit>
    {
      public const ImpulseUnit DefaultUnit = ImpulseUnit.NewtonSecond;

      private readonly double m_value;

      public Impulse(double value, ImpulseUnit unit = DefaultUnit)
        => m_value = unit switch
        {
          ImpulseUnit.NewtonSecond => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Static methods

      public static Impulse From(Force force, Time time)
        => new(force.Value / time.Value);
      #endregion Static methods

      #region Overloaded operators
      public static explicit operator double(Impulse v) => v.m_value;
      public static explicit operator Impulse(double v) => new(v);

      public static bool operator <(Impulse a, Impulse b) => a.CompareTo(b) < 0;
      public static bool operator <=(Impulse a, Impulse b) => a.CompareTo(b) <= 0;
      public static bool operator >(Impulse a, Impulse b) => a.CompareTo(b) > 0;
      public static bool operator >=(Impulse a, Impulse b) => a.CompareTo(b) >= 0;

      public static Impulse operator -(Impulse v) => new(-v.m_value);
      public static Impulse operator +(Impulse a, double b) => new(a.m_value + b);
      public static Impulse operator +(Impulse a, Impulse b) => a + b.m_value;
      public static Impulse operator /(Impulse a, double b) => new(a.m_value / b);
      public static Impulse operator /(Impulse a, Impulse b) => a / b.m_value;
      public static Impulse operator *(Impulse a, double b) => new(a.m_value * b);
      public static Impulse operator *(Impulse a, Impulse b) => a * b.m_value;
      public static Impulse operator %(Impulse a, double b) => new(a.m_value % b);
      public static Impulse operator %(Impulse a, Impulse b) => a % b.m_value;
      public static Impulse operator -(Impulse a, double b) => new(a.m_value - b);
      public static Impulse operator -(Impulse a, Impulse b) => a - b.m_value;
      #endregion Overloaded operators

      #region Implemented interfaces
      // IComparable<>
      public int CompareTo(Impulse other) => m_value.CompareTo(other.m_value);
      // IComparable
      public int CompareTo(object? other) => other is not null && other is Impulse o ? CompareTo(o) : -1;

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
      public string ToUnitString(ImpulseUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false)
        => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";

      public double ToUnitValue(ImpulseUnit unit = DefaultUnit)
        => unit switch
        {
          ImpulseUnit.NewtonSecond => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };
      #endregion Implemented interfaces

      #region Object overrides
      public override string ToString() => $"{GetType().Name}  {{ {ToQuantityString()} }}";
      #endregion Object overrides
    }
  }
}
