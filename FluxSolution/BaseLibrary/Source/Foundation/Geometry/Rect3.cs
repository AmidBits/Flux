namespace Flux.Geometry
{
  /// <summary>Rect3 is a rectangular cuboid. It is therefor a limited 3D cubiod.</summary>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
  public struct Rect3
    : System.IEquatable<Rect3>, System.IFormattable
  {
    public static readonly Rect3 Empty;
    public bool IsEmpty => Equals(Empty);

    [System.Runtime.InteropServices.FieldOffset(0)] private Point3 m_position;
    [System.Runtime.InteropServices.FieldOffset(8)] private Size3 m_size;

    public int Left { get => m_position.X; set => m_position.X = value; }
    public int X { get => m_position.X; set => m_position.X = value; }
    public int Top { get => m_position.Y; set => m_position.Y = value; }
    public int Y { get => m_position.Y; set => m_position.Y = value; }
    public int Near { get => m_position.Z; set => m_position.Z = value; }
    public int Z { get => m_position.Z; set => m_position.Z = value; }

    public int Width { get => m_size.Width; set => m_size.Width = value; }
    public int Height { get => m_size.Height; set => m_size.Height = value; }
    public int Depth { get => m_size.Depth; set => m_size.Depth = value; }

    public int Right { get => m_position.X + m_size.Width; set => m_size.Width = value - m_position.X; }
    public int Bottom { get => m_position.Y + m_size.Height; set => m_size.Height = value - m_position.Y; }
    public int Far { get => m_position.Z + m_size.Depth; set => m_size.Depth = value - m_position.Z; }

    public Rect3(Point3 position, Size3 size)
    {
      m_position = position;
      m_size = size;
    }
    public Rect3(int x, int y, int z, int width, int height, int depth)
    {
      m_position = new Point3(x, y, z);
      m_size = new Size3(width, height, depth);
    }

    public System.Numerics.Vector3 Center()
      => new System.Numerics.Vector3(X + Width / 2, Y + Height / 2, Z + Depth / 2);

    public Size3 Size
      => new Size3(Width, Height, Depth);

    #region Static methods
    private static Rect3 Create(int left, int top, int near, int right, int bottom, int far)
      => left < right && top < bottom ? new Rect3(left, top, near, right - left, bottom - top, far - near) : Empty;
    /// <summary>Determines the Rectangle structure that represents the intersection of two rectangles. Empty if there is no intersection.</summary>
    public static Rect3 Intersect(Rect3 a, Rect3 b)
      => Create(System.Math.Max(a.Left, b.Left), System.Math.Max(a.Top, b.Top), System.Math.Max(a.Near, b.Near), System.Math.Min(a.Right, b.Right), System.Math.Min(a.Bottom, b.Bottom), System.Math.Min(a.Far, b.Far));
    /// <summary>Gets a Rectangle structure that contains the union of two Rectangle structures.</summary>
    public static Rect3 Union(Rect3 a, Rect3 b)
      => Create(System.Math.Min(a.Left, b.Left), System.Math.Min(a.Top, b.Top), System.Math.Min(a.Near, b.Near), System.Math.Max(a.Right, b.Right), System.Math.Max(a.Bottom, b.Bottom), System.Math.Max(a.Far, b.Far));
    /// <summary>Creates a <see cref='Size3'/> from a <see cref='Rect3'/>.</summary>
    public static Size3 ToSize3(Rect3 size)
      => new Size3(size.Width, size.Height, size.Depth);
    #endregion Static methods

    #region Overloaded operators
    public static bool operator ==(Rect3 a, Rect3 b)
      => a.Equals(b);
    public static bool operator !=(Rect3 a, Rect3 b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    public bool Equals(Rect3 other)
      => X == other.X && Y == other.Y && Z == other.Z && Width == other.Width && Height == other.Height && Depth == other.Depth;

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? provider)
      => $"<{nameof(Rect3)}: {X}, {Y}, {Z}, {Width}, {Height}, {Depth}>";
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Rect3 o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(X, Y, Z, Width, Height, Depth);
    public override string? ToString()
      => ToString(default, System.Globalization.CultureInfo.CurrentCulture);
    #endregion Object overrides
  }
}
