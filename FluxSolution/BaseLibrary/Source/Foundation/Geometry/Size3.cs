namespace Flux.Geometry
{
  public struct Size3
    : System.IEquatable<Size3>
  {
    public static readonly Size3 Zero;

    public int Width;
    public int Height;
    public int Depth;

    public Size3(int width, int height, int depth)
    {
      Width = width;
      Height = height;
      Depth = depth;
    }

    public Point3 Center()
      => new Point3(Width / 2, Height / 2, Depth / 2);

    /// <summary>Convert a mapped index to a 3D point. This index is uniquely mapped using the size.</summary>
    public Point3 UniqueIndexToPoint(long index)
    {
      var xy = (long)Width * (long)Height;
      var irxy = index % xy;

      return new Point3((int)(irxy % Width), (int)(irxy / Width), (int)(index / xy));
    }
    /// <summary>Converts the 3D point to a mapped index. This index is uniquely mapped using the size.</summary>
    public long PointToUniqueIndex(int x, int y, int z)
      => x + (y * Width) + (z * Width * Height);
    /// <summary>Converts the 3D point to a mapped index. This index is uniquely mapped using the size.</summary>
    public long PointToUniqueIndex(in Point3 point)
      => PointToUniqueIndex(point.X, point.Y, point.X);

    #region Static methods
    /// <summary>Adds a <see cref='Size3'/> by another <see cref='Size3'/>.</summary>
    public static Size3 Add(in Size3 a, in Size3 b)
      => new Size3(unchecked(a.Width + b.Width), unchecked(a.Height + b.Height), unchecked(a.Depth + b.Depth));
    /// <summary>Adds a <see cref='Size3'/> by another <see cref='Size3'/>.</summary>
    public static Size3 Add(in Size3 a, int b)
      => new Size3(unchecked(a.Width + b), unchecked(a.Height + b), unchecked(a.Depth + b));
    /// <summary>Divides the components of the <see cref="Size3"/> by the corresponding components of another <see cref="Size3"/> producing two quotients as a new <see cref="Size3"/>.</summary>
    public static Size3 Divide(in Size3 dividend, Size3 divisor)
      => new Size3(unchecked(dividend.Width / divisor.Width), unchecked(dividend.Height / divisor.Height), unchecked(dividend.Depth / divisor.Depth));
    /// <summary>Divides the components of the <see cref="Size3"/> by a <see cref="int"/> producing two quotients as a new <see cref="Size3"/>.</summary>
    public static Size3 Divide(in Size3 dividend, int divisor)
      => new Size3(unchecked(dividend.Width / divisor), unchecked(dividend.Height / divisor), unchecked(dividend.Depth / divisor));
    /// <summary>Divides a <see cref="int"/> by the components of a <see cref="Size3"/> producing two quotients as a new <see cref="Size3"/>.</summary>
    public static Size3 Divide(int dividend, in Size3 divisors)
      => new Size3(unchecked(dividend / divisors.Width), unchecked(dividend / divisors.Height), unchecked(dividend / divisors.Depth));
    /// <summary>Multiplies <see cref="Size3"/> by an <see cref="int"/> producing <see cref="Size3"/>.</summary>
    public static Size3 Multiply(in Size3 size, int multiplier)
      => new Size3(unchecked(size.Width * multiplier), unchecked(size.Height * multiplier), unchecked(size.Depth * multiplier));
    /// <summary>Subtracts a <see cref='Size3'/> by another <see cref='Size3'/>.</summary>
    public static Size3 Subtract(in Size3 a, in Size3 b)
      => new Size3(unchecked(a.Width - b.Width), unchecked(a.Height - b.Height), unchecked(a.Depth - b.Depth));
    /// <summary>Subtracts a <see cref='Size3'/> by a <see cref='in'/>.</summary>
    public static Size3 Subtract(in Size3 a, int b)
      => new Size3(unchecked(a.Width - b), unchecked(a.Height - b), unchecked(a.Depth - b));
    /// <summary>Subtracts a <see cref='int'/> by a <see cref='Size3'/>.</summary>
    public static Size3 Subtract(int a, in Size3 b)
      => new Size3(unchecked(a - b.Width), unchecked(a - b.Height), unchecked(a - b.Depth));
    /// <summary>Creates a <see cref='Point3'/> from a <see cref='Size3'/>.</summary>
    public static Point3 ToPoint3(in Size3 size)
      => new Point3(size.Width, size.Height, size.Depth);
    #endregion Static methods

    #region Overloaded operators
    public static bool operator ==(in Size3 a, in Size3 b)
      => a.Equals(b);
    public static bool operator !=(in Size3 a, in Size3 b)
      => !a.Equals(b);

    public static Size3 operator +(in Size3 a, in Size3 b)
      => Add(a, b);
    public static Size3 operator +(in Size3 a, int b)
      => Add(a, b);
    public static Size3 operator +(int a, in Size3 b)
      => Add(b, a);
    public static Size3 operator -(in Size3 a, in Size3 b)
      => Subtract(a, b);
    public static Size3 operator -(in Size3 a, int b)
      => Subtract(a, b);
    public static Size3 operator -(int a, in Size3 b)
      => Subtract(a, b);
    public static Size3 operator *(in Size3 a, int b)
      => Multiply(a, b);
    public static Size3 operator *(int a, in Size3 b)
      => Multiply(b, a);
    public static Size3 operator /(in Size3 a, int b)
      => Divide(a, b);
    public static Size3 operator /(int a, in Size3 b)
      => Divide(a, b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    public bool Equals(Size3 other)
      => Width == other.Width && Height == other.Height && Depth == other.Depth;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Size3 o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(Width, Height, Depth);
    public override string? ToString()
      => $"<Size {Width}, {Height}, {Depth}>";
    #endregion Object overrides
  }
}
