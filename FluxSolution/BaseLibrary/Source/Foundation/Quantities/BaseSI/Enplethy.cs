namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static Enplethy Create(this EnplethyUnit unit, double value)
      => new(value, unit);
    public static string GetUnitSymbol(this EnplethyUnit unit)
      => unit switch
      {
        EnplethyUnit.Mole => @" mol",
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
    : System.IComparable<Enplethy>, System.IEquatable<Enplethy>, IValueGeneralizedUnit<double>, IValueBaseUnitSI<double>
  {
    public const EnplethyUnit DefaultUnit = EnplethyUnit.Mole;

    // The unit of the Avagadro constant is the reciprocal mole, i.e. "per" mole.
    public static Enplethy AvagadroConstant
      => new(6.02214076e23);

    private readonly double m_value;

    public Enplethy(double value, EnplethyUnit unit = DefaultUnit)
      => m_value = unit switch
      {
        EnplethyUnit.Mole => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double BaseUnitValue
      => m_value;

    public double GeneralUnitValue
      => m_value;

    public string ToUnitString(EnplethyUnit unit = DefaultUnit, string? format = null)
      => $"{(format is null ? ToUnitValue(unit) : string.Format($"{{0:{format}}}", ToUnitValue(unit)))}{unit.GetUnitSymbol()}";
    public double ToUnitValue(EnplethyUnit unit = DefaultUnit)
      => unit switch
      {
        EnplethyUnit.Mole => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Static methods
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Enplethy v)
      => v.m_value;
    public static explicit operator Enplethy(double v)
      => new(v);

    public static bool operator <(Enplethy a, Enplethy b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Enplethy a, Enplethy b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Enplethy a, Enplethy b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(Enplethy a, Enplethy b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(Enplethy a, Enplethy b)
      => a.Equals(b);
    public static bool operator !=(Enplethy a, Enplethy b)
      => !a.Equals(b);

    public static Enplethy operator -(Enplethy v)
      => new(-v.GeneralUnitValue);
    public static Enplethy operator +(Enplethy a, double b)
      => new(a.m_value + b);
    public static Enplethy operator +(Enplethy a, Enplethy b)
      => a + b.m_value;
    public static Enplethy operator /(Enplethy a, double b)
      => new(a.m_value / b);
    public static Enplethy operator /(Enplethy a, Enplethy b)
      => a / b.m_value;
    public static Enplethy operator *(Enplethy a, double b)
      => new(a.m_value * b);
    public static Enplethy operator *(Enplethy a, Enplethy b)
      => a * b.m_value;
    public static Enplethy operator %(Enplethy a, double b)
      => new(a.m_value % b);
    public static Enplethy operator %(Enplethy a, Enplethy b)
      => a % b.m_value;
    public static Enplethy operator -(Enplethy a, double b)
      => new(a.m_value - b);
    public static Enplethy operator -(Enplethy a, Enplethy b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Enplethy other)
      => m_value.CompareTo(other.m_value);

    // IEquatable
    public bool Equals(Enplethy other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Enplethy o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
    #endregion Object overrides
  }
}