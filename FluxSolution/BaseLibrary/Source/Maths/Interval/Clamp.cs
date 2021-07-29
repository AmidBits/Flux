namespace Flux
{
	public static partial class Maths
	{
		/// <summary>Returns the <paramref name="value"/> clamped, i.e. if out-of-bounds then clamp to the nearest boundary, within the closed interval [<paramref name="min"/>, <paramref name="max"/>].</summary>
		public static System.Numerics.BigInteger Clamp(System.Numerics.BigInteger value, System.Numerics.BigInteger min, System.Numerics.BigInteger max)
			=> value < min ? min : value > max ? max : value;
	}
}
