namespace Flux
{
	public static partial class StopwatchEm
	{
		/// <summary>Compute how many nanoseconds has elapsed.</summary>
		public static double GetElapsedNanoseconds(this System.Diagnostics.Stopwatch source)
			=> (source ?? throw new System.ArgumentNullException(nameof(source))).ElapsedTicks / (double)System.Diagnostics.Stopwatch.Frequency * 1000000000.0;
	}
}
