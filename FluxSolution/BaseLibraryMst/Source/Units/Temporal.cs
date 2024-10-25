using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Units
{
  [TestClass]
  public class Temporal
  {
    [TestMethod]
    public void JulianDate()
    {
      var u = new Flux.Temporal.JulianDate(1);

      Assert.AreEqual(1, u.Value);
    }

    [TestMethod]
    public void JulianDayNumber()
    {
      var u = new Flux.Temporal.JulianDayNumber(1);

      Assert.AreEqual(1, u.Value);
    }
  }
}
