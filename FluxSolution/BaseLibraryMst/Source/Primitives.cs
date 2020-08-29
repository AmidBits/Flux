using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Flux;

namespace Primitives
{
  [TestClass]
  public class Char
  {
    [TestMethod]
    public void IsDiacriticalStroke()
    {
      Assert.AreEqual(false, 'ñ'.IsDiacriticalStroke());
      Assert.AreEqual(true, 'ø'.IsDiacriticalStroke());
      Assert.AreEqual(false, 'A'.IsDiacriticalStroke());
    }

    [TestMethod]
    public void IsEnglishConsonant()
    {
      Assert.AreEqual(false, Flux.Globalization.EnUs.Language.IsEnglishConsonant('ñ', true));
      Assert.AreEqual(true, Flux.Globalization.EnUs.Language.IsEnglishConsonant('t', true));
      Assert.AreEqual(false, Flux.Globalization.EnUs.Language.IsEnglishConsonant('A', true));
    }

    [TestMethod]
    public void IsEnglishLetter()
    {
      Assert.AreEqual(false, Flux.Globalization.EnUs.Language.IsEnglishLetter('ñ'));
      Assert.AreEqual(true, Flux.Globalization.EnUs.Language.IsEnglishLetter('t'));
      Assert.AreEqual(true, Flux.Globalization.EnUs.Language.IsEnglishLetter('A'));
    }

    [TestMethod]
    public void IsEnglishLetterLower()
    {
      Assert.AreEqual(true, Flux.Globalization.EnUs.Language.IsEnglishLetterLower('t'));
      Assert.AreEqual(false, Flux.Globalization.EnUs.Language.IsEnglishLetterLower('A'));
    }

    [TestMethod]
    public void IsEnglishLetterUpper()
    {
      Assert.AreEqual(false, Flux.Globalization.EnUs.Language.IsEnglishLetterUpper('t'));
      Assert.AreEqual(true, Flux.Globalization.EnUs.Language.IsEnglishLetterUpper('A'));
    }

    [TestMethod]
    public void IsEnglishVowel()
    {
      Assert.AreEqual(false, Flux.Globalization.EnUs.Language.IsEnglishVowel('ñ', true));
      Assert.AreEqual(false, Flux.Globalization.EnUs.Language.IsEnglishVowel('y', false));
      Assert.AreEqual(true, Flux.Globalization.EnUs.Language.IsEnglishVowel('y', true));
      Assert.AreEqual(true, Flux.Globalization.EnUs.Language.IsEnglishVowel('A', false));
    }
  }

  [TestClass]
  public class Double
  {
    double[] d = new double[] { 9d, 27d, 63d, 81d };

    [TestMethod]
    public void CumulativeDistributionFunction()
    {
      Assert.AreEqual(0.5, d.CumulativeDistributionFunction(45));
    }

    [TestMethod]
    public void Mean()
    {
      Assert.AreEqual(45, d.Mean());
    }

    [TestMethod]
    public void Median()
    {
      Assert.AreEqual(45, d.Median());
    }

    [TestMethod]
    public void PercentileTopScore()
    {
      Assert.AreEqual(27, d.Percentile(50));
    }

    [TestMethod]
    public void ProbabilityMassFunction()
    {
      Assert.AreEqual(0.75, d.ProbabilityMassFunction(65, out var _, out var _));
    }

    [TestMethod]
    public void StandardDeviation()
    {
      Assert.AreEqual("28.460498941515414", d.StandardDeviation().ToString());
    }

    [TestMethod]
    public void Variance()
    {
      Assert.AreEqual(810, d.Variance().variance);
    }
  }
}
