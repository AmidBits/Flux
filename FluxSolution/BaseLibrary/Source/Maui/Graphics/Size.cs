//namespace Flux.Maui.Graphics
//{
//  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
//  public struct Size
//    : System.IEquatable<Size>
//  {
//    public static readonly Size Empty;
//    public bool IsEmpty => Equals(Empty);

//    [System.Runtime.InteropServices.FieldOffset(0)] private double m_width;
//    [System.Runtime.InteropServices.FieldOffset(4)] private double m_height;

//    public double Width { get => m_width; set => m_width = value; }
//    public double Height { get => m_height; set => m_height = value; }

//    public Point Center()
//      => new Point(m_width / 2, m_height / 2);

//    public Size(double width, double height)
//    {
//      m_width = width;
//      m_height = height;
//    }

//    #region Statics
//    /// <summary>Adds a <see cref='Size'/> by another <see cref='Size'/>.</summary>
//    public static Size Add(Size a, Size b)
//      => new Size(unchecked(a.Width + b.Width), unchecked(a.Height + b.Height));
//    /// <summary>Adds a <see cref='Size'/> by another <see cref='Size'/>.</summary>
//    public static Size Add(Size a, int b)
//      => new Size(unchecked(a.Width + b), unchecked(a.Height + b));
//    /// <summary>Divides the components of the <see cref="Size"/> by the corresponding components of another <see cref="Size"/> producing two quotients as a new <see cref="Size"/>.</summary>
//    public static Size Divide(Size dividend, Size divisor)
//      => new Size(unchecked(dividend.Width / divisor.Width), unchecked(dividend.Height / divisor.Height));
//    /// <summary>Divides the components of the <see cref="Size"/> by a <see cref="int"/> producing two quotients as a new <see cref="Size"/>.</summary>
//    public static Size Divide(Size dividend, int divisor)
//      => new Size(unchecked(dividend.Width / divisor), unchecked(dividend.Height / divisor));
//    /// <summary>Divides a <see cref="int"/> by the components of a <see cref="Size"/> producing two quotients as a new <see cref="Size"/>.</summary>
//    public static Size Divide(int dividend, Size divisors)
//      => new Size(unchecked(dividend / divisors.Width), unchecked(dividend / divisors.Height));
//    /// <summary>Multiplies <see cref="Size"/> by an <see cref="int"/> producing <see cref="Size"/>.</summary>
//    public static Size Multiply(Size size, int multiplier)
//      => new Size(unchecked(size.Width * multiplier), unchecked(size.Height * multiplier));
//    /// <summary>Subtracts a <see cref='Size'/> by another <see cref='Size'/>.</summary>
//    public static Size Subtract(Size a, Size b)
//      => new Size(unchecked(a.Width - b.Width), unchecked(a.Height - b.Height));
//    /// <summary>Subtracts a <see cref='Size'/> by a <see cref='in'/>.</summary>
//    public static Size Subtract(Size a, int b)
//      => new Size(unchecked(a.Width - b), unchecked(a.Height - b));
//    /// <summary>Subtracts a <see cref='int'/> by a <see cref='Size'/>.</summary>
//    public static Size Subtract(int a, Size b)
//      => new Size(unchecked(a - b.Width), unchecked(a - b.Height));
//    /// <summary>Creates a <see cref='Point2'/> from a <see cref='Size'/>.</summary>
//    public static Point ToPoint(Size size)
//      => new Point(size.Width, size.Height);
//    #endregion Statics

//    #region Operators
//    public static Size operator +(Size a, Size b)
//      => Add(a, b);
//    public static Size operator +(Size a, int b)
//      => Add(a, b);
//    public static Size operator +(int a, Size b)
//      => Add(b, a);
//    public static Size operator -(Size a, Size b)
//      => Subtract(a, b);
//    public static Size operator -(Size a, int b)
//      => Subtract(a, b);
//    public static Size operator -(int a, Size b)
//      => Subtract(a, b);
//    public static Size operator *(Size a, int b)
//      => Multiply(a, b);
//    public static Size operator *(int a, Size b)
//      => Multiply(b, a);
//    public static Size operator /(Size a, int b)
//      => Divide(a, b);
//    public static Size operator /(int a, Size b)
//      => Divide(a, b);

//    public static bool operator ==(Size a, Size b)
//      => a.Equals(b);
//    public static bool operator !=(Size a, Size b)
//      => !a.Equals(b);
//    #endregion Operators

//    // IEquatable
//    public bool Equals(Size other)
//      => m_width == other.m_width && m_height == other.m_height;

//    // Object (overrides)
//    public override bool Equals(object? obj)
//      => obj is Size o && Equals(o);
//    public override int GetHashCode()
//      => System.HashCode.Combine(m_width, m_height);
//    public override string? ToString()
//      => $"<Size {m_width}, {m_height}>";
//  }
//}
