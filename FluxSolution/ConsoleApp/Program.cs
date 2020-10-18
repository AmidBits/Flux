using Flux;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ConsoleApp
{
  class Program
  {
    private static void TimedMain(string[] args)
    {
Flux.Media.HexSide
      var p = new System.Numerics.Vector3[] { new System.Numerics.Vector3(-1, -1, 0), new System.Numerics.Vector3(-1, 1, 0), new System.Numerics.Vector3(1, 1, 0), new System.Numerics.Vector3(1, -1, 0) };

      System.Console.WriteLine($"AS  : {p.AngleSum(System.Numerics.Vector3.Zero)}");
      System.Console.WriteLine($"ASpb: {p.AngleSumPB(System.Numerics.Vector3.Zero)}");

      var size = new Flux.Geometry.Size2(10, 10);

      var ships = Flux.Model.Game.BattleShip.Ship.StageFleet(size, 2, 3, 3, 4, 5);

      do
      {
        System.Console.SetCursorPosition(0, 0);

        var shots = System.Linq.Enumerable.Range(0, 25).Select(n => new Flux.Geometry.Point2(Flux.Random.NumberGenerator.Crypto.Next(size.Width), Flux.Random.NumberGenerator.Crypto.Next(size.Height))).ToList();

        var any = Flux.Model.Game.BattleShip.Ship.AnyHits(ships, shots);

        System.Console.WriteLine(ships.ToConsoleString(size));

        foreach (var ship in ships)
          System.Console.WriteLine($" Ship: {string.Join(' ', ship.Locations)} (Sunk? {Flux.Model.Game.BattleShip.Ship.IsSunk(ship, shots)})");

        System.Console.WriteLine($"Shots: {string.Join(' ', shots)}");

        System.Console.WriteLine($"Any hits? {any}");
      }
      while (System.Console.ReadKey().Key != System.ConsoleKey.Escape);

      //var ttt = new Flux.Model.TicTacToe.Board();

      //for (var index = 0; index < 9; index++)
      //{
      //  System.Console.Clear();

      //  {
      //    System.Console.WriteLine(ttt.ToString());

      //    var allMoves1 = ttt.GetOptionsForPlayer1().ToList();
      //    if (allMoves1.Count > 0) System.Console.WriteLine("a1" + System.Environment.NewLine + string.Join(System.Environment.NewLine, allMoves1));
      //    var allMoves2 = ttt.GetOptionsForPlayer2().ToList();
      //    if (allMoves2.Count > 0) System.Console.WriteLine("a2" + System.Environment.NewLine + string.Join(System.Environment.NewLine, allMoves2));

      //    var allMyMoves = allMoves1;

      //    var myTopMoves = allMyMoves.Where(m => m.Score == allMyMoves.Max(m => m.Score)).ToList();
      //    if (myTopMoves.Count > 0)
      //    {ddd11
      //      myTopMoves.RandomElement(out var myTopMove);
      //      if (myTopMove.Score == -10) break;
      //      System.Console.WriteLine($"Your top move: {myTopMove}");
      //    }11w

      //    System.Console.Write("Enter row: ");
      //    var rowChar = System.Console.ReadKey().KeyChar;
      //    System.Console.Write("\r\nEnter column: ");
      //    var columnChar = System.Console.ReadKey().KeyChar;

      //    if (rowChar == '\u001b' || columnChar == '\u001b')
      //    {
      //      ttt.Clear();
      //      System.Console.Clear();
      //      continue;
      //    }

      //    var row = int.Parse(rowChar.ToString());
      //    var column = int.Parse(columnChar.ToString());

      //    System.Console.WriteLine($"\r\nYour move: {row}, {column}");

      //    ttt[row, column] = Flux.Model.TicTacToe.State.Player2;
      //  }

      //  {
      //    var allMoves1 = ttt.GetOptionsForPlayer1().ToList();
      //    if (allMoves1.Count > 0) System.Console.WriteLine("b1" + System.Environment.NewLine + string.Join(System.Environment.NewLine, allMoves1));
      //    var allMoves2 = ttt.GetOptionsForPlayer2().ToList();
      //    if (allMoves2.Count > 0) System.Console.WriteLine("b2" + System.Environment.NewLine + string.Join(System.Environment.NewLine, allMoves2));

      //    var allMoves = allMoves2;

      //    if (allMoves.Count == 0) break;

      //    foreach (var m in allMoves)
      //      System.Console.WriteLine(m);

      //    var topMoves = allMoves.Where(m => m.Score == allMoves.Min(m => m.Score)).ToList();

      //    if (topMoves.Count == 0) break;

      //    topMoves.RandomElement(out var topMove);
      //    if (topMove.Score == 10) break;

      //    if (!(topMove.Row == -1 || topMove.Column == -1))
      //      ttt[topMove.Row, topMove.Column] = Flux.Model.TicTacToe.State.Player1;//turn ? player : opponent;

      //    System.Console.WriteLine(ttt.ToString());
      //  }
      //}



      //System.Console.WriteLine($"{nameof(Flux.Locale.AppDomainName)} = \"{Flux.Locale.AppDomainName}\"");
      //System.Console.WriteLine($"{nameof(Flux.Locale.AppDomainPath)} = \"{Flux.Locale.AppDomainPath}\"");
      //System.Console.WriteLine($"{nameof(Flux.Locale.CommonLanguageRuntimeVersion)} = \"{Flux.Locale.CommonLanguageRuntimeVersion}\"");
      //System.Console.WriteLine($"{nameof(Flux.Locale.ComputerDomainName)} = \"{Flux.Locale.ComputerDomainName}\"");
      //System.Console.WriteLine($"{nameof(Flux.Locale.ComputerHostName)} = \"{Flux.Locale.ComputerHostName}\"");
      //System.Console.WriteLine($"{nameof(Flux.Locale.ComputerPrimaryDnsName)} = \"{Flux.Locale.ComputerPrimaryDnsName}\"");
      //System.Console.WriteLine($"{nameof(Flux.Locale.FrameworkTitle)} = \"{Flux.Locale.FrameworkTitle}\"");
      //System.Console.WriteLine($"{nameof(Flux.Locale.FrameworkVersion)} = \"{Flux.Locale.FrameworkVersion}\"");
      //System.Console.WriteLine($"{nameof(Flux.Locale.MachineName)} = \"{Flux.Locale.MachineName}\"");
      //System.Console.WriteLine($"{nameof(Flux.Locale.OperatingSystemTitle)} = \"{Flux.Locale.OperatingSystemTitle}\"");
      //System.Console.WriteLine($"{nameof(Flux.Locale.OperatingSystemVersion)} = \"{Flux.Locale.OperatingSystemVersion}\"");
      //System.Console.WriteLine($"{nameof(Flux.Locale.PlatformTitle)} = \"{Flux.Locale.PlatformTitle}\"");
      //System.Console.WriteLine($"{nameof(Flux.Locale.PlatformVersion)} = \"{Flux.Locale.PlatformVersion}\"");
      //System.Console.WriteLine($"{nameof(Flux.Locale.TimerTickCounter)} = \"{Flux.Locale.TimerTickCounter}\"");
      //System.Console.WriteLine($"{nameof(Flux.Locale.TimerTickResolution)} = \"{Flux.Locale.TimerTickResolution}\"");
      //System.Console.WriteLine($"{nameof(Flux.Locale.UserDomainName)} = \"{Flux.Locale.UserDomainName}\"");
      //System.Console.WriteLine($"{nameof(Flux.Locale.UserName)} = \"{Flux.Locale.UserName}\"");



    }

    static void Main(string[] args)
    {
      System.Console.InputEncoding = System.Text.Encoding.UTF8;
      System.Console.OutputEncoding = System.Text.Encoding.UTF8;

      System.Console.WriteLine(Flux.Diagnostics.Performance.Measure(() => TimedMain(args), 1));

      System.Console.WriteLine($"{System.Environment.NewLine}Press any key to exit...");
      System.Console.ReadKey();
    }
  }
}
