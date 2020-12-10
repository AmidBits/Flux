namespace Flux
{
	namespace Geometry
	{
		[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
		public struct Size3
			: System.IEquatable<Size3>, System.IFormattable
		{
			public static readonly Size3 Empty;
			public bool IsEmpty => Equals(Empty);

			[System.Runtime.InteropServices.FieldOffset(0)] private int m_width;
			[System.Runtime.InteropServices.FieldOffset(4)] private int m_height;
			[System.Runtime.InteropServices.FieldOffset(8)] private int m_depth;

			public int Width { get => m_width; set => m_width = value; }
			public int Height { get => m_height; set => m_height = value; }
			public int Depth { get => m_depth; set => m_depth = value; }

			public Point3 Center()
				=> new Point3(m_width / 2, m_height / 2, m_depth / 2);

			public Size3(int width, int height, int depth)
			{
				m_width = width;
				m_height = height;
				m_depth = depth;
			}

			// Statics

			/// <summary>Adds a <see cref='Size3'/> by another <see cref='Size3'/>.</summary>
			public static Size3 Add(Size3 a, Size3 b)
				=> new Size3(unchecked(a.Width + b.Width), unchecked(a.Height + b.Height), unchecked(a.Depth + b.Depth));
			/// <summary>Adds a <see cref='Size3'/> by another <see cref='Size3'/>.</summary>
			public static Size3 Add(Size3 a, int b)
				=> new Size3(unchecked(a.Width + b), unchecked(a.Height + b), unchecked(a.Depth + b));
			/// <summary>Divides the components of the <see cref="Size3"/> by the corresponding components of another <see cref="Size3"/> producing two quotients as a new <see cref="Size3"/>.</summary>
			public static Size3 Divide(Size3 dividend, Size3 divisor)
				=> new Size3(unchecked(dividend.Width / divisor.Width), unchecked(dividend.Height / divisor.Height), unchecked(dividend.Depth / divisor.Depth));
			/// <summary>Divides the components of the <see cref="Size3"/> by a <see cref="int"/> producing two quotients as a new <see cref="Size3"/>.</summary>
			public static Size3 Divide(Size3 dividend, int divisor)
				=> new Size3(unchecked(dividend.Width / divisor), unchecked(dividend.Height / divisor), unchecked(dividend.Depth / divisor));
			/// <summary>Divides a <see cref="int"/> by the components of a <see cref="Size3"/> producing two quotients as a new <see cref="Size3"/>.</summary>
			public static Size3 Divide(int dividend, Size3 divisors)
				=> new Size3(unchecked(dividend / divisors.Width), unchecked(dividend / divisors.Height), unchecked(dividend / divisors.Depth));
			/// <summary>Multiplies <see cref="Size3"/> by an <see cref="int"/> producing <see cref="Size3"/>.</summary>
			public static Size3 Multiply(Size3 size, int multiplier)
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
			/// <summary>Creates a <see cref='Point3'/> from a <see cref='Size3'/>.</summary>
			public static Point3 ToPoint3(Size3 size)
				=> new Point3(size.Width, size.Height, size.Depth);

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
				=> m_width == other.m_width && m_height == other.m_height && m_depth == other.m_depth;

			// IFormattable
			public string ToString(string? format, System.IFormatProvider? provider)
				=> $"<{nameof(Size3)}: {m_width}, {m_height}, {m_depth}>";

			// Object (overrides)
			public override bool Equals(object? obj)
				=> obj is Size3 o && Equals(o);
			public override int GetHashCode()
				=> System.HashCode.Combine(m_width, m_height, m_depth);
			public override string? ToString()
				=> ToString(default, System.Globalization.CultureInfo.CurrentCulture);
		}
	}
}
