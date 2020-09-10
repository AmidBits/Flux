using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Flux;

namespace Globalization
{
  [TestClass]
  public class EnUs
  {
    [TestMethod]
    public void DeaRegistrationNumber()
    {
      var parsed = Flux.Globalization.EnUs.DeaRegistrationNumber.Parse("F91234563-001AB");

      Assert.AreEqual(@"F91234563-001AB", parsed.ToString());
    }

    [TestMethod]
    public void NorthAmericanNumberingPlan()
    {
      var parsed = Flux.Globalization.EnUs.NorthAmericanNumberingPlan.Parse("+1 520 219-6204");

      Assert.AreEqual(@"1-520-219-6204", parsed.ToString());
    }

    [TestMethod]
    public void SocialSecurityNumber()
    {
      var parsed = Flux.Globalization.EnUs.SocialSecurityNumber.Parse("123-45-6789");

      Assert.AreEqual(@"123-45-6789", parsed.ToString());
    }

    [TestMethod]
    public void PimaCounty_StreetAddress()
    {
      var parsed = Flux.Globalization.EnUs.PimaCounty.StreetAddress.Parse("10250 North Krauswood LN");

      Assert.AreEqual(@"F91234563-001AB", parsed.ToString());
    }
  }
}
