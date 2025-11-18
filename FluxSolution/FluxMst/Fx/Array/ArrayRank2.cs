using Flux;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SystemArrays
{
  [TestClass]
  public class ArrayRank2
  {
    [TestMethod]
    public void Fill()
    {
      var cso = ConsoleFormatOptions.Default with { UniformWidth = true };

      var inplaceMatrix = new int[,] {
        { 1, 0, 0, 0, 0, 2, 3, 4 },
        { 5, 0, 0, 0, 0, 6, 7, 8 },
        { 9, 0, 0, 0, 0, 10, 11, 12 },
        { 13, 0, 0, 0, 0, 14, 15, 16 },
      };

      var expected = new int[,] {
        { 1, 9, 9, 9, 9, 2, 3, 4 },
        { 5, 9, 9, 9, 9, 6, 7, 8 },
        { 9, 9, 9, 9, 9, 10, 11, 12 },
        { 13, 0, 0, 0, 0, 14, 15, 16 },
      };

      System.Console.WriteLine(nameof(inplaceMatrix));
      System.Console.WriteLine(inplaceMatrix.Rank2ToConsoleString(cso));
      System.Console.WriteLine();

      inplaceMatrix.Fill(0, 1, 3, 4, [9]);
      System.Console.WriteLine(nameof(ArrayRank1Extensions.FillWith));
      System.Console.WriteLine(inplaceMatrix.Rank2ToConsoleString(cso));
      System.Console.WriteLine();

      CollectionAssert.AreEqual(expected, inplaceMatrix);
    }

    [TestMethod]
    public void Flip0InPlace()
    {
      var cso = ConsoleFormatOptions.Default with { UniformWidth = true };

      System.Console.WriteLine(nameof(Flip0InPlace));
      System.Console.WriteLine();

      var expected = new int[3, 3]
      {
        { 7, 8, 9 },
        { 4, 5, 6 },
        { 1, 2, 3 },
      };

      System.Console.WriteLine(nameof(expected));
      System.Console.WriteLine(expected.Rank2ToConsoleString(cso));
      System.Console.WriteLine();

      var flip0InPlace = new int[3, 3]
      {
        { 1, 2, 3 },
        { 4, 5, 6 },
        { 7, 8, 9 },
      };

      System.Console.WriteLine("Initial");
      System.Console.WriteLine(flip0InPlace.Rank2ToConsoleString(cso));
      System.Console.WriteLine();

      flip0InPlace.Flip0();

      System.Console.WriteLine(nameof(flip0InPlace));
      System.Console.WriteLine(flip0InPlace.Rank2ToConsoleString(cso));
      System.Console.WriteLine();

      CollectionAssert.AreEqual(expected, flip0InPlace);
    }

    [TestMethod]
    public void Flip0ToCopy()
    {
      var cso = ConsoleFormatOptions.Default with { UniformWidth = true };

      System.Console.WriteLine(nameof(Flip0ToCopy));
      System.Console.WriteLine();

      var expected = new int[,] {
        { 13, 14, 15, 16 },
        { 9, 10, 11, 12 },
        { 5, 6, 7, 8 },
        { 1, 2, 3, 4 },
      };

      System.Console.WriteLine(nameof(expected));
      System.Console.WriteLine(expected.Rank2ToConsoleString(cso));
      System.Console.WriteLine();

      var initial = new int[,] {
        { 1, 2, 3, 4 },
        { 5, 6, 7, 8 },
        { 9, 10, 11, 12 },
        { 13, 14, 15, 16 },
      };

      System.Console.WriteLine(nameof(initial));
      System.Console.WriteLine(initial.Rank2ToConsoleString(cso));
      System.Console.WriteLine();

      var flip0ToCopy = new int[4, 4];
      initial.Flip0(flip0ToCopy);
      System.Console.WriteLine(nameof(flip0ToCopy));
      System.Console.WriteLine(flip0ToCopy.Rank2ToConsoleString(cso));
      System.Console.WriteLine();

      CollectionAssert.AreEqual(expected, flip0ToCopy);
    }

    [TestMethod]
    public void Flip1InPlace()
    {
      var cso = ConsoleFormatOptions.Default with { UniformWidth = true };

      System.Console.WriteLine(nameof(Flip1InPlace));
      System.Console.WriteLine();

      var expected = new int[3, 3]
      {
        { 3, 2, 1 },
        { 6, 5, 4 },
        { 9, 8, 7 },
      };

      System.Console.WriteLine(nameof(expected));
      System.Console.WriteLine(expected.Rank2ToConsoleString(cso));
      System.Console.WriteLine();

      var flip1InPlace = new int[3, 3]
      {
        { 1, 2, 3 },
        { 4, 5, 6 },
        { 7, 8, 9 },
      };

      System.Console.WriteLine("Initial");
      System.Console.WriteLine(flip1InPlace.Rank2ToConsoleString(cso));
      System.Console.WriteLine();

      flip1InPlace.Flip1();

      System.Console.WriteLine(nameof(flip1InPlace));
      System.Console.WriteLine(flip1InPlace.Rank2ToConsoleString(cso));
      System.Console.WriteLine();

      CollectionAssert.AreEqual(expected, flip1InPlace);
    }

    [TestMethod]
    public void Flip1ToCopy()
    {
      var cso = ConsoleFormatOptions.Default with { UniformWidth = true };

      System.Console.WriteLine(nameof(Flip1ToCopy));
      System.Console.WriteLine();

      var expected = new int[,] {
        { 4, 3, 2, 1 },
        { 8, 7, 6, 5 },
        { 12, 11, 10, 9 },
        { 16, 15, 14, 13 },
      };

      System.Console.WriteLine(nameof(expected));
      System.Console.WriteLine(expected.Rank2ToConsoleString(cso));
      System.Console.WriteLine();

      var initial = new int[,] {
        { 1, 2, 3, 4 },
        { 5, 6, 7, 8 },
        { 9, 10, 11, 12 },
        { 13, 14, 15, 16 },
      };

      System.Console.WriteLine(nameof(initial));
      System.Console.WriteLine(initial.Rank2ToConsoleString(cso));
      System.Console.WriteLine();

      var flip1ToCopy = new int[4, 4];
      initial.Flip1(flip1ToCopy);
      System.Console.WriteLine(nameof(flip1ToCopy));
      System.Console.WriteLine(flip1ToCopy.Rank2ToConsoleString(cso));
      System.Console.WriteLine();

      CollectionAssert.AreEqual(expected, flip1ToCopy);
    }

    [TestMethod]
    public void InsertToCopy()
    {
      var cso = ConsoleFormatOptions.Default with { UniformWidth = true };

      System.Console.WriteLine(nameof(InsertToCopy));
      System.Console.WriteLine();

      var expected = new int[,] {
        { 1, 2, 0, 0, 3, 4 },
        { 5, 6, 0, 0, 7, 8 },
        { 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0 },
        { 9, 10, 0, 0, 11, 12 },
        { 13, 14, 0, 0, 15, 16 },
      };

      System.Console.WriteLine(nameof(expected));
      System.Console.WriteLine(expected.Rank2ToConsoleString(cso));
      System.Console.WriteLine();

      var initial = new int[,] {
        { 1, 2, 3, 4 },
        { 5, 6, 7, 8 },
        { 9, 10, 11, 12 },
        { 13, 14, 15, 16 },
      };

      System.Console.WriteLine(nameof(initial));
      System.Console.WriteLine(initial.Rank2ToConsoleString(cso));
      System.Console.WriteLine();

      var insertToCopy = initial.InsertToCopy(0, 2, 2, [0]);

      System.Console.WriteLine($"{nameof(insertToCopy)}: 2 rows");
      System.Console.WriteLine(insertToCopy.Rank2ToConsoleString(cso));
      System.Console.WriteLine();

      insertToCopy = insertToCopy.InsertToCopy(1, 2, 2, [0]);

      System.Console.WriteLine($"{nameof(insertToCopy)}: 2 columns");
      System.Console.WriteLine(insertToCopy.Rank2ToConsoleString(cso));
      System.Console.WriteLine();

      CollectionAssert.AreEqual(expected, insertToCopy);
    }

    [TestMethod]
    public void RemoveToCopy()
    {
      var cso = ConsoleFormatOptions.Default with { UniformWidth = true };

      System.Console.WriteLine(nameof(RemoveToCopy));
      System.Console.WriteLine();

      var expected = new int[,] {
        { 1, 4 },
        { 13, 16 },
      };

      System.Console.WriteLine(nameof(expected));
      System.Console.WriteLine(expected.Rank2ToConsoleString(cso));
      System.Console.WriteLine();

      var initial = new int[,] {
        { 1, 2, 3, 4 },
        { 5, 6, 7, 8 },
        { 9, 10, 11, 12 },
        { 13, 14, 15, 16 },
      };

      System.Console.WriteLine(nameof(initial));
      System.Console.WriteLine(initial.Rank2ToConsoleString(cso));
      System.Console.WriteLine();

      var removeToCopy = initial.Copy0Remove(initial.NewResize(-2, 0), [1, 2]);

      System.Console.WriteLine($"{nameof(removeToCopy)}: 2 rows");
      System.Console.WriteLine(removeToCopy.Rank2ToConsoleString(cso));
      System.Console.WriteLine();

      removeToCopy = removeToCopy.Copy1Remove(removeToCopy.NewResize(0, -2), [1, 2]);

      System.Console.WriteLine($"{nameof(removeToCopy)}: 2 columns");
      System.Console.WriteLine(removeToCopy.Rank2ToConsoleString(cso));
      System.Console.WriteLine();

      CollectionAssert.AreEqual(expected, removeToCopy);
    }

    [TestMethod]
    public void RotateCcwInPlace()
    {
      var cso = ConsoleFormatOptions.Default with { UniformWidth = true };

      System.Console.WriteLine(nameof(RotateCcwInPlace));
      System.Console.WriteLine();

      var expected = new int[,] {
        { 4, 8, 12, 16 },
        { 3, 7, 11, 15 },
        { 2, 6, 10, 14 },
        { 1, 5, 9, 13 },
      };

      System.Console.WriteLine(nameof(expected));
      System.Console.WriteLine(expected.Rank2ToConsoleString(cso));
      System.Console.WriteLine();

      var rotateCcwInPlace = new int[,] {
        { 1, 2, 3, 4 },
        { 5, 6, 7, 8 },
        { 9, 10, 11, 12 },
        { 13, 14, 15, 16 },
      };

      System.Console.WriteLine("Initial");
      System.Console.WriteLine(rotateCcwInPlace.Rank2ToConsoleString(cso));
      System.Console.WriteLine();

      rotateCcwInPlace.RotateCcw();

      System.Console.WriteLine(nameof(rotateCcwInPlace));
      System.Console.WriteLine(rotateCcwInPlace.Rank2ToConsoleString(cso));
      System.Console.WriteLine();

      CollectionAssert.AreEqual(expected, rotateCcwInPlace);
    }

    [TestMethod]
    public void RotateCcwToCopy()
    {
      var cso = ConsoleFormatOptions.Default with { UniformWidth = true };

      System.Console.WriteLine(nameof(RotateCcwToCopy));
      System.Console.WriteLine();

      var expected = new int[,] {
        { 4, 8, 12, 16 },
        { 3, 7, 11, 15 },
        { 2, 6, 10, 14 },
        { 1, 5, 9, 13 },
      };

      System.Console.WriteLine(nameof(expected));
      System.Console.WriteLine(expected.Rank2ToConsoleString(cso));
      System.Console.WriteLine();

      var initial = new int[,] {
        { 1, 2, 3, 4 },
        { 5, 6, 7, 8 },
        { 9, 10, 11, 12 },
        { 13, 14, 15, 16 },
      };

      System.Console.WriteLine(nameof(initial));
      System.Console.WriteLine(initial.Rank2ToConsoleString(cso));
      System.Console.WriteLine();

      var rotateCcwToCopy = new int[4, 4];
      initial.RotateCcw(rotateCcwToCopy);
      System.Console.WriteLine(nameof(rotateCcwToCopy));
      System.Console.WriteLine(rotateCcwToCopy.Rank2ToConsoleString(cso));
      System.Console.WriteLine();

      CollectionAssert.AreEqual(expected, rotateCcwToCopy);
    }

    [TestMethod]
    public void RotateCwInPlace()
    {
      var cso = ConsoleFormatOptions.Default with { UniformWidth = true };

      System.Console.WriteLine(nameof(RotateCwInPlace));
      System.Console.WriteLine();

      var expected = new int[,] {
        { 13, 9, 5, 1 },
        { 14, 10, 6, 2 },
        { 15, 11, 7, 3 },
        { 16, 12, 8, 4 },
      };

      System.Console.WriteLine(nameof(expected));
      System.Console.WriteLine(expected.Rank2ToConsoleString(cso));
      System.Console.WriteLine();

      var rotateCwInPlace = new int[,] {
        { 1, 2, 3, 4 },
        { 5, 6, 7, 8 },
        { 9, 10, 11, 12 },
        { 13, 14, 15, 16 },
      };

      System.Console.WriteLine("Initial");
      System.Console.WriteLine(rotateCwInPlace.Rank2ToConsoleString(cso));
      System.Console.WriteLine();

      rotateCwInPlace.RotateCw();

      System.Console.WriteLine(nameof(rotateCwInPlace));
      System.Console.WriteLine(rotateCwInPlace.Rank2ToConsoleString(cso));
      System.Console.WriteLine();

      CollectionAssert.AreEqual(expected, rotateCwInPlace);
    }

    [TestMethod]
    public void RotateCwToCopy()
    {
      var cso = ConsoleFormatOptions.Default with { UniformWidth = true };

      System.Console.WriteLine(nameof(RotateCwToCopy));
      System.Console.WriteLine();

      var expected = new int[,] {
        { 13, 9, 5, 1 },
        { 14, 10, 6, 2 },
        { 15, 11, 7, 3 },
        { 16, 12, 8, 4 },
      };

      System.Console.WriteLine(nameof(expected));
      System.Console.WriteLine(expected.Rank2ToConsoleString(cso));
      System.Console.WriteLine();

      var initial = new int[,] {
        { 1, 2, 3, 4 },
        { 5, 6, 7, 8 },
        { 9, 10, 11, 12 },
        { 13, 14, 15, 16 },
      };

      System.Console.WriteLine(nameof(initial));
      System.Console.WriteLine(initial.Rank2ToConsoleString(cso));
      System.Console.WriteLine();

      var rotateCwToCopy = new int[4, 4];

      initial.RotateCw(rotateCwToCopy);

      System.Console.WriteLine(nameof(rotateCwToCopy));
      System.Console.WriteLine(rotateCwToCopy.Rank2ToConsoleString(cso));
      System.Console.WriteLine();

      CollectionAssert.AreEqual(expected, rotateCwToCopy);
    }

    [TestMethod]
    public void TransposeInPlace()
    {
      var cso = ConsoleFormatOptions.Default with { UniformWidth = true };

      System.Console.WriteLine(nameof(TransposeInPlace));
      System.Console.WriteLine();

      var expected = new int[,] {
        { 1, 5, 9, 13 },
        { 2, 6, 10, 14 },
        { 3, 7, 11, 15 },
        { 4, 8, 12, 16 },
      };

      System.Console.WriteLine(nameof(expected));
      System.Console.WriteLine(expected.Rank2ToConsoleString(cso));
      System.Console.WriteLine();

      var transposeInPlace = new int[,] {
        { 1, 2, 3, 4 },
        { 5, 6, 7, 8 },
        { 9, 10, 11, 12 },
        { 13, 14, 15, 16 },
      };

      System.Console.WriteLine("Initial");
      System.Console.WriteLine(transposeInPlace.Rank2ToConsoleString(cso));
      System.Console.WriteLine();

      transposeInPlace.Transpose();

      System.Console.WriteLine(nameof(transposeInPlace));
      System.Console.WriteLine(transposeInPlace.Rank2ToConsoleString(cso));
      System.Console.WriteLine();

      CollectionAssert.AreEqual(expected, transposeInPlace);
    }

    [TestMethod]
    public void TransposeToCopy()
    {
      var cso = ConsoleFormatOptions.Default with { UniformWidth = true };

      System.Console.WriteLine(nameof(TransposeToCopy));
      System.Console.WriteLine();

      var expected = new int[,] {
        { 1, 5, 9, 13 },
        { 2, 6, 10, 14 },
        { 3, 7, 11, 15 },
        { 4, 8, 12, 16 },
      };

      System.Console.WriteLine(nameof(expected));
      System.Console.WriteLine(expected.Rank2ToConsoleString(cso));
      System.Console.WriteLine();

      var initial = new int[,] {
        { 1, 2, 3, 4 },
        { 5, 6, 7, 8 },
        { 9, 10, 11, 12 },
        { 13, 14, 15, 16 },
      };

      System.Console.WriteLine(nameof(initial));
      System.Console.WriteLine(initial.Rank2ToConsoleString(cso));
      System.Console.WriteLine();

      var transposeToCopy = new int[4, 4];

      initial.Transpose(transposeToCopy);

      System.Console.WriteLine(nameof(transposeToCopy));
      System.Console.WriteLine(transposeToCopy.Rank2ToConsoleString(cso));
      System.Console.WriteLine();

      CollectionAssert.AreEqual(expected, transposeToCopy);
    }
  }
}
