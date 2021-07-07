using Flux;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Foundation
{
  [TestClass]
  public class Units
  {
    [TestMethod]
    public void Angle()
    {
      var u = new Flux.Units.Angle(1);

      Assert.AreEqual((0.5403023058681398, 0.8414709848078965), u.Cartesian);
      Assert.AreEqual((0.8414709848078966, 0.5403023058681394), u.CartesianEx);
      Assert.AreEqual(57.29577951308232, u.Degree);
      Assert.AreEqual(63.66197723675813, u.Gradian);
      Assert.AreEqual(1, u.Radian);
      Assert.AreEqual(0.15915494309189535, u.Revolution);
    }

    [TestMethod]
    public void Illuminance()
    {
      var u = new Flux.Units.Illuminance(1);

      Assert.AreEqual(0.0929, u.Lumens);
      Assert.AreEqual(1, u.Lux);
    }

    [TestMethod]
    public void Length()
    {
      var u = new Flux.Units.Length(1);

      Assert.AreEqual(3.280839895013123, u.Foot);
      Assert.AreEqual(0.001, u.Kilometer);
      Assert.AreEqual(1, u.Meter);
      Assert.AreEqual(0.0006213711922373339, u.Mile);
      Assert.AreEqual(1000, u.Millimeter);
      Assert.AreEqual(0.0005399568034557236, u.NauticalMile);
    }

    [TestMethod]
    public void Mass()
    {
      var u = new Flux.Units.Mass(1);

      Assert.AreEqual(1000, u.Gram);
      Assert.AreEqual(1, u.Kilogram);
      Assert.AreEqual(2.2046226218487757, u.Pound);
    }

    [TestMethod]
    public void MidiNote()
    {
      var u = new Flux.Units.MidiNote(69);

      Assert.AreEqual(69, u.Number);
      Assert.AreEqual(4, u.Octave);
      Assert.AreEqual(440.0, u.ToFrequency().Hertz);
    }

    [TestMethod]
    public void Pressure()
    {
      var u = new Flux.Units.Pressure(1);

      Assert.AreEqual(1, u.Pascal);
      Assert.AreEqual(0.00014503773772954367, u.Psi);
    }

    [TestMethod]
    public void Semitone()
    {
      var u = new Flux.Units.Semitone(1);

      Assert.AreEqual(1, u.Value);
      Assert.AreEqual(100, u.ToCent());
      Assert.AreEqual(Flux.Units.Semitone.FrequencyRatio, u.ToFrequencyRatio());
    }

    [TestMethod]
    public void Speed()
    {
      var u = new Flux.Units.Speed(1);

      Assert.AreEqual(3.280839895013123, u.FootPerSecond);
      Assert.AreEqual(3.6, u.KilometerPerHour);
      Assert.AreEqual(1.9438444924406046, u.Knot);
      Assert.AreEqual(1, u.MeterPerSecond);
      Assert.AreEqual(2.2369362920544, u.MilePerHour);
      Assert.AreEqual(1.9438444924406, u.NauticalMilePerHour);
    }

    [TestMethod]
    public void Temperature()
    {
      var u = new Flux.Units.Temperature(1);

      Assert.AreEqual(-272.15, u.Celsius);
      Assert.AreEqual(-457.87, u.Fahrenheit);
      Assert.AreEqual(1, u.Kelvin);
      Assert.AreEqual(1.8, u.Rankine);
    }
  }
}
