namespace Flux.Media.Geometry
{
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
  public struct Rect2
    : System.IEquatable<Rect2>
  {
    public static readonly Rect2 Empty;
    public bool IsEmpty => Equals(Empty);

    [System.Runtime.InteropServices.FieldOffset(0)] private Point2 m_position;
    [System.Runtime.InteropServices.FieldOffset(8)] private Size2 m_size;

    public int Left { get => m_position.X; set => m_position.X = value; }
    public int X { get => m_position.X; set => m_position.X = value; }
    public int Top { get => m_position.Y; set => m_position.Y = value; }
    public int Y { get => m_position.Y; set => m_position.Y = value; }

    public int Width { get => m_size.Width; set => m_size.Width = value; }
    public int Height { get => m_size.Height; set => m_size.Height = value; }

    public int Right { get => m_position.X + m_size.Width; set => m_size.Width = value - m_position.X; }
    public int Bottom { get => m_position.Y + m_size.Height; set => m_size.Height = value - m_position.Y; }

    public Rect2(Point2 position, Size2 size)
    {
      m_position = position;
      m_size = size;
    }
    public Rect2(int left, int top, int width, int height)
    {
      m_position = new Point2(left, top);
      m_size = new Size2(width, height);
    }

    public Point2 Center
      => new Point2(Left + (Width / 2), Top + (Height / 2));

    public Size2 Size
      => new Size2(Width, Height);

    // Statics
    private static Rect2 Create(int left, int top, int right, int bottom)
      => left < right && top < bottom ? new Rect2(left, top, right - left, bottom - top) : Empty;
    /// <summary>Determines the Rectangle structure that represents the intersection of two rectangles. Empty if there is no intersection.</summary>
    public static Rect2 Intersect(Rect2 a, Rect2 b)
      => Create(System.Math.Max(a.Left, b.Left), System.Math.Max(a.Top, b.Top), System.Math.Min(a.Right, b.Right), System.Math.Min(a.Bottom, b.Bottom));
    /// <summary>Gets a Rectangle structure that contains the union of two Rectangle structures.</summary>
    public static Rect2 Union(Rect2 a, Rect2 b)
      => Create(System.Math.Min(a.Left, b.Left), System.Math.Min(a.Top, b.Top), System.Math.Max(a.Right, b.Right), System.Math.Max(a.Bottom, b.Bottom));
    /// <summary>Creates a <see cref='Size2'/> from a <see cref='Rect2'/>.</summary>
    public static Size2 ToSize2(Rect2 size)
      => new Size2(size.Width, size.Height);

    // Operators
    public static bool operator ==(Rect2 a, Rect2 b)
      => a.Equals(b);
    public static bool operator !=(Rect2 a, Rect2 b)
      => !a.Equals(b);

    // IEquatable
    public bool Equals(Rect2 other)
      => Left == other.Left && Top == other.Top && Width == other.Width && Height == other.Height;

    // Object (overrides)
    public override bool Equals(object? obj)
      => obj is Rect2 o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(Left, Top, Width, Height);
    public override string? ToString()
      => $"<Rect {Left}, {Top}, {Width}, {Height}>";
  }
}
