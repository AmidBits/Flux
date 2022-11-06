namespace Flux
{
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly struct Size2I
    : System.IEquatable<Size2I>
  {
    public static readonly Size2I Zero;

    private readonly int m_width;
    private readonly int m_height;

    public Size2I(int width, int height)
    {
      m_width = width;
      m_height = height;
    }

    [System.Diagnostics.Contracts.Pure] public int Width { get => m_width; init => m_width = value; }
    [System.Diagnostics.Contracts.Pure] public int Height { get => m_height; init => m_height = value; }

    public Point2 Center()
      => new(m_width / 2, m_height / 2);

    /// <summary>Convert the <see cref="Size2I"/> to a <see cref="Vector2"/>.</summary>
    public Vector2 ToCartesianCoordinate2()
      => new(m_width, m_height);

    /// <summary>Convert the <see cref="Size2I"/> to a <see cref="Point2"/>.</summary>
    public Point2 ToCartesianCoordinate2I()
      => new(m_width, m_height);

    /// <summary>Convert a mapped index to a 2D point. This index is uniquely mapped using the size</summary>
    public Point2 UniqueIndexToPoint(long index)
      => new((int)(index % m_width), (int)(index / m_width));

    /// <summary>Converts the 2D point to a mapped index. This index is uniquely mapped using the size</summary>
    public long PointToUniqueIndex(int x, int y)
      => x + (y * m_width);

    /// <summary>Converts the 2D point to a mapped index. This index is uniquely mapped using the size</summary>
    public long PointToUniqueIndex(Point2 point)
      => PointToUniqueIndex(point.X, point.Y);

    #region Static methods
    /// <summary>Adds a <see cref='Size2I'/> by another <see cref='Size2I'/>.</summary>
    public static Size2I Add(Size2I a, Size2I b)
      => new(unchecked(a.m_width + b.m_width), unchecked(a.m_height + b.m_height));
    /// <summary>Adds a <see cref='Size2I'/> by another <see cref='Size2I'/>.</summary>
    public static Size2I Add(Size2I a, int b)
      => new(unchecked(a.m_width + b), unchecked(a.m_height + b));
    /// <summary>Divides the components of the <see cref="Size2I"/> by the corresponding components of another <see cref="Size2I"/> producing two quotients as a new <see cref="Size2I"/>.</summary>
    public static Size2I Divide(Size2I dividend, Size2I divisor)
      => new(unchecked(dividend.m_width / divisor.m_width), unchecked(dividend.m_height / divisor.m_height));
    /// <summary>Divides the components of the <see cref="Size2I"/> by a <see cref="int"/> producing two quotients as a new <see cref="Size2I"/>.</summary>
    public static Size2I Divide(Size2I dividend, int divisor)
      => new(unchecked(dividend.m_width / divisor), unchecked(dividend.m_height / divisor));
    /// <summary>Divides a <see cref="int"/> by the components of a <see cref="Size2I"/> producing two quotients as a new <see cref="Size2I"/>.</summary>
    public static Size2I Divide(int dividend, Size2I divisors)
      => new(unchecked(dividend / divisors.m_width), unchecked(dividend / divisors.m_height));
    /// <summary>Multiplies <see cref="Size2I"/> by an <see cref="int"/> producing <see cref="Size2I"/>.</summary>
    public static Size2I Multiply(Size2I size, int multiplier)
      => new(unchecked(size.m_width * multiplier), unchecked(size.m_height * multiplier));
    /// <summary>Subtracts a <see cref='Size2I'/> by another <see cref='Size2I'/>.</summary>
    public static Size2I Subtract(Size2I a, Size2I b)
      => new(unchecked(a.m_width - b.m_width), unchecked(a.m_height - b.m_height));
    /// <summary>Subtracts a <see cref='Size2I'/> by a <see cref='in'/>.</summary>
    public static Size2I Subtract(Size2I a, int b)
      => new(unchecked(a.m_width - b), unchecked(a.m_height - b));
    /// <summary>Subtracts a <see cref='int'/> by a <see cref='Size2I'/>.</summary>
    public static Size2I Subtract(int a, Size2I b)
      => new(unchecked(a - b.m_width), unchecked(a - b.m_height));
    /// <summary>Creates a <see cref='Point2'/> from a <see cref='Size2I'/>.</summary>
    public static Point2 ToPoint2(Size2I size)
      => new(size.m_width, size.m_height);
    #endregion Static methods

    #region Overloaded operators
    public static bool operator ==(Size2I a, Size2I b)
      => a.Equals(b);
    public static bool operator !=(Size2I a, Size2I b)
      => !a.Equals(b);

    public static Size2I operator +(Size2I a, Size2I b)
      => Add(a, b);
    public static Size2I operator +(Size2I a, int b)
      => Add(a, b);
    public static Size2I operator +(int a, Size2I b)
      => Add(b, a);
    public static Size2I operator -(Size2I a, Size2I b)
      => Subtract(a, b);
    public static Size2I operator -(Size2I a, int b)
      => Subtract(a, b);
    public static Size2I operator -(int a, Size2I b)
      => Subtract(a, b);
    public static Size2I operator *(Size2I a, int b)
      => Multiply(a, b);
    public static Size2I operator *(int a, Size2I b)
      => Multiply(b, a);
    public static Size2I operator /(Size2I a, int b)
      => Divide(a, b);
    public static Size2I operator /(int a, Size2I b)
      => Divide(a, b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    public bool Equals(Size2I other)
      => m_width == other.m_width && m_height == other.m_height;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Size2I o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_width, m_height);
    public override string? ToString()
      => $"{GetType().Name} {{ Width = {m_width}, Height = {m_height} }}";
    #endregion Object overrides
  }
}
