using System.Linq;
using Flux;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Foundation.Dsp
{
  [TestClass]
  public class WaveGenerator
  {
    private readonly Flux.Dsp.IMonoWaveUiGeneratable m_pulseWave = new Flux.Dsp.WaveGenerator.PulseWave();
    private readonly Flux.Dsp.IMonoWaveUiGeneratable m_sawtoothWave = new Flux.Dsp.WaveGenerator.SawWave();
    private readonly Flux.Dsp.IMonoWaveUiGeneratable m_sineWave = new Flux.Dsp.WaveGenerator.SineWave();
    private readonly Flux.Dsp.IMonoWaveUiGeneratable m_squareWave = new Flux.Dsp.WaveGenerator.SquareWave();
    private readonly Flux.Dsp.IMonoWaveUiGeneratable m_triangleWave = new Flux.Dsp.WaveGenerator.TriangleWave();

    [TestMethod]
    public void PulseWave()
    {
      Assert.AreEqual((1.0), m_pulseWave.GenerateMonoWaveUi(0.25));
      Assert.AreEqual((-1.0), m_pulseWave.GenerateMonoWaveUi(0.75));
    }

    [TestMethod]
    public void SawtoothWave()
    {
      Assert.AreEqual((0.5), m_sawtoothWave.GenerateMonoWaveUi(0.25));
      Assert.AreEqual((-0.5), m_sawtoothWave.GenerateMonoWaveUi(0.75));
    }

    [TestMethod]
    public void SineWave()
    {
      Assert.AreEqual(1, m_sineWave.GenerateMonoWaveUi(0.25), Flux.GenericMath.EpsilonCpp32);
      Assert.AreEqual(-1, m_sineWave.GenerateMonoWaveUi(0.75), Flux.GenericMath.EpsilonCpp32);
    }

    [TestMethod]
    public void SquareWave()
    {
      Assert.AreEqual(1.0, m_squareWave.GenerateMonoWaveUi(0.25));
      Assert.AreEqual(-1.0, m_squareWave.GenerateMonoWaveUi(0.75));
    }

    [TestMethod]
    public void TriangleWave()
    {
      Assert.AreEqual(0.0, m_triangleWave.GenerateMonoWaveUi(0.25));
      Assert.AreEqual(0.0, m_triangleWave.GenerateMonoWaveUi(0.75));
    }
  }
}
