using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Units
{
  [TestClass]
  public class Specialized
  {
    [TestMethod]
    public void Rate()
    {
      var u = new Flux.Quantities.Rate<Flux.Quantities.Length, Flux.Quantities.Time>(new Flux.Quantities.Length(1), new Flux.Quantities.Time(1));

      Assert.AreEqual(1, u.Ratio);
    }

    [TestMethod]
    public void Ratio()
    {
      var u = new Flux.Quantities.Ratio(1, 1);

      Assert.AreEqual(1, u.Value);
    }
  }
}
