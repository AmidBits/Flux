namespace Flux.Media.Geometry
{
  /// <summary>Rect3 is a rectangular cuboid. It is therefor a limited 3D cubiod.</summary>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
  public struct Rect3
    : System.IEquatable<Rect3>, System.IFormattable
  {
    public static readonly Rect3 Empty;
    public bool IsEmpty => Equals(Empty);

    [System.Runtime.InteropServices.FieldOffset(0)] public readonly int X;
    [System.Runtime.InteropServices.FieldOffset(4)] public readonly int Y;
    [System.Runtime.InteropServices.FieldOffset(8)] public readonly int Z;
    [System.Runtime.InteropServices.FieldOffset(12)] public readonly int Width;
    [System.Runtime.InteropServices.FieldOffset(16)] public readonly int Height;
    [System.Runtime.InteropServices.FieldOffset(20)] public readonly int Depth;

    public Rect3(int x, int y, int z, int width, int height, int depth)
    {
      X = x;
      Y = y;
      Z = z;
      Width = width;
      Height = height;
      Depth = depth;
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
