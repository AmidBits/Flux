#if NET7_0_OR_GREATER
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dsp
{
  [TestClass]
  public class WaveProcessor
  {
    //private Flux.Dsp.Oscillator.AmplitudeModulator _amplitudeModulator = new Flux.Dsp.WaveModulator.AmplitudeModulator();
    //private Flux.Dsp.WaveModulator.RingModulator _ringModulator = new Flux.Dsp.WaveModulator.RingModulator();

    private Flux.Dsp.WaveProcessors.MonoFolder _folder = new Flux.Dsp.WaveProcessors.MonoFolder() { Multiplier = 2.5 };
    private Flux.Dsp.WaveProcessors.MonoInverter _inverter = new Flux.Dsp.WaveProcessors.MonoInverter();
    private Flux.Dsp.WaveProcessors.MonoLagger _lagger = new Flux.Dsp.WaveProcessors.MonoLagger();
    private Flux.Dsp.WaveProcessors.MonoQuadratic _quadratic = new Flux.Dsp.WaveProcessors.MonoQuadratic() { Exponent = 2 };
    private Flux.Dsp.WaveProcessors.MonoRectifier _fullWaveRectifier = new Flux.Dsp.WaveProcessors.MonoRectifier(Flux.Dsp.WaveProcessors.MonoRectifierMode.FullWave, 0);
    private Flux.Dsp.WaveProcessors.MonoRectifier _halfWaveRectifier = new Flux.Dsp.WaveProcessors.MonoRectifier(Flux.Dsp.WaveProcessors.MonoRectifierMode.HalfWave, 0);

    private const double _hiPhase = System.Math.PI * 0.75;
    private const double _loPhase = System.Math.PI * 0.25;

    private const double _hiSample = 0.75;
    private const double _loSample = -0.75;

    [TestMethod]
    public void FullWaveRectifier()
    {
      Assert.AreEqual(0.75, _fullWaveRectifier.ProcessMonoWave(_hiSample));
      Assert.AreEqual(0.75, _fullWaveRectifier.ProcessMonoWave(_hiSample));
      Assert.AreEqual(0.75, _fullWaveRectifier.ProcessMonoWave(_loSample));
      Assert.AreEqual(0.75, _fullWaveRectifier.ProcessMonoWave(_loSample));
    }

    [TestMethod]
    public void HalfWaveRectifier()
    {

      Assert.AreEqual(0.75, _halfWaveRectifier.ProcessMonoWave(_hiSample));
      Assert.AreEqual(0.75, _halfWaveRectifier.ProcessMonoWave(_hiSample));
      Assert.AreEqual(0, _halfWaveRectifier.ProcessMonoWave(_loSample));
      Assert.AreEqual(0, _halfWaveRectifier.ProcessMonoWave(_loSample));
    }

    [TestMethod]
    public void Inverter()
    {
      Assert.AreEqual(-0.75, _inverter.ProcessMonoWave(_hiSample));
      Assert.AreEqual(-0.75, _inverter.ProcessMonoWave(_hiSample));
      Assert.AreEqual(0.75, _inverter.ProcessMonoWave(_loSample));
      Assert.AreEqual(0.75, _inverter.ProcessMonoWave(_loSample));
    }

    [TestMethod]
    public void Folder()
    {
      Assert.AreEqual(-0.5, _folder.ProcessMonoWave(_hiSample));
      Assert.AreEqual(-0.5, _folder.ProcessMonoWave(_hiSample));
      Assert.AreEqual(0.5, _folder.ProcessMonoWave(_loSample));
      Assert.AreEqual(0.5, _folder.ProcessMonoWave(_loSample));
    }
  }
}
#endif
