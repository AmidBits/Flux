using System.Linq;

namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string ToConsoleString(this System.Collections.Generic.List<Flux.Model.Gaming.BattleShip.Vessel> ships, Geometry.Size2 size)
    {
      if (ships is null) throw new System.ArgumentNullException(nameof(ships));

      System.Console.SetCursorPosition(0, 3);

      var countAdjacentShips = 0;

      for (var i = 0; i < ships.Count; i++)
      {
        Flux.Model.Gaming.BattleShip.Vessel s = ships[i];

        for (var j = i + 1; j < ships.Count; j++)
        {
          Flux.Model.Gaming.BattleShip.Vessel t = ships[j];

          if (Flux.Model.Gaming.BattleShip.Vessel.Intersects(s, t))
            countAdjacentShips++;
        }
      }

      var placement = new char[size.Height, size.Width];
      for (int x = 0; x < size.Width; x++)
        for (int y = 0; y < size.Height; y++)
          placement[y, x] = '.';

      foreach (Flux.Model.Gaming.BattleShip.Vessel s in ships)
        foreach (Geometry.Point2 p in s.Locations)
          placement[p.Y, p.X] = (char)('0' + s.Length);

      var sb = new System.Text.StringBuilder();

      sb.AppendLine($"Placement {countAdjacentShips}:");
      sb.AppendLine(Formatting.ArrayFormatter.NoSeparatorsUniform.TwoToConsoleString(placement));

      return sb.ToString();
    }
  }

  namespace Model.Gaming.BattleShip
  {
    public struct Vessel
      : System.IEquatable<Vessel>
    {
      private static readonly double[] m_proximityProbabilities = new double[] { 1.0, 0.5, 0.3333333333333333, 0.25, 0.2, 0.1666666666666666 };
      public static System.Collections.Generic.IReadOnlyList<double> ProximityProbabilities => m_proximityProbabilities;

      public static readonly Vessel Empty;
      public bool IsEmpty => Equals(Empty);

      private readonly System.Collections.Generic.List<Geometry.Point2> m_locations;
      public Geometry.Point2 Location => m_locations.Count > 0 ? m_locations[0] : throw new System.InvalidOperationException(nameof(Empty));
      public System.Collections.Generic.IReadOnlyList<Geometry.Point2> Locations => m_locations;

      public VesselOrientation Orientation { get; set; }

      public int Length
        => m_locations.Count;

      public Vessel(int length, Geometry.Point2 location, VesselOrientation orientation)
      {
        if (length <= 1) throw new System.ArgumentOutOfRangeException(nameof(length));

        Orientation = orientation;

        m_locations = new System.Collections.Generic.List<Geometry.Point2>();
        for (int i = 0; i < length; i++)
          m_locations.Add(Orientation == VesselOrientation.Horizontal ? new Geometry.Point2(location.X + i, location.Y) : new Geometry.Point2(location.X, location.Y + i));
      }

      public bool IsValid(Geometry.Size2 boardSize)
      {
        if (m_locations[0].X < 0 || m_locations[0].Y < 0)
          return false;

        if (Orientation == VesselOrientation.Horizontal)
        {
          if (m_locations[0].Y >= boardSize.Height || m_locations[0].X + m_locations.Count > boardSize.Width)
            return false;
        }
        else
        {
          if (m_locations[0].X >= boardSize.Width || m_locations[0].Y + m_locations.Count > boardSize.Height)
            return false;
        }

        return true;
      }

      public static bool AnyHitsOn(Vessel ship, System.Collections.Generic.IEnumerable<Geometry.Point2> shots)
        => ship.m_locations.Any(location => shots.Any(shot => shot == location));
      public static bool AnyHits(System.Collections.Generic.IEnumerable<Vessel> ships, System.Collections.Generic.IEnumerable<Geometry.Point2> shots)
        => ships.Any(ship => ship.m_locations.Any(location => shots.Any(shot => shot == location)));
      public static bool IsSunk(Vessel ship, System.Collections.Generic.IEnumerable<Geometry.Point2> shots)
        => ship.m_locations.All(l => shots.Any(s => s == l));

      public static bool AreAdjacent(Vessel a, Vessel b)
      {
        foreach (Geometry.Point2 p in a.Locations)
        {
          if (Intersects(b, new Geometry.Point2(p.X + 1, p.Y + 0)))
            return true;
          if (Intersects(b, new Geometry.Point2(p.X + -1, p.Y + 0)))
            return true;
          if (Intersects(b, new Geometry.Point2(p.X + 0, p.Y + 1)))
            return true;
          if (Intersects(b, new Geometry.Point2(p.X + 0, p.Y + -1)))
            return true;
        }
        return false;
      }

      public static bool Intersects(Vessel ship, Geometry.Point2 position)
      {
        return ship.Orientation == VesselOrientation.Horizontal
        ? (ship.m_locations[0].Y == position.Y) && (ship.m_locations[0].X <= position.X) && (ship.m_locations[0].X + ship.m_locations.Count > position.X)
        : (ship.m_locations[0].X == position.X) && (ship.m_locations[0].Y <= position.Y) && (ship.m_locations[0].Y + ship.m_locations.Count > position.Y);
      }
      public static bool Intersects(Vessel a, Vessel b)
      {
        if (a.Orientation == VesselOrientation.Horizontal && b.Orientation == VesselOrientation.Horizontal)
        {
          return a.m_locations[0].Y == b.m_locations[0].Y && (b.m_locations[0].X < (a.m_locations[0].X + a.m_locations.Count)) && (a.m_locations[0].X < (b.m_locations[0].X + b.m_locations.Count));
        }
        else if (a.Orientation == VesselOrientation.Vertical && b.Orientation == VesselOrientation.Vertical)
        {
          return a.m_locations[0].X == b.m_locations[0].X && (b.m_locations[0].Y < (a.m_locations[0].Y + a.m_locations.Count)) && (a.m_locations[0].Y < (b.m_locations[0].Y + b.m_locations.Count));
        }
        else
        {
          Vessel h = a.Orientation == VesselOrientation.Horizontal ? a : b;
          Vessel v = a.Orientation == VesselOrientation.Horizontal ? b : a;

          return (h.m_locations[0].Y >= v.m_locations[0].Y) && (h.m_locations[0].Y < (v.m_locations[0].Y + v.m_locations.Count)) && (v.m_locations[0].X >= h.m_locations[0].X) && (v.m_locations[0].X < (h.m_locations[0].X + h.m_locations.Count));
        }
      }

      public static System.Collections.Generic.List<Vessel> StageFleet(Geometry.Size2 gridSize, params int[] shipSizes)
      {
        var ships = new System.Collections.Generic.List<Vessel>();

        foreach (var size in shipSizes)
        {
          Vessel ship;

          do
          {
            ship = new Vessel(size, new Geometry.Point2(Random.NumberGenerator.Crypto.Next(gridSize.Width), Random.NumberGenerator.Crypto.Next(gridSize.Height)), (VesselOrientation)Random.NumberGenerator.Crypto.Next(2));
          }
          while (!ship.IsValid(gridSize) || ships.Any(s => Intersects(ship, s)));

          ships.Add(ship);
        }

        return ships;
      }

      // Operators
      public static bool operator ==(Vessel a, Vessel b)
        => a.Equals(b);
      public static bool operator !=(Vessel a, Vessel b)
        => !a.Equals(b);

      // IEquatable
      public bool Equals(Vessel other)
      {
        if (Orientation != other.Orientation)
          return false;

        if (m_locations.Count != other.m_locations.Count)
          return false;

        for (var index = 0; index < m_locations.Count; index++)
          if (m_locations[index] != other.m_locations[index])
            return false;

        return true;
      }

      // Object (overrides)
      public override bool Equals(object? obj)
        => obj is VersionEx o && Equals(o);
      public override int GetHashCode()
        => System.HashCode.Combine(m_locations.CombineHashCore(), Orientation);
      public override string? ToString()
        => $"<{nameof(Vessel)} {m_locations.Count}, {Orientation}>";
    }
  }
}
