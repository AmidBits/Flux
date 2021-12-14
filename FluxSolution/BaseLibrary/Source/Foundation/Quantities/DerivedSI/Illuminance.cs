namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static Illuminance Create(this IlluminanceUnit unit, double value)
      => new(value, unit);
    public static string GetUnitSymbol(this IlluminanceUnit unit)
      => unit switch
      {
        IlluminanceUnit.Lux => @" lx",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  public enum IlluminanceUnit
  {
    Lux,
  }

  /// <summary>Illuminance unit of lux.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Illuminance"/>
  public struct Illuminance
    : System.IComparable<Illuminance>, System.IEquatable<Illuminance>, IValueGeneralizedUnit<double>, IValueSiDerivedUnit<double>
  {
    public const IlluminanceUnit DefaultUnit = IlluminanceUnit.Lux;

    private readonly double m_value;

    public Illuminance(double value, IlluminanceUnit unit = DefaultUnit)
      => m_value = unit switch
      {
        IlluminanceUnit.Lux => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double Value
      => m_value;

    public string ToUnitString(IlluminanceUnit unit = DefaultUnit, string? format = null)
      => $"{(format is null ? ToUnitValue(unit) : string.Format($"{{0:{format}}}", ToUnitValue(unit)))}{unit.GetUnitSymbol()}";
    public double ToUnitValue(IlluminanceUnit unit = DefaultUnit)
      => unit switch
      {
        IlluminanceUnit.Lux => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Overloaded operators
    public static explicit operator double(Illuminance v)
      => v.m_value;
    public static explicit operator Illuminance(double v)
      => new(v);

    public static bool operator <(Illuminance a, Illuminance b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Illuminance a, Illuminance b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Illuminance a, Illuminance b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(Illuminance a, Illuminance b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(Illuminance a, Illuminance b)
      => a.Equals(b);
    public static bool operator !=(Illuminance a, Illuminance b)
      => !a.Equals(b);

    public static Illuminance operator -(Illuminance v)
      => new(-v.m_value);
    public static Illuminance operator +(Illuminance a, double b)
      => new(a.m_value + b);
    public static Illuminance operator +(Illuminance a, Illuminance b)
      => a + b.m_value;
    public static Illuminance operator /(Illuminance a, double b)
      => new(a.m_value / b);
    public static Illuminance operator /(Illuminance a, Illuminance b)
      => a / b.m_value;
    public static Illuminance operator *(Illuminance a, double b)
      => new(a.m_value * b);
    public static Illuminance operator *(Illuminance a, Illuminance b)
      => a * b.m_value;
    public static Illuminance operator %(Illuminance a, double b)
      => new(a.m_value % b);
    public static Illuminance operator %(Illuminance a, Illuminance b)
      => a % b.m_value;
    public static Illuminance operator -(Illuminance a, double b)
      => new(a.m_value - b);
    public static Illuminance operator -(Illuminance a, Illuminance b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Illuminance other)
      => m_value.CompareTo(other.m_value);

    // IEquatable
    public bool Equals(Illuminance other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Illuminance o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
    #endregion Object overrides
  }
}
