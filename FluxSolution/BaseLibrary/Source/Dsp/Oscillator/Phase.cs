using System.Linq;

namespace Flux.Dsp
{
	public class PhaseEngineUC
	{
		private double m_initialPhase;
		public double InitialPhase { get => m_initialPhase; set => m_initialPhase = value >= m_minimumPhase && value < m_maximumPhase ? value : throw new System.ArgumentOutOfRangeException(nameof(value)); }

		private double m_maximumPhase;
		public double MaximumPhase { get => m_maximumPhase; }

		private double m_minimumPhase;
		public double MinimumPhase { get => m_minimumPhase; }

		private readonly double m_normalizedFrequency;
		public double NormalizedFrequency { get => m_normalizedFrequency; }

		private double m_signalFrequency;
		public double SignalFrequency { get => m_signalFrequency; }

		private double m_sampleRate;
		public double SampleRate { get => m_sampleRate; }

		private double m_value;
		public double Value { get => m_value; }

		public PhaseEngineUC(double initialPhase, double minimumPhase, double maximumPhase, double signalFrequency, double sampleRate)
		{
			if (m_initialPhase < m_minimumPhase || m_initialPhase >= m_maximumPhase) throw new System.ArgumentOutOfRangeException(nameof(initialPhase));

			m_initialPhase = initialPhase;
			m_maximumPhase = maximumPhase;
			m_minimumPhase = minimumPhase;

			m_normalizedFrequency = signalFrequency / sampleRate;

			m_signalFrequency = signalFrequency;
			m_sampleRate = sampleRate;
		}
		public PhaseEngineUC(double signalFrequency, double sampleRate)
			: this(0, 0, Maths.PiX2, signalFrequency, sampleRate)
		{
		}

		public virtual void ResetPhase()
			=> m_value = m_initialPhase;

		public virtual void UpdatePhase(double normalizedFrequency)
			=> m_value = Maths.Wrap(m_value + normalizedFrequency, m_minimumPhase, m_maximumPhase);
		public virtual void UpdatePhase()
			=> UpdatePhase(m_normalizedFrequency);
	}

	public class PhaseEngine
	{
		private readonly double[] m_midiNotePhaseShift;

		private readonly double m_phaseLength;

		private readonly int m_sampleRate;

		public PhaseEngine(double phaseLength, int sampleRate)
		{
			m_phaseLength = phaseLength;

			m_sampleRate = sampleRate;

			m_midiNotePhaseShift = System.Linq.Enumerable.Range(0, 128).Select(note => m_phaseLength * Media.Midi.Note.ToFrequency((byte)note) / sampleRate).ToArray();
		}
		public PhaseEngine()
			: this(Maths.PiX2, 44100)
		{
		}

		public double UpdatePhase(double phasePosition, int midiNote)
			=> midiNote >= 0 && midiNote <= 127 ? (phasePosition + m_midiNotePhaseShift[midiNote]) % m_phaseLength : throw new System.ArgumentOutOfRangeException(nameof(midiNote));

		public double UpdatePhase(double phasePosition, double arbitraryFrequency)
			=> (phasePosition + (m_phaseLength * arbitraryFrequency / m_sampleRate)) % m_phaseLength;
	}

	public class Phase
	{
		private double m_frequencyModulation;
		/// <summary>The amount [0, 1] of output from the frequency modulator to apply.</summary>
		public double FrequencyModulation { get => m_frequencyModulation; set => m_frequencyModulation = System.Math.Clamp(value, 0.0, 1.0); }

		/// <summary>The frequency modulator (FM) for the oscillator.</summary>
		/// <see cref="https://en.wikipedia.org/wiki/Frequency_modulation"/>
		public IOscillator? FrequencyModulator { get; set; }

		public double NormalizedFrequency { get; set; }

		private double m_offset;
		public double Offset { get => m_offset; set => m_offset = Maths.Wrap(value, m_minimumPhase, m_maximumPhase); }

		private double m_phaseModulation;
		/// <summary>The amount [0, 1] of output from the phase modulator to apply.</summary>
		public double PhaseModulation { get => m_phaseModulation; set => m_phaseModulation = System.Math.Clamp(value, 0.0, 1.0); }

		/// <summary>The pulse modulator (PM) for the oscillator.</summary>
		/// <see cref="https://en.wikipedia.org/wiki/Phase_modulation"/>
		public Oscillator? PhaseModulator { get; set; }

		private double m_position;
		public double Position { get => m_position; set => m_position = Maths.Wrap(value, m_minimumPhase, m_maximumPhase); }

		private double m_maximumPhase;
		private double m_minimumPhase;

		/// <summary>Indicates whether the phase was reset, i.e. the cycle was completed.</summary>
		public bool WasReset { get; set; }

		/// <summary>Indicates whether the direction of the phase should be reversed.</summary>
		public bool Reverse { get; set; }

		public Phase(double normalizedFrequency)
		{
			NormalizedFrequency = normalizedFrequency;

			m_minimumPhase = 0;
			m_maximumPhase = 1;

			m_position = m_offset = 0.0;
		}
		public Phase() : this(Media.Frequency.Normalized(440.0, 44100.0)) { }

		/// <summary>Resets the phase position using the phase offset. Can be used to "sync" the oscillator.</summary>
		public void Reset(bool resetModulators)
		{
			m_position = m_offset;

			if (resetModulators)
			{
				(FrequencyModulator as Oscillator)?.Reset(resetModulators);
				(PhaseModulator as Oscillator)?.Reset(resetModulators);
			}
		}

		public void Update(double? normalizedFrequency)
		{
			if (!normalizedFrequency.HasValue)
			{
				normalizedFrequency = NormalizedFrequency;
			}

			var shift = normalizedFrequency.Value; // Normal phase shift for the current frequency.

			if (PhaseModulator != null && m_phaseModulation > Flux.Maths.EpsilonCpp32)
			{
				shift += 0.1 * PhaseModulator.Next(normalizedFrequency.Value) * m_phaseModulation;
			}

			if (FrequencyModulator != null && m_frequencyModulation > Flux.Maths.EpsilonCpp32)
			{
				shift += normalizedFrequency.Value * FrequencyModulator.NextSample() * m_frequencyModulation;
			}

			if (Reverse)
			{
				shift = -shift;
			}

			m_position = Maths.Wrap(m_position + shift, m_minimumPhase, m_maximumPhase);
		}
	}
}
