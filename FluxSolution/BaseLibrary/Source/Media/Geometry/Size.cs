namespace Flux.Media.Geometry
{
  public struct Size
    : System.IEquatable<Size>, System.IFormattable
  {
    public float Width { get; set; }
    public float Height { get; set; }
    public float Depth { get; set; }

    public Numerics.Vector3D Center()
      => new Numerics.Vector3D(Width / 2, Height / 2, Depth / 2, 0);

    // Operators
    public static bool operator ==(Size a, Size b)
      => a.Equals(b);
    public static bool operator !=(Size a, Size b)
      => !a.Equals(b);
    // IEquatable
    public bool Equals(Size other)
      => Width == other.Width && Height == other.Height && Depth == other.Depth;
    // IFormattable
    public string ToString(string? format, System.IFormatProvider? provider)
      => $"<{nameof(Size)} {Width}, {Height}, {Depth}>";
    // Object (overrides)
    public override bool Equals(object? obj)
      => obj is Size s && Equals(s);
    public override int GetHashCode()
      => Flux.HashCode.CombineCore(0);
    public override string? ToString()
      => base.ToString();
  }
}
