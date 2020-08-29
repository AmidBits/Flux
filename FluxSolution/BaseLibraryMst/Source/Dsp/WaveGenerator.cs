using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Flux;

namespace Dsp
{
  [TestClass]
  public class WaveGenerator
  {
    private readonly Flux.Dsp.IWaveGenerator m_pulseWave = new Flux.Dsp.WaveGenerator.PulseWave();
    private readonly Flux.Dsp.IWaveGenerator m_sawtoothWave = new Flux.Dsp.WaveGenerator.SawtoothWave();
    private readonly Flux.Dsp.IWaveGenerator m_sineWave = new Flux.Dsp.WaveGenerator.SineWave();
    private readonly Flux.Dsp.IWaveGenerator m_squareWave = new Flux.Dsp.WaveGenerator.SquareWave();
    private readonly Flux.Dsp.IWaveGenerator m_triangleWave = new Flux.Dsp.WaveGenerator.TriangleWave();

    [TestMethod]
    public void PulseWave()
    {
      Assert.AreEqual(new Flux.Dsp.MonoSample(1), m_pulseWave.GenerateWave(0.25));
      Assert.AreEqual(new Flux.Dsp.MonoSample(-1), m_pulseWave.GenerateWave(0.75));
    }

    [TestMethod]
    public void SawtoothWave()
    {
      Assert.AreEqual(new Flux.Dsp.MonoSample(0.5), m_sawtoothWave.GenerateWave(0.25));
      Assert.AreEqual(new Flux.Dsp.MonoSample(-0.5), m_sawtoothWave.GenerateWave(0.75));
    }

    [TestMethod]
    public void SineWave()
    {
      Assert.AreEqual(1, m_sineWave.GenerateWave(0.25).FrontCenter, Flux.Maths.EpsilonCpp32);
      Assert.AreEqual(-1, m_sineWave.GenerateWave(0.75).FrontCenter, Flux.Maths.EpsilonCpp32);
    }

    [TestMethod]
    public void SquareWave()
    {
      Assert.AreEqual(new Flux.Dsp.MonoSample(1), m_squareWave.GenerateWave(0.25));
      Assert.AreEqual(new Flux.Dsp.MonoSample(-1), m_squareWave.GenerateWave(0.75));
    }

    [TestMethod]
    public void TriangleWave()
    {
      Assert.AreEqual(new Flux.Dsp.MonoSample(0), m_triangleWave.GenerateWave(0.25));
      Assert.AreEqual(new Flux.Dsp.MonoSample(0), m_triangleWave.GenerateWave(0.75));
    }
  }
}
