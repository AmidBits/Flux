//using System.Linq;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

//using Flux;

//namespace Dsp
//{
//  [TestClass]
//  public class WaveShaper
//  {
//    private Flux.Dsp.WaveModulator.AmplitudeModulator _amplitudeModulator = new Flux.Dsp.WaveModulator.AmplitudeModulator();
//    private Flux.Dsp.WaveModulator.RingModulator _ringModulator = new Flux.Dsp.WaveModulator.RingModulator();

//    private Flux.Dsp.AudioProcessor.WaveFolder _folder = new Flux.Dsp.AudioProcessor.WaveFolder() { FoldMultiplier = 2.5 };
//    private Flux.Dsp.AudioProcessor.Inverter _inverter = new Flux.Dsp.AudioProcessor.Inverter();
//    private Flux.Dsp.AudioProcessor.Lagger _lagger = new Flux.Dsp.AudioProcessor.Lagger();
//    private Flux.Dsp.AudioProcessor.Quadratic _quadratic = new Flux.Dsp.AudioProcessor.Quadratic() { Exponent = 2 };
//    private Flux.Dsp.AudioProcessor.Rectifier _fullWaveRectifier = new Flux.Dsp.AudioProcessor.Rectifier() { Mode = Flux.Dsp.AudioProcessor.RectifierMode.FullWave };
//    private Flux.Dsp.AudioProcessor.Rectifier _halfWaveRectifier = new Flux.Dsp.AudioProcessor.Rectifier() { Mode = Flux.Dsp.AudioProcessor.RectifierMode.HalfWave };

//    private const double _hiPhase = System.Math.PI * 0.75;
//    private const double _loPhase = System.Math.PI * 0.25;

//    private const double _hiSample = 0.75;
//    private const double _loSample = -0.75;

//    [TestMethod]
//    public void FullWaveRectifier()
//    {
//      Assert.AreEqual(0.75, _fullWaveRectifier.ProcessAudio(_hiSample));
//      Assert.AreEqual(0.75, _fullWaveRectifier.ProcessAudio(_hiSample));
//      Assert.AreEqual(0.75, _fullWaveRectifier.ProcessAudio(_loSample));
//      Assert.AreEqual(0.75, _fullWaveRectifier.ProcessAudio(_loSample));
//    }

//    [TestMethod]
//    public void HalfWaveRectifier()
//    {

//      Assert.AreEqual(0.75, _halfWaveRectifier.ProcessAudio(_hiSample));
//      Assert.AreEqual(0.75, _halfWaveRectifier.ProcessAudio(_hiSample));
//      Assert.AreEqual(0, _halfWaveRectifier.ProcessAudio(_loSample));
//      Assert.AreEqual(0, _halfWaveRectifier.ProcessAudio(_loSample));
//    }

//    [TestMethod]
//    public void Inverter()
//    {
//      Assert.AreEqual(-0.75, _inverter.ProcessAudio(_hiSample));
//      Assert.AreEqual(-0.75, _inverter.ProcessAudio(_hiSample));
//      Assert.AreEqual(0.75, _inverter.ProcessAudio(_loSample));
//      Assert.AreEqual(0.75, _inverter.ProcessAudio(_loSample));
//    }

//    [TestMethod]
//    public void WaveFolder()
//    {
//      Assert.AreEqual(0.125, _folder.ProcessAudio(_hiSample));
//      Assert.AreEqual(0.125, _folder.ProcessAudio(_hiSample));
//      Assert.AreEqual(-0.125, _folder.ProcessAudio(_loSample));
//      Assert.AreEqual(-0.125, _folder.ProcessAudio(_loSample));
//    }
//  }
//}
