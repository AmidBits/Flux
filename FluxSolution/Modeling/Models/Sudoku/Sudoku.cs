using System.Linq;

namespace Flux.Model
{
  /// <summary></summary>
  /// <see cref="http://www.norvig.com/sudoku.html"/>
  public static partial class Sudoku
  {
    public const string ColumnLabels = @"123456789";
    public const string RowLabels = @"ABCDEFGHI";

    /// <summary>A list of squares representing the Sudoku board using row characters and column digits, e.g. "C2".</summary>
    public static System.Collections.Generic.List<int> Squares
      => System.Linq.Enumerable.Range(0, 81).ToList();
    /// <summary>A list of all squares and their respective list of 3 unit lists, e.g. { "C2", [ ["A2", "B2", "C2", "D2", "E2", "F2", "G2", "H2", "I2"], ["C1", "C2", "C3", "C4", "C5", "C6", "C7", "C8", "C9"], ["A1", "A2", "A3", "B1", "B2", "B3", "C1", "C2", "C3"] ] }.</summary>
    public static System.Collections.Generic.List<System.Collections.Generic.List<System.Collections.Generic.List<int>>> Units
      => Squares.Select(i => new System.Collections.Generic.List<int>[] { GetUnitColumn(i).ToList(), GetUnitRow(i).ToList(), GetUnitBox(i).ToList() }.ToList()).ToList();
    //=> Squares.Select(i => System.Linq.Enumerable.Empty<System.Collections.Generic.List<int>>().Append(GetUnitColumn(i).ToList(), GetUnitRow(i).ToList(), GetUnitBox(i).ToList()).ToList()).ToList();
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
    public static System.Collections.Generic.List<string>? Solve(string puzzle)
      => Search(Parse(puzzle).Last());

    /// <summary>Create a new random sudoku puzzle.</summary>
    public static System.Collections.Generic.List<string> Create(System.Random rng, int count = 17)
    {
      var values = Squares.Select(i => ColumnLabels).ToList();

      foreach (var square in Squares.GetRandomElements(1, rng))
      {
        values[square].TryRandom(out var value, rng);

        if (Assign(values, square, value.ToString()) == null)
        {
          break;
        }

        if (values.Where(s => s.Length == 1) is System.Collections.Generic.IEnumerable<string> ones && ones.Count() >= count && ones.Distinct().Count() >= 8)
        {
          return values.Select(s => s.Length == 1 ? s : ColumnLabels).ToList();
        }
      }

      return Create(rng, count);
    }

    /// <summary>Create a new sequence representing the 9 sudoku "box" squares the index is found in.</summary>
    public static System.Collections.Generic.IEnumerable<int> GetUnitBox(int index)
    {
      var baseUnitBoxIndex = (index % 9 / 3 * 3) + (index / 27 * 27);

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
    /// <summary>Create a new sequence representing the 9 sudoku "column" (vertical) squares the index is found in.</summary>
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
    /// <summary>Create a new sequence representing the 9 sudoku "row" (horizontal) squares the index is found in.</summary>
    public static System.Collections.Generic.IEnumerable<int> GetUnitRow(int index)
    {
      var baseUnitRowIndex = index / 9 * 9;
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

    /// <summary>Creates a string made for the console.</summary>
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

      return sb.ToString()[..(sb.Length - 2)];
    }

    public static int ToIndex(string label)
      => label is null ? throw new System.ArgumentNullException(nameof(label)) : (label.Length == 2 && RowLabels.Contains(label[0], System.StringComparison.Ordinal) && ColumnLabels.Contains(label[1], System.StringComparison.Ordinal)) ? RowLabels.IndexOf(label[0], System.StringComparison.Ordinal) * 9 + ColumnLabels.IndexOf(label[1], System.StringComparison.Ordinal) : throw new System.ArgumentOutOfRangeException(nameof(label));
    public static string ToLabel(int index)
      => (index >= 0 && index < 81) ? $"{RowLabels[index / 9]}{ColumnLabels[index % 9]}" : throw new System.ArgumentOutOfRangeException(nameof(index));

    public static string ToString(System.Collections.Generic.List<string> puzzle)
      => string.Concat(puzzle.Select(s => s.Length == 1 ? s : @"."));
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
