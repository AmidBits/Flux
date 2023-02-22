namespace Flux
{
	public static partial class StopwatchEm
	{
		/// <summary>Compute how many milliseconds has elapsed.</summary>
		public static double GetElapsedMilliseconds(this System.Diagnostics.Stopwatch source)
			=> (source ?? throw new System.ArgumentNullException(nameof(source))).ElapsedTicks / (double)System.Diagnostics.Stopwatch.Frequency * 1000.0;
	}
}
