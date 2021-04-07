namespace Flux.Dsp
{
  public interface IAudioChannelBl
  {
    double BackLeft { get; }
  }
  public interface IAudioChannelBr
  {
    double BackRight { get; }
  }

  public interface IAudioChannelFc
  {
    double FrontCenter { get; }
  }

  public interface IAudioChannelFl
  {
    double FrontLeft { get; }
  }
  public interface IAudioChannelFr
  {
    double FrontRight { get; }
  }

  public interface IAudioChannelLfe
  {
    double LowFrequency { get; }
  }
}
