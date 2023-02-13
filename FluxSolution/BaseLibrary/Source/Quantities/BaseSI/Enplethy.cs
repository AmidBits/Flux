namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitString(this Quantities.EnplethyUnit unit, bool preferUnicode, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Quantities.EnplethyUnit.Mole => preferUnicode ? "\u33D6" : "mol",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Quantities
  {
    public enum EnplethyUnit
    {
      Mole,
    }

    /// <summary>Enplethy, or amount of substance. SI unit of mole. This is a base quantity.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Amount_of_substance"/>
    public readonly record struct Enplethy
      : System.IComparable, System.IComparable<Enplethy>, System.IConvertible, System.IFormattable, IUnitQuantifiable<double, EnplethyUnit>
    {
      /// <summary>The number of atoms contained in 1 mol of carbon-12 (which has the molar mass of 12 g) is called the Avogadro number. The Avogadro constant is the proportionality factor that relates the number of constituent particles (usually molecules, atoms or ions) in a sample with the amount of substance in that sample. It's unit is the reciprocal mole (or per mole). I.e. any 1 mol of any substance contains this amount of fundamental units. A fundamental unit can be atoms (e.g. iron, Fe), molecules (e.g. oxygen, O2) or formula units (e.g. water, H2O).</summary>
      public const double AvagadrosNumber = 6.02214076e23;

      public const EnplethyUnit DefaultUnit = EnplethyUnit.Mole;

      private readonly double m_value;

      public Enplethy(double value, EnplethyUnit unit = DefaultUnit)
        => m_value = unit switch
        {
          EnplethyUnit.Mole => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Static methods
      #endregion Static methods

      #region Overloaded operators
      public static explicit operator double(Enplethy v) => v.m_value;
      public static explicit operator Enplethy(double v) => new(v);

      public static bool operator <(Enplethy a, Enplethy b) => a.CompareTo(b) < 0;
      public static bool operator <=(Enplethy a, Enplethy b) => a.CompareTo(b) <= 0;
      public static bool operator >(Enplethy a, Enplethy b) => a.CompareTo(b) > 0;
      public static bool operator >=(Enplethy a, Enplethy b) => a.CompareTo(b) >= 0;

      public static Enplethy operator -(Enplethy v) => new(-v.Value);
      public static Enplethy operator +(Enplethy a, double b) => new(a.m_value + b);
      public static Enplethy operator +(Enplethy a, Enplethy b) => a + b.m_value;
      public static Enplethy operator /(Enplethy a, double b) => new(a.m_value / b);
      public static Enplethy operator /(Enplethy a, Enplethy b) => a / b.m_value;
      public static Enplethy operator *(Enplethy a, double b) => new(a.m_value * b);
      public static Enplethy operator *(Enplethy a, Enplethy b) => a * b.m_value;
      public static Enplethy operator %(Enplethy a, double b) => new(a.m_value % b);
      public static Enplethy operator %(Enplethy a, Enplethy b) => a % b.m_value;
      public static Enplethy operator -(Enplethy a, double b) => new(a.m_value - b);
      public static Enplethy operator -(Enplethy a, Enplethy b) => a - b.m_value;
      #endregion Overloaded operators

      #region Implemented interfaces
      // IComparable<>
      public int CompareTo(Enplethy other)
        => m_value.CompareTo(other.m_value);
      // IComparable
      public int CompareTo(object? other)
        => other is not null && other is Enplethy o ? CompareTo(o) : -1;

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
      public string ToQuantityString(string? format = null, bool preferUnicode = false, bool useFullName = false)
        => ToUnitString(DefaultUnit, format, preferUnicode, useFullName);

      public double Value { get => m_value; init => m_value = value; }
      // IUnitQuantifiable<>

      public string ToUnitString(EnplethyUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false)
        => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";

      public double ToUnitValue(EnplethyUnit unit = DefaultUnit)
        => unit switch
        {
          EnplethyUnit.Mole => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };
      #endregion Implemented interfaces

      #region Object overrides
      public override string ToString() => $"{GetType().Name} {{ {ToQuantityString()} }}";
      #endregion Object overrides
    }
  }
}