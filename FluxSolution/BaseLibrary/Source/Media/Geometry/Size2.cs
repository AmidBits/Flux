//namespace Flux.Media.Geometry
//{
//  public struct Size2
//    : System.IEquatable<Size3>, System.IFormattable
//  {
//    public static readonly Size3 Empty;
//    public bool IsEmpty => Equals(Empty);

//    public float Width { get; set; }
//    public float Height { get; set; }
//    public float Depth { get; set; }

//    public Numerics.Vector3D Center()
//      => new Numerics.Vector3D(Width / 2, Height / 2, Depth / 2, 0);

//    // Operators
//    public static bool operator ==(Size3 a, Size3 b)
//      => a.Equals(b);
//    public static bool operator !=(Size3 a, Size3 b)
//      => !a.Equals(b);

//    // IEquatable
//    public bool Equals(Size3 other)
//      => Width == other.Width && Height == other.Height && Depth == other.Depth;

//    // IFormattable
//    public string ToString(string? format, System.IFormatProvider? provider)
//      => $"<{nameof(Size3)}: {Width}, {Height}, {Depth}>";

//    // Object (overrides)
//    public override bool Equals(object? obj)
//      => obj is Size3 o && Equals(o);
//    public override int GetHashCode()
//      => System.HashCode.Combine(Width, Height, Depth);
//    public override string? ToString()
//      => ToString(default, System.Globalization.CultureInfo.CurrentCulture);
//  }
//}
