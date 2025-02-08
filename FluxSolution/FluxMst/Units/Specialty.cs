using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Units
{
  [TestClass]
  public class Specialized
  {
    [TestMethod]
    public void Rate()
    {
      var u = new Flux.Units.Rate<Flux.Units.Length, Flux.Units.Time>(new Flux.Units.Length(1), new Flux.Units.Time(1));

      Assert.AreEqual(1, u.Ratio);
    }

    [TestMethod]
    public void Ratio()
    {
      var u = new Flux.Units.Ratio(1, 1);

      Assert.AreEqual(1, u.Value);
    }
  }
}
