#if ZAMPLEZ
namespace Flux
{
  public static partial class Zamplez
  {
    /// <summary>Run the two-dimensional (rank equals 2) array zample.</summary>
    public static void RunArrayRank2()
    {
      System.Console.WriteLine();
      System.Console.WriteLine(nameof(RunArrayRank2));
      System.Console.WriteLine();

      var matrix = new int[,] {
        { 1, 2, 3, 4 },
        { 5, 6, 7, 8 },
        { 9, 10, 11, 12 },
        { 13, 14, 15, 16 },
      };

      var padding = 2;
      padding++;

      var (minLeft, minTop, maxLeft, maxTop) = matrix.ToConsoleStrings(uniformWidth: true).Prepend("Array-2D").WriteToConsole();

      var mrotatec = matrix.RotateClockwise();
      (minLeft, minTop, maxLeft, maxTop) = mrotatec.ToConsoleStrings(uniformWidth: true).Prepend("Rotated-CW").WriteToConsole(maxLeft + padding, minTop);

      var mrotatecc = matrix.RotateCounterClockwise();
      (minLeft, minTop, maxLeft, maxTop) = mrotatecc.ToConsoleStrings(uniformWidth: true).Prepend("Rotated-CCW").WriteToConsole(maxLeft + padding, minTop);

      var mtranspose = matrix.TransposeToCopy();
      (minLeft, minTop, maxLeft, maxTop) = mtranspose.ToConsoleStrings(uniformWidth: true).Prepend("Transposed").WriteToConsole(maxLeft + padding, minTop);

      var minsert = matrix.Insert(1, 1, 4, 0);
      (minLeft, minTop, maxLeft, maxTop) = minsert.ToConsoleStrings(uniformWidth: true).Prepend("Inserted").WriteToConsole(maxLeft + padding, minTop);
      minsert.Fill(0, 1, 3, 4, 9);
      (minLeft, minTop, maxLeft, maxTop) = minsert.ToConsoleStrings(uniformWidth: true).Prepend("Fill (Inserted)").WriteToConsole(maxLeft + padding, minTop);

      var mflip0 = matrix.FlipToCopy(0);
      (minLeft, minTop, maxLeft, maxTop) = mflip0.ToConsoleStrings(uniformWidth: true).Prepend("Flip_0").WriteToConsole(maxLeft + padding, minTop);

      var mflip1 = matrix.FlipToCopy(1);
      (minLeft, minTop, maxLeft, maxTop) = mflip1.ToConsoleStrings(uniformWidth: true).Prepend("Flip_1").WriteToConsole(maxLeft + padding, minTop);

      var mremove = matrix.Remove(1, 1);
      (minLeft, minTop, maxLeft, maxTop) = mremove.ToConsoleStrings(uniformWidth: true).Prepend("Remove").WriteToConsole(maxLeft + padding, minTop);

      //System.Console.WriteLine(nameof(matrix));
      //System.Console.WriteLine(matrix.ToConsoleBlock(uniformWidth: true));
      //System.Console.WriteLine();

      //var mrotatec = matrix.RotateClockwise();
      //System.Console.WriteLine(nameof(ExtensionMethods.RotateClockwise));
      //System.Console.WriteLine(mrotatec.ToConsoleBlock(uniformWidth: true));
      //System.Console.WriteLine();

      //var mrotatecc = matrix.RotateCounterClockwise();
      //System.Console.WriteLine(nameof(ExtensionMethods.RotateCounterClockwise));
      //System.Console.WriteLine(mrotatecc.ToConsoleBlock(uniformWidth: true));
      //System.Console.WriteLine();

      //var mtranspose = matrix.TransposeToCopy();
      //System.Console.WriteLine(nameof(ExtensionMethods.Transpose));
      //System.Console.WriteLine(mtranspose.ToConsoleBlock(uniformWidth: true));
      //System.Console.WriteLine();

      //var minsert = matrix.Insert(1, 1, 4, 0);
      //System.Console.WriteLine(nameof(ArrayRank2.Insert));
      //System.Console.WriteLine(minsert.ToConsoleBlock(uniformWidth: true));
      //System.Console.WriteLine();
      //minsert.Fill(0, 1, 3, 4, 9);
      //System.Console.WriteLine(nameof(ArrayRank2.Fill));
      //System.Console.WriteLine(minsert.ToConsoleBlock(uniformWidth: true));
      //System.Console.WriteLine();

      //var mflip0 = matrix.FlipToCopy(0);
      //System.Console.WriteLine(nameof(ArrayRank2.FlipToCopy));
      //System.Console.WriteLine(mflip0.ToConsoleBlock(uniformWidth: true));
      //System.Console.WriteLine();

      //var mflip1 = matrix.FlipToCopy(1);
      //System.Console.WriteLine(nameof(ArrayRank2.FlipToCopy));
      //System.Console.WriteLine(mflip1.ToConsoleBlock(uniformWidth: true));
      //System.Console.WriteLine();

      //var mremove = matrix.Remove(1, 1);
      //System.Console.WriteLine(nameof(ArrayRank2.Remove));
      //System.Console.WriteLine(mremove.ToConsoleBlock(uniformWidth: true));
      //System.Console.WriteLine();
    }
  }
}
#endif
