namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static Inductance Create(this InductanceUnit unit, double value)
      => new(value, unit);
    public static string GetUnitSymbol(this InductanceUnit unit)
      => unit switch
      {
        InductanceUnit.Henry => @" H",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  public enum InductanceUnit
  {
    Henry,
  }

  /// <summary>Electrical inductance unit of Henry.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Inductance"/>
  public struct Inductance
    : System.IComparable<Inductance>, System.IEquatable<Inductance>, IValueGeneralizedUnit<double>, IValueDerivedUnitSI<double>
  {
    public const InductanceUnit DefaultUnit = InductanceUnit.Henry;

    private readonly double m_value;

    public Inductance(double value, InductanceUnit unit = DefaultUnit)
      => m_value = unit switch
      {
        InductanceUnit.Henry => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double DerivedUnitValue
      => m_value;

    public double GeneralUnitValue
      => m_value;

    public string ToUnitString(InductanceUnit unit = DefaultUnit, string? format = null)
      => $"{(format is null ? ToUnitValue(unit) : string.Format($"{{0:{format}}}", ToUnitValue(unit)))}{unit.GetUnitSymbol()}";
    public double ToUnitValue(InductanceUnit unit = DefaultUnit)
      => unit switch
      {
        InductanceUnit.Henry => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Overloaded operators
    public static explicit operator double(Inductance v)
      => v.m_value;
    public static explicit operator Inductance(double v)
      => new(v);

    public static bool operator <(Inductance a, Inductance b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Inductance a, Inductance b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Inductance a, Inductance b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(Inductance a, Inductance b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(Inductance a, Inductance b)
      => a.Equals(b);
    public static bool operator !=(Inductance a, Inductance b)
      => !a.Equals(b);

    public static Inductance operator -(Inductance v)
      => new(-v.m_value);
    public static Inductance operator +(Inductance a, double b)
      => new(a.m_value + b);
    public static Inductance operator +(Inductance a, Inductance b)
      => a + b.m_value;
    public static Inductance operator /(Inductance a, double b)
      => new(a.m_value / b);
    public static Inductance operator /(Inductance a, Inductance b)
      => a / b.m_value;
    public static Inductance operator *(Inductance a, double b)
      => new(a.m_value * b);
    public static Inductance operator *(Inductance a, Inductance b)
      => a * b.m_value;
    public static Inductance operator %(Inductance a, double b)
      => new(a.m_value % b);
    public static Inductance operator %(Inductance a, Inductance b)
      => a % b.m_value;
    public static Inductance operator -(Inductance a, double b)
      => new(a.m_value - b);
    public static Inductance operator -(Inductance a, Inductance b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Inductance other)
      => m_value.CompareTo(other.m_value);

    // IEquatable
    public bool Equals(Inductance other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Inductance o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
    #endregion Object overrides
  }
}