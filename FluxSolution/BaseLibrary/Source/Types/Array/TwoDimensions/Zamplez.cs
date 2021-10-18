#if ZAMPLEZ
namespace Flux
{
  public static partial class Zamplez
  {
    public static void RunArrayRank2()
    {
      var m = new int[,] {
        { 1, 2, 3, 4 },
        { 5, 6, 7, 8 },
        { 9, 10, 11, 12 },
      };
      System.Console.WriteLine(nameof(m));
      System.Console.WriteLine(m.ToConsoleBlock(uniformWidth: true));
      System.Console.WriteLine();

      var mrotatec = m.RotateClockwise();
      System.Console.WriteLine(nameof(mrotatec));
      System.Console.WriteLine(mrotatec.ToConsoleBlock(uniformWidth: true));
      System.Console.WriteLine();

      var mrotatecc = m.RotateCounterClockwise();
      System.Console.WriteLine(nameof(mrotatecc));
      System.Console.WriteLine(mrotatecc.ToConsoleBlock(uniformWidth: true));
      System.Console.WriteLine();

      var mtranspose = m.Transpose();
      System.Console.WriteLine(nameof(mtranspose));
      System.Console.WriteLine(mtranspose.ToConsoleBlock(uniformWidth: true));
      System.Console.WriteLine();

      var minsert = m.Insert(1, 1, 4, 0);
      System.Console.WriteLine(nameof(minsert));
      System.Console.WriteLine(minsert.ToConsoleBlock(uniformWidth: true));
      System.Console.WriteLine();
      minsert.Fill(0, 1, 3, 4, 9);
      System.Console.WriteLine(nameof(minsert.Fill));
      System.Console.WriteLine(minsert.ToConsoleBlock(uniformWidth: true));
      System.Console.WriteLine();

      var mflip0 = m.Flip(0);
      System.Console.WriteLine(nameof(mflip0));
      System.Console.WriteLine(mflip0.ToConsoleBlock(uniformWidth: true));
      System.Console.WriteLine();

      var mflip1 = m.Flip(1);
      System.Console.WriteLine(nameof(mflip1));
      System.Console.WriteLine(mflip1.ToConsoleBlock(uniformWidth: true));
      System.Console.WriteLine();

      var mremove = m.Remove(1, 1);
      System.Console.WriteLine(nameof(mremove));
      System.Console.WriteLine(mremove.ToConsoleBlock(uniformWidth: true));
      System.Console.WriteLine();
    }
  }
}
#endif
