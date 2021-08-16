namespace Flux.Quantity
{
  /// <summary>Radius.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Radius"/>
  public struct Radius
    : System.IComparable<Radius>, System.IEquatable<Radius>
  {
    private readonly double m_value;

    public Radius(double radius)
      => m_value = radius;

    /// <summary>Area of a circle with the radius.</summary>
    public double Area
      => System.Math.PI * m_value * m_value;
    /// <summary>Circumference of a circle with the radius.</summary>
    public double Circumference
      => m_value * Maths.PiX2;
    /// <summary>Diameter of the radius.</summary>
    public double Diameter
      => m_value * System.Math.PI;

    public double Value
      => m_value;

    #region Static members
    /// <summary>Create radius from the specified area (of a circle).</summary>
    public static Radius FromArea(double area)
      => new Radius(System.Math.Sqrt(area / System.Math.PI));
    /// <summary>Create radius from the specified circumference.</summary>
    public static Radius FromCircumference(double circumference)
      => new Radius(circumference / Maths.PiX2);
    /// <summary>Create radius from the specified diameter.</summary>
    public static Radius FromDiameter(double diameter)
      => new Radius(diameter / System.Math.PI);
    #endregion Static members

    #region Overloaded operators
    public static explicit operator double(Radius v)
      => v.m_value;
    public static explicit operator Radius(double v)
      => new Radius(v);

    public static bool operator <(Radius a, Radius b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Radius a, Radius b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Radius a, Radius b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(Radius a, Radius b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(Radius a, Radius b)
      => a.Equals(b);
    public static bool operator !=(Radius a, Radius b)
      => !a.Equals(b);

    public static Radius operator -(Radius v)
      => new Radius(-v.m_value);
    public static Radius operator +(Radius a, double b)
      => new Radius(a.m_value + b);
    public static Radius operator +(Radius a, Radius b)
      => a + b.m_value;
    public static Radius operator /(Radius a, double b)
      => new Radius(a.m_value / b);
    public static Radius operator /(Radius a, Radius b)
      => a / b.m_value;
    public static Radius operator *(Radius a, double b)
      => new Radius(a.m_value * b);
    public static Radius operator *(Radius a, Radius b)
      => a * b.m_value;
    public static Radius operator %(Radius a, double b)
      => new Radius(a.m_value % b);
    public static Radius operator %(Radius a, Radius b)
      => a % b.m_value;
    public static Radius operator -(Radius a, double b)
      => new Radius(a.m_value - b);
    public static Radius operator -(Radius a, Radius b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Radius other)
      => m_value.CompareTo(other.m_value);

    // IEquatable
    public bool Equals(Radius other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Radius o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_value}>";
    #endregion Object overrides
  }
}
