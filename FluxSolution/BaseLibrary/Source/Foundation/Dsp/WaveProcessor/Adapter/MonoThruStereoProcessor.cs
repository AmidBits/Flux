namespace Flux.Dsp.AudioProcessor.Adapter
{
  public class MonoThruStereoProcessor
    : IWaveProcessorMono
  {
    public IWaveProcessorStereo Stereo { get; }

    public MonoThruStereoProcessor(IWaveProcessorStereo stereo)
    {
      Stereo = stereo;
    }

    public double ProcessAudio(double sample)
      => Stereo.ProcessAudio(new SampleStereo(sample)).ToMono().FrontCenter;
  }
}
