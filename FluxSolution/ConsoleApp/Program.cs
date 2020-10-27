using Flux;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
namespace ConsoleApp
{
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
  public struct Bits32
  {
    [System.Runtime.InteropServices.FieldOffset(0)] public byte Byte0;
    [System.Runtime.InteropServices.FieldOffset(1)] public byte Byte1;
    [System.Runtime.InteropServices.FieldOffset(2)] public byte Byte2;
    [System.Runtime.InteropServices.FieldOffset(3)] public byte Byte3;
    [System.Runtime.InteropServices.FieldOffset(0)] public int Int;
  }

  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
  public struct Bits64
  {
    [System.Runtime.InteropServices.FieldOffset(0)] public byte Byte0;
    [System.Runtime.InteropServices.FieldOffset(1)] public byte Byte1;
    [System.Runtime.InteropServices.FieldOffset(2)] public byte Byte2;
    [System.Runtime.InteropServices.FieldOffset(3)] public byte Byte3;
    [System.Runtime.InteropServices.FieldOffset(4)] public byte Byte4;
    [System.Runtime.InteropServices.FieldOffset(5)] public byte Byte5;
    [System.Runtime.InteropServices.FieldOffset(6)] public byte Byte6;
    [System.Runtime.InteropServices.FieldOffset(7)] public byte Byte7;
    [System.Runtime.InteropServices.FieldOffset(0)] public int Int0;
    [System.Runtime.InteropServices.FieldOffset(4)] public int Int1;
    [System.Runtime.InteropServices.FieldOffset(0)] public long Long;
  }

  class Program
  {
    private static void TimedMain(string[] _)
    {
      var html = "<h2 class=\"ofscreen\">Webontwikkeling leren</h2><h1>Regular Expressions</h1><p>\"Alle onderdelen van MDN(documenten en de website zelf) worden gemaakt door een open gemeenschap.\"</p><br/>";

      System.Console.WriteLine(html);

      var text = System.Text.RegularExpressions.Regex.Replace(html, @"(<[^>]+>)+", @" ").Trim();

      System.Console.WriteLine(text);

      var original = "ABCDEFGHI";
      var padding = "123456789";
      System.Console.WriteLine(original.PadLeft(11, @"987654321"));
      System.Console.WriteLine(original.PadRight(11, @"123456789"));
      System.Console.WriteLine(original.PadEven(11, @"987654321", @"123456789"));

      /*
        var size = new Flux.Geometry.Size2(10, 10);

        int shotCount = 17;

        System.Collections.Generic.List<Flux.Model.Game.BattleShip.Ship> ships = default;
        System.Collections.Generic.List<Flux.Geometry.Point2> shots = default;

        System.ConsoleKey key = System.ConsoleKey.Escape;

        do
        {
          if (key == System.ConsoleKey.F || key == System.ConsoleKey.Escape)
            ships = Flux.Model.Game.BattleShip.Ship.StageFleet(size, 2, 3, 3, 4, 5);
          if (key == System.ConsoleKey.S || key == System.ConsoleKey.Escape)
            shots = System.Linq.Enumerable.Range(0, shotCount).Select(n => new Flux.Geometry.Point2(Flux.Random.NumberGenerator.Crypto.Next(size.Width), Flux.Random.NumberGenerator.Crypto.Next(size.Height))).ToList();

          System.Console.SetCursorPosition(0, 0);

          System.Console.WriteLine(ships.ToConsoleString(size));

          foreach (var ship in ships)
            System.Console.WriteLine($"Ship{ship.Locations.Count}: {string.Join(' ', ship.Locations)} ({(Flux.Model.Game.BattleShip.Ship.IsSunk(ship, shots) ? @"Sunk!" : Flux.Model.Game.BattleShip.Ship.AnyHits(ship, shots) ? @"Hit!" : @"Miss!")}) ");

          System.Console.WriteLine($"Shots: {string.Join(' ', shots)}");

          System.Console.WriteLine($"Any hits at all? {Flux.Model.Game.BattleShip.Ship.AnyHits(ships, shots)} ");

          key = System.Console.ReadKey().Key;
        }
        while (key != System.ConsoleKey.Escape);
      */


      /*
        var ttt = new Flux.Model.TicTacToe.Board();

        for (var index = 0; index < 9; index++)
        {
          System.Console.Clear();

          {
            System.Console.WriteLine(ttt.ToString());

            var allMoves1 = ttt.GetOptionsForPlayer1().ToList();
            if (allMoves1.Count > 0) System.Console.WriteLine("a1" + System.Environment.NewLine + string.Join(System.Environment.NewLine, allMoves1));
            var allMoves2 = ttt.GetOptionsForPlayer2().ToList();
            if (allMoves2.Count > 0) System.Console.WriteLine("a2" + System.Environment.NewLine + string.Join(System.Environment.NewLine, allMoves2));

            var allMyMoves = allMoves1;

            var myTopMoves = allMyMoves.Where(m => m.Score == allMyMoves.Max(m => m.Score)).ToList();
            if (myTopMoves.Count > 0)
            {
              ddd11
              myTopMoves.RandomElement(out var myTopMove);
              if (myTopMove.Score == -10) break;
              System.Console.WriteLine($"Your top move: {myTopMove}");
            }
            11w

            System.Console.Write("Enter row: ");
            var rowChar = System.Console.ReadKey().KeyChar;
            System.Console.Write("\r\nEnter column: ");
            var columnChar = System.Console.ReadKey().KeyChar;

            if (rowChar == '\u001b' || columnChar == '\u001b')
            {
              ttt.Clear();
              System.Console.Clear();
              continue;
            }

            var row = int.Parse(rowChar.ToString());
            var column = int.Parse(columnChar.ToString());

            System.Console.WriteLine($"\r\nYour move: {row}, {column}");

            ttt[row, column] = Flux.Model.TicTacToe.State.Player2;
          }

          {
            var allMoves1 = ttt.GetOptionsForPlayer1().ToList();
            if (allMoves1.Count > 0) System.Console.WriteLine("b1" + System.Environment.NewLine + string.Join(System.Environment.NewLine, allMoves1));
            var allMoves2 = ttt.GetOptionsForPlayer2().ToList();
            if (allMoves2.Count > 0) System.Console.WriteLine("b2" + System.Environment.NewLine + string.Join(System.Environment.NewLine, allMoves2));

            var allMoves = allMoves2;

            if (allMoves.Count == 0) break;

            foreach (var m in allMoves)
              System.Console.WriteLine(m);

            var topMoves = allMoves.Where(m => m.Score == allMoves.Min(m => m.Score)).ToList();

            if (topMoves.Count == 0) break;

            topMoves.RandomElement(out var topMove);
            if (topMove.Score == 10) break;

            if (!(topMove.Row == -1 || topMove.Column == -1))
              ttt[topMove.Row, topMove.Column] = Flux.Model.TicTacToe.State.Player1;//turn ? player : opponent;

            System.Console.WriteLine(ttt.ToString());
          }
        }
      */

      /*
      System.Console.WriteLine($"{nameof(Flux.Locale.AppDomainName)} = \"{Flux.Locale.AppDomainName}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.AppDomainPath)} = \"{Flux.Locale.AppDomainPath}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.CommonLanguageRuntimeVersion)} = \"{Flux.Locale.CommonLanguageRuntimeVersion}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.ComputerDomainName)} = \"{Flux.Locale.ComputerDomainName}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.ComputerHostName)} = \"{Flux.Locale.ComputerHostName}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.ComputerPrimaryDnsName)} = \"{Flux.Locale.ComputerPrimaryDnsName}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.FrameworkTitle)} = \"{Flux.Locale.FrameworkTitle}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.FrameworkVersion)} = \"{Flux.Locale.FrameworkVersion}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.MachineName)} = \"{Flux.Locale.MachineName}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.OperatingSystemTitle)} = \"{Flux.Locale.OperatingSystemTitle}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.OperatingSystemVersion)} = \"{Flux.Locale.OperatingSystemVersion}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.PlatformTitle)} = \"{Flux.Locale.PlatformTitle}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.PlatformVersion)} = \"{Flux.Locale.PlatformVersion}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.TimerTickCounter)} = \"{Flux.Locale.TimerTickCounter}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.TimerTickResolution)} = \"{Flux.Locale.TimerTickResolution}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.UserDomainName)} = \"{Flux.Locale.UserDomainName}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.UserName)} = \"{Flux.Locale.UserName}\"");
      */

      //System.Console.WriteLine(nameof(Flux.Resources.Census.CountiesAllData));
      //foreach (var strings in new Flux.Resources.Census.CountiesAllData().GetStrings(Flux.Resources.Census.CountiesAllData.SourceUri))
      //  System.Console.WriteLine(string.Join('|', strings));

      //System.Console.WriteLine(nameof(Flux.Resources.ProjectGutenberg.SynonymsAndAntonymsSamuelFallows));
      //foreach (var strings in new Flux.Resources.ProjectGutenberg.SynonymsAndAntonymsSamuelFallows().GetStrings(Flux.Resources.ProjectGutenberg.SynonymsAndAntonymsSamuelFallows.SourceUri))
      //  System.Console.WriteLine(string.Join('|', strings));
      //System.Console.WriteLine(nameof(Flux.Resources.ProjectGutenberg.TableOfContents));
      //foreach (var strings in new Flux.Resources.ProjectGutenberg.TableOfContents().GetStrings(Flux.Resources.ProjectGutenberg.TableOfContents.SourceUri))
      //  System.Console.WriteLine(string.Join('|', strings));
      //System.Console.WriteLine(nameof(Flux.Resources.ProjectGutenberg.TenThousandWonderfulThings));
      //foreach (var strings in new Flux.Resources.ProjectGutenberg.TenThousandWonderfulThings().GetStrings(Flux.Resources.ProjectGutenberg.TenThousandWonderfulThings.SourceUri))
      //  System.Console.WriteLine(string.Join('|', strings));
      //System.Console.WriteLine(nameof(Flux.Resources.ProjectGutenberg.WebstersUnabridgedDictionary));
      //foreach (var strings in new Flux.Resources.ProjectGutenberg.WebstersUnabridgedDictionary().GetStrings(Flux.Resources.ProjectGutenberg.WebstersUnabridgedDictionary.SourceUri))
      //  System.Console.WriteLine(string.Join('|', strings));

      //System.Console.WriteLine(nameof(Flux.Resources.Scowl.TwoOfTwelveFull));
      //foreach (var strings in new Flux.Resources.Scowl.TwoOfTwelveFull().GetStrings(Flux.Resources.Scowl.TwoOfTwelveFull.SourceUri))
      //  System.Console.WriteLine(string.Join('|', strings));

      //System.Console.WriteLine(nameof(Flux.Resources.Scrape.ZipCodes));
      //foreach (var strings in new Flux.Resources.Scrape.ZipCodes().GetStrings(Flux.Resources.Scrape.ZipCodes.SourceUri))
      //  System.Console.WriteLine(string.Join('|', strings));

      //System.Console.WriteLine(nameof(Flux.Resources.Ucd.Blocks));
      //foreach (var strings in new Flux.Resources.Ucd.Blocks().GetStrings(Flux.Resources.Ucd.Blocks.SourceUri))
      //  System.Console.WriteLine(string.Join('|', strings));
      //System.Console.WriteLine(nameof(Flux.Resources.Ucd.UnicodeData));
      //foreach (var strings in new Flux.Resources.Ucd.UnicodeData().GetStrings(Flux.Resources.Ucd.UnicodeData.SourceUri))
      //  System.Console.WriteLine(string.Join('|', strings));

      //System.Console.WriteLine(nameof(Flux.Resources.W3c.NamedCharacterReferences));
      //foreach (var strings in new Flux.Resources.W3c.NamedCharacterReferences().GetStrings(Flux.Resources.W3c.NamedCharacterReferences.SourceUri))
      //  System.Console.WriteLine(string.Join('|', strings));

      //var cad = new Flux.Resources.Census.CountiesAllData();
      //cad.GetDataTable(Flux.Resources.Census.CountiesAllData.LocalUri);
      //return;

      //var dt = new System.Data.DataTable(@"My Table");
      //dt.Columns.Add("A");
      //dt.Columns.Add("B");
      //dt.Columns.Add("C");
      //dt.Columns.Add("D");
      //dt.Columns.Add("E");
      //dt.Rows.Add(new object[] { 1, 2, 3, 4, 5 });
      //dt.Rows.Add(new object[] { 6, 7, 8, 9, 10 });
      //dt.Rows.Add(new object[] { 11, 12, 13, 14, 15 });
      //dt.Rows.Add(new object[] { 16, 17, 18, 19, 20 });
      //dt.Rows.Add(new object[] { 21, 22, 23, 24, 25 });
      //System.Console.WriteLine(dt.ToConsoleString());

      //System.Console.WriteLine("GetValuesInColumn(2)");
      //System.Console.WriteLine(string.Join('|', dt.GetValuesInColumn(2)));

      //System.Console.WriteLine("ToArray");
      //var mda = dt.To2dArray(1, 3, 1, 3);
      //System.Console.WriteLine(mda.ToConsoleString2d());

      //System.Console.WriteLine("ReverseColumns");
      //var reversedColumns = dt.ReverseColumns();
      //System.Console.WriteLine(reversedColumns.ToConsoleString());

      //System.Console.WriteLine("ReverseColumnsInline");
      //reversedColumns.ReverseColumnsInline(1, 2);
      //System.Console.WriteLine(reversedColumns.ToConsoleString());

      //System.Console.WriteLine("ReverseRows");
      //var reversedRows = dt.ReverseRows();
      //System.Console.WriteLine(reversedRows.ToConsoleString());

      //System.Console.WriteLine("ReverseRowsInline");
      //reversedRows.ReverseRowsInline(1, 2);
      //System.Console.WriteLine(reversedRows.ToConsoleString());

      //System.Console.WriteLine("Transposed");
      //var transposed = dt.Transpose(out var _, "X", "Y", "Z");
      //System.Console.WriteLine(transposed.ToConsoleString());

      //System.Console.WriteLine("RotateLeft");
      //var rotatedLeft = dt.RotateLeft(out var _, "X", "Y", "Z");
      //System.Console.WriteLine(rotatedLeft.ToConsoleString());

      //System.Console.WriteLine("RotateRight");
      //var rotatedRight = dt.RotateRight(out var _, "X", "Y", "Z");
      //System.Console.WriteLine(rotatedRight.ToConsoleString());

      //var a1 = new object[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
      //System.Console.WriteLine(string.Join('|', a1));
      //var a2 = a1.ToNewArray(3, 3, 3, 3);
      //System.Console.WriteLine(string.Join('|', a2));
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
