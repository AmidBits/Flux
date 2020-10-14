namespace Flux
{
  namespace Geometry
  {
    public struct Size3
    : System.IEquatable<Size3>, System.IFormattable
    {
      public static readonly Size3 Empty;
      public bool IsEmpty => Equals(Empty);

      public int Width { get; set; }
      public int Height { get; set; }
      public int Depth { get; set; }

      public Point3 Center()
        => new Point3(Width / 2, Height / 2, Depth / 2);

      public Size3(int width, int height, int depth)
      {
        Width = width;
        Height = height;
        Depth = depth;
      }

      // Statics

      /// <summary>Adds a <see cref='Size3'/> by another <see cref='Size3'/>.</summary>
      public static Size3 Add(Size3 a, Size3 b)
        => new Size3(unchecked(a.Width + b.Width), unchecked(a.Height + b.Height), unchecked(a.Depth + b.Depth));
      /// <summary>Adds a <see cref='Size3'/> by another <see cref='Size3'/>.</summary>
      public static Size3 Add(Size3 a, int b)
        => new Size3(unchecked(a.Width + b), unchecked(a.Height + b), unchecked(a.Depth + b));
      /// <summary>Divides the components of the <see cref="Size3"/> by the corresponding components of another <see cref="Size3"/> producing two quotients as a new <see cref="Size3"/>.</summary>
      private static Size3 Divide(Size3 dividend, Size3 divisor)
        => new Size3(unchecked(dividend.Width / divisor.Width), unchecked(dividend.Height / divisor.Height), unchecked(dividend.Depth / divisor.Depth));
      /// <summary>Divides the components of the <see cref="Size3"/> by a <see cref="int"/> producing two quotients as a new <see cref="Size3"/>.</summary>
      private static Size3 Divide(Size3 dividend, int divisor)
        => new Size3(unchecked(dividend.Width / divisor), unchecked(dividend.Height / divisor), unchecked(dividend.Depth / divisor));
      /// <summary>Divides a <see cref="int"/> by the components of a <see cref="Size3"/> producing two quotients as a new <see cref="Size3"/>.</summary>
      private static Size3 Divide(int dividend, Size3 divisors)
        => new Size3(unchecked(dividend / divisors.Width), unchecked(dividend / divisors.Height), unchecked(dividend / divisors.Depth));
      /// <summary>Multiplies <see cref="Size3"/> by an <see cref="int"/> producing <see cref="Size3"/>.</summary>
      private static Size3 Multiply(Size3 size, int multiplier)
        => new Size3(unchecked(size.Width * multiplier), unchecked(size.Height * multiplier), unchecked(size.Depth * multiplier));
      /// <summary>Subtracts a <see cref='Size3'/> by another <see cref='Size3'/>.</summary>
      public static Size3 Subtract(Size3 a, Size3 b)
        => new Size3(unchecked(a.Width - b.Width), unchecked(a.Height - b.Height), unchecked(a.Depth - b.Depth));
      /// <summary>Subtracts a <see cref='Size3'/> by a <see cref='in'/>.</summary>
      public static Size3 Subtract(Size3 a, int b)
        => new Size3(unchecked(a.Width - b), unchecked(a.Height - b), unchecked(a.Depth - b));
      /// <summary>Subtracts a <see cref='int'/> by a <see cref='Size3'/>.</summary>
      public static Size3 Subtract(int a, Size3 b)
        => new Size3(unchecked(a - b.Width), unchecked(a - b.Height), unchecked(a - b.Depth));

      // Operators

      public static Size3 operator +(Size3 a, Size3 b)
        => Add(a, b);
      public static Size3 operator +(Size3 a, int b)
        => Add(a, b);
      public static Size3 operator +(int a, Size3 b)
        => Add(b, a);
      public static Size3 operator -(Size3 a, Size3 b)
        => Subtract(a, b);
      public static Size3 operator -(Size3 a, int b)
        => Subtract(a, b);
      public static Size3 operator -(int a, Size3 b)
        => Subtract(a, b);
      public static Size3 operator *(Size3 a, int b)
        => Multiply(a, b);
      public static Size3 operator *(int a, Size3 b)
        => Multiply(b, a);
      public static Size3 operator /(Size3 a, int b)
        => Divide(a, b);
      public static Size3 operator /(int a, Size3 b)
        => Divide(a, b);

      public static bool operator ==(Size3 a, Size3 b)
        => a.Equals(b);
      public static bool operator !=(Size3 a, Size3 b)
        => !a.Equals(b);

      // IEquatable
      public bool Equals(Size3 other)
        => Width == other.Width && Height == other.Height && Depth == other.Depth;

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? provider)
        => $"<{nameof(Size3)}: {Width}, {Height}, {Depth}>";

      // Object (overrides)
      public override bool Equals(object? obj)
        => obj is Size3 o && Equals(o);
      public override int GetHashCode()
        => System.HashCode.Combine(Width, Height, Depth);
      public override string? ToString()
        => ToString(default, System.Globalization.CultureInfo.CurrentCulture);
    }
  }
}
