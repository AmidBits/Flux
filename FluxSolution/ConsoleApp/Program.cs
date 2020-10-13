using Flux;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace ConsoleApp
{
  class Program
  {
    private static void TimedMain(string[] args)
    {
      //var x = new System.Numerics.Vector2[4] { new System.Numerics.Vector2(0, 0), new System.Numerics.Vector2(0, 1), new System.Numerics.Vector2(1, 1), new System.Numerics.Vector2(1, 0) };
      var x = new System.Numerics.Vector2[5] { new System.Numerics.Vector2(0, 0), new System.Numerics.Vector2(0, 1), new System.Numerics.Vector2(1, 1), new System.Numerics.Vector2(1, 0), new System.Numerics.Vector2(1, -1) };

      var xy = x.SplitInHalf().ToArray();

      //var grid = new System.Drawing.Size(10, 5);

      //System.Collections.Generic.List<Flux.Model.Game.BattleShip.Ship> ships = new System.Collections.Generic.List<Flux.Model.Game.BattleShip.Ship>();

      //foreach (var size in new int[] { 2, 3, 3, 4, 5 })
      //{
      //  Flux.Model.Game.BattleShip.Ship ship;

      //  do
      //  {
      //    ship = new Flux.Model.Game.BattleShip.Ship(size, new System.Drawing.Point(Flux.Random.NumberGenerator.Crypto.Next(grid.Width), Flux.Random.NumberGenerator.Crypto.Next(grid.Height)), (Flux.Model.Game.BattleShip.ShipOrientation)Flux.Random.NumberGenerator.Crypto.Next(2));
      //  }
      //  while (!ship.IsValid(grid) || ships.Any(s => Flux.Model.Game.BattleShip.Ship.Intersects(ship, s)));

      //  ships.Add(ship);
      //}

      //while (true)
      //{
      //  System.Console.Write(ships.ToConsoleString(grid));

      //  System.Console.Write("X=");
      //  var x = System.Console.ReadKey().KeyChar;
      //  if (!char.IsDigit(x)) break;

      //  System.Console.Write(", ");

      //  System.Console.Write("Y=");
      //  var y = System.Console.ReadKey().KeyChar;
      //  if (!char.IsDigit(y)) break;

      //  System.Console.WriteLine();

      //  var p = new System.Drawing.Point(x - '0', y - '0');
      //  System.Console.WriteLine($"@{p}");

      //  if (ships.Any(ship => Flux.Model.Game.BattleShip.Ship.Intersects(ship, p)) && ships.Single(ship => Flux.Model.Game.BattleShip.Ship.Intersects(ship, p)) is var ship)
      //    System.Console.WriteLine($"Yes, #{ship.Length}.");
      //  else
      //    System.Console.WriteLine($"No.".PadRight(80));
      //}

      //var d = new Flux.Model.Game.BattleShip.Dreadnought();
      //d.NewGame(new System.Drawing.Size(10, 10), new TimeSpan(0, 0, 10), new int[] { 2, 3 });
      //foreach (var ship in d.PlaceShips())
      //  System.Console.WriteLine(ship);
      //d.ShotMiss(new System.Drawing.Point(4, 4));
      //d.OpponentShot(new System.Drawing.Point(1, 1));
      //d.OpponentShot(new System.Drawing.Point(2, 2));
      //((Flux.Model.Game.BattleShip.Defense)d.defense).print_placement(ships);
      //System.Console.WriteLine(d.GetShot());

      //System.Console.WriteLine((((Flux.Model.Game.BattleShip.Offense)d.offense).state.state).ToConsoleString());
      //var g = new int[10, 10];
      //System.Array.Clear(g, 0, 100);

      //var g2 = new Flux.Model.Grid2Fixed<int>(10, 10);
      //foreach (var p in g2.AdjacentOrthogonal(new System.Drawing.Point(0, 5)))
      //{
      //  System.Console.WriteLine(p);
      //}

      //var s = "These are U+0041, U+0043, U+10FFFF, \\u0042 and \\u0044 all in all.";
      //Flux.Text.UnicodeNotation.TryParse(s, out var un);
      //Flux.Text.UnicodeStringLiteral.TryParse(s, out var usl);
      //System.Console.WriteLine($"\"{string.Join('|', un)}\", \"{string.Join('|', usl)}\"");

      //var line = "1,\"00704\",\"STANDARD\"\"\",\"PARC PARQUE\",\"PR\",\"NOT ACCEPTABLE\",17.96,-66.22,0.38,-0.87,0.30,\"NA\",\"US\",\"Parc Parque, PR\",\"NA - US - PR - PARC PARQUE\",\"false\",,,,";
      ////var line = "\"\"\"A\",\"\"\"B\"\"D\"\"\",c,\"D\"\"\"";
      //var array = line.ParseCsvLine(',');
      //System.Console.WriteLine(string.Join("|", array));

      //using var dataReader = new Flux.Resources.Scrape.ZipCodes().GetDataReader(Flux.Resources.Scrape.ZipCodes.LocalUri);
      //foreach (var dataRecord in dataReader)
      //{
      //  System.Console.WriteLine(string.Join('|', dataRecord.GetValues()));
      //  System.Console.WriteLine();
      //}
      //System.Console.WriteLine(dataReader.GetSchemaTable().ToArray(true).ToConsoleString());

      //using var dataTable = new Flux.Resources.Scrape.ZipCodes().GetDataTable(Flux.Resources.Scrape.ZipCodes.LocalUri);
      //foreach (var dataRow in dataTable.Select())
      //{
      //  System.Console.WriteLine(string.Join('|', dataRow.ItemArray));
      //  System.Console.WriteLine();
      //}
      //System.Console.WriteLine(dataTable.CreateDataReader().GetSchemaTable().ToArray(true).ToConsoleString());


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
            myTopMoves.RandomElement(out var myTopMove);
            if (myTopMove.Score == -10) break;
            System.Console.WriteLine($"Your top move: {myTopMove}");
          }

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
