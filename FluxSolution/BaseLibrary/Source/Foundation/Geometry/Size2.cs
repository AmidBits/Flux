namespace Flux
{
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public struct Size2
    : System.IEquatable<Size2>
  {
    public static readonly Size2 Zero;

    public readonly int Width;
    public readonly int Height;

    public Size2(int width, int height)
    {
      Width = width;
      Height = height;
    }

    public Point2 Center()
      => new(Width / 2, Height / 2);

    /// <summary>Convert a mapped index to a 2D point. This index is uniquely mapped using the size</summary>
    public Point2 UniqueIndexToPoint(long index)
      => new((int)(index % Width), (int)(index / Width));
    /// <summary>Converts the 2D point to a mapped index. This index is uniquely mapped using the size</summary>
    public long PointToUniqueIndex(int x, int y)
      => x + (y * Width);
    /// <summary>Converts the 2D point to a mapped index. This index is uniquely mapped using the size</summary>
    public long PointToUniqueIndex(Point2 point)
      => PointToUniqueIndex(point.X, point.Y);

    #region Static methods
    /// <summary>Adds a <see cref='Size2'/> by another <see cref='Size2'/>.</summary>
    public static Size2 Add(Size2 a, Size2 b)
      => new(unchecked(a.Width + b.Width), unchecked(a.Height + b.Height));
    /// <summary>Adds a <see cref='Size2'/> by another <see cref='Size2'/>.</summary>
    public static Size2 Add(Size2 a, int b)
      => new(unchecked(a.Width + b), unchecked(a.Height + b));
    /// <summary>Divides the components of the <see cref="Size2"/> by the corresponding components of another <see cref="Size2"/> producing two quotients as a new <see cref="Size2"/>.</summary>
    public static Size2 Divide(Size2 dividend, Size2 divisor)
      => new(unchecked(dividend.Width / divisor.Width), unchecked(dividend.Height / divisor.Height));
    /// <summary>Divides the components of the <see cref="Size2"/> by a <see cref="int"/> producing two quotients as a new <see cref="Size2"/>.</summary>
    public static Size2 Divide(Size2 dividend, int divisor)
      => new(unchecked(dividend.Width / divisor), unchecked(dividend.Height / divisor));
    /// <summary>Divides a <see cref="int"/> by the components of a <see cref="Size2"/> producing two quotients as a new <see cref="Size2"/>.</summary>
    public static Size2 Divide(int dividend, Size2 divisors)
      => new(unchecked(dividend / divisors.Width), unchecked(dividend / divisors.Height));
    /// <summary>Multiplies <see cref="Size2"/> by an <see cref="int"/> producing <see cref="Size2"/>.</summary>
    public static Size2 Multiply(Size2 size, int multiplier)
      => new(unchecked(size.Width * multiplier), unchecked(size.Height * multiplier));
    /// <summary>Subtracts a <see cref='Size2'/> by another <see cref='Size2'/>.</summary>
    public static Size2 Subtract(Size2 a, Size2 b)
      => new(unchecked(a.Width - b.Width), unchecked(a.Height - b.Height));
    /// <summary>Subtracts a <see cref='Size2'/> by a <see cref='in'/>.</summary>
    public static Size2 Subtract(Size2 a, int b)
      => new(unchecked(a.Width - b), unchecked(a.Height - b));
    /// <summary>Subtracts a <see cref='int'/> by a <see cref='Size2'/>.</summary>
    public static Size2 Subtract(int a, Size2 b)
      => new(unchecked(a - b.Width), unchecked(a - b.Height));
    /// <summary>Creates a <see cref='Point2'/> from a <see cref='Size2'/>.</summary>
    public static Point2 ToPoint2(Size2 size)
      => new(size.Width, size.Height);
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
      => Width == other.Width && Height == other.Height;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Size2 o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(Width, Height);
    public override string? ToString()
      => $"{GetType().Name} {{ Width = {Width}, Height = {Height} }}";
    #endregion Object overrides
  }
}
