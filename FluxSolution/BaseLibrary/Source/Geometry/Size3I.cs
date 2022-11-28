namespace Flux
{
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly struct Size3I
    : System.IEquatable<Size3I>
  {
    public static readonly Size3I Zero;

    private readonly int m_width;
    private readonly int m_height;
    private readonly int m_depth;

    public Size3I(int width, int height, int depth)
    {
      m_width = width;
      m_height = height;
      m_depth = depth;
    }

    [System.Diagnostics.Contracts.Pure] public int Width { get => m_width; init => m_width = value; }
    [System.Diagnostics.Contracts.Pure] public int Height { get => m_height; init => m_height = value; }
    [System.Diagnostics.Contracts.Pure] public int Depth { get => m_depth; init => m_depth = value; }

    public Point3 Center()
      => new(m_width / 2, m_height / 2, m_depth / 2);

    /// <summary>Convert the <see cref="Size3I"/> to a <see cref="Vector3"/>.</summary>
    public Vector3 ToCartesianCoordinate3()
      => new(m_width, m_height, m_depth);

    /// <summary>Convert the <see cref="Size3I"/> to a <see cref="Point3"/>.</summary>
    public Point3 ToCartesianCoordinate3I()
      => new(m_width, m_height, m_depth);

    /// <summary>Convert a mapped index to a 3D point. This index is uniquely mapped using the size.</summary>
    public Point3 UniqueIndexToPoint(long index)
    {
      var xy = (long)m_width * (long)m_height;
      var irxy = index % xy;

      return new Point3((int)(irxy % m_width), (int)(irxy / m_width), (int)(index / xy));
    }
    /// <summary>Converts the 3D point to a mapped index. This index is uniquely mapped using the size.</summary>
    public long PointToUniqueIndex(int x, int y, int z)
      => x + (y * m_width) + (z * m_width * m_height);
    /// <summary>Converts the 3D point to a mapped index. This index is uniquely mapped using the size.</summary>
    public long PointToUniqueIndex(in Point3 point)
      => PointToUniqueIndex(point.X, point.Y, point.X);

    #region Static methods
    /// <summary>Adds a <see cref='Size3I'/> by another <see cref='Size3I'/>.</summary>
    public static Size3I Add(in Size3I a, in Size3I b)
      => new(unchecked(a.m_width + b.m_width), unchecked(a.m_height + b.m_height), unchecked(a.m_depth + b.m_depth));
    /// <summary>Adds a <see cref='Size3I'/> by another <see cref='Size3I'/>.</summary>
    public static Size3I Add(in Size3I a, int b)
      => new(unchecked(a.m_width + b), unchecked(a.m_height + b), unchecked(a.m_depth + b));
    /// <summary>Divides the components of the <see cref="Size3I"/> by the corresponding components of another <see cref="Size3I"/> producing two quotients as a new <see cref="Size3I"/>.</summary>
    public static Size3I Divide(in Size3I dividend, Size3I divisor)
      => new(unchecked(dividend.m_width / divisor.m_width), unchecked(dividend.m_height / divisor.m_height), unchecked(dividend.m_depth / divisor.m_depth));
    /// <summary>Divides the components of the <see cref="Size3I"/> by a <see cref="int"/> producing two quotients as a new <see cref="Size3I"/>.</summary>
    public static Size3I Divide(in Size3I dividend, int divisor)
      => new(unchecked(dividend.m_width / divisor), unchecked(dividend.m_height / divisor), unchecked(dividend.m_depth / divisor));
    /// <summary>Divides a <see cref="int"/> by the components of a <see cref="Size3I"/> producing two quotients as a new <see cref="Size3I"/>.</summary>
    public static Size3I Divide(int dividend, in Size3I divisors)
      => new(unchecked(dividend / divisors.m_width), unchecked(dividend / divisors.m_height), unchecked(dividend / divisors.m_depth));
    /// <summary>Multiplies <see cref="Size3I"/> by an <see cref="int"/> producing <see cref="Size3I"/>.</summary>
    public static Size3I Multiply(in Size3I size, int multiplier)
      => new(unchecked(size.m_width * multiplier), unchecked(size.m_height * multiplier), unchecked(size.m_depth * multiplier));
    /// <summary>Subtracts a <see cref='Size3I'/> by another <see cref='Size3I'/>.</summary>
    public static Size3I Subtract(in Size3I a, in Size3I b)
      => new(unchecked(a.m_width - b.m_width), unchecked(a.m_height - b.m_height), unchecked(a.m_depth - b.m_depth));
    /// <summary>Subtracts a <see cref='Size3I'/> by a <see cref='in'/>.</summary>
    public static Size3I Subtract(in Size3I a, int b)
      => new(unchecked(a.m_width - b), unchecked(a.m_height - b), unchecked(a.m_depth - b));
    /// <summary>Subtracts a <see cref='int'/> by a <see cref='Size3I'/>.</summary>
    public static Size3I Subtract(int a, in Size3I b)
      => new(unchecked(a - b.m_width), unchecked(a - b.m_height), unchecked(a - b.m_depth));
    /// <summary>Creates a <see cref='Point3'/> from a <see cref='Size3I'/>.</summary>
    public static Point3 ToPoint3(in Size3I size)
      => new(size.m_width, size.m_height, size.m_depth);
    #endregion Static methods

    #region Overloaded operators
    public static bool operator ==(in Size3I a, in Size3I b)
      => a.Equals(b);
    public static bool operator !=(in Size3I a, in Size3I b)
      => !a.Equals(b);

    public static Size3I operator +(in Size3I a, in Size3I b)
      => Add(a, b);
    public static Size3I operator +(in Size3I a, int b)
      => Add(a, b);
    public static Size3I operator +(int a, in Size3I b)
      => Add(b, a);
    public static Size3I operator -(in Size3I a, in Size3I b)
      => Subtract(a, b);
    public static Size3I operator -(in Size3I a, int b)
      => Subtract(a, b);
    public static Size3I operator -(int a, in Size3I b)
      => Subtract(a, b);
    public static Size3I operator *(in Size3I a, int b)
      => Multiply(a, b);
    public static Size3I operator *(int a, in Size3I b)
      => Multiply(b, a);
    public static Size3I operator /(in Size3I a, int b)
      => Divide(a, b);
    public static Size3I operator /(int a, in Size3I b)
      => Divide(a, b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    public bool Equals(Size3I other)
      => m_width == other.m_width && m_height == other.m_height && m_depth == other.m_depth;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Size3I o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_width, m_height, m_depth);
    public override string? ToString()
      => $"{GetType().Name} {{ Width = {m_width}, Height = {m_height}, Depth = {m_depth} }}";
    #endregion Object overrides
  }
}
