namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static SolidAngle Create(this SolidAngleUnit unit, double value)
      => new(value, unit);
    public static string GetUnitSymbol(this SolidAngleUnit unit)
      => unit switch
      {
        SolidAngleUnit.Steradian => @" sr",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  public enum SolidAngleUnit
  {
    Steradian,
  }

  /// <summary>Solid angle. Unit of steradian.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Solid_angle"/>
  public struct SolidAngle
    : System.IComparable<SolidAngle>, System.IEquatable<SolidAngle>, IValueGeneralizedUnit<double>, IValueSiDerivedUnit<double>
  {
    public const SolidAngleUnit DefaultUnit = SolidAngleUnit.Steradian;

    private readonly double m_value;

    public SolidAngle(double value, SolidAngleUnit unit = DefaultUnit)
      => m_value = unit switch
      {
        SolidAngleUnit.Steradian => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double Value
      => m_value;

    public string ToUnitString(SolidAngleUnit unit = DefaultUnit, string? format = null)
      => $"{(format is null ? ToUnitValue(unit) : string.Format($"{{0:{format}}}", ToUnitValue(unit)))}{unit.GetUnitSymbol()}";
    public double ToUnitValue(SolidAngleUnit unit = DefaultUnit)
      => unit switch
      {
        SolidAngleUnit.Steradian => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Overloaded operators
    public static explicit operator double(SolidAngle v)
      => v.m_value;
    public static explicit operator SolidAngle(double v)
      => new(v);

    public static bool operator <(SolidAngle a, SolidAngle b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(SolidAngle a, SolidAngle b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(SolidAngle a, SolidAngle b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(SolidAngle a, SolidAngle b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(SolidAngle a, SolidAngle b)
      => a.Equals(b);
    public static bool operator !=(SolidAngle a, SolidAngle b)
      => !a.Equals(b);

    public static SolidAngle operator -(SolidAngle v)
      => new(-v.m_value);
    public static SolidAngle operator +(SolidAngle a, double b)
      => new(a.m_value + b);
    public static SolidAngle operator +(SolidAngle a, SolidAngle b)
      => a + b.m_value;
    public static SolidAngle operator /(SolidAngle a, double b)
      => new(a.m_value / b);
    public static SolidAngle operator /(SolidAngle a, SolidAngle b)
      => a / b.m_value;
    public static SolidAngle operator *(SolidAngle a, double b)
      => new(a.m_value * b);
    public static SolidAngle operator *(SolidAngle a, SolidAngle b)
      => a * b.m_value;
    public static SolidAngle operator %(SolidAngle a, double b)
      => new(a.m_value % b);
    public static SolidAngle operator %(SolidAngle a, SolidAngle b)
      => a % b.m_value;
    public static SolidAngle operator -(SolidAngle a, double b)
      => new(a.m_value - b);
    public static SolidAngle operator -(SolidAngle a, SolidAngle b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(SolidAngle other)
      => m_value.CompareTo(other.m_value);

    // IEquatable
    public bool Equals(SolidAngle other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is SolidAngle o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
    #endregion Object overrides
  }
}
