using System.Linq;
using Flux;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluxMst.Colors
{
  [TestClass]
  public class Colors
  {
    private static readonly System.Drawing.Color m_argb = System.Drawing.Color.FromArgb(0xF7, 0x43, 0x89, 0xCF);
    private static readonly System.Drawing.Color m_rgb = System.Drawing.Color.FromArgb(m_argb.R, m_argb.G, m_argb.B);

    [TestMethod]
    public void CmykToRgb()
    {
      var expected = System.Drawing.Color.FromArgb(0, 41, 31, 20);
      var actual = Flux.Color.FromAcmyk(0, 0.20, 0.40, 0.60, 0.80);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void HsiToRgb()
    {
      var expected = System.Drawing.Color.FromArgb(0, 115, 58, 57);
      var actual = Flux.Color.FromAhsi(0, 0.20, 0.25, 0.30);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void HslToHsv()
    {
      var expected = Flux.Color.FromAhsv(0, 0.2, 0.40000000000000013, 0.375);
      var (H, S, V) = Flux.Color.ConvertHslToHsv(0.20, 0.25, 0.30);
      var actual = Flux.Color.FromAhsv(0, H, S, V);
      Assert.AreEqual(expected, actual);
    }

    //[TestMethod]
    //public void HslToRgb()
    //{
    //  var hsl = new Flux.Colors.Hsl(0.20, 0.25, 0.30);
    //  var rgb = hsl.ToRgb();
    //  var rgb_expected = new Flux.Colors.Rgb(96, 58, 57);
    //  Assert.AreEqual(rgb_expected, rgb);
    //}

    //[TestMethod]
    //public void HsvToHsl()
    //{
    //  var hsv = new Flux.Colors.Hsv(0.20, 0.25, 0.30);
    //  var hsl = hsv.ToHsl();
    //  var hsl_expected = new Flux.Colors.Hsl(0.2, 0.14285714285714277, 0.2625);
    //  Assert.AreEqual(hsl_expected, hsl);
    //}

    //[TestMethod]
    //public void HsvToHwb()
    //{
    //  var hsv = new Flux.Colors.Hsv(0.20, 0.25, 0.30);
    //  var hwb = hsv.ToHwb();
    //  var hwb_expected = new Flux.Colors.Hwb(0.2, 0.22499999999999998, 0.7);
    //  Assert.AreEqual(hwb_expected, hwb);
    //}

    //[TestMethod]
    //public void HsvToRgb()
    //{
    //  var hsv = new Flux.Colors.Hsv(0.20, 0.25, 0.30);
    //  var rgb = hsv.ToRgb();
    //  var rgb_expected = new Flux.Colors.Rgb(76, 57, 57);
    //  Assert.AreEqual(rgb_expected, rgb);
    //}

    //[TestMethod]
    //public void HwbToHsv()
    //{
    //  var hwb = new Flux.Colors.Hwb(0.20, 0.25, 0.30);
    //  var hsv = hwb.ToHsv();
    //  var hsv_expected = new Flux.Colors.Hsv(0.2, 0.64285714285714279, 0.7);
    //  Assert.AreEqual(hsv_expected, hsv);
    //}

    //[TestMethod]
    //public void HwbToRgb()
    //{
    //  var hwb = new Flux.Colors.Hwb(0.20, 0.25, 0.30);
    //  var rgb = hwb.ToRgb();
    //  var rgb_expected = new Flux.Colors.Rgb(178, 64, 64);
    //  Assert.AreEqual(rgb_expected, rgb);
    //}

    [TestMethod]
    public void Rgb()
    {
      Assert.AreEqual(0x43, m_rgb.R);
      Assert.AreEqual(0x89, m_rgb.G);
      Assert.AreEqual(0xCF, m_rgb.B);
    }

    //[TestMethod]
    //public void RgbToCmyk()
    //{
    //  var rgb = new Flux.Colors.Rgb(67, 137, 207);
    //  var cmyk = rgb.ToCmyk();
    //  var cmyk_expected = new Flux.Colors.Cmyk(0.67632850241545894, 0.33816425120772947, 0, 0.18823529411764706);
    //  Assert.AreEqual(cmyk_expected, cmyk);
    //}

    [TestMethod]
    public void RgbToGrayscaleAverage()
    {
      var argb = System.Drawing.Color.FromArgb(67, 137, 207);
      var grayscale_average_actual = argb.ToGrayscale(Flux.Colors.GrayscaleMethod.Average);
      var grayscale_average_expected = System.Drawing.Color.FromArgb(22, 45, 69);
      Assert.AreEqual(grayscale_average_expected, grayscale_average_actual);
    }

    [TestMethod]
    public void RgbToGrayscaleLuminosity601()
    {
      var rgb = System.Drawing.Color.FromArgb(67, 137, 207);
      var grayscale_luminosity_actual = rgb.ToGrayscale(Flux.Colors.GrayscaleMethod.Luminosity601);
      var grayscale_luminosity_expected = System.Drawing.Color.FromArgb(20, 80, 22);
      Assert.AreEqual(grayscale_luminosity_expected, grayscale_luminosity_actual);
    }

    [TestMethod]
    public void RgbToGrayscaleLuminosity709()
    {
      var argb = System.Drawing.Color.FromArgb(67, 137, 207);
      var grayscale_lightness_actual = argb.ToGrayscale(Flux.Colors.GrayscaleMethod.Luminosity709);
      var grayscale_lightness_expected = System.Drawing.Color.FromArgb(14, 98, 14);
      Assert.AreEqual(grayscale_lightness_expected, grayscale_lightness_actual);
    }

    //[TestMethod]
    //public void RgbToHsi()
    //{
    //  var rgb = new Flux.Colors.Rgb(67, 137, 207);
    //  var hsi = rgb.ToHsi();
    //  var hsi_expected = new Flux.Colors.Hsi(210, 0.51094890510948909, 0.53725490196078429);
    //  Assert.AreEqual(hsi_expected, hsi);
    //}

    //[TestMethod]
    //public void RgbToHsl()
    //{
    //  var rgb = new Flux.Colors.Rgb(67, 137, 207);
    //  var hsl = rgb.ToHsl();
    //  var hsl_expected = new Flux.Colors.Hsl(210, 0.59322033898305082, 0.53725490196078429);
    //  Assert.AreEqual(hsl_expected, hsl);
    //}

    //[TestMethod]
    //public void RgbToHsv()
    //{
    //  var rgb = new Flux.Colors.Rgb(67, 137, 207);
    //  var hsv = rgb.ToHsv();
    //  var hsv_expected = new Flux.Colors.Hsv(210, 0.67632850241545894, 0.81176470588235294);
    //  Assert.AreEqual(hsv_expected, hsv);
    //}

    //[TestMethod]
    //public void RgbToHwb()
    //{
    //  var rgb = new Flux.Colors.Rgb(67, 137, 207);
    //  var hwb = rgb.ToHwb();
    //  var hwb_expected = new Flux.Colors.Hwb(210, 0.2627450980392157, 0.18823529411764706);
    //  Assert.AreEqual(hwb_expected, hwb);
    //}

    //[TestMethod]
    //public void RgbToInt()
    //{
    //  var rgb = new Flux.Colors.Rgb(0x43, 0x89, 0xCF);
    //  var i = rgb.ToInt();
    //  Assert.AreEqual(0x004389CF, i);
    //}

    //[TestMethod]
    //public void RgbToString()
    //{
    //  var rgb = new Flux.Colors.Rgb(67, 137, 207);
    //  var s = rgb.ToString();
    //  Assert.AreEqual(@"Rgb { Red = 67, Green = 137, Blue = 207 }", s);
    //}

    //[TestMethod]
    //public void RgbToStringHtml()
    //{
    //  var rgb = new Flux.Colors.Rgb(67, 137, 207);
    //  var shh = rgb.ToHtmlHexString();
    //  Assert.AreEqual(@"#4389CF", shh);
    //  var shr = rgb.ToHtmlColorString();
    //  Assert.AreEqual(@"rgb(67, 137, 207)", shr);
    //}
  }
}
