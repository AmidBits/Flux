//namespace Flux
//{
//	public static partial class Maths
//	{
//		/// <summary>Max routine for 3 values of T (where T : System.IComparable<T>).</summary>
//		public static T Max<T>(T a, T b, T c)
//			where T : System.IComparable<T>
//			=> a.CompareTo(b) >= 0 ? (a.CompareTo(c) >= 0 ? a : c) : (b.CompareTo(c) >= 0 ? b : c);

//		/// <summary>Max routine for 3 values.</summary>
//		public static System.Numerics.BigInteger Max(System.Numerics.BigInteger a, System.Numerics.BigInteger b, System.Numerics.BigInteger c)
//			=> System.Numerics.BigInteger.Max(System.Numerics.BigInteger.Max(a, b), c);

//		/// <summary>Max routine for 3 values.</summary>
//		public static decimal Max(decimal a, decimal b, decimal c)
//			=> System.Math.Max(System.Math.Max(a, b), c);

//		/// <summary>Max routine for 3 values. Provided for consistent call site. Internally using System.Math.Max().</summary>
//		public static double Max(double a, double b, double c)
//			=> System.Math.Max(System.Math.Max(a, b), c);
//		/// <summary>Max routine for 3 values. Provided for consistent call site. Internally using System.Math.Max().</summary>
//		public static float Max(float a, float b, float c)
//			=> System.Math.Max(System.Math.Max(a, b), c);

//		/// <summary>Max routine for 3 values.</summary>
//		public static int Max(int a, int b, int c)
//			=> System.Math.Max(System.Math.Max(a, b), c);
//		/// <summary>Max routine for 3 values.</summary>
//		public static long Max(long a, long b, long c)
//			=> System.Math.Max(System.Math.Max(a, b), c);

//		/// <summary>Max routine for 3 values.</summary>
//		[System.CLSCompliant(false)]
//		public static uint Max(uint a, uint b, uint c)
//			=> System.Math.Max(System.Math.Max(a, b), c);
//		/// <summary>Max routine for 3 values.</summary>
//		[System.CLSCompliant(false)]
//		public static ulong Max(ulong a, ulong b, ulong c)
//			=> System.Math.Max(System.Math.Max(a, b), c);
//	}
//}
