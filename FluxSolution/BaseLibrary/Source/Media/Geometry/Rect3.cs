namespace Flux.Media.Geometry
{
  /// <summary>Rect3 is a rectangular cuboid. It is therefor a limited 3D cubiod.</summary>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
  public struct Rect3
    : System.IEquatable<Rect3>, System.IFormattable
  {
    public static readonly Rect3 Empty;
    public bool IsEmpty => Equals(Empty);

    [System.Runtime.InteropServices.FieldOffset(0)] private int m_x;
    [System.Runtime.InteropServices.FieldOffset(4)] private int m_y;
    [System.Runtime.InteropServices.FieldOffset(8)] private int m_z;
    [System.Runtime.InteropServices.FieldOffset(12)] private int m_width;
    [System.Runtime.InteropServices.FieldOffset(16)] private int m_height;
    [System.Runtime.InteropServices.FieldOffset(20)] private int m_depth;

    public Rect3(int x, int y, int z, int width, int height, int depth)
    {
      m_x = x;
      m_y = y;
      m_z = z;
      m_width = width;
      m_height = height;
      m_depth = depth;
    }

    public int X { get => m_x; set => m_x = value; }
    public int Y { get => m_y; set => m_y = value; }
    public int Z { get => m_z; set => m_z = value; }
    public int Width { get => m_width; set => m_width = value; }
    public int Height { get => m_height; set => m_height = value; }
    public int Depth { get => m_depth; set => m_depth = value; }

    public System.Numerics.Vector3 Center()
      => new System.Numerics.Vector3(X + Width / 2, Y + Height / 2, Z + Depth / 2);

    public Point3 Location
    {
      get => new Point3(X, Y, Z);
      set
      {
        X = value.X;
        Y = value.Y;
        Z = value.Z;
      }
    }

    public Size3 Size
    {
      get => new Size3(Width, Height, Depth);
      set
      {
        Width = value.Width;
        Height = value.Height;
        Depth = value.Depth;
      }
    }

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
