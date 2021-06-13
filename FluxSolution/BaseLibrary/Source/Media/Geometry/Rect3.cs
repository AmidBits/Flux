namespace Flux.Media.Geometry
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

    // Operators
    public static bool operator ==(Rect3 a, Rect3 b)
      => a.Equals(b);
    public static bool operator !=(Rect3 a, Rect3 b)
      => !a.Equals(b);

    // IEquatable
    public bool Equals(Rect3 other)
      => X == other.X && Y == other.Y && Z == other.Z && Width == other.Width && Height == other.Height && Depth == other.Depth;

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? provider)
      => $"<{nameof(Rect3)}: {X}, {Y}, {Z}, {Width}, {Height}, {Depth}>";

    // Object (overrides)
    public override bool Equals(object? obj)
      => obj is Rect3 o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(X, Y, Z, Width, Height, Depth);
    public override string? ToString()
      => ToString(default, System.Globalization.CultureInfo.CurrentCulture);
  }
}
