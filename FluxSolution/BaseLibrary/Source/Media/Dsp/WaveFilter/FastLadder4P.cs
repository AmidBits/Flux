namespace Flux.Media.Dsp.WaveFilter
{
	// https://github.com/vinniefalco/DSPFilters/blob/master/shared/DSPFilters/source/PoleFilter.cpp
	// http://musicdsp.org/files/Audio-EQ-Cookbook.txt

	// http://musicdsp.org/showArchiveComment.php?ArchiveID=240 // Karlsen 24 dB Ladder
	public class FastLadder4P
		: IWaveFilterMono, IWaveProcessorMono
	{
		private double m_resonance;
		/// <value>Typical audio range settings are between 0.1 to 10, but no restrictions are enforced.</value>
		public double Resonance { get => m_resonance; set => SetCoefficients(value, m_cutoffFrequency, m_sampleRate); }

		private double m_cutoffFrequency;
		/// <value>Typical audio range settings are between 20 to 20,000 Hz, but no restrictions are enforced.</value>
		public double CutoffFrequency { get => m_cutoffFrequency; set => SetCoefficients(m_resonance, value, m_sampleRate); }

		private double m_sampleRate;
		/// <summary>Sets the sample rate used for filter calculations.</summary>
		public double SampleRate { get => m_sampleRate; set => SetCoefficients(m_cutoffFrequency, m_resonance, value); }

		public FastLadder4P(double resonance, double cutoff, double sampleRate = 44100.0)
		{
			ClearState();

			SetCoefficients(resonance, cutoff, sampleRate);
		}

		private double buf1, buf2, buf3, buf4;

		public void ClearState()
			=> buf1 = buf2 = buf3 = buf4 = 0;

		private double m_normalizedCutoffFrequency;

		/// <summary>Abstract method which computes the needed coefficients for the filter in which it is implemented.</summary>
		/// <param name="cutoff">The filter cutoff frequency, in Hz.</param>
		/// <param name="resonance">Typical audio range settings are between 0.1 to 10, but no restrictions are enforced.</param>
		/// <param name="sampleRate">The sample rate in Hz, defaults to 44.1 kHz.</param>
		private void SetCoefficients(double resonance, double cutoff, double sampleRate)
		{
			m_resonance = resonance;
			m_cutoffFrequency = cutoff;
			m_sampleRate = sampleRate;

			m_normalizedCutoffFrequency = 2 * System.Math.PI * (m_cutoffFrequency / m_sampleRate);
		}

		public double FilterAudioMono(double value)
		{
			var rclp = buf4 > 1 ? 1 : buf4; // Clip resonance buffer, if needed.

			// rclp = (-rclp * _resonance) + value; // Asymmetrical clipping (original version by Ove Karlsen).
			rclp = value - (rclp * m_resonance); // Symmetrical clipping (by Peter Schoffhauzer).

			buf1 = (rclp - buf1) * m_normalizedCutoffFrequency + buf1;
			buf2 = (buf1 - buf2) * m_normalizedCutoffFrequency + buf2;
			buf3 = (buf2 - buf3) * m_normalizedCutoffFrequency + buf3;
			buf4 = (buf3 - buf4) * m_normalizedCutoffFrequency + buf4;

			return buf4;
		}

		public double ProcessAudio(double sample)
			=> FilterAudioMono(sample);
	}
}