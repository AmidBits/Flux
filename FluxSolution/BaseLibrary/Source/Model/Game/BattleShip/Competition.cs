//using System.Linq;

//namespace Flux.Model.Game.BattleShip
//{
//  using System;
//  using System.Collections.Generic;
//  using System.Diagnostics;
//  using System.Linq;

//  public class BattleshipCompetition
//  {
//    private IPlayer op1;
//    private IPlayer op2;
//    private TimeSpan timePerGame;
//    private int wins;
//    private bool playOut;
//    private System.Drawing.Size boardSize;
//    private List<int> shipSizes;

//    public BattleshipCompetition(IPlayer op1, IPlayer op2, TimeSpan timePerGame, int wins, bool playOut, System.Drawing.Size boardSize, params int[] shipSizes)
//    {
//      if (op1 == null)
//      {
//        throw new ArgumentNullException("op1");
//      }

//      if (op2 == null)
//      {
//        throw new ArgumentNullException("op2");
//      }

//      if (timePerGame.TotalMilliseconds <= 0)
//      {
//        throw new ArgumentOutOfRangeException("timePerGame");
//      }

//      if (wins <= 0)
//      {
//        throw new ArgumentOutOfRangeException("wins");
//      }

//      if (boardSize.Width <= 2 || boardSize.Height <= 2)
//      {
//        throw new ArgumentOutOfRangeException("boardSize");
//      }

//      if (shipSizes == null || shipSizes.Length < 1)
//      {
//        throw new ArgumentNullException("shipSizes");
//      }

//      if (shipSizes.Where(s => s <= 0).Any())
//      {
//        throw new ArgumentOutOfRangeException("shipSizes");
//      }

//      if (shipSizes.Sum() >= (boardSize.Width * boardSize.Height))
//      {
//        throw new ArgumentOutOfRangeException("shipSizes");
//      }

//      this.op1 = op1;
//      this.op2 = op2;
//      this.timePerGame = timePerGame;
//      this.wins = wins;
//      this.playOut = playOut;
//      this.boardSize = boardSize;
//      this.shipSizes = new List<int>(shipSizes);
//    }

//    public Dictionary<IPlayer, int> RunCompetition()
//    {
//      var first = new Player
//      {
//        Opponent = this.op1,
//        Score = 0,
//        Stopwatch = new Stopwatch(),
//        Shots = new HashSet<System.Drawing.Point>(),
//      };

//      var second = new Player
//      {
//        Opponent = this.op2,
//        Score = 0,
//        Stopwatch = new Stopwatch(),
//        Shots = new HashSet<System.Drawing.Point>(),
//      };

//      var rand = new Random();
//      if (rand.NextDouble() >= 0.5)
//      {
//        var swap = first;
//        first = second;
//        second = swap;
//      }

//      first.Opponent.NewMatch(second.Opponent.Name + " " + second.Opponent.Version.ToString());
//      second.Opponent.NewMatch(first.Opponent.Name + " " + first.Opponent.Version.ToString());

//      while (true)
//      {
//        if ((!this.playOut && Math.Max(first.Score, second.Score) >= this.wins) ||
//            (this.playOut && (first.Score + second.Score) >= (this.wins * 2 - 1)))
//        {
//          break;
//        }

//        Swap(ref first, ref second);

//        first.Reset();
//        second.Reset();

//        try
//        {
//          first.Time(o => o.NewGame(this.boardSize, this.timePerGame, this.shipSizes.ToArray()), this.timePerGame);
//          second.Time(o => o.NewGame(this.boardSize, this.timePerGame, this.shipSizes.ToArray()), this.timePerGame);

//          SetUpShips(first);
//          SetUpShips(second);

//          while (true)
//          {
//            System.Drawing.Point shot = GetShot(first);

//            second.Time(o => o.OpponentShot(shot), this.timePerGame);

//            var ship = second.Ships.Where(s => Ship.Intersect(s, shot)).SingleOrDefault();

//            if (ship != null)
//            {
//              var sunk = Ship.IsSunk(ship, second.Shots);
//              first.Time(o => o.ShotHit(shot, sunk), this.timePerGame);

//              if (sunk)
//              {
//                second.Ships.Remove(ship);
//              }
//            }
//            else
//            {
//              first.Time(o => o.ShotMiss(shot), this.timePerGame);
//            }

//            if (second.Ships.Count == 0) { RecordWin(first, second); break; }

//            Swap(ref first, ref second);
//          }
//        }
//        catch (PlayerTimedOutException ex)
//        {
//          RecordWin(ex.Player == first ? second : first, ex.Player);
//        }
//      }

//      first.Opponent.MatchOver();
//      first.Opponent.MatchOver();

//      var results = new Dictionary<IPlayer, int>();
//      results.Add(first.Opponent, first.Score);
//      results.Add(second.Opponent, second.Score);
//      return results;
//    }

//    private IList<Ship> SetUpShips(Player player)
//    {
//      IList<Ship> ships;
//      do
//      {
//        ships = player.Time(o => o.PlaceShips(), this.timePerGame);
//        player.Ships = Validate(ships);
//      } while (player.Ships == null);

//      return ships;
//    }

//    private System.Drawing.Point GetShot(Player player)
//    {
//      System.Drawing.Point shot;
//      do
//      {
//        shot = player.Time(o => o.GetShot(), this.timePerGame);
//      } while (!player.Shots.Add(shot));
//      return shot;
//    }

//    private void Swap(ref Player first, ref Player second)
//    {
//      var swap = first;
//      first = second;
//      second = swap;
//    }

//    private List<Ship> Validate(IList<Ship> ships)
//    {
//      if (ships == null)
//      {
//        return null;
//      }

//      var copy = ships.ToList();

//      if (copy.Any(s => s == null || !s.IsValid(this.boardSize)))
//      {
//        return null;
//      }

//      var lengths = this.shipSizes.ToList();
//      foreach (var s in copy)
//      {
//        if (!lengths.Remove(s.Length))
//        {
//          return null;
//        }
//      }

//      if (lengths.Any())
//      {
//        return null;
//      }

//      for (int i = 0; i < copy.Count; i++)
//      {
//        for (int j = i + 1; j < copy.Count; j++)
//        {
//          if (Ship.Intersect(copy[i], copy[j]))
//          {
//            return null;
//          }
//        }
//      }

//      return copy;
//    }

//    private void RecordWin(Player winner, Player loser)
//    {
//      winner.Score++;
//      winner.Opponent.GameWon();
//      loser.Opponent.GameLost();
//    }

//    private class Player
//    {
//      public IPlayer Opponent { get; set; }

//      public int Score { get; set; }

//      public Stopwatch Stopwatch { get; set; }

//      public HashSet<System.Drawing.Point> Shots { get; set; }

//      public IList<Ship> Ships { get; set; }

//      public void Reset()
//      {
//        this.Stopwatch.Reset();
//        this.Shots.Clear();
//      }

//      public void Time(Action<IPlayer> action, TimeSpan timePerGame)
//      {
//        this.Stopwatch.Start();
//        action(this.Opponent);
//        this.Stopwatch.Stop();

//        if (this.Stopwatch.Elapsed > timePerGame)
//        {
//          throw new PlayerTimedOutException(this);
//        }
//      }

//      public T Time<T>(Func<IPlayer, T> action, TimeSpan timePerGame)
//      {
//        this.Stopwatch.Start();
//        var result = action(this.Opponent);
//        this.Stopwatch.Stop();

//        if (this.Stopwatch.Elapsed > timePerGame)
//        {
//          throw new PlayerTimedOutException(this);
//        }

//        return result;
//      }
//    }

//    private class PlayerTimedOutException : Exception
//    {
//      public PlayerTimedOutException(Player player)
//      {
//        this.Player = player;
//      }

//      public Player Player { get; private set; }
//    }
//  }
//}
