namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitString(this EnplethyUnit unit, bool useFullName = false, bool preferUnicode = false)
      => useFullName ? unit.ToString() : unit switch
      {
        EnplethyUnit.Mole => preferUnicode ? "\u33d6" : "mol",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  public enum EnplethyUnit
  {
    Mole,
  }

  /// <summary>Enplethy, or amount of substance. SI unit of mole. This is a base quantity.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Amount_of_substance"/>
  public struct Enplethy
    : System.IComparable, System.IComparable<Enplethy>, System.IConvertible, System.IEquatable<Enplethy>, System.IFormattable, IUnitQuantifiable<double, EnplethyUnit>
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
    [System.Diagnostics.Contracts.Pure] public static explicit operator double(Enplethy v) => v.m_value;
    [System.Diagnostics.Contracts.Pure] public static explicit operator Enplethy(double v) => new(v);

    [System.Diagnostics.Contracts.Pure] public static bool operator <(Enplethy a, Enplethy b) => a.CompareTo(b) < 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator <=(Enplethy a, Enplethy b) => a.CompareTo(b) <= 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator >(Enplethy a, Enplethy b) => a.CompareTo(b) > 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator >=(Enplethy a, Enplethy b) => a.CompareTo(b) >= 0;

    [System.Diagnostics.Contracts.Pure] public static bool operator ==(Enplethy a, Enplethy b) => a.Equals(b);
    [System.Diagnostics.Contracts.Pure] public static bool operator !=(Enplethy a, Enplethy b) => !a.Equals(b);

    [System.Diagnostics.Contracts.Pure] public static Enplethy operator -(Enplethy v) => new(-v.Value);
    [System.Diagnostics.Contracts.Pure] public static Enplethy operator +(Enplethy a, double b) => new(a.m_value + b);
    [System.Diagnostics.Contracts.Pure] public static Enplethy operator +(Enplethy a, Enplethy b) => a + b.m_value;
    [System.Diagnostics.Contracts.Pure] public static Enplethy operator /(Enplethy a, double b) => new(a.m_value / b);
    [System.Diagnostics.Contracts.Pure] public static Enplethy operator /(Enplethy a, Enplethy b) => a / b.m_value;
    [System.Diagnostics.Contracts.Pure] public static Enplethy operator *(Enplethy a, double b) => new(a.m_value * b);
    [System.Diagnostics.Contracts.Pure] public static Enplethy operator *(Enplethy a, Enplethy b) => a * b.m_value;
    [System.Diagnostics.Contracts.Pure] public static Enplethy operator %(Enplethy a, double b) => new(a.m_value % b);
    [System.Diagnostics.Contracts.Pure] public static Enplethy operator %(Enplethy a, Enplethy b) => a % b.m_value;
    [System.Diagnostics.Contracts.Pure] public static Enplethy operator -(Enplethy a, double b) => new(a.m_value - b);
    [System.Diagnostics.Contracts.Pure] public static Enplethy operator -(Enplethy a, Enplethy b) => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable<>
    [System.Diagnostics.Contracts.Pure]
    public int CompareTo(Enplethy other)
      => m_value.CompareTo(other.m_value);
    // IComparable
    [System.Diagnostics.Contracts.Pure]
    public int CompareTo(object? other)
      => other is not null && other is Enplethy o ? CompareTo(o) : -1;

    #region IConvertible
    [System.Diagnostics.Contracts.Pure] public System.TypeCode GetTypeCode() => System.TypeCode.Object;
    [System.Diagnostics.Contracts.Pure] public bool ToBoolean(System.IFormatProvider? provider) => Value != 0;
    [System.Diagnostics.Contracts.Pure] public byte ToByte(System.IFormatProvider? provider) => System.Convert.ToByte(Value);
    [System.Diagnostics.Contracts.Pure] public char ToChar(System.IFormatProvider? provider) => System.Convert.ToChar(Value);
    [System.Diagnostics.Contracts.Pure] public System.DateTime ToDateTime(System.IFormatProvider? provider) => System.Convert.ToDateTime(Value);
    [System.Diagnostics.Contracts.Pure] public decimal ToDecimal(System.IFormatProvider? provider) => System.Convert.ToDecimal(Value);
    [System.Diagnostics.Contracts.Pure] public double ToDouble(System.IFormatProvider? provider) => System.Convert.ToDouble(Value);
    [System.Diagnostics.Contracts.Pure] public short ToInt16(System.IFormatProvider? provider) => System.Convert.ToInt16(Value);
    [System.Diagnostics.Contracts.Pure] public int ToInt32(System.IFormatProvider? provider) => System.Convert.ToInt32(Value);
    [System.Diagnostics.Contracts.Pure] public long ToInt64(System.IFormatProvider? provider) => System.Convert.ToInt64(Value);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public sbyte ToSByte(System.IFormatProvider? provider) => System.Convert.ToSByte(Value);
    [System.Diagnostics.Contracts.Pure] public float ToSingle(System.IFormatProvider? provider) => System.Convert.ToSingle(Value);
    [System.Diagnostics.Contracts.Pure] public string ToString(System.IFormatProvider? provider) => string.Format(provider, "{0}", Value);
    [System.Diagnostics.Contracts.Pure] public object ToType(System.Type conversionType, System.IFormatProvider? provider) => System.Convert.ChangeType(Value, conversionType, provider);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public ushort ToUInt16(System.IFormatProvider? provider) => System.Convert.ToUInt16(Value);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public uint ToUInt32(System.IFormatProvider? provider) => System.Convert.ToUInt32(Value);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public ulong ToUInt64(System.IFormatProvider? provider) => System.Convert.ToUInt64(Value);
    #endregion IConvertible

    // IEquatable<>
    [System.Diagnostics.Contracts.Pure] public bool Equals(Enplethy other) => m_value == other.m_value;

    // IFormattable
    [System.Diagnostics.Contracts.Pure] public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

    // IUnitQuantifiable<>
    [System.Diagnostics.Contracts.Pure]
    public double Value
      => m_value;
    [System.Diagnostics.Contracts.Pure]
    public string ToUnitString(EnplethyUnit unit = DefaultUnit, string? format = null, bool useFullName = false, bool preferUnicode = false)
      => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString()}";
    [System.Diagnostics.Contracts.Pure]
    public double ToUnitValue(EnplethyUnit unit = DefaultUnit)
      => unit switch
      {
        EnplethyUnit.Mole => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
    #endregion Implemented interfaces

    #region Object overrides
    [System.Diagnostics.Contracts.Pure] public override bool Equals(object? obj) => obj is Enplethy o && Equals(o);
    [System.Diagnostics.Contracts.Pure] public override int GetHashCode() => m_value.GetHashCode();
    [System.Diagnostics.Contracts.Pure] public override string ToString() => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
    #endregion Object overrides
  }
}
