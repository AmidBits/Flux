namespace Flux.Dsp.AudioProcessor.Adapter
{
  /// <summary>Process a stereo signal using one mono audio processor (IAudioProcessorMono).</summary>
  public class StereoThroughOneMono
    : IAudioProcessorStereo
  {
    public IAudioProcessorMono Mono { get; set; }

    public StereoThroughOneMono(IAudioProcessorMono mono)
    {
      Mono = mono;
    }

    public StereoSample ProcessAudio(StereoSample sample)
      => new StereoSample(Mono.ProcessAudio(new MonoSample(sample.FrontLeft)).FrontCenter, Mono.ProcessAudio(new MonoSample(sample.FrontRight)).FrontCenter);
  }

  /// <summary>Process a stereo signal using two mono audio processors (IAudioProcessorMono).</summary>
  public class StereoThroughTwoMono
    : IAudioProcessorStereo
  {
    public IAudioProcessorMono Left { get; set; }
    public IAudioProcessorMono Right { get; set; }

    public StereoThroughTwoMono(IAudioProcessorMono left, IAudioProcessorMono right)
    {
      Left = left;
      Right = right;
    }

    public StereoSample ProcessAudio(StereoSample sample)
      => new StereoSample(Left.ProcessAudio(new MonoSample(sample.FrontLeft)).FrontCenter, Right.ProcessAudio(new MonoSample(sample.FrontRight)).FrontCenter);
  }
}
