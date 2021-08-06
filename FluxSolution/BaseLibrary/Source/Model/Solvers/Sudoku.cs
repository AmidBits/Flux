using System.Linq;

namespace Flux.Model
{
  /// <summary></summary>
  /// <see cref="http://www.norvig.com/sudoku.html"/>
  public static class Sudoku
  {
    public const string ColumnLabels = @"123456789";
    public const string RowLabels = @"ABCDEFGHI";

    /// <summary>A list of squares representing the Sudoku board using row characters and column digits, e.g. "C2".</summary>
    public static System.Collections.Generic.List<int> Squares
      => System.Linq.Enumerable.Range(0, 81).ToList();

    /// <summary>A list of all squares and their respective list of 3 unit lists, e.g. { "C2", [ ["A2", "B2", "C2", "D2", "E2", "F2", "G2", "H2", "I2"], ["C1", "C2", "C3", "C4", "C5", "C6", "C7", "C8", "C9"], ["A1", "A2", "A3", "B1", "B2", "B3", "C1", "C2", "C3"] ] }.</summary>
    public static System.Collections.Generic.List<System.Collections.Generic.List<System.Collections.Generic.List<int>>> Units
      => Squares.Select(i => System.Linq.Enumerable.Empty<System.Collections.Generic.List<int>>().Append(GetUnitColumn(i).ToList(), GetUnitRow(i).ToList(), GetUnitBox(i).ToList()).ToList()).ToList();

    /// <summary>A list of all squares and their respective list of 20 peer squares, e.g. ["A2", "B2", "D2", "E2", "F2", "G2", "H2", "I2", "C1", "C3", "C4", "C5", "C6", "C7", "C8", "C9", "A1", "A3", "B1", "B3"].</summary>
    public static System.Collections.Generic.List<System.Collections.Generic.List<int>> Peers
      => Squares.Select(i => Units[i].SelectMany(l => l).Distinct().Where(sx => sx != i).ToList()).ToList();

    /// <summary>Assign a single digit to a square, by eliminating all the other values (except the specified digit) from values[square] and propagate.</summary>
    /// <returns>Null in case of failure to assign, otherwise the updated values dictionary is returned.</returns>
    public static System.Collections.Generic.List<string>? Assign(System.Collections.Generic.List<string> values, int square, string digit)
    {
      if (values is null) throw new System.ArgumentNullException(nameof(values));

      if (values[square].Where(d => d.ToString() != digit).Any(d => Eliminate(values, square, d.ToString()) == null))
        return null;

      return values;
    }
    /// <summary>Eliminate a digit from values[square] and propagate when values or places is less than or equal to 2.</summary>
    /// <returns>Null in case of failure to eliminate, otherwise the updated values dictionary is returned.</returns>
    public static System.Collections.Generic.List<string>? Eliminate(System.Collections.Generic.List<string> values, int square, string digit)
    {
      if (values is null) throw new System.ArgumentNullException(nameof(values));

      if (!values[square].Contains(digit, System.StringComparison.Ordinal))
      {
        return values; // already eliminated
      }

      values[square] = values[square].Replace(digit, string.Empty, System.StringComparison.Ordinal);

      #region 1: if a square is reduced to only one possible digit (d), then eliminate d from the square's peers

      if (values[square].Length == 0) // contradiction, the last digit was just removed
      {
        return null;
      }
      else if (values[square].Length == 1)
      {
        var d = values[square];

        if (Peers[square].Any(s => Eliminate(values, s, d) == null))
        {
          return null;
        }
      }
      #endregion

      #region 2: if a unit is reduced to only one possible place for a digit, then assign it there.
      foreach (var unit in Units[square])
      {
        var places = unit.Where(s => values[s].Contains(digit, System.StringComparison.Ordinal));

        if (!places.Any()) // contradiction; there is no place for this digit
        {
          return null;
        }
        else if (places.Count() == 1) // digit can only be in one place in a unit; assign it there
        {
          if (Assign(values, places.First(), digit) == null)
          {
            return null;
          }
        }
      }
      #endregion

      return values;
    }
    /// <summary>Parse puzzle into a dictionary of possible values, {square: digits}. Yield returns the state after each numeric alteration made while parsing.</summary>
    /// <returns>Null in case of failure to parse puzzle, otherwise a new values dictionary is returned, with the initial knowns processed via the constraint propagation loop.</returns>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<string>?> Parse(string puzzle)
    {
      var values = Squares.Select(i => ColumnLabels).ToList();

      foreach (var (square, character) in Squares.Zip(puzzle, (s, c) => (square: s, character: c)))
      {
        if (char.IsDigit(character))
        {
          if (char.IsDigit(character) && Assign(values, square, character.ToString()) == null)
          {
            yield return null;
            yield break;
          }

          yield return values;
        }
      }
    }
    /// <summary>Search for a solution, by using depth-first search and propagation, try all possible values.</summary>
    /// <returns>Null in case of failure to search and find a solution, otherwise the updated values dictionary is returned.</returns>
    public static System.Collections.Generic.List<string>? Search(System.Collections.Generic.List<string>? values)
    {
      if (values == null)
      {
        return null; // failed earlier in the process
      }

      if (values.All(digits => digits.Length == 1))
      {
        return values; // puzzle has been solved
      }

      var square = values.Select((digits, index) => (digits, index)).Where(t => t.digits.Length > 1).OrderBy(t => t.digits.Length).First().index; // choose an unassigned square with the fewest possibilities

      return values[square].Select(digit => Search(Assign(values.ToList(), square, digit.ToString()))).FirstOrDefault(d => d != null);
    }

    /// <summary>Solve the specified puzzle by process of variable/value ordering searching and constraint propagation.</summary>
    /// <returns>Null in case of failure to solve and find a solution, otherwise a solved values dictionary is returned.</returns>
    public static System.Collections.Generic.List<string>? Solve(string puzzle) => Search(Parse(puzzle).Last());

    /// <summary>Create a new random sudoku puzzle.</summary>
    public static System.Collections.Generic.List<string> Create(int count = 17)
    {
      var values = Squares.Select(i => ColumnLabels).ToList();

      foreach (var square in Squares.RandomElements(1))
      {
        values[square].RandomElement(out var value);

        if (Assign(values, square, value.ToString()) == null)
        {
          break;
        }

        if (values.Where(s => s.Length == 1) is System.Collections.Generic.IEnumerable<string> ones && ones.Count() >= count && ones.Distinct().Count() >= 8)
        {
          return values.Select(s => s.Length == 1 ? s : ColumnLabels).ToList();
        }
      }

      return Create(count);
    }

    public static System.Collections.Generic.IEnumerable<int> GetUnitBox(int index)
    {
      var baseUnitBoxIndex = (((index % 9) / 3) * 3) + ((index / 27) * 27);

      yield return baseUnitBoxIndex + 0;
      yield return baseUnitBoxIndex + 1;
      yield return baseUnitBoxIndex + 2;
      yield return baseUnitBoxIndex + 9;
      yield return baseUnitBoxIndex + 10;
      yield return baseUnitBoxIndex + 11;
      yield return baseUnitBoxIndex + 18;
      yield return baseUnitBoxIndex + 19;
      yield return baseUnitBoxIndex + 20;
    }

    public static System.Collections.Generic.IEnumerable<int> GetUnitColumn(int index)
    {
      var baseUnitColumnIndex = index % 9;

      yield return baseUnitColumnIndex + 0;
      yield return baseUnitColumnIndex + 9;
      yield return baseUnitColumnIndex + 18;
      yield return baseUnitColumnIndex + 27;
      yield return baseUnitColumnIndex + 36;
      yield return baseUnitColumnIndex + 45;
      yield return baseUnitColumnIndex + 54;
      yield return baseUnitColumnIndex + 63;
      yield return baseUnitColumnIndex + 72;
    }

    public static System.Collections.Generic.IEnumerable<int> GetUnitRow(int index)
    {
      var baseUnitRowIndex = (index / 9) * 9;

      yield return baseUnitRowIndex + 0;
      yield return baseUnitRowIndex + 1;
      yield return baseUnitRowIndex + 2;
      yield return baseUnitRowIndex + 3;
      yield return baseUnitRowIndex + 4;
      yield return baseUnitRowIndex + 5;
      yield return baseUnitRowIndex + 6;
      yield return baseUnitRowIndex + 7;
      yield return baseUnitRowIndex + 8;
    }

    public static string ToConsoleString(System.Collections.Generic.List<string> values)
    {
      if (values is null) throw new System.ArgumentNullException(nameof(values));

      var sb = new System.Text.StringBuilder();

      var width = 1 + values.Max(s => s.Length);

      foreach (var r in Sudoku.RowLabels)
      {
        foreach (var c in Sudoku.ColumnLabels)
        {
          sb.Append(values[Sudoku.ToIndex(r.ToString() + c.ToString())].ToStringBuilder().PadEven(width, ' ', ' '));

          if (c == '9') { sb.AppendLine(); } // After each unit row.
        }
      }

      return sb.ToString().Substring(0, sb.Length - 2);
    }

    public static int ToIndex(string label) => label is null ? throw new System.ArgumentNullException(nameof(label)) : (label.Length == 2 && RowLabels.Contains(label[0], System.StringComparison.Ordinal) && ColumnLabels.Contains(label[1], System.StringComparison.Ordinal)) ? RowLabels.IndexOf(label[0], System.StringComparison.Ordinal) * 9 + ColumnLabels.IndexOf(label[1], System.StringComparison.Ordinal) : throw new System.ArgumentOutOfRangeException(nameof(label));
    public static string ToLabel(int index) => (index >= 0 && index < 81) ? $"{RowLabels[index / 9]}{ColumnLabels[index % 9]}" : throw new System.ArgumentOutOfRangeException(nameof(index));

    public static string ToString(System.Collections.Generic.List<string> puzzle) => string.Concat(puzzle.Select(s => s.Length == 1 ? s : @"."));

    #region Preset Puzzles
    public const string Puzzle1439 = ".....5.8....6.1.43..........1.5........1.6...3.......553.....61........4.........";

    public static readonly string[] Puzzles11 = new string[]
    {
      "85...24..72......9..4.........1.7..23.5...9...4...........8..7..17..........36.4.",
      "..53.....8......2..7..1.5..4....53...1..7...6..32...8..6.5....9..4....3......97..",
      "12..4......5.69.1...9...5.........7.7...52.9..3......2.9.6...5.4..9..8.1..3...9.4",
      "...57..3.1......2.7...234......8...4..7..4...49....6.5.42...3.....7..9....18.....",
      "7..1523........92....3.....1....47.8.......6............9...5.6.4.9.7...8....6.1.",
      "1....7.9..3..2...8..96..5....53..9...1..8...26....4...3......1..4......7..7...3..",
      "1...34.8....8..5....4.6..21.18......3..1.2..6......81.52..7.9....6..9....9.64...2",
      "...92......68.3...19..7...623..4.1....1...7....8.3..297...8..91...5.72......64...",
      ".6.5.4.3.1...9...8.........9...5...6.4.6.2.7.7...4...5.........4...8...1.5.2.3.4.",
      "7.....4...2..7..8...3..8.799..5..3...6..2..9...1.97..6...3..9...3..4..6...9..1.35",
      "....7..2.8.......6.1.2.5...9.54....8.........3....85.1...3.2.8.4.......9.7..6...."
    };

    public static readonly string[] Puzzles95 = new string[]
    {
      "4.....8.5.3..........7......2.....6.....8.4......1.......6.3.7.5..2.....1.4......",
      "52...6.........7.13...........4..8..6......5...........418.........3..2...87.....",
      "6.....8.3.4.7.................5.4.7.3..2.....1.6.......2.....5.....8.6......1....",
      "48.3............71.2.......7.5....6....2..8.............1.76...3.....4......5....",
      "....14....3....2...7..........9...3.6.1.............8.2.....1.4....5.6.....7.8...",
      "......52..8.4......3...9...5.1...6..2..7........3.....6...1..........7.4.......3.",
      "6.2.5.........3.4..........43...8....1....2........7..5..27...........81...6.....",
      ".524.........7.1..............8.2...3.....6...9.5.....1.6.3...........897........",
      "6.2.5.........4.3..........43...8....1....2........7..5..27...........81...6.....",
      ".923.........8.1...........1.7.4...........658.........6.5.2...4.....7.....9.....",
      "6..3.2....5.....1..........7.26............543.........8.15........4.2........7..",
      ".6.5.1.9.1...9..539....7....4.8...7.......5.8.817.5.3.....5.2............76..8...",
      "..5...987.4..5...1..7......2...48....9.1.....6..2.....3..6..2.......9.7.......5..",
      "3.6.7...........518.........1.4.5...7.....6.....2......2.....4.....8.3.....5.....",
      "1.....3.8.7.4..............2.3.1...........958.........5.6...7.....8.2...4.......",
      "6..3.2....4.....1..........7.26............543.........8.15........4.2........7..",
      "....3..9....2....1.5.9..............1.2.8.4.6.8.5...2..75......4.1..6..3.....4.6.",
      "45.....3....8.1....9...........5..9.2..7.....8.........1..4..........7.2...6..8..",
      ".237....68...6.59.9.....7......4.97.3.7.96..2.........5..47.........2....8.......",
      "..84...3....3.....9....157479...8........7..514.....2...9.6...2.5....4......9..56",
      ".98.1....2......6.............3.2.5..84.........6.........4.8.93..5...........1..",
      "..247..58..............1.4.....2...9528.9.4....9...1.........3.3....75..685..2...",
      "4.....8.5.3..........7......2.....6.....5.4......1.......6.3.7.5..2.....1.9......",
      ".2.3......63.....58.......15....9.3....7........1....8.879..26......6.7...6..7..4",
      "1.....7.9.4...72..8.........7..1..6.3.......5.6..4..2.........8..53...7.7.2....46",
      "4.....3.....8.2......7........1...8734.......6........5...6........1.4...82......",
      ".......71.2.8........4.3...7...6..5....2..3..9........6...7.....8....4......5....",
      "6..3.2....4.....8..........7.26............543.........8.15........8.2........7..",
      ".47.8...1............6..7..6....357......5....1..6....28..4.....9.1...4.....2.69.",
      "......8.17..2........5.6......7...5..1....3...8.......5......2..4..8....6...3....",
      "38.6.......9.......2..3.51......5....3..1..6....4......17.5..8.......9.......7.32",
      "...5...........5.697.....2...48.2...25.1...3..8..3.........4.7..13.5..9..2...31..",
      ".2.......3.5.62..9.68...3...5..........64.8.2..47..9....3.....1.....6...17.43....",
      ".8..4....3......1........2...5...4.69..1..8..2...........3.9....6....5.....2.....",
      "..8.9.1...6.5...2......6....3.1.7.5.........9..4...3...5....2...7...3.8.2..7....4",
      "4.....5.8.3..........7......2.....6.....5.8......1.......6.3.7.5..2.....1.8......",
      "1.....3.8.6.4..............2.3.1...........958.........5.6...7.....8.2...4.......",
      "1....6.8..64..........4...7....9.6...7.4..5..5...7.1...5....32.3....8...4........",
      "249.6...3.3....2..8.......5.....6......2......1..4.82..9.5..7....4.....1.7...3...",
      "...8....9.873...4.6..7.......85..97...........43..75.......3....3...145.4....2..1",
      "...5.1....9....8...6.......4.1..........7..9........3.8.....1.5...2..4.....36....",
      "......8.16..2........7.5......6...2..1....3...8.......2......7..3..8....5...4....",
      ".476...5.8.3.....2.....9......8.5..6...1.....6.24......78...51...6....4..9...4..7",
      ".....7.95.....1...86..2.....2..73..85......6...3..49..3.5...41724................",
      ".4.5.....8...9..3..76.2.....146..........9..7.....36....1..4.5..6......3..71..2..",
      ".834.........7..5...........4.1.8..........27...3.....2.6.5....5.....8........1..",
      "..9.....3.....9...7.....5.6..65..4.....3......28......3..75.6..6...........12.3.8",
      ".26.39......6....19.....7.......4..9.5....2....85.....3..2..9..4....762.........4",
      "2.3.8....8..7...........1...6.5.7...4......3....1............82.5....6...1.......",
      "6..3.2....1.....5..........7.26............843.........8.15........8.2........7..",
      "1.....9...64..1.7..7..4.......3.....3.89..5....7....2.....6.7.9.....4.1....129.3.",
      ".........9......84.623...5....6...453...1...6...9...7....1.....4.5..2....3.8....9",
      ".2....5938..5..46.94..6...8..2.3.....6..8.73.7..2.........4.38..7....6..........5",
      "9.4..5...25.6..1..31......8.7...9...4..26......147....7.......2...3..8.6.4.....9.",
      "...52.....9...3..4......7...1.....4..8..453..6...1...87.2........8....32.4..8..1.",
      "53..2.9...24.3..5...9..........1.827...7.........981.............64....91.2.5.43.",
      "1....786...7..8.1.8..2....9........24...1......9..5...6.8..........5.9.......93.4",
      "....5...11......7..6.....8......4.....9.1.3.....596.2..8..62..7..7......3.5.7.2..",
      ".47.2....8....1....3....9.2.....5...6..81..5.....4.....7....3.4...9...1.4..27.8..",
      "......94.....9...53....5.7..8.4..1..463...........7.8.8..7.....7......28.5.26....",
      ".2......6....41.....78....1......7....37.....6..412....1..74..5..8.5..7......39..",
      "1.....3.8.6.4..............2.3.1...........758.........7.5...6.....8.2...4.......",
      "2....1.9..1..3.7..9..8...2.......85..6.4.........7...3.2.3...6....5.....1.9...2.5",
      "..7..8.....6.2.3...3......9.1..5..6.....1.....7.9....2........4.83..4...26....51.",
      "...36....85.......9.4..8........68.........17..9..45...1.5...6.4....9..2.....3...",
      "34.6.......7.......2..8.57......5....7..1..2....4......36.2..1.......9.......7.82",
      "......4.18..2........6.7......8...6..4....3...1.......6......2..5..1....7...3....",
      ".4..5..67...1...4....2.....1..8..3........2...6...........4..5.3.....8..2........",
      ".......4...2..4..1.7..5..9...3..7....4..6....6..1..8...2....1..85.9...6.....8...3",
      "8..7....4.5....6............3.97...8....43..5....2.9....6......2...6...7.71..83.2",
      ".8...4.5....7..3............1..85...6.....2......4....3.26............417........",
      "....7..8...6...5...2...3.61.1...7..2..8..534.2..9.......2......58...6.3.4...1....",
      "......8.16..2........7.5......6...2..1....3...8.......2......7..4..8....5...3....",
      ".2..........6....3.74.8.........3..2.8..4..1.6..5.........1.78.5....9..........4.",
      ".52..68.......7.2.......6....48..9..2..41......1.....8..61..38.....9...63..6..1.9",
      "....1.78.5....9..........4..2..........6....3.74.8.........3..2.8..4..1.6..5.....",
      "1.......3.6.3..7...7...5..121.7...9...7........8.1..2....8.64....9.2..6....4.....",
      "4...7.1....19.46.5.....1......7....2..2.3....847..6....14...8.6.2....3..6...9....",
      "......8.17..2........5.6......7...5..1....3...8.......5......2..3..8....6...4....",
      "963......1....8......2.5....4.8......1....7......3..257......3...9.2.4.7......9..",
      "15.3......7..4.2....4.72.....8.........9..1.8.1..8.79......38...........6....7423",
      "..........5724...98....947...9..3...5..9..12...3.1.9...6....25....56.....7......6",
      "....75....1..2.....4...3...5.....3.2...8...1.......6.....1..48.2........7........",
      "6.....7.3.4.8.................5.4.8.7..2.....1.3.......2.....5.....7.9......1....",
      "....6...4..6.3....1..4..5.77.....8.5...8.....6.8....9...2.9....4....32....97..1..",
      ".32.....58..3.....9.428...1...4...39...6...5.....1.....2...67.8.....4....95....6.",
      "...5.3.......6.7..5.8....1636..2.......4.1.......3...567....2.8..4.7.......2..5..",
      ".5.3.7.4.1.........3.......5.8.3.61....8..5.9.6..1........4...6...6927....2...9..",
      "..5..8..18......9.......78....4.....64....9......53..2.6.........138..5....9.714.",
      "..........72.6.1....51...82.8...13..4.........37.9..1.....238..5.4..9.........79.",
      "...658.....4......12............96.7...3..5....2.8...3..19..8..3.6.....4....473..",
      ".2.3.......6..8.9.83.5........2...8.7.9..5........6..4.......1...1...4.22..7..8.9",
      ".5..9....1.....6.....3.8.....8.4...9514.......3....2..........4.8...6..77..15..6.",
      ".....2.......7...17..3...9.8..7......2.89.6...13..6....9..5.824.....891..........",
      "3...8.......7....51..............36...2..4....7...........6.13..452...........8.."
    };
    #endregion
  }
}

/*
    var create = Flux.Model.Sudoku.Create();
    var createString = Flux.Model.Sudoku.ToString(create);

    var search = Flux.Model.Sudoku.Solve(createString);
    var searchString = Flux.Model.Sudoku.ToString(search);

    System.Console.WriteLine(createString);
    System.Console.WriteLine(searchString);
*/
