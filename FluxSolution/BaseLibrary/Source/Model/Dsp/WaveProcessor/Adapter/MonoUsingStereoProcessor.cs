namespace Flux.Dsp.AudioProcessor.Adapter
{
  public class MonoUsingStereoProcessor
    : IWaveProcessorMono
  {
    public IWaveProcessorStereo Stereo { get; }

    public MonoUsingStereoProcessor(IWaveProcessorStereo stereo)
    {
      Stereo = stereo;
    }

    public double ProcessAudio(double sample)
      => Stereo.ProcessAudio(new StereoSample(sample)).ToMono();
  }
}
