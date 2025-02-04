namespace Flux
{
  namespace Dsp
  {
    /// <summary>A mono wave, range [-1.0, +1.0].</summary>
    public readonly record struct SampleMono<TSelf>
      : ISampleMono<TSelf>
      where TSelf : System.Numerics.INumber<TSelf>
    {
      public readonly static ISampleMono<TSelf> Silence = new SampleMono<TSelf>();

      private readonly TSelf m_sample;

      public SampleMono(TSelf value) => m_sample = value;

      public TSelf Sample { get => m_sample; init => m_sample = value; }

      /// <summary>Convert a mono sample into a stereo sample.</summary>
      public Dsp.SampleStereo<TSelf> ToStereoSample() => new(m_sample, m_sample);

      #region Static methods

      /// <summary>Convert a mono sample into a set of stereo samples.</summary>
      public static (double sampleL, double sampleR) MonoToStereo(double sampleM)
        => (sampleM, sampleM);

      #endregion // Static methods

      #region Overloaded operators

      public static explicit operator TSelf(SampleMono<TSelf> mono) => mono.Sample;
      public static explicit operator SampleMono<TSelf>(TSelf mono) => new(mono);

      #endregion // Overloaded operators
    }
  }
}