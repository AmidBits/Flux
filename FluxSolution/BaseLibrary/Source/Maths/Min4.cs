namespace Flux
{
	public static partial class Maths
	{
		/// <summary>Min routine for 4 values of T (where T : System.IComparable<T>).</summary>
		public static T Min<T>(T a, T b, T c, T d)
			where T : System.IComparable<T>
			=> a.CompareTo(b) <= 0 ? (a.CompareTo(c) <= 0 ? (a.CompareTo(d) <= 0 ? a : d) : (c.CompareTo(d) <= 0 ? c : d)) : (b.CompareTo(c) <= 0 ? (b.CompareTo(d) <= 0 ? b : d) : (c.CompareTo(d) <= 0 ? c : d));

		/// <summary>Min routine for 4 values.</summary>
		public static System.Numerics.BigInteger Min(System.Numerics.BigInteger a, System.Numerics.BigInteger b, System.Numerics.BigInteger c, System.Numerics.BigInteger d)
			=> System.Numerics.BigInteger.Min(System.Numerics.BigInteger.Min(a, b), System.Numerics.BigInteger.Min(c, d));

		/// <summary>Min routine for 4 values.</summary>
		public static decimal Min(decimal a, decimal b, decimal c, decimal d)
			=> System.Math.Min(System.Math.Min(a, b), System.Math.Min(c, d));

		/// <summary>Min routine for 4 values. Provided for consistent call site. Internally using System.Math.Min().</summary>
		public static double Min(double a, double b, double c, double d)
			=> System.Math.Min(System.Math.Min(a, b), System.Math.Min(c, d));
		/// <summary>Min routine for 4 values. Provided for consistent call site. Internally using System.Math.Min().</summary>
		public static float Min(float a, float b, float c, float d)
			=> System.Math.Min(System.Math.Min(a, b), System.Math.Min(c, d));

		/// <summary>Min routine for 4 values.</summary>
		public static int Min(int a, int b, int c, int d)
			=> System.Math.Min(System.Math.Min(a, b), System.Math.Min(c, d));
		/// <summary>Min routine for 4 values.</summary>
		public static long Min(long a, long b, long c, long d)
			=> System.Math.Min(System.Math.Min(a, b), System.Math.Min(c, d));

		/// <summary>Min routine for 4 values.</summary>
		[System.CLSCompliant(false)]
		public static uint Min(uint a, uint b, uint c, uint d)
			=> System.Math.Min(System.Math.Min(a, b), System.Math.Min(c, d));
		/// <summary>Min routine for 4 values.</summary>
		[System.CLSCompliant(false)]
		public static ulong Min(ulong a, ulong b, ulong c, ulong d)
			=> System.Math.Min(System.Math.Min(a, b), System.Math.Min(c, d));
	}
}
