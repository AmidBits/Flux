namespace Flux
{
	public static partial class Maths
	{
		/// <summary>Returns a number that is clamped between a minimum and a maximum number.</summary>
		public static System.Numerics.BigInteger Clamp(System.Numerics.BigInteger value, System.Numerics.BigInteger minimum, System.Numerics.BigInteger maximum)
			=> value < minimum ? minimum : value > maximum ? maximum : value;
	}
}
