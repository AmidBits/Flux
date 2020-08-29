using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Flux;

namespace Generic
{
  [TestClass]
  public class ICollection
  {
    int[] integers = new int[] { 17, 19, 23, 57, 91 };

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
