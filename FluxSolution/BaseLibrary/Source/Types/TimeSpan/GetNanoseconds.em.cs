namespace Flux
{
	public static partial class TimeSpanEm
	{
		/// <summary>Compute how many nanoseconds have elapsed. One tick is equal to 100 nanoseconds.</summary>
		public static double GetTotalNanoseconds(this System.TimeSpan source)
			=> source.Ticks * 100.0;
	}
}
