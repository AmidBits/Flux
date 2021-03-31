namespace Flux.Dsp
{
  public interface IChannelFl
  {
    double FrontLeft { get; }
  }
  public interface IChannelFr
  {
    double FrontRight { get; }
  }

  public interface IChannelFc
  {
    double FrontCenter { get; }
  }

  public interface IChannelLfe
  {
    double LowFrequency { get; }
  }

  public interface IChannelBl
  {
    double BackLeft { get; }
  }
  public interface IChannelBr
  {
    double BackRight { get; }
  }
}
