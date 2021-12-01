namespace Flux.Dsp.AudioProcessor.Adapter
{
#if NET5_0
  public sealed class MonoThruStereoProcessor
#else
  public record struct MonoThruStereoProcessor
#endif
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
