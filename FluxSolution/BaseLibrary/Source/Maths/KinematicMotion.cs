namespace Flux
{
	public static partial class Maths
	{
		public static double MaxRange(double initialVelocityMPS, double initialAngleRAD, double gravitationalPull = EarthNullGravityMPS)
			=> System.Math.Sin(2 * initialAngleRAD) * initialVelocityMPS * initialVelocityMPS / gravitationalPull;
		public static double MaxHeight(double initialVelocityMPS, double initialAngleRAD, double gravitationalPull = EarthNullGravityMPS)
			=> System.Math.Pow(System.Math.Sin(initialAngleRAD), 2) * initialVelocityMPS * initialVelocityMPS / (2 * gravitationalPull);
	}
}
