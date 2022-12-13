namespace Flux.Numerics
{
  public readonly record struct Size3<TSelf>
    : ISize3<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
    public static readonly Size3<TSelf> Zero;

    private readonly TSelf m_width;
    private readonly TSelf m_height;
    private readonly TSelf m_depth;

    public Size3(TSelf width, TSelf height, TSelf depth)
    {
      m_width = width;
      m_height = height;
      m_depth = depth;
    }

    public TSelf Width { get => m_width; init => m_width = value; }
    public TSelf Height { get => m_height; init => m_height = value; }
    public TSelf Depth { get => m_depth; init => m_depth = value; }

    public Numerics.CartesianCoordinate3<TSelf> Center()
      => new(
        m_width.Divide(2),
        m_height.Divide(2),
        m_depth.Divide(2)
      );

    /// <summary>Convert the <see cref="Size3{TSelf}"/> to a <see cref="CartesianCoordinate3{TSelf}"/>.</summary>
    public Numerics.CartesianCoordinate3<TSelf> ToCartesianCoordinate3()
      => new(
        m_width,
        m_height,
        m_depth
      );

    /// <summary>Convert a mapped index to a 3D point. This index is uniquely mapped using the size.</summary>
    public Numerics.CartesianCoordinate3<TSelf> UniqueIndexToPoint(TSelf index)
    {
      var xy = m_width * m_height;
      var irxy = index % xy;

      return new((irxy % m_width), (irxy / m_width), (index / xy));
    }
    /// <summary>Converts the 3D point to a mapped index. This index is uniquely mapped using the size.</summary>
    public TSelf PointToUniqueIndex(TSelf x, TSelf y, TSelf z)
      => x + (y * m_width) + (z * m_width * m_height);
    ///// <summary>Converts the 3D point to a mapped index. This index is uniquely mapped using the size.</summary>
    //public long PointToUniqueIndex(in Point3 point)
    //  => PointToUniqueIndex(point.X, point.Y, point.X);

    #region Static methods
    /// <summary>Adds a <see cref='Size3'/> by another <see cref='Size3'/>.</summary>
    public static Size3<TSelf> Add(Size3<TSelf> a, Size3<TSelf> b)
      => new(unchecked(a.m_width + b.m_width), unchecked(a.m_height + b.m_height), unchecked(a.m_depth + b.m_depth));
    /// <summary>Adds a <see cref='Size3'/> by another <see cref='Size3'/>.</summary>
    public static Size3<TSelf> Add(Size3<TSelf> a, TSelf b)
      => new(unchecked(a.m_width + b), unchecked(a.m_height + b), unchecked(a.m_depth + b));
    /// <summary>Divides the components of the <see cref="Size3"/> by the corresponding components of another <see cref="Size3"/> producing two quotients as a new <see cref="Size3"/>.</summary>
    public static Size3<TSelf> Divide(Size3<TSelf> dividend, Size3<TSelf> divisor)
      => new(unchecked(dividend.m_width / divisor.m_width), unchecked(dividend.m_height / divisor.m_height), unchecked(dividend.m_depth / divisor.m_depth));
    /// <summary>Divides the components of the <see cref="Size3"/> by a <see cref="int"/> producing two quotients as a new <see cref="Size3"/>.</summary>
    public static Size3<TSelf> Divide(Size3<TSelf> dividend, TSelf divisor)
      => new(unchecked(dividend.m_width / divisor), unchecked(dividend.m_height / divisor), unchecked(dividend.m_depth / divisor));
    /// <summary>Divides a <see cref="int"/> by the components of a <see cref="Size3"/> producing two quotients as a new <see cref="Size3"/>.</summary>
    public static Size3<TSelf> Divide(TSelf dividend, Size3<TSelf> divisors)
      => new(unchecked(dividend / divisors.m_width), unchecked(dividend / divisors.m_height), unchecked(dividend / divisors.m_depth));
    /// <summary>Multiplies <see cref="Size3"/> by an <see cref="int"/> producing <see cref="Size3"/>.</summary>
    public static Size3<TSelf> Multiply(Size3<TSelf> size, TSelf multiplier)
      => new(unchecked(size.m_width * multiplier), unchecked(size.m_height * multiplier), unchecked(size.m_depth * multiplier));
    /// <summary>Subtracts a <see cref='Size3'/> by another <see cref='Size3'/>.</summary>
    public static Size3<TSelf> Subtract(Size3<TSelf> a, Size3<TSelf> b)
      => new(unchecked(a.m_width - b.m_width), unchecked(a.m_height - b.m_height), unchecked(a.m_depth - b.m_depth));
    /// <summary>Subtracts a <see cref='Size3'/> by a <see cref='in'/>.</summary>
    public static Size3<TSelf> Subtract(Size3<TSelf> a, TSelf b)
      => new(unchecked(a.m_width - b), unchecked(a.m_height - b), unchecked(a.m_depth - b));
    /// <summary>Subtracts a <see cref='int'/> by a <see cref='Size3'/>.</summary>
    public static Size3<TSelf> Subtract(TSelf a, Size3<TSelf> b)
      => new(unchecked(a - b.m_width), unchecked(a - b.m_height), unchecked(a - b.m_depth));
    /// <summary>Creates a new <see cref='CartesianCoordinate3<int>'/> from a <see cref='Size3'/>.</summary>
    public static Numerics.CartesianCoordinate3<TSelf> ToPoint3(Size3<TSelf> size)
      => new(size.m_width, size.m_height, size.m_depth);
    #endregion Static methods

    #region Overloaded operators
    public static Size3<TSelf> operator +(Size3<TSelf> a, Size3<TSelf> b)
      => Add(a, b);
    public static Size3<TSelf> operator +(Size3<TSelf> a, TSelf b)
      => Add(a, b);
    public static Size3<TSelf> operator +(TSelf a, Size3<TSelf> b)
      => Add(b, a);
    public static Size3<TSelf> operator -(Size3<TSelf> a, Size3<TSelf> b)
      => Subtract(a, b);
    public static Size3<TSelf> operator -(Size3<TSelf> a, TSelf b)
      => Subtract(a, b);
    public static Size3<TSelf> operator -(TSelf a, Size3<TSelf> b)
      => Subtract(a, b);
    public static Size3<TSelf> operator *(Size3<TSelf> a, TSelf b)
      => Multiply(a, b);
    public static Size3<TSelf> operator *(TSelf a, Size3<TSelf> b)
      => Multiply(b, a);
    public static Size3<TSelf> operator /(Size3<TSelf> a, TSelf b)
      => Divide(a, b);
    public static Size3<TSelf> operator /(TSelf a, Size3<TSelf> b)
      => Divide(a, b);
    #endregion Overloaded operators

    #region Object overrides
    public override string? ToString()
      => $"{GetType().Name} {{ Width = {m_width}, Height = {m_height}, Depth = {m_depth} }}";
    #endregion Object overrides
  }
}
