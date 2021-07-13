using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Flux;

namespace Foundation
{
  [TestClass]
  public class Colors
  {
    [TestMethod]
    public void Cmyk()
    {
      var cmyk = new Flux.Colors.Cmyk(0.20, 0.40, 0.60, 0.80);
      var rgb = cmyk.ToRgb();

      Assert.AreEqual(41, rgb.Red);
      Assert.AreEqual(31, rgb.Green);
      Assert.AreEqual(20, rgb.Blue);
    }

    [TestMethod]
    public void Cmyka()
    {
      var cmyka = new Flux.Colors.Cmyka(0.20, 0.40, 0.60, 0.80, 1.00);
      var rgba = cmyka.ToRgba();

      Assert.AreEqual(41, rgba.RGB.Red);
      Assert.AreEqual(31, rgba.RGB.Green);
      Assert.AreEqual(20, rgba.RGB.Blue);
      Assert.AreEqual(255, rgba.Alpha);
    }

    [TestMethod]
    public void Hsi()
    {
      var hsi = new Flux.Colors.Hsi(0.20, 0.25, 0.30);
      var rgb = hsi.ToRgb();

      Assert.AreEqual(115, rgb.Red);
      Assert.AreEqual(58, rgb.Green);
      Assert.AreEqual(57, rgb.Blue);
    }

    [TestMethod]
    public void Hsia()
    {
      var hsia = new Flux.Colors.Hsia(0.20, 0.25, 0.30, 0.35);
      var rgba = hsia.ToRgba();

      Assert.AreEqual(115, rgba.RGB.Red);
      Assert.AreEqual(58, rgba.RGB.Green);
      Assert.AreEqual(57, rgba.RGB.Blue);
      Assert.AreEqual(89, rgba.Alpha);
    }

    [TestMethod]
    public void Hsl()
    {
      var hsi = new Flux.Colors.Hsl(0.20, 0.25, 0.30);
      var rgb = hsi.ToRgb();

      Assert.AreEqual(96, rgb.Red);
      Assert.AreEqual(58, rgb.Green);
      Assert.AreEqual(57, rgb.Blue);
    }

    [TestMethod]
    public void Hsla()
    {
      var hsia = new Flux.Colors.Hsla(0.20, 0.25, 0.30, 0.35);
      var rgba = hsia.ToRgba();

      Assert.AreEqual(96, rgba.RGB.Red);
      Assert.AreEqual(58, rgba.RGB.Green);
      Assert.AreEqual(57, rgba.RGB.Blue);
      Assert.AreEqual(89, rgba.Alpha);
    }

    [TestMethod]
    public void Hsv()
    {
      var hsi = new Flux.Colors.Hsv(0.20, 0.25, 0.30);
      var rgb = hsi.ToRgb();

      Assert.AreEqual(76, rgb.Red);
      Assert.AreEqual(57, rgb.Green);
      Assert.AreEqual(57, rgb.Blue);
    }

    [TestMethod]
    public void Hsva()
    {
      var hsia = new Flux.Colors.Hsva(0.20, 0.25, 0.30, 0.35);
      var rgba = hsia.ToRgba();

      Assert.AreEqual(76, rgba.RGB.Red);
      Assert.AreEqual(57, rgba.RGB.Green);
      Assert.AreEqual(57, rgba.RGB.Blue);
      Assert.AreEqual(89, rgba.Alpha);
    }

    [TestMethod]
    public void Hwb()
    {
      var hsi = new Flux.Colors.Hwb(0.20, 0.25, 0.30);
      var rgb = hsi.ToRgb();

      Assert.AreEqual(178, rgb.Red);
      Assert.AreEqual(64, rgb.Green);
      Assert.AreEqual(64, rgb.Blue);
    }

    [TestMethod]
    public void Hwba()
    {
      var hsia = new Flux.Colors.Hwba(0.20, 0.25, 0.30, 0.35);
      var rgba = hsia.ToRgba();

      Assert.AreEqual(178, rgba.RGB.Red);
      Assert.AreEqual(64, rgba.RGB.Green);
      Assert.AreEqual(64, rgba.RGB.Blue);
      Assert.AreEqual(89, rgba.Alpha);
    }

    [TestMethod]
    public void Rgb()
    {
      var rgb = new Flux.Colors.Rgb(67, 137, 207);

      Assert.AreEqual(67, rgb.Red);
      Assert.AreEqual(137, rgb.Green);
      Assert.AreEqual(207, rgb.Blue);
    }

    [TestMethod]
    public void Rgba()
    {
      var rgba = new Flux.Colors.Rgba(67, 137, 207, 247);

      Assert.AreEqual(67, rgba.RGB.Red);
      Assert.AreEqual(137, rgba.RGB.Green);
      Assert.AreEqual(207, rgba.RGB.Blue);
      Assert.AreEqual(247, rgba.Alpha);
    }
  }
}
