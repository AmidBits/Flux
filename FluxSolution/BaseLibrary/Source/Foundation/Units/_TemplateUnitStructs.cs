//namespace Flux.Units
//{
//  /// <summary>This is a template for typical unit structs (comparable, equatable).</summary>
//  public struct TemplateUnitStructs
//    : System.IComparable<TemplateUnitStructs>, System.IEquatable<TemplateUnitStructs>
//  {
//    private readonly double m_unit;

//    public TemplateUnitStructs(double unit)
//      => m_unit = unit;

//    public double Unit
//      => m_unit;

//    #region Static methods
//    public static TemplateUnitStructs Add(TemplateUnitStructs left, TemplateUnitStructs right)
//      => new TemplateUnitStructs(left.m_unit + right.m_unit);
//    public static TemplateUnitStructs Divide(TemplateUnitStructs left, TemplateUnitStructs right)
//      => new TemplateUnitStructs(left.m_unit / right.m_unit);
//    public static TemplateUnitStructs FromRectangule(double lengthInMeters, double widthInMeters)
//      => new TemplateUnitStructs(lengthInMeters * widthInMeters);
//    public static TemplateUnitStructs Multiply(TemplateUnitStructs left, TemplateUnitStructs right)
//      => new TemplateUnitStructs(left.m_unit * right.m_unit);
//    public static TemplateUnitStructs Negate(TemplateUnitStructs value)
//      => new TemplateUnitStructs(-value.m_unit);
//    public static TemplateUnitStructs Remainder(TemplateUnitStructs dividend, TemplateUnitStructs divisor)
//      => new TemplateUnitStructs(dividend.m_unit % divisor.m_unit);
//    public static TemplateUnitStructs Subtract(TemplateUnitStructs left, TemplateUnitStructs right)
//      => new TemplateUnitStructs(left.m_unit - right.m_unit);
//    #endregion Static methods

//    #region Overloaded operators
//    public static implicit operator double(TemplateUnitStructs v)
//      => v.m_unit;
//    public static implicit operator TemplateUnitStructs(double v)
//      => new TemplateUnitStructs(v);

//    public static bool operator <(TemplateUnitStructs a, TemplateUnitStructs b)
//      => a.CompareTo(b) < 0;
//    public static bool operator <=(TemplateUnitStructs a, TemplateUnitStructs b)
//      => a.CompareTo(b) <= 0;
//    public static bool operator >(TemplateUnitStructs a, TemplateUnitStructs b)
//      => a.CompareTo(b) < 0;
//    public static bool operator >=(TemplateUnitStructs a, TemplateUnitStructs b)
//      => a.CompareTo(b) <= 0;

//    public static bool operator ==(TemplateUnitStructs a, TemplateUnitStructs b)
//      => a.Equals(b);
//    public static bool operator !=(TemplateUnitStructs a, TemplateUnitStructs b)
//      => !a.Equals(b);

//    public static TemplateUnitStructs operator +(TemplateUnitStructs a, TemplateUnitStructs b)
//      => Add(a, b);
//    public static TemplateUnitStructs operator /(TemplateUnitStructs a, TemplateUnitStructs b)
//      => Divide(a, b);
//    public static TemplateUnitStructs operator *(TemplateUnitStructs a, TemplateUnitStructs b)
//      => Multiply(a, b);
//    public static TemplateUnitStructs operator -(TemplateUnitStructs v)
//      => Negate(v);
//    public static TemplateUnitStructs operator %(TemplateUnitStructs a, TemplateUnitStructs b)
//      => Remainder(a, b);
//    public static TemplateUnitStructs operator -(TemplateUnitStructs a, TemplateUnitStructs b)
//      => Subtract(a, b);
//    #endregion Overloaded operators

//    #region Implemented interfaces
//    // IComparable
//    public int CompareTo(TemplateUnitStructs other)
//      => m_unit.CompareTo(other.m_unit);

//    // IEquatable
//    public bool Equals(TemplateUnitStructs other)
//      => m_unit == other.m_unit;
//    #endregion Implemented interfaces

//    #region Object overrides
//    public override bool Equals(object? obj)
//      => obj is TemplateUnitStructs o && Equals(o);
//    public override int GetHashCode()
//      => m_unit.GetHashCode();
//    public override string ToString()
//      => $"<{GetType().Name}: {m_unit} *unitSymbol*>";
//    #endregion Object overrides
//  }
//}
