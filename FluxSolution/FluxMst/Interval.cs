#if NET7_0_OR_GREATER
using Flux;

namespace Numerics
{
  [TestClass]
  public class Interval
  {
    //[TestMethod]
    //public void Distance()
    //{
    //	Assert.AreEqual(5.0, (5.0).Distance(10));
    //}

    [TestMethod]
    public void Fold()
    {
      Assert.AreEqual(3.5, Number.FoldAcross(6.5, 0, 5));
    }

    [TestMethod]
    public void Rescale()
    {
      Assert.AreEqual(25, Number.Rescale(7.5, 0, 5, 10, 20));
    }

    [TestMethod]
    public void WrapAround()
    {
      Assert.AreEqual(2.5, IntervalNotation.Closed.WrapAround(7.5, 1.0, 5.0));
    }

    [TestMethod]
    public void WrapAroundHalfOpenMax()
    {
      var expected = -1;
      var actual = Flux.IntervalNotation.HalfOpenRight.WrapAround(7, -2, 2);

      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void WrapAroundHalfOpenMin()
    {
      var expected = -1;
      var actual = Flux.IntervalNotation.HalfOpenLeft.WrapAround(7, -2, 2);

      Assert.AreEqual(expected, actual);
    }
  }
}
#endif
