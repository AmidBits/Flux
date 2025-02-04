using System.Linq;
using Flux;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Collections.Generic
{
  [TestClass]
  public class IEnumerable
  {
    private readonly int[] integers = new int[] { 17, 17, 19, 23, 23, 57 };

    [TestMethod]
    public void Append()
    {
      CollectionAssert.AreEqual(new int[] { 17, 17, 19, 23, 23, 57, 13 }, integers.Append(13).ToArray());
    }

    [TestMethod]
    public void Choose()
    {
      var expected = new int[] { 17, 23, 57 };
      var actual = integers.SelectWhere((e, i) => e, (e, i, r) => ((i & 1) == 1)).ToArray();
      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void CompareCount()
    {
      Assert.AreEqual(1, integers.CompareCount(4)); // Has more than 4 elements.
      Assert.AreEqual(-1, integers.CompareCount(4000)); // Has less than 4000 elements.
      Assert.AreEqual(0, integers.CompareCount(6, (e, i) => (e & 1) == 1)); // Has 6 odd elements.
    }

    [TestMethod]
    public void ContainsAll()
    {
      Assert.IsTrue(integers.ContainsAll(new int[] { 17, 23, 57 }));
    }

    [TestMethod]
    public void ContainsAny()
    {
      Assert.IsTrue(integers.ContainsAny(new int[] { 23, 57 }));
    }

    [TestMethod]
    public void CommonPrefixLength()
    {
      Assert.AreEqual(4, integers.CommonPrefixLength(new int[] { 17, 17, 19, 23 }));
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
    public void GetExtremum()
    {
      var (minIndex, minItem, minValue, maxIndex, maxItem, maxValue) = integers.GetExtremum(v => v);

      Assert.AreEqual(5, maxIndex);
      Assert.AreEqual(0, minIndex);
    }

    //[TestMethod]
    //public void IsCountBetween()
    //{
    //  Assert.IsFalse(integers.IsCountBetween(3, 8, (e, i) => new int[] { 19, 57 }.Contains(e)));
    //  Assert.IsTrue(integers.IsCountBetween(3, 8, (e, i) => new int[] { 17, 23 }.Contains(e)));
    //}

    [TestMethod]
    public void Medoid()
    {
      var medoid = integers.Select(i => (System.Numerics.BigInteger)i).Medoid(out var index, out int count);

      Assert.AreEqual(19, medoid, nameof(medoid));
      Assert.AreEqual(2, index, nameof(index));
      Assert.AreEqual(6, count, nameof(count));
    }

    [TestMethod]
    public void Mode()
    {
      var (mode, count) = integers.Mode().First();

      Assert.AreEqual((17, 2), (mode, count));
    }

    [TestMethod]
    public void Prepend()
    {
      CollectionAssert.AreEqual(new int[] { 13, 17, 17, 19, 23, 23, 57 }, integers.Prepend(13).ToArray());
    }

    [TestMethod]
    public void Repeat()
    {
      CollectionAssert.AreEqual(new int[] { 17, 17, 19, 23, 23, 57, 17, 17, 19, 23, 23, 57 }, integers.Repeat(1).ToArray());
    }

    //[TestMethod]
    //public void OrderedSequenceEqual()
    //{
    //  var a = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    //  var b = new int[] { 9, 8, 7, 6, 5, 4, 3, 2, 1 };
    //  var c = new int[] { 1, 9, 2, 8, 3, 7, 4, 6, 5 };

    //  Assert.IsTrue(a.OrderedSequenceEqual(b, v => v));
    //  Assert.IsTrue(a.OrderedSequenceEqual(c, v => v));
    //  Assert.IsTrue(b.OrderedSequenceEqual(c, v => v));
    //}

    [TestMethod]
    public void SequenceContentEqualByXor()
    {
      var a = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
      var b = new int[] { 9, 8, 7, 6, 5, 4, 3, 2, 1 };
      var c = new int[] { 1, 9, 2, 8, 3, 7, 4, 6, 5 };

      Assert.IsTrue(a.SequenceHashCodeByXor() == b.SequenceHashCodeByXor());
      Assert.IsTrue(a.SequenceHashCodeByXor() == c.SequenceHashCodeByXor());
      Assert.IsTrue(b.SequenceHashCodeByXor() == c.SequenceHashCodeByXor());
    }

    [TestMethod]
    public void SkipEvery()
    {
      CollectionAssert.AreEqual(new int[] { 17, 23, 57 }, integers.SkipEvery(2, Flux.OptionEvery.First).ToArray());
    }

    [TestMethod]
    public void SkipLastWhile()
    {
      CollectionAssert.AreEqual(new int[] { 17, 17, 19, 23, 23 }, integers.SkipLastWhile((e, i) => (i & 1) == 1).ToArray());
    }

    //[TestMethod]
    //public void SkipUntil()
    //{
    //  CollectionAssert.AreEqual(new int[] { 23, 57 }, integers.SkipUntil((e, i) => i > 2).ToArray());
    //}

    [TestMethod]
    public void StartsWith()
    {
      Assert.IsTrue(integers.IsCommonPrefix(new int[] { 17, 17, 19 }));
    }

    [TestMethod]
    public void TakeEvery()
    {
      CollectionAssert.AreEqual(new int[] { 17, 19, 23 }, integers.TakeEvery(2, Flux.OptionEvery.First).ToArray());
    }

    [TestMethod]
    public void TakeLastWhile()
    {
      CollectionAssert.AreEqual(new int[] { 57 }, integers.TakeLastWhile((e, i) => (i & 1) == 1).ToArray());
    }

    //[TestMethod]
    //public void TakeUntil()
    //{
    //  CollectionAssert.AreEqual(new int[] { 17, 17, 19, 23 }, integers.TakeUntil((e, i) => i > 2).ToArray());
    //}
  }
}
