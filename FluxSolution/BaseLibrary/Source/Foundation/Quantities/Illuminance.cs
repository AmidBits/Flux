namespace Flux.Quantity
{
  public enum IlluminanceUnit
  {
    Lux,
  }

  /// <summary>Illuminance unit of lux.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Illuminance"/>
#if NET5_0
  public struct Illuminance
    : System.IComparable<Illuminance>, System.IEquatable<Illuminance>, IValuedUnit<double>
#else
  public record struct Illuminance
    : System.IComparable<Illuminance>, IValuedUnit<double>
#endif
  {
    private readonly double m_value;

    public Illuminance(double value, IlluminanceUnit unit = IlluminanceUnit.Lux)
      => m_value = unit switch
      {
        IlluminanceUnit.Lux => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double Value
      => m_value;

    public double ToUnitValue(IlluminanceUnit unit = IlluminanceUnit.Lux)
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

#if NET5_0
    public static bool operator ==(Illuminance a, Illuminance b)
      => a.Equals(b);
    public static bool operator !=(Illuminance a, Illuminance b)
      => !a.Equals(b);
#endif

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

#if NET5_0
    // IEquatable
    public bool Equals(Illuminance other)
      => m_value == other.m_value;
#endif
    #endregion Implemented interfaces

    #region Object overrides
#if NET5_0
    public override bool Equals(object? obj)
      => obj is Illuminance o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
#endif
    public override string ToString()
      => $"{GetType().Name} {{ Value = {m_value} lx }}";
    #endregion Object overrides
  }
}
