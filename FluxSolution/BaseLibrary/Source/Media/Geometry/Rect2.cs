namespace Flux.Media.Geometry
{
	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
	public struct Rect2
		: System.IEquatable<Rect2>
	{
		public static readonly Rect2 Empty;
		public bool IsEmpty => Equals(Empty);

		[System.Runtime.InteropServices.FieldOffset(0)] private readonly int m_left;
		[System.Runtime.InteropServices.FieldOffset(4)] private readonly int m_top;
		[System.Runtime.InteropServices.FieldOffset(8)] private readonly int m_width;
		[System.Runtime.InteropServices.FieldOffset(12)] private readonly int m_height;

		public Rect2(int left, int top, int width, int height)
		{
			m_left = left;
			m_top = top;
			m_width = width;
			m_height = height;
		}

		public int Left
			=> m_left;
		public int Top
			=> m_top;
		public int Width
			=> m_width;
		public int Height
			=> m_height;

		public int Right
			=> Left + Width;
		public int Bottom
			=> Top + Height;

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
