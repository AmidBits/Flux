namespace Flux.Media.Geometry
{
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
  public struct Size2
    : System.IEquatable<Size2>
  {
    public static readonly Size2 Empty;
    public bool IsEmpty => Equals(Empty);

    [System.Runtime.InteropServices.FieldOffset(0)] private int m_width;
    [System.Runtime.InteropServices.FieldOffset(4)] private int m_height;

    public int Width { get => m_width; set => m_width = value; }
    public int Height { get => m_height; set => m_height = value; }

    public Point2 Center()
      => new Point2(m_width / 2, m_height / 2);

    public Size2(int width, int height)
    {
      m_width = width;
      m_height = height;
    }

    #region Statics
    /// <summary>Adds a <see cref='Size2'/> by another <see cref='Size2'/>.</summary>
    public static Size2 Add(Size2 a, Size2 b)
      => new Size2(unchecked(a.Width + b.Width), unchecked(a.Height + b.Height));
    /// <summary>Adds a <see cref='Size2'/> by another <see cref='Size2'/>.</summary>
    public static Size2 Add(Size2 a, int b)
      => new Size2(unchecked(a.Width + b), unchecked(a.Height + b));
    /// <summary>Divides the components of the <see cref="Size2"/> by the corresponding components of another <see cref="Size2"/> producing two quotients as a new <see cref="Size2"/>.</summary>
    public static Size2 Divide(Size2 dividend, Size2 divisor)
      => new Size2(unchecked(dividend.Width / divisor.Width), unchecked(dividend.Height / divisor.Height));
    /// <summary>Divides the components of the <see cref="Size2"/> by a <see cref="int"/> producing two quotients as a new <see cref="Size2"/>.</summary>
    public static Size2 Divide(Size2 dividend, int divisor)
      => new Size2(unchecked(dividend.Width / divisor), unchecked(dividend.Height / divisor));
    /// <summary>Divides a <see cref="int"/> by the components of a <see cref="Size2"/> producing two quotients as a new <see cref="Size2"/>.</summary>
    public static Size2 Divide(int dividend, Size2 divisors)
      => new Size2(unchecked(dividend / divisors.Width), unchecked(dividend / divisors.Height));
    /// <summary>Multiplies <see cref="Size2"/> by an <see cref="int"/> producing <see cref="Size2"/>.</summary>
    public static Size2 Multiply(Size2 size, int multiplier)
      => new Size2(unchecked(size.Width * multiplier), unchecked(size.Height * multiplier));
    /// <summary>Subtracts a <see cref='Size2'/> by another <see cref='Size2'/>.</summary>
    public static Size2 Subtract(Size2 a, Size2 b)
      => new Size2(unchecked(a.Width - b.Width), unchecked(a.Height - b.Height));
    /// <summary>Subtracts a <see cref='Size2'/> by a <see cref='in'/>.</summary>
    public static Size2 Subtract(Size2 a, int b)
      => new Size2(unchecked(a.Width - b), unchecked(a.Height - b));
    /// <summary>Subtracts a <see cref='int'/> by a <see cref='Size2'/>.</summary>
    public static Size2 Subtract(int a, Size2 b)
      => new Size2(unchecked(a - b.Width), unchecked(a - b.Height));
    /// <summary>Creates a <see cref='Point2'/> from a <see cref='Size2'/>.</summary>
    public static Point2 ToPoint2(Size2 size)
      => new Point2(size.Width, size.Height);
    #endregion Statics

    #region Operators
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

    public static bool operator ==(Size2 a, Size2 b)
      => a.Equals(b);
    public static bool operator !=(Size2 a, Size2 b)
      => !a.Equals(b);
    #endregion Operators

    // IEquatable
    public bool Equals(Size2 other)
      => m_width == other.m_width && m_height == other.m_height;

    // Object (overrides)
    public override bool Equals(object? obj)
      => obj is Size2 o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_width, m_height);
    public override string? ToString()
      => $"<Size {m_width}, {m_height}>";
  }
}
