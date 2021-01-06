namespace Flux
{
	public static partial class SystemTimeSpanEm
	{
		/// <summary>Compute how many microseconds have elapsed. There are ten ticks to a microsecond.</summary>
		public static double GetTotalMicroseconds(this System.TimeSpan source)
			=> source.Ticks / 10.0;
	}
}
