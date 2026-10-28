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
      Assert.AreEqual(3.5, Number.ReflectedFold(6.5, 0, 5));
    }

    [TestMethod]
    public void Rescale()
    {
      Assert.AreEqual(25, Number.Remap(7.5, 0, 5, 10, 20));
    }

    [TestMethod]
    public void WrapClosed()
    {
      Assert.AreEqual(3.5, Number.WrapClosed(7.5, 1.0, 5.0));
    }

    [TestMethod]
    public void WrapHalfOpenRight()
    {
      var expected = -1;
      var actual = Number.WrapHalfOpenRight(7, -2, 2);

      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void WrapHalfOpenLeft()
    {
      var expected = -1;
      var actual = Number.WrapHalfOpenLeft(7, -2, 2);

      Assert.AreEqual(expected, actual);
    }
  }
}
#endif
