#if NET7_0_OR_GREATER
using Flux;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dsp
{
  [TestClass]
  public class WaveGenerator
  {
    private readonly Flux.Dsp.WaveGenerators.IMonoWaveUiGeneratable m_pulseWave = new Flux.Dsp.WaveGenerators.PulseWave();
    private readonly Flux.Dsp.WaveGenerators.IMonoWaveUiGeneratable m_sawtoothWave = new Flux.Dsp.WaveGenerators.SawWave();
    private readonly Flux.Dsp.WaveGenerators.IMonoWaveUiGeneratable m_sineWave = new Flux.Dsp.WaveGenerators.SineWave();
    private readonly Flux.Dsp.WaveGenerators.IMonoWaveUiGeneratable m_squareWave = new Flux.Dsp.WaveGenerators.SquareWave();
    private readonly Flux.Dsp.WaveGenerators.IMonoWaveUiGeneratable m_triangleWave = new Flux.Dsp.WaveGenerators.TriangleWave();

    [TestMethod]
    public void PulseWave()
    {
      Assert.AreEqual((1.0), m_pulseWave.GenerateMonoWaveUi(0.25).Wave);
      Assert.AreEqual((-1.0), m_pulseWave.GenerateMonoWaveUi(0.75).Wave);
    }

    [TestMethod]
    public void SawtoothWave()
    {
      Assert.AreEqual((0.5), m_sawtoothWave.GenerateMonoWaveUi(0.25).Wave);
      Assert.AreEqual((-0.5), m_sawtoothWave.GenerateMonoWaveUi(0.75).Wave);
    }

    [TestMethod]
    public void SineWave()
    {
      Assert.AreEqual(1, m_sineWave.GenerateMonoWaveUi(0.25).Wave, XtensionSingle.MaxDefaultTolerance);
      Assert.AreEqual(-1, m_sineWave.GenerateMonoWaveUi(0.75).Wave, XtensionSingle.MaxDefaultTolerance);
    }

    [TestMethod]
    public void SquareWave()
    {
      Assert.AreEqual(1.0, m_squareWave.GenerateMonoWaveUi(0.25).Wave);
      Assert.AreEqual(-1.0, m_squareWave.GenerateMonoWaveUi(0.75).Wave);
    }

    [TestMethod]
    public void TriangleWave()
    {
      Assert.AreEqual(0.0, m_triangleWave.GenerateMonoWaveUi(0.25).Wave);
      Assert.AreEqual(0.0, m_triangleWave.GenerateMonoWaveUi(0.75).Wave);
    }
  }
}
#endif
