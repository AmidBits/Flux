namespace Flux
{
	public static partial class Maths
	{
		/// <summary>Max routine for 2 values of T (where T : System.IComparable<T>).</summary>
		public static T Max<T>(T a, T b)
			where T : System.IComparable<T>
			=> a.CompareTo(b) >= 0 ? a : b;

		/// <summary>Max routine for 2 values. Provided for consistent call site.</summary>
		public static System.Numerics.BigInteger Max(System.Numerics.BigInteger a, System.Numerics.BigInteger b)
			=> (a > b) ? a : b;
		/// <summary>Max routine for 2 values. Provided for consistent call site.</summary>
		public static decimal Max(decimal a, decimal b)
			=> (a > b) ? a : b;
		/// <summary>Max routine for 2 values. Provided for consistent call site. Internally using System.Math.Max().</summary>
		public static double Max(double a, double b)
			=> System.Math.Max(a, b);
		/// <summary>Max routine for 2 values. Provided for consistent call site. Internally using System.Math.Max().</summary>
		public static float Max(float a, float b)
			=> System.Math.Max(a, b);
		/// <summary>Max routine for 2 values. Provided for consistent call site.</summary>
		public static int Max(int a, int b)
			=> (a > b) ? a : b;
		/// <summary>Max routine for 2 values. Provided for consistent call site.</summary>
		public static long Max(long a, long b)
			=> (a > b) ? a : b;
		/// <summary>Max routine for 2 values. Provided for consistent call site.</summary>
		[System.CLSCompliant(false)]
		public static uint Max(uint a, uint b)
			=> (a > b) ? a : b;
		/// <summary>Max routine for 2 values. Provided for consistent call site.</summary>
		[System.CLSCompliant(false)]
		public static ulong Max(ulong a, ulong b)
			=> (a > b) ? a : b;

		/// <summary>Max routine for 3 values of T (where T : System.IComparable<T>).</summary>
		public static T Max<T>(T a, T b, T c)
			where T : System.IComparable<T>
			=> a.CompareTo(b) >= 0 ? (a.CompareTo(c) >= 0 ? a : c) : (b.CompareTo(c) >= 0 ? b : c);

		/// <summary>Max routine for 3 values.</summary>
		public static System.Numerics.BigInteger Max(System.Numerics.BigInteger a, System.Numerics.BigInteger b, System.Numerics.BigInteger c)
			=> (a > b) ? (a > c ? a : c) : (b > c ? b : c);
		/// <summary>Max routine for 3 values.</summary>
		public static decimal Max(decimal a, decimal b, decimal c)
			=> (a > b) ? (a > c ? a : c) : (b > c ? b : c);
		/// <summary>Max routine for 3 values. Provided for consistent call site. Internally using System.Math.Max().</summary>
		public static double Max(double a, double b, double c)
			=> System.Math.Max(System.Math.Max(a, b), c);
		/// <summary>Max routine for 3 values. Provided for consistent call site. Internally using System.Math.Max().</summary>
		public static float Max(float a, float b, float c)
			=> System.Math.Max(System.Math.Max(a, b), c);
		/// <summary>Max routine for 3 values.</summary>
		public static int Max(int a, int b, int c)
			=> (a > b) ? (a > c ? a : c) : (b > c ? b : c);
		/// <summary>Max routine for 3 values.</summary>
		public static long Max(long a, long b, long c)
			=> (a > b) ? (a > c ? a : c) : (b > c ? b : c);
		/// <summary>Max routine for 3 values.</summary>
		[System.CLSCompliant(false)]
		public static uint Max(uint a, uint b, uint c)
			=> (a > b) ? (a > c ? a : c) : (b > c ? b : c);
		/// <summary>Max routine for 3 values.</summary>
		[System.CLSCompliant(false)]
		public static ulong Max(ulong a, ulong b, ulong c)
			=> (a > b) ? (a > c ? a : c) : (b > c ? b : c);

		/// <summary>Max routine for 4 values of T (where T : System.IComparable<T>).</summary>
		public static T Max<T>(T a, T b, T c, T d)
			where T : System.IComparable<T>
			=> a.CompareTo(b) >= 0 ? (a.CompareTo(c) >= 0 ? (a.CompareTo(d) >= 0 ? a : d) : (c.CompareTo(d) >= 0 ? c : d)) : (b.CompareTo(c) >= 0 ? (b.CompareTo(d) >= 0 ? b : d) : (c.CompareTo(d) >= 0 ? c : d));

		/// <summary>Max routine for 4 values.</summary>
		public static System.Numerics.BigInteger Max(System.Numerics.BigInteger a, System.Numerics.BigInteger b, System.Numerics.BigInteger c, System.Numerics.BigInteger d)
			=> (a > b) ? (a > c ? (a > d ? a : d) : (c > d ? c : d)) : (b > c ? (b > d ? b : d) : (c > d ? c : d));
		/// <summary>Max routine for 4 values.</summary>
		public static decimal Max(decimal a, decimal b, decimal c, decimal d)
			=> (a > b) ? (a > c ? (a > d ? a : d) : (c > d ? c : d)) : (b > c ? (b > d ? b : d) : (c > d ? c : d));
		/// <summary>Max routine for 4 values. Provided for consistent call site. Internally using System.Math.Max().</summary>
		public static double Max(double a, double b, double c, double d)
			=> System.Math.Max(System.Math.Max(a, b), System.Math.Max(c, d));
		/// <summary>Max routine for 4 values. Provided for consistent call site. Internally using System.Math.Max().</summary>
		public static float Max(float a, float b, float c, float d)
			=> System.Math.Max(System.Math.Max(a, b), System.Math.Max(c, d));
		/// <summary>Max routine for 4 values.</summary>
		public static int Max(int a, int b, int c, int d)
			=> (a > b) ? (a > c ? (a > d ? a : d) : (c > d ? c : d)) : (b > c ? (b > d ? b : d) : (c > d ? c : d));
		/// <summary>Max routine for 4 values.</summary>
		public static long Max(long a, long b, long c, long d)
			=> (a > b) ? (a > c ? (a > d ? a : d) : (c > d ? c : d)) : (b > c ? (b > d ? b : d) : (c > d ? c : d));
		/// <summary>Max routine for 4 values.</summary>
		[System.CLSCompliant(false)]
		public static uint Max(uint a, uint b, uint c, uint d)
			=> (a > b) ? (a > c ? (a > d ? a : d) : (c > d ? c : d)) : (b > c ? (b > d ? b : d) : (c > d ? c : d));
		/// <summary>Max routine for 4 values.</summary>
		[System.CLSCompliant(false)]
		public static ulong Max(ulong a, ulong b, ulong c, ulong d)
			=> (a > b) ? (a > c ? (a > d ? a : d) : (c > d ? c : d)) : (b > c ? (b > d ? b : d) : (c > d ? c : d));
	}
}
