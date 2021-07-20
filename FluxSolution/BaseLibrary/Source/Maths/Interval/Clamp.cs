namespace Flux
{
	public static partial class Maths
	{
		/// <summary>Returns the <paramref name="value"/> clamped, i.e. if out-of-bounds then clamp to the nearest boundary, within the closed interval [<paramref name="minimum"/>, <paramref name="maximum"/>].</summary>
		public static System.Numerics.BigInteger Clamp(System.Numerics.BigInteger value, System.Numerics.BigInteger minimum, System.Numerics.BigInteger maximum)
			=> value < minimum ? minimum : value > maximum ? maximum : value;
	}
}
