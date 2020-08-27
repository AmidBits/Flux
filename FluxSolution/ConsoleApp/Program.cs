using Flux;
using Flux.Model;
using Flux.Text;
using System;
using System.Dynamic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace ConsoleApp
{
  class Program
  {
    private static void TimedMain(string[] args)
    {
      var originalLatitude = 32.221667;
      var originalLongitude = -110.926389;
      System.Console.WriteLine($"{originalLatitude}, {originalLongitude}");

      var geoposition = new Flux.Geoposition(originalLatitude, originalLongitude, EarthRadii.MeanInMeters);
      System.Console.WriteLine($"Loaded: {geoposition}");

      var parsedLatitude = string.Format(new Flux.IFormatProvider.DmsFormatter(), "{0:DMSNS}", geoposition.Latitude);
      var parsedLongitude = string.Format(new Flux.IFormatProvider.DmsFormatter(), "{0:DMSEW}", geoposition.Longitude);

      Flux.Geoposition.TryParse(parsedLatitude, parsedLongitude, out var geo2);
      System.Console.WriteLine($"Parsed: {geo2}");


      System.Console.WriteLine(string.Format(new Flux.IFormatProvider.DmsFormatter(), "{0:DMSNS} = {0:DMNS} = {0:DNS} \u2248 {0:D6NS}", originalLatitude));
      System.Console.WriteLine(string.Format(new Flux.IFormatProvider.DmsFormatter(), "{0:DMSEW} = {0:DMEW} = {0:DEW} \u2248 {0:D6EW}", originalLongitude));
      System.Console.WriteLine(string.Format(new Flux.IFormatProvider.RadixFormatter(), "{0:RADIX2}", 95));
      return;

      //var deg0 = 135;
      //var rad0 = Flux.Angles.DegreeToRadian(deg0);
      //var crt1 = Flux.Angles.RotationAngleToCartesian(rad0, out var x1, out var y1);
      //var crt2 = Flux.Angles.RotationAngleToCartesianEx(rad0, out var x2, out var y2);
      //var rad1 = Flux.Angles.CartesianToRotationAngle(x1, y1);
      //var rad2 = Flux.Angles.CartesianToRotationAngleEx(x2, y2);
      //var deg1 = Flux.Angles.RadianToDegree(rad1);
      //var deg2 = Flux.Angles.RadianToDegree(rad2);

      //System.Console.WriteLine($"{deg0} = {rad0} = ({x1:N5}, {y1:N5}) = [{deg1:N1}] = ({x2:N5}, {y2:N5}) = [{deg2:N1}] ... ({Maths.PiX2:N5})");
      var st1 = "This is the 191 time.";

      System.Console.WriteLine(Flux.Diagnostics.Performance.Measure(() => "This is the 191 time.".ToStringBuilder().Geminate('t', 's', 'i').InsertOrdinalIndicatorSuffix().Reverse().NormalizeAdjacent().ToLowerCaseInvariant().LeftMost(10)));

      var tokenizer = new Flux.Text.Tokenization.Rune.Tokenizer();
      foreach (var token in tokenizer.GetTokens("This is the 191 time.".ToStringBuilder().Geminate('t', 's', 'i').InsertOrdinalIndicatorSuffix().Reverse().NormalizeAdjacent().ToString()))
      {
        System.Console.WriteLine(token);
      }

      return;



      //var bst = Flux.Collections.Immutable.BinarySearchTree<int, int>.Empty;

      //bst = bst.Add(3, 3);
      //bst = bst.Add(5, 5);
      //bst = bst.Add(2, 2);
      //bst = bst.Add(9, 9);
      //bst = bst.Add(12, 12);
      //bst = bst.Add(5, 5);
      //bst = bst.Add(23, 23);
      //bst = bst.Add(23, 23);

      //var res = bst.Minimax(0, true, Flux.Bitwise.Log2(bst.GetNodeCount()), i => i);

      //System.Console.WriteLine(res);
      //return;

      var ttt = new Flux.Model.TicTacToe.Board();
      System.Console.WriteLine(ttt.ToString());

      //var player = 'x';
      //var opponent = 'o';

      //var turn = true;

      for (var index = 0; index < 9; index++)
      {
        {
          var myAllMoves = ttt.GetOptionsForPlayer2().ToList();
          if (myAllMoves.Count > 0)
            System.Console.WriteLine(string.Join(System.Environment.NewLine, myAllMoves));
          var myTopMoves = myAllMoves.Where(m => m.Score == myAllMoves.Max(m => m.Score)).ToList();
          if (myTopMoves.Count > 0)
          {
            var myTopMove = myTopMoves.RandomElement();
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

          System.Console.WriteLine(ttt.ToString());
        }

        {
          var allMoves = ttt.GetOptionsForPlayer1().ToList();

          if (allMoves.Count == 0) break;

          foreach (var m in allMoves)
            System.Console.WriteLine(m);

          var topMoves = allMoves.Where(m => m.Score == allMoves.Max(m => m.Score)).ToList();

          if (topMoves.Count == 0) break;

          var topMove = topMoves.RandomElement();
          if (topMove.Score == 10) break;

          if (!(topMove.Row == -1 || topMove.Column == -1))
            ttt[topMove.Row, topMove.Column] = Flux.Model.TicTacToe.State.Player1;//turn ? player : opponent;

          System.Console.WriteLine(ttt.ToString());
        }

        //if (move.Score == +10 || move.Score == -10) break;

        //var tmp = player;
        //player = opponent;
        //opponent = tmp;
        //turn = !turn;
      }
      static int log2(int n)
      {
        return (n == 1) ? 0 : 1 + log2(n / 2);
      }
      int[] scores = { 3, 5, 2, 9, 12, 5, 23, 23 };
      int n = scores.Length;
      int h = log2(n);
      int resx = Flux.Model.TicTacToe.Board.Minimax(0, 0, true, scores, h);
      Console.WriteLine("The optimal value is : " + resx);
      return;

      var sss = new System.ReadOnlySpan<int>(new int[10]);

      var s = "Wooloomooloo";
      var t = "olo";
      s.LevenshteinDistance(t);
      var sb = new System.Text.StringBuilder(s);
      // sb.Equals()
      var ss = new System.Span<char>(s.ToCharArray());
      var sbna = sb.NormalizeAdjacent(null, 'o', 'm');
      var ssna = ss.NormalizeAdjacent(null, 'o', 'm');
      System.Console.WriteLine($"\"{s}\" = \"{sbna}\" ({sbna.Length}) = {sb.IndexOfAny(t)}");
      System.Console.WriteLine($"\"{s}\" = \"{sbna}\" ({sbna.Length})");
      System.Console.WriteLine($"\"{s}\" = \"{ssna.ToString()}\" ({ssna.Length})");
      return;

      System.Collections.Generic.Dictionary<string, string> m_soundexTests = new System.Collections.Generic.Dictionary<string, string>()
      {
        { "Ashcroft", "A261" },
        { "Fusedale", "F234" },
        { "Grayley", "G640" },
        { "Hatcher", "H326" },
        { "Honeyman", "H555" },
        { "Parade", "P630" },
        { "Pfister", "P236" },
        { "Rubin", "R150" },
        { "Rupert", "R163" },
        { "Tymczak", "T522" },
        { "Catherine", "CTHRN" }
      };

      foreach (var kvp in m_soundexTests)
      {
        System.Console.WriteLine($"{kvp.Key} = \"{(new Flux.Text.PhoneticAlgorithm.MatchRatingApproach().Encode(kvp.Key) is var mra ? mra : throw new System.Exception())}\" ({kvp.Value}) == {mra.Equals(kvp.Value)}");
        //System.Console.WriteLine($"{kvp.Key} = \"{(new Flux.Text.PhoneticAlgorithm.Soundex().Encode(kvp.Key) is var code ? code : throw new System.Exception())}\" ({kvp.Value}) == {code.Equals(kvp.Value)}");
        //System.Console.WriteLine($"{kvp.Key} = \"{(new Flux.Text.PhoneticAlgorithm.SqlSoundex().Encode(kvp.Key) is var sql ? sql : throw new System.Exception())}\" ({kvp.Value}) == {sql.Equals(kvp.Value)}");
      }

      System.Collections.Generic.Dictionary<string, string> m_refinedSoundexTests = new System.Collections.Generic.Dictionary<string, string>()
      {
        { "Braz", "B1905" },
        { "Charon", "C30908" },
        { "Hayers", "H093" },
        { "Lambert", "L7081096" },
        { "Nolton", "N807608" },
      };

      foreach (var kvp in m_refinedSoundexTests)
      {
        System.Console.WriteLine($"{kvp.Key} = \"{(new Flux.Text.PhoneticAlgorithm.RefinedSoundex().Encode(kvp.Key) is var code ? code : throw new System.Exception())}\" ({kvp.Value}) == {code.Equals(kvp.Value)}");
      }



      return;

      //var x = " Blañk";
      //var y = "blank";

      //System.Console.WriteLine(x);
      //System.Console.WriteLine(y);
      //var a = new ReadOnlySpan<string>(x.Select(c => c.ToString()).ToArray());
      //var b = new ReadOnlySpan<string>(y.Select(c => c.ToString()).ToArray());

      ////a = "AGGTAB".AsSpan();
      ////b = "GXTXAYB".AsSpan();
      //System.Console.WriteLine(a.ToString());
      //System.Console.WriteLine(b.ToString());

      //System.Console.WriteLine($"DamerauLevenshteinDistance: {a.DamerauLevenshteinDistance(b, Flux.StringComparerEx.CurrentCulture)}, {a.DamerauLevenshteinDistance(b, Flux.StringComparerEx.CurrentCultureIgnoreCase)}, {a.DamerauLevenshteinDistance(b, Flux.StringComparerEx.CurrentCultureIgnoreNonSpace)}, {a.DamerauLevenshteinDistance(b, Flux.StringComparerEx.CurrentCultureIgnoreNonSpaceAndCase)}");
      //System.Console.WriteLine($"   JackardIndexCoefficient: {a.JackardIndexCoefficient(b, Flux.StringComparerEx.CurrentCulture)}, {a.JackardIndexCoefficient(b, Flux.StringComparerEx.CurrentCultureIgnoreCase)}, {a.JackardIndexCoefficient(b, Flux.StringComparerEx.CurrentCultureIgnoreNonSpace)}, {a.JackardIndexCoefficient(b, Flux.StringComparerEx.CurrentCultureIgnoreNonSpaceAndCase)}");
      //System.Console.WriteLine($"     JaroWinklerSimilarity: {a.JaroWinklerSimilarity(b, Flux.StringComparerEx.CurrentCulture)}, {a.JaroWinklerSimilarity(b, Flux.StringComparerEx.CurrentCultureIgnoreCase)}, {a.JaroWinklerSimilarity(b, Flux.StringComparerEx.CurrentCultureIgnoreNonSpace)}, {a.JaroWinklerSimilarity(b, Flux.StringComparerEx.CurrentCultureIgnoreNonSpaceAndCase)}");
      //System.Console.WriteLine($"       LevenshteinDistance: {a.LevenshteinDistance(b, Flux.StringComparerEx.CurrentCulture)}, {a.LevenshteinDistance(b, Flux.StringComparerEx.CurrentCultureIgnoreCase)}, {a.LevenshteinDistance(b, Flux.StringComparerEx.CurrentCultureIgnoreNonSpace)}, {a.LevenshteinDistance(b, Flux.StringComparerEx.CurrentCultureIgnoreNonSpaceAndCase)}");
      //System.Console.WriteLine($"  LongestCommonSubsequence: {a.LongestCommonSubsequence(b, Flux.StringComparerEx.CurrentCulture)}, {a.LongestCommonSubsequence(b, Flux.StringComparerEx.CurrentCultureIgnoreCase)}, {a.LongestCommonSubsequence(b, Flux.StringComparerEx.CurrentCultureIgnoreNonSpace)}, {a.LongestCommonSubsequence(b, Flux.StringComparerEx.CurrentCultureIgnoreNonSpaceAndCase)}");
      //System.Console.WriteLine($"    LongestCommonSubstring: {a.LongestCommonSubstring(b, Flux.StringComparerEx.CurrentCulture)}, {a.LongestCommonSubstring(b, Flux.StringComparerEx.CurrentCultureIgnoreCase)}, {a.LongestCommonSubstring(b, Flux.StringComparerEx.CurrentCultureIgnoreNonSpace)}, {a.LongestCommonSubstring(b, Flux.StringComparerEx.CurrentCultureIgnoreNonSpaceAndCase)}");
      //System.Console.WriteLine($"    OptimalStringAlignment: {a.OptimalStringAlignment(b, Flux.StringComparerEx.CurrentCulture)}, {a.OptimalStringAlignment(b, Flux.StringComparerEx.CurrentCultureIgnoreCase)}, {a.OptimalStringAlignment(b, Flux.StringComparerEx.CurrentCultureIgnoreNonSpace)}, {a.OptimalStringAlignment(b, Flux.StringComparerEx.CurrentCultureIgnoreNonSpaceAndCase)}");
      //System.Console.WriteLine($"        OverlapCoefficient: {a.OverlapCoefficient(b, Flux.StringComparerEx.CurrentCulture)}, {a.OverlapCoefficient(b, Flux.StringComparerEx.CurrentCultureIgnoreCase)}, {a.OverlapCoefficient(b, Flux.StringComparerEx.CurrentCultureIgnoreNonSpace)}, {a.OverlapCoefficient(b, Flux.StringComparerEx.CurrentCultureIgnoreNonSpaceAndCase)}");
      //System.Console.WriteLine($"         SørensenDiceIndex: {a.SørensenDiceIndex(b, Flux.StringComparerEx.CurrentCulture)}, {a.SørensenDiceIndex(b, Flux.StringComparerEx.CurrentCultureIgnoreCase)}, {a.SørensenDiceIndex(b, Flux.StringComparerEx.CurrentCultureIgnoreNonSpace)}, {a.SørensenDiceIndex(b, Flux.StringComparerEx.CurrentCultureIgnoreNonSpaceAndCase)}");
      ////System.Console.WriteLine($"                   Soundex: {x.AsSpan().ToUpperCase().SoundexEncode().ToString()}, {y.AsSpan().ToUpperCase().SoundexEncode().ToString()}");
      ////System.Console.WriteLine($"         SoundexDifference: {x.AsSpan().ToUpperCase().SoundexEncode().SoundexDifference(y.AsSpan().ToUpperCase().SoundexEncode())}");
      ////System.Console.WriteLine($"            RefinedSoundex: {x.AsSpan().ToUpperCase().RefinedSoundexEncode().ToString()}, {y.AsSpan().ToUpperCase().RefinedSoundexEncode().ToString()}");

      return;

      //for (var angle = -Flux.Math.PiX2; angle <= Flux.Math.PiX2; angle += Flux.Math.PiX2 / 12)
      //{
      //  var (x1, y1) = Flux.Math.RotationAngleToCartesian(angle);
      //  var (x2, y2) = Flux.Math.RotationAngleToCartesianEx(angle);

      //  var rad1 = Flux.Math.CartesianToRotationAngle(x1, y1);
      //  var rad2 = Flux.Math.CartesianToRotationAngleEx(x2, y2);

      //  System.Console.WriteLine($"{angle:N6} ({Flux.Math.RadianToDegree(angle):N3}) = ({x1:N6}, {y1:N6}) = {rad1:N6} ({Flux.Math.RadianToDegree(rad1):N3}) = Ex({x2:N6}, {y2:N6}) = {rad2:N6} ({Flux.Math.RadianToDegree(rad2):N3})");
      //}
      //return;

      var op = "Microsoft GS Wavetable Synth";

      using var mop = Flux.Media.Midi.MidiOut.Create(op);

      System.Console.WriteLine($"{mop.Index} = {mop.Capabilities.Manufacturer}, {mop.Capabilities.Name}, {mop.Capabilities.DriverVersion}");

      var c = new byte[] { 0x90, 0x3C, 0x7F };
      var e = new byte[] { 0x90, 0x40, 0x7F };
      var g = new byte[] { 0x90, 0x43, 0x7F };

      var chord = new byte[] { 0x90, 0x3C, 0x7F, 0x90, 0x40, 0x7F, 0x90, 0x43, 0x7F };


      foreach (var index in Flux.LinqEx.AlternatingRange(60, 24, 1, LinqEx.AlternatingRangeDirection.AwayFromMean))
      {
        mop.NoteOn(0, index, 127);
        System.Threading.Thread.Sleep(40);
        mop.NoteOff(0, index, 127);
        System.Threading.Thread.Sleep(40);
      }
      //mop.TrySend(0x90, 0x3C, 0x7F );
      //mop.TrySend(0x007F3C90);
      System.Console.ReadKey();
      //System.Console.WriteLine($"Out: {Flux.Media.WinMm.midiOutGetNumDevs()}");
      //if(Flux.Media.WinMm.midiOutGetNumDevs() > 0)
      //{
      //  foreach(var c in Flux.Media.MidiOutPort.GetDevicePorts())
      //  {
      //    System.Console.WriteLine($"{c.Manufacturer}, {c.Name}, {c.DriverVersion}");
      //  }
      //}


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
