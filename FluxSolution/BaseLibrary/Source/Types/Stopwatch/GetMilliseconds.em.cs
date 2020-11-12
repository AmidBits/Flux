namespace Flux
{
	public static partial class Xtensions
	{
		/// <summary>Compute how many milliseconds has elapsed.</summary>
		public static double GetTotalMilliseconds(this System.Diagnostics.Stopwatch source)
			=> (double)(source ?? throw new System.ArgumentNullException(nameof(source))).ElapsedTicks / (double)System.Diagnostics.Stopwatch.Frequency * 1000.0;
	}
}
