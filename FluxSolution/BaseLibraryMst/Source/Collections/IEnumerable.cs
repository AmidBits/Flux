using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Flux;

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
			CollectionAssert.AreEqual(new int[] { 17, 23, 57 }, integers.Choose((e, i) => ((i & 1) == 1, e)).ToArray());
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
		public void CountEqualAtStart()
		{
			Assert.AreEqual(4, integers.CountEqualAtStart(new int[] { 17, 17, 19, 23 }));
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
			Assert.AreEqual(5, integers.IndexOfMax(v => v, out var _));
		}
		[TestMethod]
		public void IndexOfMin()
		{
			Assert.AreEqual(0, integers.IndexOfMin(v => v, out var _));
		}

		[TestMethod]
		public void IsCountBetween()
		{
			Assert.IsFalse(integers.IsCountBetween(3, 8, (e, i) => new int[] { 19, 57 }.Contains(e)));
			Assert.IsTrue(integers.IsCountBetween(3, 8, (e, i) => new int[] { 17, 23 }.Contains(e)));
		}

		[TestMethod]
		public void IsCountAtLeast()
		{
			Assert.IsTrue(integers.IsCountAtLeast(4, (e, i) => true));
		}

		[TestMethod]
		public void IsCountAtMost()
		{
			Assert.IsTrue(integers.IsCountAtMost(4000, (e, i) => true));
		}

		[TestMethod]
		public void Medoid()
		{
			var medoid = integers.Medoid(out var index, out int count);

			Assert.AreEqual(19, medoid, nameof(medoid));
			Assert.AreEqual(2, index, nameof(index));
			Assert.AreEqual(6, count, nameof(count));
		}

		[TestMethod]
		public void Mode()
		{
			Assert.AreEqual(new System.Collections.Generic.KeyValuePair<int, int>(17, 2), integers.Mode(v => v).First());
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

		[TestMethod]
		public void SequenceContentEqualOrderBy()
		{
			var a = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
			var b = new int[] { 9, 8, 7, 6, 5, 4, 3, 2, 1 };
			var c = new int[] { 1, 9, 2, 8, 3, 7, 4, 6, 5 };

			Assert.IsTrue(a.SequenceContentEqualOrderBy(b));
			Assert.IsTrue(a.SequenceContentEqualOrderBy(c));
			Assert.IsTrue(b.SequenceContentEqualOrderBy(c));
		}

		[TestMethod]
		public void SequenceContentEqualByXor()
		{
			var a = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
			var b = new int[] { 9, 8, 7, 6, 5, 4, 3, 2, 1 };
			var c = new int[] { 1, 9, 2, 8, 3, 7, 4, 6, 5 };

			Assert.IsTrue(a.SequenceContentEqualByXor(b));
			Assert.IsTrue(a.SequenceContentEqualByXor(c));
			Assert.IsTrue(b.SequenceContentEqualByXor(c));
		}

		[TestMethod]
		public void SkipLastWhile()
		{
			CollectionAssert.AreEqual(new int[] { 17, 17, 19, 23, 23 }, integers.SkipLastWhile((e, i) => (i & 1) == 1).ToArray());
		}

		[TestMethod]
		public void SkipUntil()
		{
			CollectionAssert.AreEqual(new int[] { 23, 57 }, integers.SkipUntil((e, i) => i > 2).ToArray());
		}

		[TestMethod]
		public void StartsWith()
		{
			Assert.IsTrue(integers.StartsWith(new int[] { 17, 17, 19 }));
		}

		[TestMethod]
		public void TakeEvery()
		{
			CollectionAssert.AreEqual(new int[] { 17, 19, 23 }, integers.TakeEvery(2).ToArray());
		}

		[TestMethod]
		public void TakeLastWhile()
		{
			CollectionAssert.AreEqual(new int[] { 57 }, integers.TakeLastWhile((e, i) => (i & 1) == 1).ToArray());
		}

		[TestMethod]
		public void TakeUntil()
		{
			CollectionAssert.AreEqual(new int[] { 17, 17, 19, 23 }, integers.TakeUntil((e, i) => i > 2).ToArray());
		}
	}
}
