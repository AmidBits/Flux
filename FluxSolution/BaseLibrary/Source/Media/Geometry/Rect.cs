using System.Dynamic;

namespace Flux
{
  namespace Geometry
  {
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
    public struct Rect
      : System.IEquatable<Rect>
    {
      public static readonly Rect Empty;
      public bool IsEmpty => Equals(Empty);

      [System.Runtime.InteropServices.FieldOffset(0)] private int m_left;
      [System.Runtime.InteropServices.FieldOffset(4)] private int m_top;
      [System.Runtime.InteropServices.FieldOffset(8)] private int m_right;
      [System.Runtime.InteropServices.FieldOffset(0)] private int m_bottom;

      public int Left { get => m_left; set => m_left = value; }
      public int Top { get => m_top; set => m_top = value; }
      public int Right { get => m_right; set => m_right = value; }
      public int Bottom { get => m_bottom; set => m_bottom = value; }

      public int X { get => m_left; set => m_left = value; }
      public int Y { get => m_top; set => m_top = value; }

      public Point2 Location
      {
        get => new Point2(m_left, m_top);
        set
        {
          m_left = value.X;
          m_top = value.Y;
        }
      }

      public int Width { get => m_right - m_left; set => m_right = m_left + value; }
      public int Height { get => m_bottom - m_top; set => m_bottom = m_top + value; }

      public Size2 Size
      {
        get => new Size2(Width, Height);
        set
        {
          Width = value.Width;
          Height = value.Height;
        }
      }

      public Point2 Center()
        => new Point2(Left + (Width / 2), Top + (Height / 2));

      public Rect(int left, int top, int width, int height)
      {
        m_left = left;
        m_top = top;
        m_right = left + width;
        m_bottom = top + height;
      }

      // Statics
      private static Rect Create(int left, int top, int right, int bottom)
        => left < right && top < bottom ? new Rect() { m_left = left, m_top = top, m_right = right, m_bottom = bottom } : Empty;
      /// <summary>Determines the Rectangle structure that represents the intersection of two rectangles. Empty if there is no intersection.</summary>
      public static Rect Intersect(Rect a, Rect b)
        => Create(System.Math.Max(a.m_left, b.m_left), System.Math.Max(a.m_top, b.m_top), System.Math.Min(a.m_right, b.m_right), System.Math.Min(a.m_bottom, b.m_bottom));
      /// <summary>Gets a Rectangle structure that contains the union of two Rectangle structures.</summary>
      public static Rect Union(Rect a, Rect b)
        => Create(System.Math.Min(a.m_left, b.m_left), System.Math.Min(a.m_top, b.m_top), System.Math.Max(a.m_right, b.m_right), System.Math.Max(a.m_bottom, b.m_bottom));
      /// <summary>Creates a <see cref='Size2'/> from a <see cref='Rect'/>.</summary>
      public static Size2 ToSize2(Rect size)
        => new Size2(size.Width, size.Height);

      // Operators
      public static bool operator ==(Rect a, Rect b)
        => a.Equals(b);
      public static bool operator !=(Rect a, Rect b)
        => !a.Equals(b);

      // IEquatable
      public bool Equals(Rect other)
        => m_left == other.m_left && m_top == other.m_top && m_right == other.m_right && m_bottom == other.m_bottom;

      // Object (overrides)
      public override bool Equals(object? obj)
        => obj is Rect o && Equals(o);
      public override int GetHashCode()
        => System.HashCode.Combine(m_left, m_top, m_right, m_bottom);
      public override string? ToString()
        => $"<Rect {m_left}, {m_top}, {m_right}, {m_bottom}>";
    }
  }
}
