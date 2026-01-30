using Flux;

namespace NetFx.ReadOnlySpan.Special
{
  [TestClass]
  public class Set
  {
    [TestMethod]
    public void SetExcept()
    {
      var a = System.Linq.Enumerable.Range(1, 5).ToArray().AsSpan();
      var b = System.Linq.Enumerable.Range(4, 9).ToArray().AsSpan();

      var expected = new int[] { 1, 2, 3 };
      var actual = a.Except(b).ToArray();

      CollectionAssert.AreEqual(expected, actual, "SetExcept");
    }

    [TestMethod]
    public void SetIntersect()
    {
      var a = System.Linq.Enumerable.Range(1, 5).ToArray().AsSpan();
      var b = System.Linq.Enumerable.Range(4, 9).ToArray().AsSpan();

      var expected = new int[] { 4, 5 };
      var actual = a.Intersect(b).Order().ToArray();

      CollectionAssert.AreEqual(expected, actual, "SetIntersect");
    }

    [TestMethod]
    public void SetPowerSet()
    {
      var a = System.Linq.Enumerable.Range(1, 5).ToArray().AsSpan();
      var b = System.Linq.Enumerable.Range(4, 9).ToArray().AsSpan();

      var expected = new int[][] { [], [1], [2], [3], [4], [5], [1, 2], [1, 3], [1, 4], [1, 5], [2, 3], [2, 4], [2, 5], [3, 4], [3, 5], [4, 5], [1, 2, 3], [1, 2, 4], [1, 2, 5], [1, 3, 4], [1, 3, 5], [1, 4, 5], [2, 3, 4], [2, 3, 5], [2, 4, 5], [3, 4, 5], [1, 2, 3, 4], [1, 2, 3, 5], [1, 2, 4, 5], [1, 3, 4, 5], [2, 3, 4, 5], [1, 2, 3, 4, 5] };

      var actual = a.PowerSet().OrderBy(a => a.Length).ThenBy(a => string.Join(",", a.Order())).ToArray();

      CollectionAssert.AreEqual(expected, actual, "SetPowerSet");
    }

    [TestMethod]
    public void SetSymmetricExcept()
    {
      var a = System.Linq.Enumerable.Range(1, 5).ToArray().AsSpan();
      var b = System.Linq.Enumerable.Range(4, 9).ToArray().AsSpan();

      var expected = new int[] { 1, 2, 3, 6, 7, 8, 9, 10, 11, 12 };
      var actual = a.SymmetricExcept(b).ToArray(); System.Array.Sort(actual);

      CollectionAssert.AreEqual(expected, actual, "SetIntersect");
    }

    [TestMethod]
    public void SetUnion()
    {
      var a = System.Linq.Enumerable.Range(1, 5).ToArray().AsSpan();
      var b = System.Linq.Enumerable.Range(4, 9).ToArray().AsSpan();

      var expected = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
      var actual = a.Union(b).ToArray();

      CollectionAssert.AreEqual(expected, actual, "SetUnion");
    }

    [TestMethod]
    public void SetUnionAll()
    {
      var a = System.Linq.Enumerable.Range(1, 5).ToArray().AsSpan();
      var b = System.Linq.Enumerable.Range(4, 9).ToArray().AsSpan();

      var expected = new int[] { 1, 2, 3, 4, 5, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
      var actual = a.UnionAll(b);

      CollectionAssert.AreEqual(expected, actual, "SetUnionAll");
    }
  }
}
