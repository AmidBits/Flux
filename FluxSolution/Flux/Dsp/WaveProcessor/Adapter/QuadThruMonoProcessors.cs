namespace Flux.Dsp.WaveProcessor.Adapter
{
  /// <summary>Process a stereo signal using two mono audio processors (IAudioProcessorMono).</summary>
  public record class QuadThruMonoProcessors
    : IQuadWaveProcessable
  {
    public IMonoWaveProcessable BackLeft { get; set; }
    public IMonoWaveProcessable BackRight { get; set; }
    public IMonoWaveProcessable FrontLeft { get; set; }
    public IMonoWaveProcessable FrontRight { get; set; }

    public QuadThruMonoProcessors(IMonoWaveProcessable backLeft, IMonoWaveProcessable backRight, IMonoWaveProcessable frontLeft, IMonoWaveProcessable frontRight)
    {
      BackLeft = backLeft;
      BackRight = backRight;
      FrontLeft = frontLeft;
      FrontRight = frontRight;
    }

    public Waves.IWaveQuad<double> ProcessQuadWave(Waves.IWaveQuad<double> stereo)
      => new Waves.WaveQuad<double>(
        BackLeft.ProcessMonoWave(new Waves.WaveMono<double>(stereo.WaveBackLeft)).Wave,
        BackRight.ProcessMonoWave(new Waves.WaveMono<double>(stereo.WaveBackRight)).Wave,
        FrontLeft.ProcessMonoWave(new Waves.WaveMono<double>(stereo.WaveFrontLeft)).Wave,
        FrontRight.ProcessMonoWave(new Waves.WaveMono<double>(stereo.WaveFrontRight)).Wave
      );
  }
}
