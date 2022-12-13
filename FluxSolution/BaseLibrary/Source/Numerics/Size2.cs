namespace Flux.Numerics
{
  public readonly record struct Size2<TSelf>
    : ISize2<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
    public static readonly Size2<TSelf> Zero;

    private readonly TSelf m_width;
    private readonly TSelf m_height;

    public Size2(TSelf width, TSelf height)
    {
      m_width = width;
      m_height = height;
    }

    public TSelf Width { get => m_width; init => m_width = value; }
    public TSelf Height { get => m_height; init => m_height = value; }

    public Numerics.CartesianCoordinate2<TSelf> Center()
      => new(
        m_width.Divide(2),
        m_height.Divide(2)
      );

    ///// <summary>Convert the <see cref="Size2"/> to a <see cref="Vector2"/>.</summary>
    //public Vector2 ToCartesianCoordinate2()
    //  => new(m_width, m_height);

    ///// <summary>Convert the <see cref="Size2"/> to a <see cref="Point2"/>.</summary>
    //public Point2 ToCartesianCoordinate2I()
    //  => new(m_width, m_height);

    ///// <summary>Convert a mapped index to a 2D point. This index is uniquely mapped using the size</summary>
    //public Point2 UniqueIndexToPoint(long index)
    //  => new((int)(index % m_width), (int)(index / m_width));

    /// <summary>Converts the 2D point to a mapped index. This index is uniquely mapped using the size</summary>
    public TSelf PointToUniqueIndex(TSelf x, TSelf y)
      => x + (y * m_width);

    ///// <summary>Converts the 2D point to a mapped index. This index is uniquely mapped using the size</summary>
    //public long PointToUniqueIndex(Point2 point)
    //  => PointToUniqueIndex(point.X, point.Y);

    #region Static methods
    /// <summary>Adds a <see cref='Size2'/> by another <see cref='Size2'/>.</summary>
    public static Size2<TSelf> Add(Size2<TSelf> a, Size2<TSelf> b)
      => new(unchecked(a.m_width + b.m_width), unchecked(a.m_height + b.m_height));
    /// <summary>Adds a <see cref='Size2'/> by another <see cref='Size2'/>.</summary>
    public static Size2<TSelf> Add(Size2<TSelf> a, TSelf b)
      => new(unchecked(a.m_width + b), unchecked(a.m_height + b));
    /// <summary>Divides the components of the <see cref="Size2"/> by the corresponding components of another <see cref="Size2"/> producing two quotients as a new <see cref="Size2"/>.</summary>
    public static Size2<TSelf> Divide(Size2<TSelf> dividend, Size2<TSelf> divisor)
      => new(unchecked(dividend.m_width / divisor.m_width), unchecked(dividend.m_height / divisor.m_height));
    /// <summary>Divides the components of the <see cref="Size2"/> by a <see cref="int"/> producing two quotients as a new <see cref="Size2"/>.</summary>
    public static Size2<TSelf> Divide(Size2<TSelf> dividend, TSelf divisor)
      => new(unchecked(dividend.m_width / divisor), unchecked(dividend.m_height / divisor));
    /// <summary>Divides a <see cref="int"/> by the components of a <see cref="Size2"/> producing two quotients as a new <see cref="Size2"/>.</summary>
    public static Size2<TSelf> Divide(TSelf dividend, Size2<TSelf> divisors)
      => new(unchecked(dividend / divisors.m_width), unchecked(dividend / divisors.m_height));
    /// <summary>Multiplies <see cref="Size2"/> by an <see cref="int"/> producing <see cref="Size2"/>.</summary>
    public static Size2<TSelf> Multiply(Size2<TSelf> size, TSelf multiplier)
      => new(unchecked(size.m_width * multiplier), unchecked(size.m_height * multiplier));
    /// <summary>Subtracts a <see cref='Size2'/> by another <see cref='Size2'/>.</summary>
    public static Size2<TSelf> Subtract(Size2<TSelf> a, Size2<TSelf> b)
      => new(unchecked(a.m_width - b.m_width), unchecked(a.m_height - b.m_height));
    /// <summary>Subtracts a <see cref='Size2'/> by a <see cref='in'/>.</summary>
    public static Size2<TSelf> Subtract(Size2<TSelf> a, TSelf b)
      => new(unchecked(a.m_width - b), unchecked(a.m_height - b));
    /// <summary>Subtracts a <see cref='int'/> by a <see cref='Size2'/>.</summary>
    public static Size2<TSelf> Subtract(TSelf a, Size2<TSelf> b)
      => new(unchecked(a - b.m_width), unchecked(a - b.m_height));
    ///// <summary>Creates a <see cref='Point2'/> from a <see cref='Size2'/>.</summary>
    //public static Point2 ToPoint2(Size2<TSelf> size)
    //  => new(size.m_width, size.m_height);
    #endregion Static methods

    #region Overloaded operators
    public static Size2<TSelf> operator +(Size2<TSelf> a, Size2<TSelf> b)
      => Add(a, b);
    public static Size2<TSelf> operator +(Size2<TSelf> a, TSelf b)
      => Add(a, b);
    public static Size2<TSelf> operator +(TSelf a, Size2<TSelf> b)
      => Add(b, a);
    public static Size2<TSelf> operator -(Size2<TSelf> a, Size2<TSelf> b)
      => Subtract(a, b);
    public static Size2<TSelf> operator -(Size2<TSelf> a, TSelf b)
      => Subtract(a, b);
    public static Size2<TSelf> operator -(TSelf a, Size2<TSelf> b)
      => Subtract(a, b);
    public static Size2<TSelf> operator *(Size2<TSelf> a, TSelf b)
      => Multiply(a, b);
    public static Size2<TSelf> operator *(TSelf a, Size2<TSelf> b)
      => Multiply(b, a);
    public static Size2<TSelf> operator /(Size2<TSelf> a, TSelf b)
      => Divide(a, b);
    public static Size2<TSelf> operator /(TSelf a, Size2<TSelf> b)
      => Divide(a, b);
    #endregion Overloaded operators

    #region Object overrides
    public override string? ToString()
      => $"{GetType().Name} {{ Width = {m_width}, Height = {m_height} }}";
    #endregion Object overrides
  }
}
