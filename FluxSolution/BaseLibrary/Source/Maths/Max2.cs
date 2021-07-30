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
	}
}
