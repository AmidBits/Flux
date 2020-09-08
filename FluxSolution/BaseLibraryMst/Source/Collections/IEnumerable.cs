using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Flux;

namespace Collections.Generic
{
  [TestClass]
  public class IEnumerable
  {
    int[] integers = new int[] { 17, 17, 19, 23, 23, 57 };

    [TestMethod]
    public void Append()
    {
      CollectionAssert.AreEqual(new int[] { 17, 17, 19, 23, 23, 57, 13 }, integers.Append(13));
    }

    [TestMethod]
    public void GroupAdjacent()
    {
      foreach (var adj in integers.GroupAdjacent(v => v))
        switch (adj.Key)
        {
          case 17:
            Assert.AreEqual(2, adj.Count());
            break;
          case 19:
            Assert.AreEqual(1, adj.Count());
            break;
          case 23:
            Assert.AreEqual(2, adj.Count());
            break;
          case 57:
            Assert.AreEqual(1, adj.Count());
            break;
          default:
            throw new System.Exception();
        }
    }

    [TestMethod]
    public void IndexOfMax()
    {
      Assert.AreEqual(5, integers.IndexOfMax(v => v));
    }
    [TestMethod]
    public void IndexOfMin()
    {
      Assert.AreEqual(0, integers.IndexOfMin(v => v));
    }

    [TestMethod]
    public void Medoid()
    {
      var medoid = integers.Medoid(out var index, out int count);

      Assert.AreEqual(19, medoid, nameof(medoid));
      Assert.AreEqual(19, index, nameof(index));
      Assert.AreEqual(19, count, nameof(count));
    }

    [TestMethod]
    public void Prepend()
    {
      CollectionAssert.AreEqual(new int[] { 13, 17, 17, 19, 23, 23, 57 }, integers.Prepend(13));
    }
  }
}
