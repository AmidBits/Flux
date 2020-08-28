namespace Flux.Dsp.AudioProcessor
{
  public class LaggerMono : IAudioProcessorMono
  {
    private double m_amount;
    /// <summary>The amount of lag desired in the range [0, 1], where 0 is none and 1 is the most.</summary>
    public double Amount { get => m_amount; set => m_amount = Maths.Clamp(value, 0.0, 1.0); }

    private double m_previousSample = 0.0;

    public LaggerMono(double amount) => m_amount = amount;
    public LaggerMono() : this(0.75) { }

    public ISampleMono ProcessAudio(ISampleMono sample) => new MonoSample(m_amount > Maths.EpsilonCpp32 ? m_previousSample = Maths.InterpolateCosine(sample.FrontCenter, m_previousSample, m_amount) : sample.FrontCenter);
  }

  public class LaggerStereo : IAudioProcessorStereo
  {
    public LaggerMono Left { get; }
    public LaggerMono Right { get; }

    /// <summary>The amount of lag desired in the range [0, 1].</summary>
    public double Amount { get => Left.Amount; set => Right.Amount = Left.Amount = Maths.Clamp(value, 0.0, 1.0); }

    public LaggerStereo(double amountL, double amountR)
    {
      Left = new LaggerMono(amountL);
      Right = new LaggerMono(amountR);
    }
    public LaggerStereo(double amount) : this(amount, amount) { }
    public LaggerStereo() : this(0.75) { }

    public ISampleStereo ProcessAudio(ISampleStereo sample) => new StereoSample(Left.ProcessAudio(new MonoSample(sample.FrontLeft)).FrontCenter, Right.ProcessAudio(new MonoSample(sample.FrontRight)).FrontCenter);
  }
}
