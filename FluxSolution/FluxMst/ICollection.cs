using System.Linq;
using Flux;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluxMst.Generic
{
  [TestClass]
  public class ICollection
  {
    readonly int[] integers = [17, 19, 23, 57, 91];

    [TestMethod]
    public void IsEmpty()
    {
      //Assert.AreEqual(false, integers.IsEmpty());
    }

    [TestMethod]
    public void Medoid()
    {
      //Assert.AreEqual(23, integers.Medoid());
    }
  }
}
