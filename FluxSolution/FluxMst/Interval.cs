#if NET7_0_OR_GREATER
using Flux;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
      Assert.AreEqual(3.5, (6.5).FoldAcross(0, 5));
    }

    [TestMethod]
    public void Rescale()
    {
      Assert.AreEqual(25, (7.5).Rescale(0, 5, 10, 20));
    }

    [TestMethod]
    public void Wrap()
    {
      Assert.AreEqual(2.5, (7.5).WrapAround(1, 5));
    }

    [TestMethod]
    public void WrapAroundHalfOpenMax()
    {
      Assert.AreEqual(-1, (7).WrapAroundHalfOpenMax(-2, 2));
    }

    [TestMethod]
    public void WrapAroundHalfOpenMin()
    {
      Assert.AreEqual(-1, (7).WrapAroundHalfOpenMin(-2, 2));
    }
  }
}
#endif
