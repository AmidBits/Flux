using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Units
{
  [TestClass]
  public class Temporal
  {
    [TestMethod]
    public void JulianDate()
    {
      var u = new Flux.Quantities.JulianDate(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void JulianDayNumber()
    {
      var u = new Flux.Quantities.JulianDayNumber(1);

      Assert.AreEqual(1, u.Value);
    }
  }
}
