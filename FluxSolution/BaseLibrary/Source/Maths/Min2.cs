namespace Flux
{
	public static partial class Maths
	{
		/// <summary>Min routine for 2 values of T (where T : System.IComparable<T>).</summary>
		public static T Min<T>(T a, T b)
			where T : System.IComparable<T>
			=> a.CompareTo(b) <= 0 ? a : b;

		/// <summary>Min routine for 2 values. Provided for consistent call site.</summary>
		public static System.Numerics.BigInteger Min(System.Numerics.BigInteger a, System.Numerics.BigInteger b)
			=> (a < b) ? a : b;
		/// <summary>Min routine for 2 values. Provided for consistent call site.</summary>
		public static decimal Min(decimal a, decimal b)
			=> (a < b) ? a : b;
		/// <summary>Min routine for 2 values. Provided for consistent call site. Internally using System.Math.Min().</summary>
		public static double Min(double a, double b)
			=> System.Math.Min(a, b);
		/// <summary>Min routine for 2 values. Provided for consistent call site. Internally using System.Math.Min().</summary>
		public static float Min(float a, float b)
			=> System.Math.Min(a, b);
		/// <summary>Min routine for 2 values. Provided for consistent call site.</summary>
		public static int Min(int a, int b)
			=> (a < b) ? a : b;
		/// <summary>Min routine for 2 values. Provided for consistent call site.</summary>
		public static long Min(long a, long b)
			=> (a < b) ? a : b;
		/// <summary>Min routine for 2 values. Provided for consistent call site.</summary>
		[System.CLSCompliant(false)]
		public static uint Min(uint a, uint b)
			=> (a < b) ? a : b;
		/// <summary>Min routine for 2 values. Provided for consistent call site.</summary>
		[System.CLSCompliant(false)]
		public static ulong Min(ulong a, ulong b)
			=> (a < b) ? a : b;
	}
}
