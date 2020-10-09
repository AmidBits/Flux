//using System.Linq;

//namespace Flux.Model.Game.BattleShip
//{
//  using System;
//  using System.Collections.Generic;
//  using System.Diagnostics;

//  public interface IOffense
//  {
//    void startGame(int[] ship_sizes);

//    System.Drawing.Point getShot();

//    void shotMiss(System.Drawing.Point p);

//    void shotHit(System.Drawing.Point p);

//    void shotSunk(System.Drawing.Point p);

//    void endGame();
//  }

//  public interface IDefense
//  {
//    List<Ship> startGame(int[] ship_sizes);

//    void shot(System.Drawing.Point p);

//    void endGame();
//  }

//  public enum SeaState
//  {
//    Clear = 0,
//    Miss = 1,
//    Hit = 2,
//    Sunk = 3,
//  }

//  public class GameState
//  {
//    public SeaState[,] state;
//    List<int> orig_ship_sizes;
//    System.Drawing.Size boardSize;

//    Dictionary<int, int> size_counts;

//    // possible positions for ships.  Keyed by ship size.
//    Dictionary<int, List<Ship>> positions;

//    // list of possibilities for ships with at least one hit.
//    public List<List<Ship>> ship_possibilities;

//    public GameState(int w, int h, int[] ship_sizes)
//    {
//      boardSize = new System.Drawing.Size(w, h);
//      state = new SeaState[w, h];
//      orig_ship_sizes = new List<int>();
//      size_counts = new Dictionary<int, int>();
//      foreach (int s in ship_sizes)
//      {
//        orig_ship_sizes.Add(s);
//        if (!size_counts.ContainsKey(s)) size_counts[s] = 0;
//        size_counts[s]++;
//      }
//      ship_possibilities = new List<List<Ship>>();
//      ship_possibilities.Add(new List<Ship>());

//      int max_size = 0;
//      foreach (int size in orig_ship_sizes) if (size > max_size) max_size = size;

//      positions = new Dictionary<int, List<Ship>>();
//      foreach (int len in size_counts.Keys)
//      {
//        positions[len] = new List<Ship>();
//        for (int x = 0; x < w; x++)
//        {
//          for (int y = 0; y < h; y++)
//          {
//            for (int orient = 0; orient < 2; orient++)
//            {
//              int dy = orient;
//              int dx = 1 - dy;
//              if (x + len * dx > w) continue;
//              if (y + len * dy > h) continue;
//              Ship s = new Ship(len, new System.Drawing.Point(x, y), (ShipOrientation)orient);
//              positions[len].Add(s);
//            }
//          }
//        }
//      }
//    }

//    public bool valid()
//    {
//      // no place for some ship
//      foreach (int size in size_counts.Keys)
//      {
//        if (positions[size].Count < size_counts[size]) return false;
//      }
//      // something inconsistent about possibilities
//      if (ship_possibilities.Count == 0) return false;
//      return true;
//    }

//    public SeaState get(int x, int y)
//    {
//      if (!valid()) throw new ApplicationException("get on bad state");
//      return state[x, y];
//    }

//    public SeaState get(System.Drawing.Point p)
//    {
//      return get(p.X, p.Y);
//    }

//    public List<int> remaining_ship_sizes()
//    {
//      if (!valid()) throw new ApplicationException("get on bad state");
//      int max_size = 0;
//      foreach (int size in orig_ship_sizes) if (size > max_size) max_size = size;
//      int[] orig_histo = new int[max_size + 1];
//      foreach (int size in orig_ship_sizes) orig_histo[size]++;

//      int[] max_histo = new int[max_size + 1];
//      int[] histo = new int[max_size + 1];
//      foreach (List<Ship> list in ship_possibilities)
//      {
//        for (int i = 0; i <= max_size; i++) histo[i] = orig_histo[i];
//        foreach (Ship s in list)
//        {
//          histo[s.Length]--;
//        }
//        for (int i = 0; i <= max_size; i++)
//        {
//          if (histo[i] > max_histo[i]) max_histo[i] = histo[i];
//        }
//      }
//      List<int> sizes = new List<int>();
//      for (int i = 0; i <= max_size; i++)
//      {
//        for (int j = 0; j < max_histo[i]; j++)
//        {
//          sizes.Add(i);
//        }
//      }
//#if DEBUG
//      Console.Write("remaining sizes:");
//      foreach (int s in sizes) Console.Write(" {0}", s);
//      Console.WriteLine();
//#endif
//      return sizes;
//    }

//    public void addMiss(System.Drawing.Point p)
//    {
//      if (state[p.X, p.Y] != SeaState.Clear)
//      {
//        ship_possibilities.Clear();
//        return;
//      }
//      state[p.X, p.Y] = SeaState.Miss;
//      updatePossibilitiesMiss(p);

//      // delete any possible ship positions containing the miss
//      foreach (List<Ship> list in positions.Values)
//      {
//        int j = 0;
//        for (int i = 0; i < list.Count; i++)
//        {
//          Ship s = list[i];
//          if (!s.IsAt(p)) list[j++] = s;
//        }
//        list.RemoveRange(j, list.Count - j);
//      }
//    }

//    public void addHit(System.Drawing.Point p)
//    {
//      if (state[p.X, p.Y] != SeaState.Clear)
//      {
//        ship_possibilities.Clear();
//        return;
//      }
//      state[p.X, p.Y] = SeaState.Hit;
//      updatePossibilitiesHit(p);
//    }

//    public void addSunk(System.Drawing.Point p)
//    {
//      if (state[p.X, p.Y] != SeaState.Clear)
//      {
//        ship_possibilities.Clear();
//        return;
//      }
//      state[p.X, p.Y] = SeaState.Sunk;
//      updatePossibilitiesSunk(p);
//    }

//    [Conditional("DEBUG")]
//    public void print()
//    {
//      for (int y = 0; y < boardSize.Height; y++)
//      {
//        for (int x = 0; x < boardSize.Width; x++)
//        {
//          char c = ' ';
//          if (state[x, y] == SeaState.Clear) c = '.';
//          if (state[x, y] == SeaState.Miss) c = '!';
//          if (state[x, y] == SeaState.Hit) c = 'H';
//          if (state[x, y] == SeaState.Sunk) c = 'S';
//          Console.Write("{0}", c);
//        }
//        Console.WriteLine();
//      }
//      Console.WriteLine("ship possibilities: {0}", ship_possibilities.Count);
//      if (ship_possibilities.Count <= 10)
//      {
//        foreach (List<Ship> list in ship_possibilities)
//        {
//          Console.Write("   ");
//          foreach (Ship s in list)
//          {
//            Console.Write(" {0}({1},{2},{3})", s.Length, s.Location.X, s.Location.Y, s.Orientation);
//          }
//          Console.WriteLine();
//        }
//      }
//    }

//    private static System.Drawing.Size[] dirs = {new System.Drawing.Size(1, 0), new System.Drawing.Size(-1, 0),
//                                   new System.Drawing.Size(0, 1), new System.Drawing.Size(0, -1)};

//    private IEnumerable<System.Drawing.Point> getNeighbors(System.Drawing.Point p)
//    {
//      foreach (System.Drawing.Size d in dirs)
//      {
//        System.Drawing.Point q = p + d;
//        if (q.X >= 0 && q.X < boardSize.Width && q.Y >= 0 && q.Y < boardSize.Height) yield return q;
//      }
//    }

//    public bool adjacent(Ship s, Ship t)
//    {
//      foreach (System.Drawing.Point p in s.Locations)
//      {
//        foreach (System.Drawing.Point q in getNeighbors(p))
//        {
//          if (t.IsAt(q)) return true;
//        }
//      }
//      return false;
//    }

//    public bool isSunk(Ship s)
//    {
//      if (!valid()) throw new ApplicationException("isSunk on bad state");
//      foreach (System.Drawing.Point p in s.Locations)
//      {
//        if (state[p.X, p.Y] == SeaState.Sunk) return true;
//      }
//      return false;
//    }

//    public bool allSunk(List<Ship> list)
//    {
//      foreach (Ship s in list)
//      {
//        if (!isSunk(s)) return false;
//      }
//      return true;
//    }

//    private bool isAt(List<Ship> list, System.Drawing.Point p)
//    {
//      foreach (Ship s in list)
//      {
//        if (Ship.Intersect(s, p)) return true;
//      }
//      return false;
//    }

//    public double probability(List<Ship> list)
//    {
//      double r = 1;
//      foreach (Ship s in list)
//      {
//        r *= probability(s);
//      }
//      foreach (Ship s in list)
//      {
//        foreach (Ship t in list)
//        {
//          if (s == t) continue;
//          if (adjacent(s, t)) r *= 0.5;
//        }
//      }
//      return r;
//    }

//    // probability that a given ship configuration appears
//    // in a configuration.
//    // indexed by (ship length, # of clears)
//    private static double[][] probs = new double[][] {
//      new double[]{},
//      new double[]{},
//      new double[]{1, 1.0/16},   // size 2
//      new double[]{1, 1.0/4, 1.0/32}, // size 3
//      new double[]{1, 1.0/4, 1.0/8, 1.0/32},  // size 4
//      new double[]{1, 1.0/4, 1.0/8, 1.0/16, 1.0/32}, // size 5
//    };

//    public double probability(Ship s)
//    {
//      int clear_cnt = 0;
//      foreach (System.Drawing.Point p in s.Locations)
//      {
//        if (state[p.X, p.Y] == SeaState.Clear) clear_cnt++;
//      }
//      return probs[s.Length][clear_cnt];
//    }

//    private void updatePossibilitiesMiss(System.Drawing.Point p)
//    {
//      int j = 0;
//      for (int i = 0; i < ship_possibilities.Count; i++)
//      {
//        List<Ship> list = ship_possibilities[i];
//        if (!isAt(list, p)) ship_possibilities[j++] = list;
//      }
//      ship_possibilities.RemoveRange(j, ship_possibilities.Count - j);
//    }

//    private void updatePossibilitiesSunk(System.Drawing.Point p)
//    {
//      // We take advantage of the fact that no ships are length 1, so
//      // any sinking must be of a ship already hit, and thus already
//      // in the possibilities array.
//      int j = 0;
//      for (int i = 0; i < ship_possibilities.Count; i++)
//      {
//        List<Ship> list = ship_possibilities[i];

//        // find ship that was sunk
//        Ship hit_ship = null;
//        foreach (Ship s in list)
//        {
//          if (s.IsAt(p))
//          {
//            hit_ship = s;
//          }
//        }
//        if (hit_ship == null) continue;  // sink location wasn't on a ship

//        // make sure the whole ship was hit (except for the SINK just registered)
//        bool valid = true;
//        foreach (System.Drawing.Point q in hit_ship.Locations)
//        {
//          if (q == p) continue; // the new sunk
//          if (state[q.X, q.Y] != SeaState.Hit)
//          {
//            valid = false;
//            break;
//          }
//        }
//        if (!valid) continue;

//        ship_possibilities[j++] = list;
//      }
//      ship_possibilities.RemoveRange(j, ship_possibilities.Count - j);
//    }

//    private void updatePossibilitiesHit(System.Drawing.Point p)
//    {
//      // This is the hard one.  If a hit was on a ship in the list,
//      // check a few things.  Otherwise, we need to add to the list
//      // all possible ships/positions that can cover the new hit.
//      List<List<Ship>> new_possibilities = new List<List<Ship>>();
//      foreach (List<Ship> list in ship_possibilities)
//      {
//        // find ship that was hit
//        Ship hit_ship = null;
//        foreach (Ship s in list)
//        {
//          if (s.IsAt(p))
//          {
//            hit_ship = s;
//          }
//        }
//        if (hit_ship != null)
//        {
//          // make sure the whole ship wasn't hit, because then this would have been a sink
//          foreach (System.Drawing.Point q in hit_ship.Locations)
//          {
//            if (state[q.X, q.Y] == SeaState.Clear)
//            {
//              new_possibilities.Add(list);
//              break;
//            }
//          }
//          continue;
//        }

//        // Hit outside any current ship in the list.  Add all possible
//        // new positions of a ship that intersects this point.
//        List<int> t = new List<int>(orig_ship_sizes);
//        foreach (Ship s in list) t.Remove(s.Length);
//        List<int> possible_sizes = new List<int>();
//        foreach (int v in t)
//        { // remove duplicates
//          if (!possible_sizes.Contains(v)) possible_sizes.Add(v);
//        }

//        foreach (int size in possible_sizes)
//        {
//          for (int offset = 0; offset < size; offset++)
//          {
//            for (int orient = 0; orient < 2; orient++)
//            {
//              int dy = orient;
//              int dx = 1 - dy;
//              int x = p.X - offset * dx;
//              int y = p.Y - offset * dy;
//              if (x < 0 || y < 0) continue;
//              if (x + size * dx > boardSize.Width) continue;
//              if (y + size * dy > boardSize.Height) continue;
//              bool valid = true;
//              for (int i = 0; i < size; i++)
//              {
//                if (i == offset) continue;  // the new hit
//                if (state[x + i * dx, y + i * dy] != SeaState.Clear)
//                {
//                  valid = false;
//                  break;
//                }
//              }
//              if (!valid) continue;
//              Ship s = new Ship(size, new System.Drawing.Point(x, y), (ShipOrientation)orient);
//              foreach (Ship w in list)
//              {
//                if (s.ConflictsWith(w))
//                {
//                  valid = false;
//                  break;
//                }
//              }
//              if (!valid) continue;
//              List<Ship> new_list = new List<Ship>(list);
//              new_list.Add(s);
//              new_possibilities.Add(new_list);
//            }
//          }
//        }
//      }
//      ship_possibilities = new_possibilities;
//    }
//  }

//  public class Dreadnought
//    : IPlayer
//  {
//    public string Name
//    {
//      get
//      {
//        String s = "Dreadnought";
//        foreach (String opt in options)
//        {
//          s += "," + opt;
//        }
//        return s;
//      }
//    }

//    public Version Version { get { return new Version(1, 1); } }

//    System.Drawing.Size gameSize;
//    int[] shipSizes;
//    public IOffense offense;
//    public IDefense defense;
//    public List<String> options = new List<String>();

//    public void setOption(String option)
//    {
//      options.Add(option);
//    }

//    public void NewMatch(string opponent) { }

//    public void NewGame(System.Drawing.Size size, TimeSpan timeSpan, int[] shipSizes)
//    {
//      if (size != gameSize)
//      {
//        offense = new Offense(size, options);
//        defense = new Defense(size, options);
//        gameSize = size;
//      }

//      this.shipSizes = shipSizes;
//    }

//    public IList<Ship> PlaceShips()
//    {
//      offense.startGame(shipSizes);
//      return defense.startGame(shipSizes);
//    }

//    public System.Drawing.Point GetShot()
//    {
//      System.Drawing.Point p = offense.getShot();
//#if DEBUG
//      Console.WriteLine("shoot at {0},{1}", p.X, p.Y);
//#endif
//      return p;
//    }

//    public void OpponentShot(System.Drawing.Point shot)
//    {
//#if DEBUG
//      Console.WriteLine("opponent shot {0},{1}", shot.X, shot.Y);
//#endif
//      defense.shot(shot);
//    }

//    public void ShotHit(System.Drawing.Point shot, bool sunk)
//    {
//#if DEBUG
//      Console.WriteLine("shot at {0},{1} hit{2}", shot.X, shot.Y, sunk ? " and sunk" : "");
//#endif
//      if (sunk) offense.shotSunk(shot);
//      else offense.shotHit(shot);
//    }

//    public void ShotMiss(System.Drawing.Point shot)
//    {
//#if DEBUG
//      Console.WriteLine("shot at {0},{1} missed", shot.X, shot.Y);
//#endif
//      offense.shotMiss(shot);
//    }

//    public void GameWon()
//    {
//#if DEBUG
//      Console.WriteLine("game won");
//#endif
//      offense.endGame();
//      defense.endGame();
//    }

//    public void GameLost()
//    {
//#if DEBUG
//      Console.WriteLine("game lost");
//#endif
//      offense.endGame();
//      defense.endGame();
//    }

//    public void MatchOver() { }
//  }

//  public class Offense : IOffense
//  {
//    private int w;
//    private int h;
//    private Random rand = new Random();
//    public GameState state;
//    private int apriori_types = 2;
//    private int apriori_type;
//    private int total_ship_sizes;

//    // option flags
//    public bool fully_resolve_hits;
//    public bool assume_notouching;

//    // statistics kept about opponent's layout behavior
//    int shots_in_game;
//    int[,] statistics_shot_hit;
//    int[,] statistics_shot_miss;

//    public Offense(System.Drawing.Size size, List<String> options)
//    {
//      w = size.Width;
//      h = size.Height;
//      statistics_shot_hit = new int[w, h];
//      statistics_shot_miss = new int[w, h];
//      print_apriori();
//      apriori_type = rand.Next(apriori_types);
//      apriori_type = 0; // TODO: remove
//      fully_resolve_hits = options.Exists(x => x == "fully_resolve_hits");
//      assume_notouching = options.Exists(x => x == "assume_notouching");
//    }

//    public void startGame(int[] ship_sizes)
//    {
//      state = new GameState(w, h, ship_sizes);
//      total_ship_sizes = 0;
//      foreach (int i in ship_sizes) total_ship_sizes += i;
//      shots_in_game = 0;
//    }

//    public void shotMiss(System.Drawing.Point shot)
//    {
//      state.addMiss(shot);
//      statistics_shot_miss[shot.X, shot.Y]++;
//    }

//    public void shotHit(System.Drawing.Point shot)
//    {
//      state.addHit(shot);
//      statistics_shot_hit[shot.X, shot.Y]++;
//    }

//    public void shotSunk(System.Drawing.Point shot)
//    {
//      state.addSunk(shot);
//      if (assume_notouching)
//      {
//        foreach (System.Drawing.Point p in getSquares())
//        {
//          if (state.get(p) == SeaState.Hit || state.get(p) == SeaState.Sunk)
//          {
//            foreach (System.Drawing.Point q in neighbors(p))
//            {
//              if (state.get(q) == SeaState.Clear)
//              {
//                state.addMiss(q);
//                break;
//              }
//            }
//          }
//        }
//      }
//      statistics_shot_hit[shot.X, shot.Y]++;
//    }

//    public void endGame()
//    {
//#if DEBUG
//      //Console.WriteLine("history probability");
//      //for (int y = 0; y < h; y++)
//      //{
//      //  Console.Write("   ");
//      //  for (int x = 0; x < w; x++)
//      //  {
//      //    double p = history_probability(x, y);
//      //    Console.Write(" {0,-4}", (int)(1000 * p));
//      //  }
//      //  Console.WriteLine();
//      //}
//#endif
//    }

//    public System.Drawing.Point getShot()
//    {
//#if DEBUG
//      Console.WriteLine("getting shot {0}", shots_in_game++);
//#endif
//      state.print();
//      List<System.Drawing.Point> choices = getShot_ExtendShips();
//      if (choices.Count > 0)
//      {
//#if DEBUG
//        Console.Write("extendships ");
//        foreach (System.Drawing.Point p in choices) Console.Write("{0} ", p);
//        Console.WriteLine();
//#endif
//        return choices[rand.Next(choices.Count)];
//      }
//      System.Drawing.Point r = getShot_Random();
//#if DEBUG
//      foreach (System.Drawing.Point q in neighbors(r))
//      {
//        if (state.get(q) == SeaState.Hit || state.get(q) == SeaState.Sunk) Console.WriteLine("adjacent to ship!");
//      }
//#endif
//      return r;
//    }

//    // returns all squares on the board.
//    private IEnumerable<System.Drawing.Point> getSquares()
//    {
//      for (int x = 0; x < w; x++)
//      {
//        for (int y = 0; y < h; y++)
//        {
//          yield return new System.Drawing.Point(x, y);
//        }
//      }
//    }

//    private static System.Drawing.Size[] dirs = {new System.Drawing.Size(1, 0), new System.Drawing.Size(-1, 0),
//                                   new System.Drawing.Size(0, 1), new System.Drawing.Size(0, -1)};

//    // returns the <= 4 neighbor squares of the given point
//    //  0
//    // 1*2
//    //  3
//    private IEnumerable<System.Drawing.Point> neighbors(System.Drawing.Point p)
//    {
//      foreach (System.Drawing.Size d in dirs)
//      {
//        System.Drawing.Point q = p + d;
//        if (q.X >= 0 && q.X < w && q.Y >= 0 && q.Y < h) yield return q;
//      }
//    }

//    // public for testing
//    public List<System.Drawing.Point> getShot_ExtendShips()
//    {
//      List<System.Drawing.Point> choices = new List<System.Drawing.Point>();

//      if (!fully_resolve_hits)
//      {
//        foreach (List<Ship> list in state.ship_possibilities)
//        {
//          if (state.allSunk(list)) return choices;
//        }
//      }

//      // algorithm: choose spot which, if a miss, maximizes the
//      // number of ship layout possibilities (weighted by probability)
//      // which we eliminate.
//      double[,] weight = new double[w, h];
//      foreach (List<Ship> list in state.ship_possibilities)
//      {
//        double wt = state.probability(list);
//        foreach (Ship s in list)
//        {
//          foreach (System.Drawing.Point p in s.Locations)
//          {
//            if (state.get(p) == SeaState.Clear)
//            {
//              weight[p.X, p.Y] += wt;
//            }
//          }
//        }
//      }
//#if DEBUG
//      Console.WriteLine("weights:");
//      for (int y = 0; y < h; y++)
//      {
//        for (int x = 0; x < w; x++)
//        {
//          Console.Write("{0,-4} ", (int)(1000 * weight[x, y]));
//        }
//        Console.WriteLine();
//      }
//#endif

//      // return maximum weight squares
//      double maxw = 0.0;
//      foreach (System.Drawing.Point p in getSquares())
//      {
//        if (weight[p.X, p.Y] > maxw)
//        {
//          maxw = weight[p.X, p.Y];
//          choices.Clear();
//        }
//        if (weight[p.X, p.Y] == maxw)
//        {
//          choices.Add(p);
//        }
//      }
//      return choices;
//    }

//    private System.Drawing.Point getShot_Random()
//    {
//      // find out which hits are definitely sunk and which might still be
//      // on live ships.
//      bool[,] possible_unsunk_hits = new bool[w, h];
//      foreach (List<Ship> list in state.ship_possibilities)
//      {
//        foreach (Ship s in list)
//        {
//          if (state.isSunk(s)) continue;
//          foreach (System.Drawing.Point p in s.Locations)
//          {
//            if (state.get(p) == SeaState.Hit) possible_unsunk_hits[p.X, p.Y] = true;
//          }
//        }
//      }
//#if DEBUG
//      Console.WriteLine("possible unsunk hits");
//      for (int y = 0; y < h; y++)
//      {
//        for (int x = 0; x < w; x++)
//        {
//          Console.Write("{0}", possible_unsunk_hits[x, y] ? 'H' : '.');
//        }
//        Console.WriteLine();
//      }
//#endif

//      // find out which squares could hold the remaining ships, and if so
//      // what the probability of each square is.
//      double[,] ship_prob = new double[w, h];
//      foreach (int len in state.remaining_ship_sizes())
//      {
//        double[,] aposteriori_prob = new double[w, h];
//        for (int x = 0; x < w; x++)
//        {
//          for (int y = 0; y < h; y++)
//          {
//            for (int orient = 0; orient < 2; orient++)
//            {
//              int dy = orient;
//              int dx = 1 - dy;
//              if (x + len * dx > w) continue;
//              if (y + len * dy > h) continue;
//              bool good = true;
//              for (int i = 0; i < len; i++)
//              {
//                SeaState st = state.get(x + i * dx, y + i * dy);
//                if (!(st == SeaState.Clear ||
//                      (!fully_resolve_hits && st == SeaState.Hit && possible_unsunk_hits[x + i * dx, y + i * dy])))
//                {
//                  good = false;
//                  break;
//                }
//              }
//              if (!good) continue;
//              double p = apriori_prob(len, new System.Drawing.Point(x, y), (ShipOrientation)orient);
//              bool next_to_other_ship = false;
//              for (int i = 0; i < len; i++)
//              {
//                foreach (System.Drawing.Point n in neighbors(new System.Drawing.Point(x + i * dx, y + i * dy)))
//                {
//                  if (state.get(n) == SeaState.Hit || state.get(n) == SeaState.Sunk) next_to_other_ship = true;
//                }
//              }
//              if (next_to_other_ship) p *= .2;  // TODO: set to .2?
//              for (int i = 0; i < len; i++)
//              {
//                if (state.get(x + i * dx, y + i * dy) == SeaState.Hit) continue;
//                double wt = history_probability(x + i * dx, y + i * dy);
//                aposteriori_prob[x + i * dx, y + i * dy] += p * wt;
//              }
//            }
//          }
//        }
//        // normalize aposteriori probability
//        if (!normalize_prob(aposteriori_prob))
//        {
//          // this condition triggers when there is no place
//          // to put a ship of a particular size.
//          continue;
//        }

//        // TODO: weight sum of probabilities, for example
//        // to prioritize the finding of the 2-ship?
//        for (int x = 0; x < w; x++)
//        {
//          for (int y = 0; y < h; y++)
//          {
//            ship_prob[x, y] += aposteriori_prob[x, y];
//          }
//        }
//      }

//      double max_prob = 0;
//      foreach (double p in ship_prob) if (p > max_prob) max_prob = p;

//#if DEBUG
//      Console.WriteLine("random choice probabilities");
//      for (int y = 0; y < h; y++)
//      {
//        for (int x = 0; x < w; x++)
//        {
//          Console.Write("{0,-3}{1} ", (int)(ship_prob[x, y] * 1000), ship_prob[x, y] == max_prob ? "*" : " ");
//        }
//        Console.WriteLine();
//      }
//#endif

//      // pick random one of the prob maximizing spots
//      List<System.Drawing.Point> max_points = new List<System.Drawing.Point>();
//      for (int x = 0; x < w; x++)
//      {
//        for (int y = 0; y < h; y++)
//        {
//          if (ship_prob[x, y] == max_prob)
//          {
//            max_points.Add(new System.Drawing.Point(x, y));
//          }
//        }
//      }

//      return max_points[rand.Next(max_points.Count)];
//    }

//    private double apriori_prob(int len, System.Drawing.Point p, ShipOrientation orient)
//    {
//      if (apriori_type == 0)
//      {
//        // uniform distribution
//        if (orient == ShipOrientation.Horizontal) return 1.0 / (w - len + 1) / h;
//        else return 1.0 / (h - len + 1) / w;
//      }
//      else if (apriori_type == 1)
//      {
//        // weighted towards edge
//        double scale = Math.Sqrt(2.0) - 1; // factor of 2.0 from center to edge
//        if (orient == ShipOrientation.Horizontal)
//        {
//          int min = 0;
//          int max = w - len;
//          double mid = (max - min) / 2.0;
//          double r = 1.0 + scale * Math.Abs(p.X - mid) / (max - mid);

//          min = 0;
//          max = h - 1;
//          mid = (max - min) / 2.0;
//          r *= 1.0 + scale * Math.Abs(p.Y - mid) / (max - mid);
//          return r;
//        }
//        else
//        {
//          int min = 0;
//          int max = w - 1;
//          double mid = (max - min) / 2.0;
//          double r = 1.0 + scale * Math.Abs(p.X - mid) / (max - mid);

//          min = 0;
//          max = h - len;
//          mid = (max - min) / 2.0;
//          r *= 1.0 + scale * Math.Abs(p.Y - mid) / (max - mid);
//          return r;
//        }
//      }
//      else
//      {
//        return 0.0;
//      }
//    }

//    // normalizes entries in prob so they total 1.0.
//    private bool normalize_prob(double[,] prob)
//    {
//      double total_prob = 0.0;
//      foreach (double x in prob) total_prob += x;
//      if (total_prob == 0) return false;

//      for (int x = 0; x < prob.GetLength(0); x++)
//      {
//        for (int y = 0; y < prob.GetLength(1); y++)
//        {
//          prob[x, y] /= total_prob;
//        }
//      }
//      return true;
//    }

//    // returns the fraction of shots at this square that resulted
//    // in a hit (smoothed somewhat).
//    private double history_probability(int x, int y)
//    {
//      // These estimates are hard to do in general, as we don't pick
//      // our sampling points randomly.  But we don't need exact answers
//      // here, so we assume our samples were chosen randomly.  We
//      // use a simple frequency-based approach.

//      int hits = statistics_shot_hit[x, y];
//      int misses = statistics_shot_miss[x, y];
//      int shots = hits + misses;

//      // # of samples to weight prior (17/100) and current data 50/50.
//      double fake_shots = 25;
//      double fake_hits = fake_shots * total_ship_sizes / (w * h);

//      double res = (hits + fake_hits) / (shots + fake_shots);
//      return res;
//    }

//    [Conditional("DEBUG")]
//    private void print_apriori()
//    {
//      //for (apriori_type = 0; apriori_type < apriori_types; apriori_type++)
//      //{
//      //  Console.WriteLine("apriori type: {0}", apriori_type);
//      //  for (int size = 2; size <= 5; size++)
//      //  {
//      //    for (int orient = 0; orient < 2; orient++)
//      //    {
//      //      Console.WriteLine("  {0}{1}:", size, orient == 0 ? "H" : "V");
//      //      double sum = 0.0;
//      //      for (int y = 0; y < h; y++)
//      //      {
//      //        Console.Write("   ");
//      //        for (int x = 0; x < w; x++)
//      //        {
//      //          double p;
//      //          if ((orient == 0 && x + size > w) || (orient == 1 && y + size > h))
//      //          {
//      //            p = 0.0;
//      //          }
//      //          else
//      //          {
//      //            p = apriori_prob(size, new System.Drawing.Point(x, y), (ShipOrientation)orient);
//      //          }
//      //          Console.Write(" {0,-4}", (int)(10000 * p));
//      //          sum += p;
//      //        }
//      //        Console.WriteLine();
//      //      }
//      //      Console.WriteLine("    sum: {0}", sum);
//      //    }
//      //  }
//      //}
//    }
//  }

//  public class Defense : IDefense
//  {
//    int w;
//    int h;
//    Random rand = new Random();

//    bool place_notouching = false;   // if true, no generated boards have touching ships.
//    bool standard_touching = false;  // if true, touching is ignored
//                                     // otherwise, we thin out touching to about 1/4 of generated boards.

//    // statistics kept about opponent's behavior
//    int nshots_in_game;
//    int[,] opponent_shots;

//    public Defense(System.Drawing.Size size, List<String> options)
//    {
//      w = size.Width;
//      h = size.Height;
//      place_notouching = options.Exists(x => x == "place_notouching");
//      standard_touching = options.Exists(x => x == "standard_touching");
//      opponent_shots = new int[w, h];
//    }

//    public List<Ship> startGame(int[] ship_sizes)
//    {
//      List<Ship> placement = placeShips(ship_sizes);
//      print_placement(placement);
//      nshots_in_game = 0;
//      return placement;
//    }

//    public void shot(System.Drawing.Point p)
//    {
//      opponent_shots[p.X, p.Y] += Math.Max(1, 50 - nshots_in_game);
//      nshots_in_game++;
//    }

//    public void endGame()
//    {
//    }

//    private List<Ship> placeShips(int[] ship_sizes)
//    {
//      int max_opp_shots = 0;
//      foreach (int x in opponent_shots) max_opp_shots = Math.Max(max_opp_shots, x);
//      max_opp_shots++;  // prevent divide by 0

//#if DEBUG
//      Console.WriteLine("square shot scores");
//      for (int y = 0; y < h; y++)
//      {
//        for (int x = 0; x < w; x++)
//        {
//          Console.Write("{0,-4} ", 1000 * opponent_shots[x, y] / max_opp_shots);
//        }
//        Console.WriteLine();
//      }
//#endif

//      // generate 100 valid placements
//      const int N = 100;
//      List<List<Ship>> allocations = new List<List<Ship>>();
//      for (int n = 0; n < N; n++)
//      {
//        List<Ship> allocation = new List<Ship>();
//        foreach (int size in ship_sizes)
//        {
//          Ship s;
//          while (true)
//          {
//            int x = rand.Next(w);
//            int y = rand.Next(h);
//            int orient = rand.Next(2);
//            s = new Ship(size, new System.Drawing.Point(x, y), (ShipOrientation)orient);
//            if (!s.IsValid(new System.Drawing.Size(w, h))) continue;

//            bool ok = true;
//            foreach (Ship t in allocation)
//            {
//              if (s.ConflictsWith(t)) { ok = false; break; }
//            }
//            if (ok) break;
//          }
//          allocation.Add(s);
//        }
//        allocations.Add(allocation);
//      }

//      // score the allocations, pick best one
//      int minscore = 1000000000;
//      List<Ship> min_allocation = null;
//      foreach (List<Ship> allocation in allocations)
//      {
//        int score = 0;
//        foreach (Ship s in allocation)
//        {
//          foreach (System.Drawing.Point p in s.Locations)
//          {
//            score += 100 * opponent_shots[p.X, p.Y] / max_opp_shots;
//          }
//          foreach (Ship t in allocation)
//          {
//            if (!standard_touching && shipsAdjacent(s, t)) score += 20;
//            if (place_notouching && shipsAdjacent(s, t)) score += 1000000;
//          }
//        }
//        score += rand.Next(15); // some inherent randomness
//        if (score < minscore)
//        {
//          minscore = score;
//          min_allocation = allocation;
//        }
//      }
//      return min_allocation;
//    }

//    // returns true if these ships touch.
//    private bool shipsAdjacent(Ship s, Ship t)
//    {
//      foreach (System.Drawing.Point p in s.Locations)
//      {
//        if (t.IsAt(new System.Drawing.Point(p.X + 1, p.Y + 0))) return true;
//        if (t.IsAt(new System.Drawing.Point(p.X + -1, p.Y + 0))) return true;
//        if (t.IsAt(new System.Drawing.Point(p.X + 0, p.Y + 1))) return true;
//        if (t.IsAt(new System.Drawing.Point(p.X + 0, p.Y + -1))) return true;
//      }
//      return false;
//    }

//    [Conditional("DEBUG")]
//    public void print_placement(List<Ship> ships)
//    {
//      int adj = 0;
//      for (int i = 0; i < ships.Count; i++)
//      {
//        Ship s = ships[i];
//        for (int j = i + 1; j < ships.Count; j++)
//        {
//          Ship t = ships[j];
//          if (shipsAdjacent(s, t)) adj++;
//        }
//      }

//      char[,] placement = new char[w, h];
//      for (int x = 0; x < w; x++)
//      {
//        for (int y = 0; y < h; y++)
//        {
//          placement[x, y] = '.';
//        }
//      }
//      foreach (Ship s in ships)
//      {
//        foreach (System.Drawing.Point p in s.GetAllLocations())
//        {
//          placement[p.X, p.Y] = (char)('0' + s.Length);
//        }
//      }
//      Console.WriteLine("placement {0}:", adj);
//      for (int y = 0; y < h; y++)
//      {
//        Console.Write("  ");
//        for (int x = 0; x < w; x++)
//        {
//          Console.Write(placement[x, y]);
//        }
//        Console.WriteLine();
//      }
//    }
//  }
//}
