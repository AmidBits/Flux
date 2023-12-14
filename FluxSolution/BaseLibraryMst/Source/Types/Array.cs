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
      var actual = original.ToCopy(0, 0, 3, 3, 0, 0, 0, 0);

      CollectionAssert.AreEqual(original, actual);
    }

    [TestMethod]
    public void EmFillDim0()
    {
      var clone = (int[,])original.Clone();

      CollectionAssert.AreEqual(original, clone);

      var array0 = clone.GetAllElements(0).Select(vt => vt.item).ToArray();

      var original0 = new int[9] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

      CollectionAssert.AreEqual(original0, array0);
    }

    [TestMethod]
    public void EmFillDim1()
    {
      var clone = (int[,])original.Clone();

      CollectionAssert.AreEqual(original, clone);

      var array1 = clone.GetAllElements(1).Select(vt => vt.item).ToArray();

      var original1 = new int[9] { 1, 4, 7, 2, 5, 8, 3, 6, 9 };

      CollectionAssert.AreEqual(original1, array1);
    }

    [TestMethod]
    public void EmFlipDim0()
    {
      var flip0 = (int[,])original.FlipToCopy(0);

      var expected = new int[3, 3] { { 7, 8, 9 }, { 4, 5, 6 }, { 1, 2, 3 } };

      CollectionAssert.AreEqual(expected, flip0);
    }

    [TestMethod]
    public void EmFlipDim1()
    {
      var flip1 = (int[,])original.FlipToCopy(1);

      var expected = new int[3, 3] { { 3, 2, 1 }, { 6, 5, 4 }, { 9, 8, 7 } };

      CollectionAssert.AreEqual(expected, flip1);
    }

    [TestMethod]
    public void EmFlipInPlaceDim0()
    {
      var copyThenFlip0 = (int[,])original.Clone();

      Flux.Fx.FlipInPlace(copyThenFlip0, 0);

      var expected = new int[3, 3] { { 7, 8, 9 }, { 4, 5, 6 }, { 1, 2, 3 } };

      CollectionAssert.AreEqual(expected, copyThenFlip0);
    }

    [TestMethod]
    public void EmFlipInPlaceDim1()
    {
      var copyThenFlip1 = (int[,])original.Clone();

      Flux.Fx.FlipInPlace(copyThenFlip1, 1);

      var expected = new int[3, 3] { { 3, 2, 1 }, { 6, 5, 4 }, { 9, 8, 7 } };

      CollectionAssert.AreEqual(expected, copyThenFlip1);
    }

    [TestMethod]
    public void EmInsertDim0()
    {
      var insert0 = (int[,])original.InsertToCopy(0, 1, 1, -1, -2, -3);

      var expected = new int[4, 3] { { 1, 2, 3 }, { -1, -2, -3 }, { 4, 5, 6 }, { 7, 8, 9 } };

      CollectionAssert.AreEqual(expected, insert0);
    }

    [TestMethod]
    public void EmInsertDim1()
    {
      var insert1 = (int[,])original.InsertToCopy(1, 1, 1, -1, -2, -3);

      var expected = new int[3, 4] { { 1, -1, 2, 3 }, { 4, -2, 5, 6 }, { 7, -3, 8, 9 } };

      CollectionAssert.AreEqual(expected, insert1);
    }

    [TestMethod]
    public void EmRemoveDim0()
    {
      var remove0 = original.RemoveToCopy(0, 1);

      var expected = new int[2, 3] { { 1, 2, 3 }, { 7, 8, 9 } };

      CollectionAssert.AreEqual(expected, remove0);
    }

    [TestMethod]
    public void EmRemoveDim1()
    {
      var remove1 = original.RemoveToCopy(1, 1);

      var expected = new int[3, 2] { { 1, 3 }, { 4, 6 }, { 7, 9 } };

      CollectionAssert.AreEqual(expected, remove1);
    }

    [TestMethod]
    public void EmRotateClockwise()
    {
      var rotateCw = original.RotateToCopyCw();

      var expected = new int[3, 3] { { 7, 4, 1 }, { 8, 5, 2 }, { 9, 6, 3 } };

      CollectionAssert.AreEqual(expected, rotateCw);
    }

    [TestMethod]
    public void EmRotateCounterClockwise()
    {
      var rotateCcw = original.RotateToCopyCcw();

      var expected = new int[3, 3] { { 3, 6, 9 }, { 2, 5, 8 }, { 1, 4, 7 } };

      CollectionAssert.AreEqual(expected, rotateCcw);
    }

    [TestMethod]
    public void EmTranspose()
    {
      var transpose = original.TransposeToCopy();

      var expected = new int[3, 3] { { 1, 4, 7 }, { 2, 5, 8 }, { 3, 6, 9 } };

      CollectionAssert.AreEqual(expected, transpose);
    }

    [TestMethod]
    public void EmTransposeRef()
    {
      var copyThenTranspose = (int[,])original.Clone();

      Flux.Fx.TransposeInPlace(copyThenTranspose);

      var expected = new int[3, 3] { { 1, 4, 7 }, { 2, 5, 8 }, { 3, 6, 9 } };

      CollectionAssert.AreEqual(expected, copyThenTranspose);
    }

  }
}
