//namespace Flux
//{
//	public static partial class Maths
//	{
//		/// <summary>Min routine for 3 values of T (where T : System.IComparable<T>).</summary>
//		public static T Min<T>(T a, T b, T c)
//			where T : System.IComparable<T>
//			=> a.CompareTo(b) <= 0 ? (a.CompareTo(c) <= 0 ? a : c) : (b.CompareTo(c) <= 0 ? b : c);

//		/// <summary>Min routine for 3 values.</summary>
//		public static System.Numerics.BigInteger Min(System.Numerics.BigInteger a, System.Numerics.BigInteger b, System.Numerics.BigInteger c)
//			=> System.Numerics.BigInteger.Min(System.Numerics.BigInteger.Min(a, b), c);

//		/// <summary>Min routine for 3 values.</summary>
//		public static decimal Min(decimal a, decimal b, decimal c)
//			=> System.Math.Min(System.Math.Min(a, b), c);

//		/// <summary>Min routine for 3 values. Provided for consistent call site. Internally using System.Math.Min().</summary>
//		public static double Min(double a, double b, double c)
//			=> System.Math.Min(System.Math.Min(a, b), c);
//		/// <summary>Min routine for 3 values. Provided for consistent call site. Internally using System.Math.Min().</summary>
//		public static float Min(float a, float b, float c)
//			=> System.Math.Min(System.Math.Min(a, b), c);

//		/// <summary>Min routine for 3 values.</summary>
//		public static int Min(int a, int b, int c)
//			=> System.Math.Min(System.Math.Min(a, b), c);
//		/// <summary>Min routine for 3 values.</summary>
//		public static long Min(long a, long b, long c)
//			=> System.Math.Min(System.Math.Min(a, b), c);

//		/// <summary>Min routine for 3 values.</summary>
//		[System.CLSCompliant(false)]
//		public static uint Min(uint a, uint b, uint c)
//			=> System.Math.Min(System.Math.Min(a, b), c);
//		/// <summary>Min routine for 3 values.</summary>
//		[System.CLSCompliant(false)]
//		public static ulong Min(ulong a, ulong b, ulong c)
//			=> System.Math.Min(System.Math.Min(a, b), c);
//	}
//}
