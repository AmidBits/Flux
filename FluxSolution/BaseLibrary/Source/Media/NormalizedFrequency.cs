namespace Flux.Media
{
	/// <summary>Normalized frequency is a unit of measurement of frequency equivalent to cycles/sample.</summary>
	public class NormalizedFrequency
	{
		public Frequency Frequency { get; set; }

		public SampleRate SampleRate { get; set; }

		public double Value
			=> Frequency.Value / SampleRate.Value;

		public NormalizedFrequency(Frequency frequency, SampleRate sampleRate)
		{
			Frequency = frequency;
			SampleRate = sampleRate;
		}
	}
}
