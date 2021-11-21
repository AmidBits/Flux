using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Flux;

namespace Foundation
{
  [TestClass]
  public class Colors
  {
    [TestMethod]
    public void Acmyk()
    {
      var acmyk = new Flux.Colors.Acmyk(1.00, 0.20, 0.40, 0.60, 0.80);

      Assert.AreEqual(1, acmyk.Alpha);
      Assert.AreEqual(0.2, acmyk.CMYK.Cyan);
      Assert.AreEqual(0.4, acmyk.CMYK.Magenta);
      Assert.AreEqual(0.6, acmyk.CMYK.Yellow);
      Assert.AreEqual(0.8, acmyk.CMYK.Key);
    }

    [TestMethod]
    public void Ahsi()
    {
      var ahsi = new Flux.Colors.Ahsi(0.35, 0.20, 0.25, 0.30);

      Assert.AreEqual(0.35, ahsi.Alpha);
      Assert.AreEqual(0.2, ahsi.HSI.Hue);
      Assert.AreEqual(0.25, ahsi.HSI.Saturation);
      Assert.AreEqual(0.3, ahsi.HSI.Intensity);
    }

    [TestMethod]
    public void Ahsl()
    {
      var ahsl = new Flux.Colors.Ahsl(0.35, 0.20, 0.25, 0.30);

      Assert.AreEqual(0.35, ahsl.Alpha);
      Assert.AreEqual(0.2, ahsl.HSL.Hue);
      Assert.AreEqual(0.25, ahsl.HSL.Saturation);
      Assert.AreEqual(0.3, ahsl.HSL.Lightness);
    }

    [TestMethod]
    public void Ahsv()
    {
      var ahsv = new Flux.Colors.Ahsv(0.35, 0.20, 0.25, 0.30);

      Assert.AreEqual(0.35, ahsv.Alpha);
      Assert.AreEqual(0.2, ahsv.HSV.Hue);
      Assert.AreEqual(0.25, ahsv.HSV.Saturation);
      Assert.AreEqual(0.3, ahsv.HSV.Value);
    }

    [TestMethod]
    public void Ahwb()
    {
      var ahwb = new Flux.Colors.Ahwb(0.35, 0.20, 0.25, 0.30);

      Assert.AreEqual(0.35, ahwb.Alpha);
      Assert.AreEqual(0.2, ahwb.HWB.Hue);
      Assert.AreEqual(0.25, ahwb.HWB.White);
      Assert.AreEqual(0.3, ahwb.HWB.Black);
    }

    [TestMethod]
    public void Argb()
    {
      var argb = new Flux.Colors.Argb(247, 67, 137, 207);

      Assert.AreEqual(247, argb.Alpha);
      Assert.AreEqual(67, argb.RGB.Red);
      Assert.AreEqual(137, argb.RGB.Green);
      Assert.AreEqual(207, argb.RGB.Blue);
    }

    [TestMethod]
    public void Cmyk()
    {
      var cmyk = new Flux.Colors.Cmyk(0.20, 0.40, 0.60, 0.80);

      Assert.AreEqual(0.20, cmyk.Cyan);
      Assert.AreEqual(0.40, cmyk.Magenta);
      Assert.AreEqual(0.60, cmyk.Yellow);
      Assert.AreEqual(0.80, cmyk.Key);
    }

    [TestMethod]
    public void CmykToRgb()
    {
      var cmyk = new Flux.Colors.Cmyk(0.20, 0.40, 0.60, 0.80);
      var rgb = cmyk.ToRgb();
      var rgb_expected = new Flux.Colors.Rgb(41, 31, 20);
      Assert.AreEqual(rgb_expected, rgb);
    }

    [TestMethod]
    public void Hsi()
    {
      var hsi = new Flux.Colors.Hsi(0.20, 0.25, 0.30);

      Assert.AreEqual(0.2, hsi.Hue);
      Assert.AreEqual(0.25, hsi.Saturation);
      Assert.AreEqual(0.3, hsi.Intensity);
    }

    [TestMethod]
    public void HsiToRgb()
    {
      var hsi = new Flux.Colors.Hsi(0.20, 0.25, 0.30);
      var rgb = hsi.ToRgb();
      var rgb_expected = new Flux.Colors.Rgb(115, 58, 57);
      Assert.AreEqual(rgb_expected, rgb);
    }

    [TestMethod]
    public void Hsl()
    {
      var hsl = new Flux.Colors.Hsl(0.20, 0.25, 0.30);

      Assert.AreEqual(0.2, hsl.Hue);
      Assert.AreEqual(0.25, hsl.Saturation);
      Assert.AreEqual(0.3, hsl.Lightness);
    }

    [TestMethod]
    public void HslToHsv()
    {
      var hsl = new Flux.Colors.Hsl(0.20, 0.25, 0.30);
      var hsv = hsl.ToHsv();
      var hsv_expected = new Flux.Colors.Hsv(0.2, 0.40000000000000013, 0.375);
      Assert.AreEqual(hsv_expected, hsv);
    }

    [TestMethod]
    public void HslToRgb()
    {
      var hsl = new Flux.Colors.Hsl(0.20, 0.25, 0.30);
      var rgb = hsl.ToRgb();
      var rgb_expected = new Flux.Colors.Rgb(96, 58, 57);
      Assert.AreEqual(rgb_expected, rgb);
    }

    [TestMethod]
    public void Hsv()
    {
      var hsv = new Flux.Colors.Hsv(0.20, 0.25, 0.30);

      Assert.AreEqual(0.20, hsv.Hue);
      Assert.AreEqual(0.25, hsv.Saturation);
      Assert.AreEqual(0.30, hsv.Value);
    }

    [TestMethod]
    public void HsvToHsl()
    {
      var hsv = new Flux.Colors.Hsv(0.20, 0.25, 0.30);
      var hsl = hsv.ToHsl();
      var hsl_expected = new Flux.Colors.Hsl(0.2, 0.14285714285714277, 0.2625);
      Assert.AreEqual(hsl_expected, hsl);
    }

    [TestMethod]
    public void HsvToHwb()
    {
      var hsv = new Flux.Colors.Hsv(0.20, 0.25, 0.30);
      var hwb = hsv.ToHwb();
      var hwb_expected = new Flux.Colors.Hwb(0.2, 0.22499999999999998, 0.7);
      Assert.AreEqual(hwb_expected, hwb);
    }

    [TestMethod]
    public void HsvToRgb()
    {
      var hsv = new Flux.Colors.Hsv(0.20, 0.25, 0.30);
      var rgb = hsv.ToRgb();
      var rgb_expected = new Flux.Colors.Rgb(76, 57, 57);
      Assert.AreEqual(rgb_expected, rgb);
    }

    [TestMethod]
    public void Hwb()
    {
      var hwb = new Flux.Colors.Hwb(0.20, 0.25, 0.30);

      Assert.AreEqual(0.2, hwb.Hue);
      Assert.AreEqual(0.25, hwb.White);
      Assert.AreEqual(0.3, hwb.Black);
    }

    [TestMethod]
    public void HwbToHsv()
    {
      var hwb = new Flux.Colors.Hwb(0.20, 0.25, 0.30);
      var hsv = hwb.ToHsv();
      var hsv_expected = new Flux.Colors.Hsv(0.2, 0.64285714285714279, 0.7);
      Assert.AreEqual(hsv_expected, hsv);
    }

    [TestMethod]
    public void HwbToRgb()
    {
      var hwb = new Flux.Colors.Hwb(0.20, 0.25, 0.30);
      var rgb = hwb.ToRgb();
      var rgb_expected = new Flux.Colors.Rgb(178, 64, 64);
      Assert.AreEqual(rgb_expected, rgb);
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
    public void RgbToCmyk()
    {
      var rgb = new Flux.Colors.Rgb(67, 137, 207);
      var cmyk = rgb.ToCmyk();
      var cmyk_expected = new Flux.Colors.Cmyk(0.67632850241545894, 0.33816425120772947, 0, 0.18823529411764706);
      Assert.AreEqual(cmyk_expected, cmyk);
    }

    [TestMethod]
    public void RgbToGrayscaleAverage()
    {
      var rgb = new Flux.Colors.Rgb(67, 137, 207);
      var grayscale_average = rgb.ToGrayscale(Flux.Colors.GrayscaleMethod.Average);
      var grayscale_average_expected = new Flux.Colors.Rgb(137, 137, 137);
      Assert.AreEqual(grayscale_average_expected, grayscale_average);
    }

    [TestMethod]
    public void RgbToGrayscaleLightness()
    {
      var rgb = new Flux.Colors.Rgb(67, 137, 207);
      var grayscale_lightness = rgb.ToGrayscale(Flux.Colors.GrayscaleMethod.Lightness);
      var grayscale_lightness_expected = new Flux.Colors.Rgb(137, 137, 137);
      Assert.AreEqual(grayscale_lightness_expected, grayscale_lightness);
    }

    [TestMethod]
    public void RgbToGrayscaleLuminosity()
    {
      var rgb = new Flux.Colors.Rgb(67, 137, 207);
      var grayscale_luminosity = rgb.ToGrayscale(Flux.Colors.GrayscaleMethod.Luminosity);
      var grayscale_luminosity_expected = new Flux.Colors.Rgb(127, 127, 127);
      Assert.AreEqual(grayscale_luminosity_expected, grayscale_luminosity);
    }

    [TestMethod]
    public void RgbToHsi()
    {
      var rgb = new Flux.Colors.Rgb(67, 137, 207);
      var hsi = rgb.ToHsi();
      var hsi_expected = new Flux.Colors.Hsi(210, 0.51094890510948909, 0.53725490196078429);
      Assert.AreEqual(hsi_expected, hsi);
    }

    [TestMethod]
    public void RgbToHsl()
    {
      var rgb = new Flux.Colors.Rgb(67, 137, 207);
      var hsl = rgb.ToHsl();
      var hsl_expected = new Flux.Colors.Hsl(210, 0.59322033898305082, 0.53725490196078429);
      Assert.AreEqual(hsl_expected, hsl);
    }

    [TestMethod]
    public void RgbToHsv()
    {
      var rgb = new Flux.Colors.Rgb(67, 137, 207);
      var hsv = rgb.ToHsv();
      var hsv_expected = new Flux.Colors.Hsv(210, 0.67632850241545894, 0.81176470588235294);
      Assert.AreEqual(hsv_expected, hsv);
    }

    [TestMethod]
    public void RgbToHwb()
    {
      var rgb = new Flux.Colors.Rgb(67, 137, 207);
      var hwb = rgb.ToHwb();
      var hwb_expected = new Flux.Colors.Hwb(210, 0.2627450980392157, 0.18823529411764706);
      Assert.AreEqual(hwb_expected, hwb);
    }

    [TestMethod]
    public void RgbToInt()
    {
      var rgb = new Flux.Colors.Rgb(67, 137, 207);
      var i = rgb.ToInt();
      Assert.AreEqual(4391119, i);
    }

    [TestMethod]
    public void RgbToString()
    {
      var rgb = new Flux.Colors.Rgb(67, 137, 207);
      var s = rgb.ToString();
      Assert.AreEqual(@"Rgb { R = 0x43, G = 0x89, B = 0xCF }", s);
    }

    [TestMethod]
    public void RgbToStringHtml()
    {
      var rgb = new Flux.Colors.Rgb(67, 137, 207);
      var shh = rgb.ToStringHtmlHex();
      Assert.AreEqual(@"#4389CF", shh);
      var shr = rgb.ToStringHtmlRgb();
      Assert.AreEqual(@"rgb(67, 137, 207)", shr);
    }
  }
}
