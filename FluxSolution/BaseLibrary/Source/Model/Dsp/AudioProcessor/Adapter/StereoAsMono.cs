namespace Flux.Dsp.AudioProcessor.Adapter
{
  public class StereoAsMono : IAudioProcessorMono
  {
    public IAudioProcessorStereo Stereo { get; }

    public StereoAsMono(IAudioProcessorStereo stereo)
    {
      Stereo = stereo;
    }

    public ISampleMono ProcessAudio(ISampleMono sample)
    {
      var stereo = Stereo.ProcessAudio(new StereoSample(sample.FrontCenter, sample.FrontCenter));

      return new MonoSample(Dsp.Convert.StereoToMono(stereo.FrontLeft, stereo.FrontRight).FrontCenter);
    }
  }
}
