namespace Flux
{
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public struct Size2
    : System.IEquatable<Size2>
  {
    public static readonly Size2 Zero;

    private readonly int m_width;
    private readonly int m_height;

    public Size2(int width, int height)
    {
      m_width = width;
      m_height = height;
    }

    [System.Diagnostics.Contracts.Pure] public int Width => m_width;
    [System.Diagnostics.Contracts.Pure] public int Height => m_height;

    public CartesianCoordinateI2 Center()
      => new(m_width / 2, m_height / 2);

    /// <summary>Convert the <see cref="Size2"/> to a <see cref="CartesianCoordinateI2"/>.</summary>
    public CartesianCoordinateI2 ToCartesianCoordinateI2()
      => new(m_width, m_height);

    /// <summary>Convert a mapped index to a 2D point. This index is uniquely mapped using the size</summary>
    public CartesianCoordinateI2 UniqueIndexToPoint(long index)
      => new((int)(index % m_width), (int)(index / m_width));
    /// <summary>Converts the 2D point to a mapped index. This index is uniquely mapped using the size</summary>
    public long PointToUniqueIndex(int x, int y)
      => x + (y * m_width);
    /// <summary>Converts the 2D point to a mapped index. This index is uniquely mapped using the size</summary>
    public long PointToUniqueIndex(CartesianCoordinateI2 point)
      => PointToUniqueIndex(point.X, point.Y);

    #region Static methods
    /// <summary>Adds a <see cref='Size2'/> by another <see cref='Size2'/>.</summary>
    public static Size2 Add(Size2 a, Size2 b)
      => new(unchecked(a.m_width + b.m_width), unchecked(a.m_height + b.m_height));
    /// <summary>Adds a <see cref='Size2'/> by another <see cref='Size2'/>.</summary>
    public static Size2 Add(Size2 a, int b)
      => new(unchecked(a.m_width + b), unchecked(a.m_height + b));
    /// <summary>Divides the components of the <see cref="Size2"/> by the corresponding components of another <see cref="Size2"/> producing two quotients as a new <see cref="Size2"/>.</summary>
    public static Size2 Divide(Size2 dividend, Size2 divisor)
      => new(unchecked(dividend.m_width / divisor.m_width), unchecked(dividend.m_height / divisor.m_height));
    /// <summary>Divides the components of the <see cref="Size2"/> by a <see cref="int"/> producing two quotients as a new <see cref="Size2"/>.</summary>
    public static Size2 Divide(Size2 dividend, int divisor)
      => new(unchecked(dividend.m_width / divisor), unchecked(dividend.m_height / divisor));
    /// <summary>Divides a <see cref="int"/> by the components of a <see cref="Size2"/> producing two quotients as a new <see cref="Size2"/>.</summary>
    public static Size2 Divide(int dividend, Size2 divisors)
      => new(unchecked(dividend / divisors.m_width), unchecked(dividend / divisors.m_height));
    /// <summary>Multiplies <see cref="Size2"/> by an <see cref="int"/> producing <see cref="Size2"/>.</summary>
    public static Size2 Multiply(Size2 size, int multiplier)
      => new(unchecked(size.m_width * multiplier), unchecked(size.m_height * multiplier));
    /// <summary>Subtracts a <see cref='Size2'/> by another <see cref='Size2'/>.</summary>
    public static Size2 Subtract(Size2 a, Size2 b)
      => new(unchecked(a.m_width - b.m_width), unchecked(a.m_height - b.m_height));
    /// <summary>Subtracts a <see cref='Size2'/> by a <see cref='in'/>.</summary>
    public static Size2 Subtract(Size2 a, int b)
      => new(unchecked(a.m_width - b), unchecked(a.m_height - b));
    /// <summary>Subtracts a <see cref='int'/> by a <see cref='Size2'/>.</summary>
    public static Size2 Subtract(int a, Size2 b)
      => new(unchecked(a - b.m_width), unchecked(a - b.m_height));
    /// <summary>Creates a <see cref='CartesianCoordinateI2'/> from a <see cref='Size2'/>.</summary>
    public static CartesianCoordinateI2 ToPoint2(Size2 size)
      => new(size.m_width, size.m_height);
    #endregion Static methods

    #region Overloaded operators
    public static bool operator ==(Size2 a, Size2 b)
      => a.Equals(b);
    public static bool operator !=(Size2 a, Size2 b)
      => !a.Equals(b);

    public static Size2 operator +(Size2 a, Size2 b)
      => Add(a, b);
    public static Size2 operator +(Size2 a, int b)
      => Add(a, b);
    public static Size2 operator +(int a, Size2 b)
      => Add(b, a);
    public static Size2 operator -(Size2 a, Size2 b)
      => Subtract(a, b);
    public static Size2 operator -(Size2 a, int b)
      => Subtract(a, b);
    public static Size2 operator -(int a, Size2 b)
      => Subtract(a, b);
    public static Size2 operator *(Size2 a, int b)
      => Multiply(a, b);
    public static Size2 operator *(int a, Size2 b)
      => Multiply(b, a);
    public static Size2 operator /(Size2 a, int b)
      => Divide(a, b);
    public static Size2 operator /(int a, Size2 b)
      => Divide(a, b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    public bool Equals(Size2 other)
      => m_width == other.m_width && m_height == other.m_height;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Size2 o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_width, m_height);
    public override string? ToString()
      => $"{GetType().Name} {{ Width = {m_width}, Height = {m_height} }}";
    #endregion Object overrides
  }
}
