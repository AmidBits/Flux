namespace Flux.Dsp
{
  public interface IMonoWaveFilterable
  {
    static IMonoWaveFilterable Empty => new EmptyMonoWaveFilter();

    /// <summary>Apply some audio filter on the specified wave sample.</summary>
    /// <param name="sample">A sample in the [-1.0, 1.0] range.</param>
    /// <returns>The filtered audio sample in the [-1.0, 1.0] range.</returns>
    double FilterMonoWave(double sample);

    IWaveMono<double> FilterMonoWave(IWaveMono<double> sample);

    private sealed class EmptyMonoWaveFilter
      : IMonoWaveFilterable
    {
      public double FilterMonoWave(double sample) => throw new System.NotImplementedException(nameof(EmptyMonoWaveFilter));

      public IWaveMono<double> FilterMonoWave(IWaveMono<double> sample) => throw new System.NotImplementedException(nameof(EmptyMonoWaveFilter));
    }
  }
}

//namespace Flux.Dsp.AudioFilter
//{
//public class Karlsen3
//{
//  double b_fres; // b_fres = resonance amount. 0..4 typical "to selfoscillation", 0.6 covers a more saturated range.

//  double aflt1, aflt2, aflt3, aflt4;

//  public double Process(double value)
//  {
//    //        // for nice low sat, or sharper type low deemphasis saturation, one can use a onepole shelf before the filter.
//    //        b_lf = b_lf + ((-b_lf + b_v) * b_lfcut); // b_lfcut 0..1
//    //        double b_lfhp = b_v - b_lf;
//    //        b_v = b_lf + (b_lf1hp * ((b_lfgain*0.5)+1));   

//    double b_rez = aflt4 - value; // no attenuation with rez, makes a stabler filter.
//    value = value - (b_rez * b_fres); // b_fres = resonance amount. 0..4 typical "to selfoscillation", 0.6 covers a more saturated range.

//    double b_vnc = value; // clip, and adding back some nonclipped, to get a dynamic like analog.
//    if (value > 1) { value = 1; } else if (value < -1) { value = -1; }
//    value = b_vnc + ((-b_vnc + value) * 0.9840);

//    aflt1 = aflt1 + ((-aflt1 + value) * b_fenv); // straightforward 4 pole filter, (4 normalized feedback paths in series)
//    aflt2 = aflt2 + ((-aflt2 + aflt1) * b_fenv);
//    aflt3 = aflt3 + ((-aflt3 + aflt2) * b_fenv);
//    aflt4 = aflt4 + ((-aflt4 + aflt3) * b_fenv);

//    value = aflt4;
//  }
//}

//public class Lpf4P
//{
//  double low, high, band, notch;

//  public double Process(double input, double cutoff, double qvalue, double x, double y, double sampleRate=44100.0)
//  {
//    //sr: samplerate;
//    //cutoff: 20 - 20k;
//    //qvalue: 0 - 100;
//    //x, y: 0 - 1
//    var slider1 = 1;
//    var q = System.Math.Sqrt(1 - System.Math.Atan(System.Math.Sqrt(qvalue)) * 2 / System.Math.PI);
//    var scale = System.Math.Sqrt(q);
//    var f = slider1 / sampleRate * 2; // * 2 here instead of 4

//    //----------sample loop

//    //set 'input' here

//    //os x2
//    for (var i = 0; i < 2; i++)
//    {
//      low = low + f * band;
//      high = scale * input - low - q * band;
//      band = f * high + band;
//      notch = high + low;
//    }

//    //  x,y pad scheme
//    //     
//    //  high -- notch
//    //  |           |
//    //  |           |
//    //  low ---- band

//    var pair1 = low * y + high * (1 - y);
//    var pair2 = band * y + notch * (1 - y);

//    return pair2 * x + pair1 * (1 - x);
//  }

//}

/// <summary>A discussion of topics related to audio DSP, with an emphasis on useful and practical information.</summary>
/// <see cref="http://www.earlevel.com"/>
//public static class EarLevel
//{
//	/// <summary></summary>
//	/// <see cref="http://www.earlevel.com/main/2012/11/26/biquad-c-source-code/"/>
//	public abstract class BiQuad : IFilter
//	{
//		protected double a0, a1, a2, b1, b2;
//		protected double z1, z2;

//		private double _frequency;
//		/// <value>Typical audio range settings are between 20 to 20,000 Hz, but no restrictions are enforced.</value>
//		public double Frequency { get => _frequency; set => SetBiQuad(value, _Q, _gain); }

//		private double _gain;
//		/// <value>Typical audio range settings are between -30 to 30 dB, but no restrictions are enforced.</value>
//		public double PeakGain { get => _gain; set => SetBiQuad(_frequency, _Q, value); }

//		private double _Q;
//		/// <value>Typical audio range settings are between 0.1 to 10, but no restrictions are enforced.</value>
//		/// <seealso cref="http://www.earlevel.com/main/2016/09/29/cascading-filters/"/>
//		public double Q { get => _Q; set => SetBiQuad(_frequency, value, _gain); }

//		private double _sampleRate;
//		/// <summary>Sets the sample rate used for filter calculations.</summary>
//		public double SampleRate { get => _sampleRate; set => SetBiQuad(_frequency, _Q, _gain, value); }

//		public void SetBiQuad(double frequency, double Q, double gain, double sampleRate = 44100.0)
//		{
//			_frequency = frequency;
//			_Q = Q;
//			_gain = gain;
//			_sampleRate = sampleRate;

//			CalculateCoefficients(_frequency / _sampleRate, _Q, _gain);
//		}

//		/// <summary>Abstract method which computes the needed coefficients for the filter in which it is implemented.</summary>
//		/// <param name="Fc">Uses normalized (to sample rate) frequency, where 1.0 is the sample rate, i.e. divide the frequency you want to set (in Hz) by your sample rate to get normalized frequency (2438.0/44100 for 2438 Hz at a sample rate of 44100 Hz).</param>
//		/// <param name="Q"></param>
//		/// <param name="peakGain">Peak gain in dB, where negative numbers are for cut, and positive numbers for boost.</param>
//		protected abstract void CalculateCoefficients(double Fc, double Q, double peakGain);

//		public double Process(double value)
//		{
//			double o = value * a0 + z1;

//			z1 = value * a1 + z2 - b1 * o;
//			z2 = value * a2 - b2 * o;

//			return o;
//		}

//		/// <summary>A low-pass filter is used to cut unwanted high-frequency signals.</summary>
//		/// <see href="https://en.wikipedia.org/wiki/Low-pass_filter"/>
//		public class LowPass : BiQuad
//		{
//			protected override void CalculateCoefficients(double Fc, double Q, double peakGain)
//			{
//				var K = System.Math.Tan(System.Math.PI * Fc);

//				var norm = 1 / (1 + K / Q + K * K);

//				a0 = K * K * norm;
//				a1 = 2 * a0;
//				a2 = a0;
//				b1 = 2 * (K * K - 1) * norm;
//				b2 = (1 - K / Q + K * K) * norm;
//			}
//		}

//		/// <summary>A high-pass filter passes high frequencies fairly well; it is helpful as a filter to cut any unwanted low-frequency components.</summary>
//		/// <see href="https://en.wikipedia.org/wiki/High-pass_filter"/>
//		public class HighPass : BiQuad
//		{
//			protected override void CalculateCoefficients(double Fc, double Q, double peakGain)
//			{
//				var K = System.Math.Tan(System.Math.PI * Fc);

//				var norm = 1 / (1 + K / Q + K * K);

//				a0 = 1 * norm;
//				a1 = -2 * a0;
//				a2 = a0;
//				b1 = 2 * (K * K - 1) * norm;
//				b2 = (1 - K / Q + K * K) * norm;
//			}
//		}

//		/// <summary>A band-pass filter passes a limited range of frequencies.</summary>
//		/// <see href="https://en.wikipedia.org/wiki/Band-pass_filter"/>
//		public class BandPass : BiQuad
//		{
//			protected override void CalculateCoefficients(double Fc, double Q, double peakGain)
//			{
//				var K = System.Math.Tan(System.Math.PI * Fc);

//				var norm = 1 / (1 + K / Q + K * K);

//				a0 = K / Q * norm;
//				a1 = 0;
//				a2 = -a0;
//				b1 = 2 * (K * K - 1) * norm;
//				b2 = (1 - K / Q + K * K) * norm;
//			}
//		}

//		/// <summary>A band-stop filter passes frequencies above and below a certain range. A very narrow band-stop filter is known as a notch filter.</summary>
//		/// <see href="https://en.wikipedia.org/wiki/Band-stop_filter"/>
//		public class Notch : BiQuad
//		{
//			protected override void CalculateCoefficients(double Fc, double Q, double peakGain)
//			{
//				var K = System.Math.Tan(System.Math.PI * Fc);

//				var norm = 1 / (1 + K / Q + K * K);

//				a0 = (1 + K * K) * norm;
//				a1 = 2 * (K * K - 1) * norm;
//				a2 = a0;
//				b1 = a1;
//				b2 = (1 - K / Q + K * K) * norm;
//			}
//		}

//		/// <summary>A peak filter makes a peak or a dip in the frequency response, commonly used in parametric equalizers.</summary>
//		/// <see href="https://en.wikipedia.org/wiki/Band-pass_filter"/>
//		public class Peak : BiQuad
//		{
//			protected override void CalculateCoefficients(double Fc, double Q, double peakGain)
//			{
//				var V = System.Math.Pow(10, System.Math.Abs(peakGain) / 20.0);
//				var K = System.Math.Tan(System.Math.PI * Fc);

//				if(peakGain >= 0) // boost
//				{
//					var norm = 1 / (1 + 1 / Q * K + K * K);

//					a0 = (1 + V / Q * K + K * K) * norm;
//					a1 = 2 * (K * K - 1) * norm;
//					a2 = (1 - V / Q * K + K * K) * norm;
//					b1 = a1;
//					b2 = (1 - 1 / Q * K + K * K) * norm;
//				}
//				else // cut
//				{
//					var norm = 1 / (1 + V / Q * K + K * K);

//					a0 = (1 + 1 / Q * K + K * K) * norm;
//					a1 = 2 * (K * K - 1) * norm;
//					a2 = (1 - 1 / Q * K + K * K) * norm;
//					b1 = a1;
//					b2 = (1 - V / Q * K + K * K) * norm;
//				}
//			}
//		}

//		/// <summary>A low-shelf filter passes all frequencies, but increases or reduces frequencies below the shelf frequency by specified amount.</summary>
//		public class LowShelf : BiQuad
//		{
//			protected override void CalculateCoefficients(double Fc, double Q, double peakGain)
//			{
//				var V = System.Math.Pow(10, System.Math.Abs(peakGain) / 20.0);
//				var K = System.Math.Tan(System.Math.PI * Fc);

//				var Sqrt2 = System.Math.Sqrt(2);
//				var Sqrt2V = System.Math.Sqrt(2 * V);

//				if(peakGain >= 0) // boost
//				{
//					var norm = 1 / (1 + Sqrt2 * K + K * K);

//					a0 = (1 + Sqrt2V * K + V * K * K) * norm;
//					a1 = 2 * (V * K * K - 1) * norm;
//					a2 = (1 - Sqrt2V * K + V * K * K) * norm;
//					b1 = 2 * (K * K - 1) * norm;
//					b2 = (1 - Sqrt2 * K + K * K) * norm;
//				}
//				else // cut
//				{
//					var norm = 1 / (1 + Sqrt2V * K + V * K * K);

//					a0 = (1 + Sqrt2 * K + K * K) * norm;
//					a1 = 2 * (K * K - 1) * norm;
//					a2 = (1 - Sqrt2 * K + K * K) * norm;
//					b1 = 2 * (V * K * K - 1) * norm;
//					b2 = (1 - Sqrt2V * K + V * K * K) * norm;
//				}
//			}
//		}

//		/// <summary>A high-shelf filter passes all frequencies, but increases or reduces frequencies above the shelf frequency by specified amount.</summary>
//		public class HighShelf : BiQuad
//		{
//			protected override void CalculateCoefficients(double Fc, double Q, double peakGain)
//			{
//				var V = System.Math.Pow(10, System.Math.Abs(peakGain) / 20.0);
//				var K = System.Math.Tan(System.Math.PI * Fc);

//				var Sqrt2 = System.Math.Sqrt(2);
//				var Sqrt2V = System.Math.Sqrt(2 * V);

//				if(peakGain >= 0) // boost
//				{
//					var norm = 1 / (1 + Sqrt2 * K + K * K);

//					a0 = (V + Sqrt2V * K + K * K) * norm;
//					a1 = 2 * (K * K - V) * norm;
//					a2 = (V - Sqrt2V * K + K * K) * norm;
//					b1 = 2 * (K * K - 1) * norm;
//					b2 = (1 - Sqrt2 * K + K * K) * norm;
//				}
//				else // cut
//				{
//					var norm = 1 / (V + Sqrt2V * K + K * K);

//					a0 = (1 + Sqrt2 * K + K * K) * norm;
//					a1 = 2 * (K * K - 1) * norm;
//					a2 = (1 - Sqrt2 * K + K * K) * norm;
//					b1 = 2 * (K * K - V) * norm;
//					b2 = (V - Sqrt2V * K + K * K) * norm;
//				}
//			}
//		}
//	}

//	public class HighPass1P : IFilter
//	{
//		protected double a0, b1, z1;

//		private double _cutoffFrequency = 5000.0;
//		/// <summary>The cutoff frequency is a boundary at which point the frequencies begins to be reduced (attenuated or reflected) rather than passing through.</summary>
//		public double CutoffFrequency { get => _cutoffFrequency; set => Set(value, _sampleRate); }

//		private double _sampleRate = 44100.0;
//		/// <summary>Sets the sample rate used for filter calculations.</summary>
//		public double SampleRate { get => _sampleRate; set => Set(_cutoffFrequency, value); }

//		public void Set(double cutoffFrequency, double sampleRate)
//		{
//			_cutoffFrequency = cutoffFrequency;
//			_sampleRate = sampleRate;

//			b1 = -System.Math.Exp(-2.0 * System.Math.PI * (0.5 - (cutoffFrequency / sampleRate)));

//			a0 = 1.0 - b1;
//		}

//		public double Process(double sample) => (z1 = sample * a0 + z1 * b1);
//	}

//	public class LowPass1P : IFilter
//	{
//		protected double a0, b1, z1;

//		private double _cutoffFrequency = 70.0;
//		/// <summary>Sets the cutoff frequency for the filter.</summary>
//		public double CutoffFrequency { get => _cutoffFrequency; set => Set(value, _sampleRate); }

//		private double _sampleRate = 44100.0;
//		/// <summary>Sets the sample rate used for filter calculations.</summary>
//		public double SampleRate { get => _sampleRate; set => Set(_cutoffFrequency, value); }

//		public void Set(double cutoffFrequency, double sampleRate = 44100.0)
//		{
//			_cutoffFrequency = cutoffFrequency;
//			_sampleRate = sampleRate;

//			b1 = System.Math.Exp(-2.0 * System.Math.PI * (cutoffFrequency / sampleRate));

//			a0 = 1.0 - b1;
//		}

//		public double Process(double sample) => (z1 = sample * a0 + z1 * b1);
//	}
//}

//public static class LinearTrapezoidalIntegrated
//{
//	/// <summary>Sallen Key Filter (SKF) collection</summary>
//	/// <see href="https://cytomic.com/index.php?q=technical-papers"/>
//	/// <seealso cref="https://cytomic.com/files/dsp/SkfLinearTrapOptimised2.pdf"/>
//	public abstract class SallenKeyFilter : IFilter
//	{
//		#region Variables
//		protected double ic1eq, ic2eq;

//		protected double g, k, a0, a1, a2, a3, a4, a5;

//		protected double v0, v1, v2;

//		private double _frequency;
//		/// <value>Typical audio range settings are between 20 and 20,000 Hz, but no restrictions are enforced.</value>
//		public double Frequency { get => _frequency; set => Set(value, _resonance, _gain); }

//		private double _gain;
//		/// <value>Typical audio range settings are between -30 to 30 dB, but no restrictions are enforced.</value>
//		public double PeakGain { get => _gain; set => Set(_frequency, _resonance, value); }

//		private double _resonance;
//		/// <value>Typical audio range settings are between 0.1 to 10, but no restrictions are enforced.</value>
//		public double Resonance { get => _resonance; set => Set(_frequency, value, _gain); }

//		private double _sampleRate;
//		/// <summary>Sets the sample rate in Hz, used for filter calculations.</summary>
//		public double SampleRate { get => _sampleRate; set => Set(_frequency, _resonance, _gain, value); }
//		#endregion

//		public void Set(double frequency, double resonance, double gain, double sampleRate = 44100.0)
//		{
//			_frequency = frequency;
//			_resonance = resonance;
//			_gain = gain;
//			_sampleRate = sampleRate;

//			CalculateCoefficients(_frequency / _sampleRate, _resonance, _gain);
//		}

//		/// <summary>Abstract method which computes the needed coefficients for the filter in which it is implemented.</summary>
//		/// <param name="normlizedHz">Uses normalized (to sample rate) frequency, where 1.0 is the sample rate, i.e. divide the frequency you want to set (in Hz) by your sample rate to get normalized frequency (2438.0/44100 for 2438 Hz at a sample rate of 44100 Hz).</param>
//		/// <param name="Q"></param>
//		/// <param name="dB">Peak gain in dB, where negative numbers are for cut, and positive numbers for boost.</param>
//		protected abstract void CalculateCoefficients(double normlizedHz, double resonance, double dB);

//		public abstract double Process(double value);

//		/// <summary>A low-pass filter is used to cut unwanted high-frequency signals.</summary>
//		/// <see href="https://en.wikipedia.org/wiki/Low-pass_filter"/>
//		public class LowPassV1 : SallenKeyFilter
//		{
//			protected override void CalculateCoefficients(double normlizedHz, double resonance, double dB)
//			{
//				var g = System.Math.Tan(System.Math.PI * normlizedHz);
//				var k = 2 * resonance;
//				a0 = 1 / (System.Math.Pow(1 + g, 2) - g * k);
//				a1 = k * a0;
//				a2 = (1 + g) * a0;
//				a3 = g * a2;
//				a4 = g * a0;
//				a5 = g * a4;
//			}

//			public override double Process(double value)
//			{
//				v1 = a1 * ic2eq + a2 * ic1eq + a3 * v0;
//				v2 = a2 * ic2eq + a4 * ic1eq + a5 * v0;
//				ic1eq = 2 * (v1 - k * v2) - ic1eq;
//				ic2eq = 2 * v2 - ic2eq;

//				return v2;
//			}
//		}

//		/// <summary>A low-pass filter is used to cut unwanted high-frequency signals.</summary>
//		/// <see href="https://en.wikipedia.org/wiki/Low-pass_filter"/>
//		public class LowPassV2 : SallenKeyFilter
//		{
//			protected override void CalculateCoefficients(double normlizedHz, double resonance, double dB)
//			{
//				var g = System.Math.Tan(System.Math.PI * normlizedHz);
//				var k = 2.0 * resonance;
//				a0 = 1.0 / (System.Math.Pow(1 + g, 2.0) - g * k);
//				a1 = k * a0;
//				a2 = (1.0 + g) * a0;
//				a3 = g * a2;
//				a4 = 1.0 / (1.0 + g);
//				a5 = g * a4;
//			}

//			public override double Process(double value)
//			{
//				v1 = a1 * ic2eq + a2 * ic1eq + a3 * v0;
//				v2 = a4 * ic2eq + a5 * v1;
//				ic1eq = 2 * (v1 - k * v2) - ic1eq;
//				ic2eq = 2 * v2 - ic2eq;

//				return v2;
//			}
//		}
//	}

//	/// <summary>State Variable Filter (SVF) collection</summary>
//	/// <see href="https://cytomic.com/index.php?q=technical-papers"/>
//	/// <seealso cref="https://cytomic.com/files/dsp/SvfLinearTrapOptimised2.pdf"/>
//	public abstract class StateVariableFilter : IFilter
//	{
//		#region Variables
//		protected double ic1eq, ic2eq;

//		protected double g, k, a1, a2, a3, m0, m1, m2;

//		protected double v0, v1, v2, v3;

//		private double _frequency;
//		/// <value>Typical audio range settings are between 20 to 20,000 Hz, but no restrictions are enforced.</value>
//		public double Frequency { get => _frequency; set => Set(value, _Q, _gain); }

//		private double _gain;
//		/// <value>Typical audio range settings are between -30 to 30 dB, but no restrictions are enforced.</value>
//		public double PeakGain { get => _gain; set => Set(_frequency, _Q, value); }

//		private double _Q;
//		/// <value>Typical audio range settings are between 0.1 to 10, but no restrictions are enforced.</value>
//		public double Q { get => _Q; set => Set(_frequency, value, _gain); }

//		private double _sampleRate;
//		/// <summary>Sets the sample rate used for filter calculations.</summary>
//		public double SampleRate { get => _sampleRate; set => Set(_frequency, _Q, _gain, value); }
//		#endregion

//		public void Set(double frequency, double Q, double gain, double sampleRate = 44100.0)
//		{
//			_frequency = frequency;
//			_Q = Q;
//			_gain = gain;
//			_sampleRate = sampleRate;

//			CalculateCoefficients(_frequency / _sampleRate, _Q, _gain);
//		}

//		/// <summary>Abstract method which computes the needed coefficients for the filter in which it is implemented.</summary>
//		/// <param name="normlizedHz">Uses normalized (to sample rate) frequency, where 1.0 is the sample rate, i.e. divide the frequency you want to set (in Hz) by your sample rate to get normalized frequency (2438.0/44100 for 2438 Hz at a sample rate of 44100 Hz).</param>
//		/// <param name="Q"></param>
//		/// <param name="dB">Peak gain in dB, where negative numbers are for cut, and positive numbers for boost.</param>
//		protected abstract void CalculateCoefficients(double normlizedHz, double Q, double dB);

//		public double Process(double value)
//		{
//			v3 = v0 - ic2eq;
//			v1 = a1 * ic1eq + a2 * v3;
//			v2 = ic2eq + a2 * ic1eq + a3 * v3;
//			ic1eq = 2 * v1 - ic1eq;
//			ic2eq = 2 * v2 - ic2eq;

//			return m0 * v0 + m1 * v1 + m2 * v2;
//		}

//		/// <summary>A low-pass filter is used to cut unwanted high-frequency signals.</summary>
//		/// <see href="https://en.wikipedia.org/wiki/Low-pass_filter"/>
//		public class LowPass : StateVariableFilter
//		{

//			protected override void CalculateCoefficients(double normlizedHz, double Q, double dB)
//			{
//				var g = System.Math.Tan(System.Math.PI * normlizedHz);
//				var k = 1 / Q;
//				a1 = 1 / (1 + g * (g + k));
//				a2 = g * a1;
//				a3 = g * a2;
//				m0 = 0;
//				m1 = 0;
//				m2 = 1;
//			}
//		}

//		/// <summary>A band-pass filter passes a limited range of frequencies.</summary>
//		/// <see href="https://en.wikipedia.org/wiki/Band-pass_filter"/>
//		public class BandPass : StateVariableFilter
//		{
//			protected override void CalculateCoefficients(double normlizedHz, double Q, double dB)
//			{
//				var g = System.Math.Tan(System.Math.PI * normlizedHz);
//				var k = 1 / Q;
//				a1 = 1 / (1 + g * (g + k));
//				a2 = g * a1;
//				a3 = g * a2;
//				m0 = 0;
//				m1 = 1;
//				m2 = 0;
//			}
//		}

//		/// <summary>A high-pass filter passes high frequencies fairly well; it is helpful as a filter to cut any unwanted low-frequency components.</summary>
//		/// <see href="https://en.wikipedia.org/wiki/High-pass_filter"/>
//		public class HighPass : StateVariableFilter
//		{
//			protected override void CalculateCoefficients(double normlizedHz, double Q, double dB)
//			{
//				var g = System.Math.Tan(System.Math.PI * normlizedHz);
//				var k = 1 / Q;
//				a1 = 1 / (1 + g * (g + k));
//				a2 = g * a1;
//				a3 = g * a2;
//				m0 = 1;
//				m1 = -k;
//				m2 = -1;
//			}
//		}

//		/// <summary>A band-stop filter passes frequencies above and below a certain range. A very narrow band-stop filter is known as a notch filter.</summary>
//		/// <see href="https://en.wikipedia.org/wiki/Band-stop_filter"/>
//		public class Notch : StateVariableFilter
//		{
//			protected override void CalculateCoefficients(double normlizedHz, double Q, double dB)
//			{
//				var g = System.Math.Tan(System.Math.PI * normlizedHz);
//				var k = 1 / Q;
//				a1 = 1 / (1 + g * (g + k));
//				a2 = g * a1;
//				a3 = g * a2;
//				m0 = 1;
//				m1 = -k;
//				m2 = 0;
//			}
//		}

//		/// <summary>A peak filter makes a peak or a dip in the frequency response, commonly used in parametric equalizers.</summary>
//		/// <see href="https://en.wikipedia.org/wiki/Band-pass_filter"/>
//		public class Peak : StateVariableFilter
//		{
//			protected override void CalculateCoefficients(double normlizedHz, double Q, double dB)
//			{
//				var g = System.Math.Tan(System.Math.PI * normlizedHz);
//				var k = 1 / Q;
//				a1 = 1 / (1 + g * (g + k));
//				a2 = g * a1;
//				a3 = g * a2;
//				m0 = 1;
//				m1 = -k;
//				m2 = -2;
//			}
//		}

//		/// <summary>An all-pass filter is a signal processing filter that passes all frequencies equally in gain, but changes the phase relationship among various frequencies.</summary>
//		/// <see href="https://en.wikipedia.org/wiki/All-pass_filter"/>
//		public class AllPass : StateVariableFilter
//		{
//			protected override void CalculateCoefficients(double normlizedHz, double Q, double dB)
//			{
//				var g = System.Math.Tan(System.Math.PI * normlizedHz);
//				var k = 1 / Q;
//				a1 = 1 / (1 + g * (g + k));
//				a2 = g * a1;
//				a3 = g * a2;
//				m0 = 1;
//				m1 = -2 * k;
//				m2 = 0;
//			}
//		}

//		public class Bell : StateVariableFilter
//		{
//			protected override void CalculateCoefficients(double normlizedHz, double Q, double dB)
//			{
//				var A = System.Math.Pow(10, dB / 40);
//				var g = System.Math.Tan(System.Math.PI * normlizedHz) * System.Math.Sqrt(A);
//				var k = 1 / (Q * A);
//				a1 = 1 / (1 + g * (g + k));
//				a2 = g * a1;
//				a3 = g * a2;
//				m0 = 1;
//				m1 = k * (A * A - 1);
//				m2 = 0;
//			}
//		}

//		/// <summary>A low-shelf filter passes all frequencies, but increases or reduces frequencies below the shelf frequency by specified amount.</summary>
//		public class LowShelf : StateVariableFilter
//		{
//			protected override void CalculateCoefficients(double normlizedHz, double Q, double dB)
//			{
//				var A = System.Math.Pow(10, dB / 40);
//				var g = System.Math.Tan(System.Math.PI * normlizedHz) * System.Math.Sqrt(A);
//				var k = 1 / Q;
//				a1 = 1 / (1 + g * (g + k));
//				a2 = g * a1;
//				a3 = g * a2;
//				m0 = 1;
//				m1 = k * (A - 1);
//				m2 = (A * A - 1);
//			}
//		}

//		/// <summary>A high-shelf filter passes all frequencies, but increases or reduces frequencies above the shelf frequency by specified amount.</summary>
//		public class HighShelf : StateVariableFilter
//		{
//			protected override void CalculateCoefficients(double normlizedHz, double Q, double dB)
//			{
//				var A = System.Math.Pow(10, dB / 40);
//				var g = System.Math.Tan(System.Math.PI * normlizedHz) * System.Math.Sqrt(A);
//				var k = 1 / Q;
//				a1 = 1 / (1 + g * (g + k));
//				a2 = g * a1;
//				a3 = g * a2;
//				m0 = A * A;
//				m1 = k * (1 - A) * A;
//				m2 = (1 - A * A);
//			}
//		}
//	}
//}
//}
