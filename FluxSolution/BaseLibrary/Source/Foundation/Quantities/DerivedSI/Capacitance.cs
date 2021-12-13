namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static Capacitance Create(this CapacitanceUnit unit, double value)
      => new(value, unit);
    public static string GetUnitSymbol(this CapacitanceUnit unit)
      => unit switch
      {
        CapacitanceUnit.Farad => @" F",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  public enum CapacitanceUnit
  {
    Farad,
  }

  /// <summary>Electrical capacitance unit of Farad.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Capacitance"/>
  public struct Capacitance
    : System.IComparable<Capacitance>, System.IEquatable<Capacitance>, IValueGeneralizedUnit<double>, IValueDerivedUnitSI<double>
  {
    public const CapacitanceUnit DefaultUnit = CapacitanceUnit.Farad;

    private readonly double m_value;

    public Capacitance(double value, CapacitanceUnit unit = DefaultUnit)
      => m_value = unit switch
      {
        CapacitanceUnit.Farad => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double DerivedUnitValue
      => m_value;

    public double GeneralUnitValue
      => m_value;

    public string ToUnitString(CapacitanceUnit unit = DefaultUnit, string? format = null)
      => $"{(format is null ? ToUnitValue(unit) : string.Format($"{{0:{format}}}", ToUnitValue(unit)))}{unit.GetUnitSymbol()}";
    public double ToUnitValue(CapacitanceUnit unit = DefaultUnit)
      => unit switch
      {
        CapacitanceUnit.Farad => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Overloaded operators
    public static explicit operator double(Capacitance v)
      => v.m_value;
    public static explicit operator Capacitance(double v)
      => new(v);

    public static bool operator <(Capacitance a, Capacitance b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Capacitance a, Capacitance b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Capacitance a, Capacitance b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(Capacitance a, Capacitance b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(Capacitance a, Capacitance b)
      => a.Equals(b);
    public static bool operator !=(Capacitance a, Capacitance b)
      => !a.Equals(b);

    public static Capacitance operator -(Capacitance v)
      => new(-v.m_value);
    public static Capacitance operator +(Capacitance a, double b)
      => new(a.m_value + b);
    public static Capacitance operator +(Capacitance a, Capacitance b)
      => a + b.m_value;
    public static Capacitance operator /(Capacitance a, double b)
      => new(a.m_value / b);
    public static Capacitance operator /(Capacitance a, Capacitance b)
      => a / b.m_value;
    public static Capacitance operator *(Capacitance a, double b)
      => new(a.m_value * b);
    public static Capacitance operator *(Capacitance a, Capacitance b)
      => a * b.m_value;
    public static Capacitance operator %(Capacitance a, double b)
      => new(a.m_value % b);
    public static Capacitance operator %(Capacitance a, Capacitance b)
      => a % b.m_value;
    public static Capacitance operator -(Capacitance a, double b)
      => new(a.m_value - b);
    public static Capacitance operator -(Capacitance a, Capacitance b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Capacitance other)
      => m_value.CompareTo(other.m_value);

    // IEquatable
    public bool Equals(Capacitance other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Capacitance o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
    #endregion Object overrides
  }
}
