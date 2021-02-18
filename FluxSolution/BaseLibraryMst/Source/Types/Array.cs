using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Flux;

namespace Types
{
	[TestClass]
	public class Array
	{
		readonly int[,] original = new int[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };

		[TestMethod]
		public void EmCreate()
		{
			var actual = original.Duplicate(3, 3, 3, 3, 0, 0, 0, 0);

			CollectionAssert.AreEqual(original, actual);
		}

		[TestMethod]
		public void EmFillDim0()
		{
			var clone = (int[,])original.Clone();

			CollectionAssert.AreEqual(original, clone);

			var array0 = clone.GetElements(0).Select(vt => vt.item).ToArray();

			var original0 = new int[9] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

			CollectionAssert.AreEqual(original0, array0);
		}

		[TestMethod]
		public void EmFillDim1()
		{
			var clone = (int[,])original.Clone();

			CollectionAssert.AreEqual(original, clone);

			var array1 = clone.GetElements(1).Select(vt => vt.item).ToArray();

			var original1 = new int[9] { 1, 4, 7, 2, 5, 8, 3, 6, 9 };

			CollectionAssert.AreEqual(original1, array1);
		}

		[TestMethod]
		public void EmInsertDim0()
		{
			var copy0 = (int[,])original.Insert(0, 1, -1, -2, -3);

			var insert0 = new int[4, 3] { { 1, 2, 3 }, { -1, -2, -3 }, { 4, 5, 6 }, { 7, 8, 9 } };

			CollectionAssert.AreEqual(insert0, copy0);
		}

		[TestMethod]
		public void EmInsertDim1()
		{
			var copy1 = (int[,])original.Insert(1, 1, -1, -2, -3);

			var insert1 = new int[3, 4] { { 1, -1, 2, 3 }, { 4, -2, 5, 6 }, { 7, -3, 8, 9 } };

			CollectionAssert.AreEqual(insert1, copy1);
		}

		[TestMethod]
		public void EmRemoveDim0()
		{
			var copy0 = original.Remove(0, 1);

			var remove0 = new int[2, 3] { { 1, 2, 3 }, { 7, 8, 9 } };

			CollectionAssert.AreEqual(remove0, copy0);
		}

		[TestMethod]
		public void EmRemoveDim1()
		{
			var copy1 = original.Remove(1, 1);

			var remove1 = new int[3, 2] { { 1, 3 }, { 4, 6 }, { 7, 9 } };

			CollectionAssert.AreEqual(remove1, copy1);
		}

		[TestMethod]
		public void EmReverseDim0()
		{
			var copy0 = original.Flip(0);

			var reverse0 = new int[3, 3] { { 3, 2, 1 }, { 6, 5, 4 }, { 9, 8, 7 } };

			CollectionAssert.AreEqual(reverse0, copy0);
		}

		[TestMethod]
		public void EmReverseDim1()
		{
			var copy1 = original.Flip(1);

			var reverse1 = new int[3, 3] { { 7, 8, 9 }, { 4, 5, 6 }, { 1, 2, 3 } };

			CollectionAssert.AreEqual(reverse1, copy1);
		}

		[TestMethod]
		public void EmReverseInPlaceDim0()
		{
			var copy0 = (int[,])original.Clone();

			Flux.ArrayRank2.FlipInPlace(ref copy0, 0);

			var reverse0 = new int[3, 3] { { 3, 2, 1 }, { 6, 5, 4 }, { 9, 8, 7 } };

			CollectionAssert.AreEqual(reverse0, copy0);
		}

		[TestMethod]
		public void EmReverseInPlaceDim1()
		{
			var copy1 = (int[,])original.Clone();

			Flux.ArrayRank2.FlipInPlace(ref copy1, 1);

			var reverse1 = new int[3, 3] { { 7, 8, 9 }, { 4, 5, 6 }, { 1, 2, 3 } };

			CollectionAssert.AreEqual(reverse1, copy1);
		}

		[TestMethod]
		public void EmRotateClockwise()
		{
			var copy = original.RotateClockwise();

			var rotate = new int[3, 3] { { 7, 4, 1 }, { 8, 5, 2 }, { 9, 6, 3 } };

			CollectionAssert.AreEqual(rotate, copy);
		}

		[TestMethod]
		public void EmRotateCounterClockwise()
		{
			var copy = original.RotateCounterClockwise();

			var rotate = new int[3, 3] { { 3, 6, 9 }, { 2, 5, 8 }, { 1, 4, 7 } };

			CollectionAssert.AreEqual(rotate, copy);
		}

		[TestMethod]
		public void EmTranspose()
		{
			var copy = original.Transpose();

			var reverse = new int[3, 3] { { 1, 4, 7 }, { 2, 5, 8 }, { 3, 6, 9 } };

			CollectionAssert.AreEqual(reverse, copy);
		}

		[TestMethod]
		public void EmTransposeInPlace()
		{
			var copy = (int[,])original.Clone();

			Flux.ArrayRank2.TransposeInPlace(ref copy);

			var reverse = new int[3, 3] { { 1, 4, 7 }, { 2, 5, 8 }, { 3, 6, 9 } };

			CollectionAssert.AreEqual(reverse, copy);
		}

	}
}
