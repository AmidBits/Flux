namespace Flux.Media
{
	public class SampleRate
	{
		public double Value { get; set; }

		public SampleRate(double value)
			=> Value = value;
		public SampleRate()
			: this(44100)
		{
		}
	}
}
