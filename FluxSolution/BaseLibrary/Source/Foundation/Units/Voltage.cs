namespace Flux.Units
{
  /// <summary>Voltage.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Voltage"/>
  public struct Voltage
    : System.IComparable<Voltage>, System.IEquatable<Voltage>, System.IFormattable
  {
    private readonly double m_volt;

    public Voltage(double volt)
      => m_volt = volt;

    public double Volt
      => m_volt;

    #region Static methods
    public static Voltage Add(Voltage left, Voltage right)
      => new Voltage(left.m_volt + right.m_volt);
    public static Voltage Divide(Voltage left, Voltage right)
      => new Voltage(left.m_volt / right.m_volt);
    public static Voltage Multiply(Voltage left, Voltage right)
      => new Voltage(left.m_volt * right.m_volt);
    public static Voltage Negate(Voltage value)
      => new Voltage(-value.m_volt);
    public static Voltage Remainder(Voltage dividend, Voltage divisor)
      => new Voltage(dividend.m_volt % divisor.m_volt);
    public static Voltage Subtract(Voltage left, Voltage right)
      => new Voltage(left.m_volt - right.m_volt);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Voltage v)
      => v.m_volt;
    public static implicit operator Voltage(double v)
      => new Voltage(v);

    public static bool operator <(Voltage a, Voltage b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Voltage a, Voltage b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Voltage a, Voltage b)
      => a.CompareTo(b) < 0;
    public static bool operator >=(Voltage a, Voltage b)
      => a.CompareTo(b) <= 0;

    public static bool operator ==(Voltage a, Voltage b)
      => a.Equals(b);
    public static bool operator !=(Voltage a, Voltage b)
      => !a.Equals(b);

    public static Voltage operator +(Voltage a, Voltage b)
      => Add(a, b);
    public static Voltage operator /(Voltage a, Voltage b)
      => Divide(a, b);
    public static Voltage operator *(Voltage a, Voltage b)
      => Multiply(a, b);
    public static Voltage operator -(Voltage v)
      => Negate(v);
    public static Voltage operator %(Voltage a, Voltage b)
      => Remainder(a, b);
    public static Voltage operator -(Voltage a, Voltage b)
      => Subtract(a, b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Voltage other)
      => m_volt.CompareTo(other.m_volt);

    // IEquatable
    public bool Equals(Voltage other)
      => m_volt == other.m_volt;

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => string.Format(formatProvider, format ?? $"<{nameof(Voltage)}: {{0:D3}}>", this);
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Voltage o && Equals(o);
    public override int GetHashCode()
      => m_volt.GetHashCode();
    public override string ToString()
      => ToString(null, null);
    #endregion Object overrides
  }
}
