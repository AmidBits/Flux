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

      inplaceMatrix.FillWith(0, 1, 3, 4, 9);
      System.Console.WriteLine(nameof(Arrays.FillWith));
      System.Console.WriteLine(inplaceMatrix.Rank2ToConsoleString(cso));
      System.Console.WriteLine();

      CollectionAssert.AreEqual(expected, inplaceMatrix);
    }

    [TestMethod]
    public void FlipInPlace()
    {
      var inplaceMatrix = new int[3, 3]
      {
        { 1, 2, 3 },
        { 4, 5, 6 },
        { 7, 8, 9 },
      };

      var expected0 = new int[3, 3]
      {
        { 7, 8, 9 },
        { 4, 5, 6 },
        { 1, 2, 3 },
      };

      inplaceMatrix.FlipInPlace(0);

      CollectionAssert.AreEqual(expected0, inplaceMatrix);

      var expected1 = new int[3, 3]
      {
        { 9, 8, 7 },
        { 6, 5, 4 },
        { 3, 2, 1 },
      };

      inplaceMatrix.FlipInPlace(1);

      CollectionAssert.AreEqual(expected1, inplaceMatrix);
    }

    [TestMethod]
    public void FlipToCopy()
    {
      var cso = ConsoleFormatOptions.Default with { UniformWidth = true };

      var matrix = new int[,] {
        { 1, 2, 3, 4 },
        { 5, 6, 7, 8 },
        { 9, 10, 11, 12 },
        { 13, 14, 15, 16 },
      };

      var expected0 = new int[,] {
        { 13, 14, 15, 16 },
        { 9, 10, 11, 12 },
        { 5, 6, 7, 8 },
        { 1, 2, 3, 4 },
      };

      System.Console.WriteLine(nameof(matrix));
      System.Console.WriteLine(matrix.Rank2ToConsoleString(cso));
      System.Console.WriteLine();

      var actual0 = matrix.FlipToCopy(0);
      System.Console.WriteLine(nameof(Arrays.FlipToCopy));
      System.Console.WriteLine(actual0.Rank2ToConsoleString(cso));
      System.Console.WriteLine();

      CollectionAssert.AreEqual(expected0, actual0);

      var expected1 = new int[,] {
        { 4, 3, 2, 1 },
        { 8, 7, 6, 5 },
        { 12, 11, 10, 9 },
        { 16, 15, 14, 13 },
      };

      var actual1 = matrix.FlipToCopy(0);
      System.Console.WriteLine(nameof(Arrays.FlipToCopy));
      System.Console.WriteLine(actual1.Rank2ToConsoleString(cso));
      System.Console.WriteLine();

      CollectionAssert.AreEqual(expected0, actual1);

    }

    [TestMethod]
    public void InsertToCopy()
    {
      var cso = ConsoleFormatOptions.Default with { UniformWidth = true };

      var matrix = new int[,] {
        { 1, 2, 3, 4 },
        { 5, 6, 7, 8 },
        { 9, 10, 11, 12 },
        { 13, 14, 15, 16 },
      };

      var expected = new int[,] {
        { 1, 0, 0, 0, 0, 2, 3, 4 },
        { 5, 0, 0, 0, 0, 6, 7, 8 },
        { 9, 0, 0, 0, 0, 10, 11, 12 },
        { 13, 0, 0, 0, 0, 14, 15, 16 },
      };

      System.Console.WriteLine(nameof(matrix));
      System.Console.WriteLine(matrix.Rank2ToConsoleString(cso));
      System.Console.WriteLine();

      var actual = matrix.InsertToCopy(1, 1, 4, 0);
      System.Console.WriteLine(nameof(Arrays.InsertToCopy));
      System.Console.WriteLine(actual.Rank2ToConsoleString(cso));
      System.Console.WriteLine();

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void RemoveToCopy()
    {
      var cso = ConsoleFormatOptions.Default with { UniformWidth = true };

      var matrix = new int[,] {
        { 1, 2, 3, 4 },
        { 5, 6, 7, 8 },
        { 9, 10, 11, 12 },
        { 13, 14, 15, 16 },
      };

      var expected = new int[,] {
        { 1, 3, 4 },
        { 5, 7, 8 },
        { 9, 11, 12 },
        { 13, 15, 16 },
      };

      System.Console.WriteLine(nameof(matrix));
      System.Console.WriteLine(matrix.Rank2ToConsoleString(cso));
      System.Console.WriteLine();

      var actual = matrix.RemoveToCopy(1, 1);
      System.Console.WriteLine(nameof(Arrays.RemoveToCopy));
      System.Console.WriteLine(actual.Rank2ToConsoleString(cso));
      System.Console.WriteLine();

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void RotateToCopyCcw()
    {
      var cso = ConsoleFormatOptions.Default with { UniformWidth = true };

      var matrix = new int[,] {
        { 1, 2, 3, 4 },
        { 5, 6, 7, 8 },
        { 9, 10, 11, 12 },
        { 13, 14, 15, 16 },
      };

      var expected = new int[,] {
        { 4, 8, 12, 16 },
        { 3, 7, 11, 15 },
        { 2, 6, 10, 14 },
        { 1, 5, 9, 13 },
      };

      System.Console.WriteLine(nameof(matrix));
      System.Console.WriteLine(matrix.Rank2ToConsoleString(cso));
      System.Console.WriteLine();

      var actual = matrix.RotateToCopy(RotationalDirection.CounterClockWise);
      System.Console.WriteLine(nameof(Fx.RotateToCopyCcw));
      System.Console.WriteLine(actual.Rank2ToConsoleString(cso));
      System.Console.WriteLine();

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void RotateToCopyCw()
    {
      var cso = ConsoleFormatOptions.Default with { UniformWidth = true };

      var matrix = new int[,] {
        { 1, 2, 3, 4 },
        { 5, 6, 7, 8 },
        { 9, 10, 11, 12 },
        { 13, 14, 15, 16 },
      };

      var expected = new int[,] {
        { 13, 9, 5, 1 },
        { 14, 10, 6, 2 },
        { 15, 11, 7, 3 },
        { 16, 12, 8, 4 },
      };

      System.Console.WriteLine(nameof(matrix));
      System.Console.WriteLine(matrix.Rank2ToConsoleString(cso));
      System.Console.WriteLine();

      var actual = matrix.RotateToCopy(RotationalDirection.ClockWise);
      System.Console.WriteLine(nameof(Fx.RotateToCopyCw));
      System.Console.WriteLine(actual.Rank2ToConsoleString(cso));
      System.Console.WriteLine();

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TransposeInPlace()
    {
      var cso = ConsoleFormatOptions.Default with { UniformWidth = true };

      var inplaceMatrix = new int[,] {
        { 1, 2, 3, 4 },
        { 5, 6, 7, 8 },
        { 9, 10, 11, 12 },
        { 13, 14, 15, 16 },
      };

      System.Console.WriteLine(nameof(inplaceMatrix));
      System.Console.WriteLine(inplaceMatrix.Rank2ToConsoleString(cso));
      System.Console.WriteLine();

      var expected = new int[,] {
        { 1, 5, 9, 13 },
        { 2, 6, 10, 14 },
        { 3, 7, 11, 15 },
        { 4, 8, 12, 16 },
      };

      inplaceMatrix.TransposeInPlace();
      System.Console.WriteLine(nameof(Arrays.TransposeInPlace));
      System.Console.WriteLine(inplaceMatrix.Rank2ToConsoleString(cso));
      System.Console.WriteLine();

      CollectionAssert.AreEqual(expected, inplaceMatrix);
    }

    [TestMethod]
    public void TransposeToCopy()
    {
      var cso = ConsoleFormatOptions.Default with { UniformWidth = true };

      var matrix = new int[,] {
        { 1, 2, 3, 4 },
        { 5, 6, 7, 8 },
        { 9, 10, 11, 12 },
        { 13, 14, 15, 16 },
      };

      var expected = new int[,] {
        { 1, 5, 9, 13 },
        { 2, 6, 10, 14 },
        { 3, 7, 11, 15 },
        { 4, 8, 12, 16 },
      };

      System.Console.WriteLine(nameof(matrix));
      System.Console.WriteLine(matrix.Rank2ToConsoleString(cso));
      System.Console.WriteLine();

      var actual = matrix.TransposeToCopy();
      System.Console.WriteLine(nameof(Fx.TransposeToCopy));
      System.Console.WriteLine(actual.Rank2ToConsoleString(cso));
      System.Console.WriteLine();

      CollectionAssert.AreEqual(expected, actual);
    }
  }
}
