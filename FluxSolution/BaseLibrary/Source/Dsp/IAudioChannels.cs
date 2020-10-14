namespace Flux.Dsp
{
  public interface IChannelBl
  {
    double BackLeft { get; }
  }
  public interface IChannelBr
  {
    double BackRight { get; }
  }
  public interface IChannelFc
  {
    double FrontCenter { get; }
  }
  public interface IChannelFl
  {
    double FrontLeft { get; }
  }
  public interface IChannelFr
  {
    double FrontRight { get; }
  }
}
