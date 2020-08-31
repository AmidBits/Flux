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
      => new StereoSample(Mono.ProcessAudio(sample.FrontLeft), Mono.ProcessAudio(sample.FrontRight));
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
      => new StereoSample(Left.ProcessAudio(sample.FrontLeft), Right.ProcessAudio(sample.FrontRight));
  }
}
