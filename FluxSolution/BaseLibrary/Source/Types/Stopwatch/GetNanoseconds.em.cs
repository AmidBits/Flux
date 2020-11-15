namespace Flux
{
	public static partial class Xtensions
	{
		/// <summary>Compute how many nanoseconds has elapsed.</summary>
		public static double GetTotalNanoseconds(this System.Diagnostics.Stopwatch source)
			=> (double)(source ?? throw new System.ArgumentNullException(nameof(source))).ElapsedTicks / (double)System.Diagnostics.Stopwatch.Frequency * 1000000000.0;
	}
}
