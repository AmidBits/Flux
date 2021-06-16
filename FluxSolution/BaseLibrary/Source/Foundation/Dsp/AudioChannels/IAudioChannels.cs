namespace Flux.Dsp
{
  /// <summary>Audio channel back left.</summary>
  public interface IAudioChannelBl
  {
    double BackLeft { get; }
  }
  /// <summary>Audio channel back right.</summary>
  public interface IAudioChannelBr
  {
    double BackRight { get; }
  }

  /// <summary>Audio channel front center.</summary>
  public interface IAudioChannelFc
  {
    double FrontCenter { get; }
  }

  /// <summary>Audio channel front left.</summary>
  public interface IAudioChannelFl
  {
    double FrontLeft { get; }
  }
  /// <summary>Audio channel front right.</summary>
  public interface IAudioChannelFr
  {
    double FrontRight { get; }
  }

  /// <summary>Audio channel low frequency effect (sub-woofer).</summary>
  public interface IAudioChannelLfe
  {
    double LowFrequency { get; }
  }
}
