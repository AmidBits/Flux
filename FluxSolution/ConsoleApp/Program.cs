using Flux;
using System;
using System.Data.Common;
using System.Linq;

namespace ConsoleApp
{
  class Program
  {
    private static void TimedMain(string[] args)
    {

      //var line = "1,\"00704\",\"STANDARD\"\"\",\"PARC PARQUE\",\"PR\",\"NOT ACCEPTABLE\",17.96,-66.22,0.38,-0.87,0.30,\"NA\",\"US\",\"Parc Parque, PR\",\"NA - US - PR - PARC PARQUE\",\"false\",,,,";
      ////var line = "\"\"\"A\",\"\"\"B\"\"D\"\"\",c,\"D\"\"\"";
      //var array = line.ParseCsv(',');
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

      var dataReader = new Flux.Resources.ProjectGutenberg.TenThousandWonderfulThings().GetDataReader(Flux.Resources.ProjectGutenberg.TenThousandWonderfulThings.LocalUri);
      var index = 0;
      foreach (var dataRow in dataReader)
      {
        System.Console.WriteLine(++index);
        System.Console.WriteLine($"{string.Join('|', dataRow.GetValues())}");
        System.Console.WriteLine();
      }

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
      */

      System.Console.WriteLine($"{nameof(Flux.Locale.AppDomainName)} = \"{Flux.Locale.AppDomainName}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.ClrVersion)} = \"{Flux.Locale.ClrVersion}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.ComputerDomainName)} = \"{Flux.Locale.ComputerDomainName}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.ComputerHostName)} = \"{Flux.Locale.ComputerHostName}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.ComputerPrimaryDnsName)} = \"{Flux.Locale.ComputerPrimaryDnsName}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.FrameworkDescription)} = \"{Flux.Locale.FrameworkDescription}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.FrameworkVersion)} = \"{Flux.Locale.FrameworkVersion}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.MachineName)} = \"{Flux.Locale.MachineName}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.OperatingSystemDescription)} = \"{Flux.Locale.OperatingSystemDescription}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.OperatingSystemVersion)} = \"{Flux.Locale.OperatingSystemVersion}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.UserDomainName)} = \"{Flux.Locale.UserDomainName}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.UserName)} = \"{Flux.Locale.UserName}\"");
      System.Console.WriteLine($"{nameof(System.Environment.OSVersion)} = \"{System.Environment.OSVersion}\"");
      //System.Console.WriteLine($"{nameof(Flux.Locale.)} = \"{Flux.Locale.}\"");
      //System.Console.WriteLine($"{} = \"{}\"");
    }

    static void Main(string[] args)
    {
      System.Console.InputEncoding = System.Text.Encoding.UTF8;
      System.Console.OutputEncoding = System.Text.Encoding.UTF8;

      System.Console.WriteLine(Flux.Diagnostics.Performance.Measure(() => TimedMain(args), 1));

      System.Console.WriteLine(System.Environment.NewLine + @"Press any key to exit...");
      System.Console.ReadKey();
    }
  }
}
