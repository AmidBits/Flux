#if NET7_0_OR_GREATER
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dsp
{
  [TestClass]
  public class WaveProcessor
  {
    //private Flux.Dsp.Oscillator.AmplitudeModulator _amplitudeModulator = new Flux.Dsp.WaveModulator.AmplitudeModulator();
    //private Flux.Dsp.WaveModulator.RingModulator _ringModulator = new Flux.Dsp.WaveModulator.RingModulator();

    private Flux.Dsp.WaveProcessor.MonoFolder _folder = new Flux.Dsp.WaveProcessor.MonoFolder() { Multiplier = 2.5 };
    private Flux.Dsp.WaveProcessor.MonoInverter _inverter = new Flux.Dsp.WaveProcessor.MonoInverter();
    private Flux.Dsp.WaveProcessor.MonoLagger _lagger = new Flux.Dsp.WaveProcessor.MonoLagger();
    private Flux.Dsp.WaveProcessor.MonoQuadratic _quadratic = new Flux.Dsp.WaveProcessor.MonoQuadratic() { Exponent = 2 };
    private Flux.Dsp.WaveProcessor.MonoRectifier _fullWaveRectifier = new Flux.Dsp.WaveProcessor.MonoRectifier(Flux.Dsp.WaveProcessor.MonoRectifierMode.FullWave, 0);
    private Flux.Dsp.WaveProcessor.MonoRectifier _halfWaveRectifier = new Flux.Dsp.WaveProcessor.MonoRectifier(Flux.Dsp.WaveProcessor.MonoRectifierMode.HalfWave, 0);

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
