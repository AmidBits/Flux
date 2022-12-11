namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitString(this Quantities.ActionUnit unit, bool preferUnicode, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Quantities.ActionUnit.JouleSecond => " J\u22C5s",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Quantities
  {
    public enum ActionUnit
    {
      JouleSecond,
    }

    /// <summary>Action. Unit of Joule second.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Action_(physics)"/>
    public readonly struct Action
      : System.IComparable, System.IComparable<Action>, System.IConvertible, System.IEquatable<Action>, System.IFormattable, IUnitQuantifiable<double, ActionUnit>
    {
      public const ActionUnit DefaultUnit = ActionUnit.JouleSecond;

      private readonly double m_value;

      public Action(double value, ActionUnit unit = DefaultUnit)
        => m_value = unit switch
        {
          ActionUnit.JouleSecond => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Overloaded operators
       public static explicit operator double(Action v) => v.m_value;
       public static explicit operator Action(double v) => new(v);

       public static bool operator <(Action a, Action b) => a.CompareTo(b) < 0;
       public static bool operator <=(Action a, Action b) => a.CompareTo(b) <= 0;
       public static bool operator >(Action a, Action b) => a.CompareTo(b) > 0;
       public static bool operator >=(Action a, Action b) => a.CompareTo(b) >= 0;

       public static bool operator ==(Action a, Action b) => a.Equals(b);
       public static bool operator !=(Action a, Action b) => !a.Equals(b);

       public static Action operator -(Action v) => new(-v.m_value);
       public static Action operator +(Action a, double b) => new(a.m_value + b);
       public static Action operator +(Action a, Action b) => a + b.m_value;
       public static Action operator /(Action a, double b) => new(a.m_value / b);
       public static Action operator /(Action a, Action b) => a / b.m_value;
       public static Action operator *(Action a, double b) => new(a.m_value * b);
       public static Action operator *(Action a, Action b) => a * b.m_value;
       public static Action operator %(Action a, double b) => new(a.m_value % b);
       public static Action operator %(Action a, Action b) => a % b.m_value;
       public static Action operator -(Action a, double b) => new(a.m_value - b);
       public static Action operator -(Action a, Action b) => a - b.m_value;
      #endregion Overloaded operators

      #region Implemented interfaces
      // IComparable<>
       public int CompareTo(Action other) => m_value.CompareTo(other.m_value);
      // IComparable
       public int CompareTo(object? other) => other is not null && other is Action o ? CompareTo(o) : -1;

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
       public bool Equals(Action other) => m_value == other.m_value;

      // IFormattable
       public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

      // IQuantifiable<>
       public double Value { get => m_value; init => m_value = value; }
      // IUnitQuantifiable<>
      
      public string ToUnitString(ActionUnit unit = DefaultUnit, string? format = null, bool preferUnicode = false, bool useFullName = false)
        => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";
      
      public double ToUnitValue(ActionUnit unit = DefaultUnit)
        => unit switch
        {
          ActionUnit.JouleSecond => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };
      #endregion Implemented interfaces

      #region Object overrides
       public override bool Equals(object? obj) => obj is Action o && Equals(o);
       public override int GetHashCode() => m_value.GetHashCode();
       public override string ToString() => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
      #endregion Object overrides
    }
  }
}
