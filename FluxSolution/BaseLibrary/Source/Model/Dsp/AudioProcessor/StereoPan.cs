namespace Flux.Dsp.AudioProcessor
{
  public class StereoPan : IAudioProcessorStereo
  {
    private double m_position = 0;
    // The position of the pan across the stereo field in the range [-1, 1], where -1 is left and 1 is right.
    public double Position
    {
      get => m_position; set
      {
        m_position = Math.Clamp(value, -1, 1);
      }
    }

    public ISampleStereo ProcessAudio(ISampleStereo sample) => ApplyStereoPan(m_position, sample);
    //public StereoSample ProcessAudio2(StereoSample sample) => (_position > Flux.Math.EpsilonCpp32 && _position * 0.5 is var scaledR) ? new StereoSample(sample.FrontLeft * (1.0 - _position), sample.FrontLeft * scaledR + sample.FrontRight * (1.0 - scaledR)) : (_position < -Flux.Math.EpsilonCpp32 && _position * -0.5 is var scaledL) ? new StereoSample(sample.FrontLeft * (1.0 - scaledL) + sample.FrontRight * scaledL, sample.FrontRight * (1.0 + _position)) : sample;

    /// <summary>Apply stereo pan using the specified position to an arbitrary stereo signal sample</summary>
    /// <param name="position">The pan position of the stereo samples [-1, 1] across the stereo field, where negative is to the left, positive is to the right and 0 is the center.</param>
    /// <param name="left">The left stereo sample in the range [-1, 1].</param>
    ///// <param name="right">The right stereo sample in the range [-1, 1].</param>
    //public static (double left, double right) ApplyStereoPan(double position, double left, double right) => (position > Flux.Math.EpsilonCpp32 && position * 0.5 is var scaledR) ? (left * (1.0 - position), left * scaledR + right * (1.0 - scaledR)) : (position < -Flux.Math.EpsilonCpp32 && position * -0.5 is var scaledL) ? (left * (1.0 - scaledL) + right * scaledL, right * (1.0 + position)) : (left, right);
    public static ISampleStereo ApplyStereoPan(double position, ISampleStereo sample) => (position > Flux.Math.EpsilonCpp32 && position * 0.5 is var scaledR) ? new StereoSample(sample.FrontLeft * (1.0 - position), sample.FrontLeft * scaledR + sample.FrontRight * (1.0 - scaledR)) : (position < -Flux.Math.EpsilonCpp32 && position * -0.5 is var scaledL) ? new StereoSample(sample.FrontLeft * (1.0 - scaledL) + sample.FrontRight * scaledL, sample.FrontRight * (1.0 + position)) : sample;
  }
}
