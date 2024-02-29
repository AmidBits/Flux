namespace Flux
{
  namespace Dsp
  {
    /// <summary>Stereo (left and right) sample.</summary>
    public interface ISampleStereo<TSelf>
      where TSelf : System.Numerics.INumber<TSelf>
    {
      TSelf SampleLeft { get; }
      TSelf SampleRight { get; }
    }
  }
}
