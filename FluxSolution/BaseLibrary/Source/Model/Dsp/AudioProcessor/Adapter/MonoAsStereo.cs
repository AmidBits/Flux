namespace Flux.Dsp.AudioProcessor.Adapter
{
  /// <summary>Process a stereo signal using one mono audio processor (IAudioProcessorMono).</summary>
  public class OneMonoAsStereo : IAudioProcessorStereo
  {
    public IAudioProcessorMono Mono;

    public OneMonoAsStereo(IAudioProcessorMono mono)
    {
      Mono = mono;
    }

    public ISampleStereo ProcessAudio(ISampleStereo sample)
    {
      return new StereoSample(Mono.ProcessAudio(new MonoSample(sample.FrontLeft)).FrontCenter, Mono.ProcessAudio(new MonoSample(sample.FrontRight)).FrontCenter);
    }
  }

  /// <summary>Process a stereo signal using two mono audio processors (IAudioProcessorMono).</summary>
  public class TwoMonoAsStereo : IAudioProcessorStereo
  {
    public IAudioProcessorMono Left, Right;

    public TwoMonoAsStereo(IAudioProcessorMono left, IAudioProcessorMono right)
    {
      Left = left;
      Right = right;
    }

    public ISampleStereo ProcessAudio(ISampleStereo sample)
    {
      return new StereoSample(Left.ProcessAudio(new MonoSample(sample.FrontLeft)).FrontCenter, Right.ProcessAudio(new MonoSample(sample.FrontRight)).FrontCenter);
    }
  }
}
