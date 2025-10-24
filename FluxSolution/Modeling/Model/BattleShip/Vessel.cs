namespace Flux
{
  public static partial class VesselEm
  {
    public static string ToConsoleString(this System.Collections.Generic.IList<Model.BattleShip.Vessel> ships, System.Drawing.Point size, int screenPositionLeft, int screenPositionTop)
    {
      if (ships is null) throw new System.ArgumentNullException(nameof(ships));

      System.Console.SetCursorPosition(screenPositionLeft, screenPositionTop);

      var countAdjacentShips = 0;

      for (var i = 0; i < ships.Count; i++)
      {
        Model.BattleShip.Vessel s = ships[i];

        for (var j = i + 1; j < ships.Count; j++)
        {
          Model.BattleShip.Vessel t = ships[j];

          if (Model.BattleShip.Vessel.Intersects(s, t))
            countAdjacentShips++;
        }
      }

      var placement = new char[size.Y, size.X];
      for (int x = 0; x < size.X; x++)
        for (int y = 0; y < size.Y; y++)
          placement[y, x] = '.';

      foreach (Model.BattleShip.Vessel s in ships)
        foreach (System.Drawing.Point p in s.Locations)
          placement[p.Y, p.X] = (char)('0' + s.Length);

      var sb = new System.Text.StringBuilder();

      sb.AppendLine($"Placement {countAdjacentShips}:");
      sb.AppendLine(placement.Rank2ToConsoleString(new ConsoleFormatOptions() { HorizontalSeparator = null, VerticalSeparator = null, UniformWidth = true }));

      return sb.ToString();
    }
  }

  namespace Model.BattleShip
  {
    public record class Vessel
    {
      private readonly System.Collections.Generic.List<System.Drawing.Point> m_positions;

      public VesselOrientation m_orientation;

      public Vessel(int length, System.Drawing.Point location, VesselOrientation orientation)
      {
        if (length <= 1) throw new System.ArgumentOutOfRangeException(nameof(length));

        m_orientation = orientation;

        m_positions = new System.Collections.Generic.List<System.Drawing.Point>();

        for (int i = 0; i < length; i++)
          m_positions.Add(orientation == VesselOrientation.Horizontal ? new System.Drawing.Point(location.X + i, location.Y) : new System.Drawing.Point(location.X, location.Y + i));
      }

      public int Length
        => m_positions.Count;

      public System.Drawing.Point Location
        => m_positions[0];
      public System.Collections.Generic.IReadOnlyList<System.Drawing.Point> Locations
        => m_positions;

      public VesselOrientation Orientation
        => m_orientation;

      public bool IsValid(System.Drawing.Point boardSize)
      {
        if (m_positions[0].X < 0 || m_positions[0].Y < 0)
          return false;

        if (m_orientation == VesselOrientation.Horizontal)
        {
          if (m_positions[0].Y >= boardSize.Y || m_positions[0].X + m_positions.Count > boardSize.X)
            return false;
        }
        else
        {
          if (m_positions[0].X >= boardSize.X || m_positions[0].Y + m_positions.Count > boardSize.Y)
            return false;
        }

        return true;
      }

      public static bool AnyHitsOn(Vessel ship, System.Collections.Generic.IEnumerable<System.Drawing.Point> shots)
        => ship.m_positions.Any(location => shots.Any(shot => shot == location));
      public static bool AnyHits(System.Collections.Generic.IEnumerable<Vessel> ships, System.Collections.Generic.IEnumerable<System.Drawing.Point> shots)
        => ships.Any(ship => AnyHitsOn(ship, shots));
      public static bool IsSunk(Vessel ship, System.Collections.Generic.IEnumerable<System.Drawing.Point> shots)
        => ship.m_positions.All(l => shots.Any(s => s == l));

      public static bool AreAdjacent(Vessel a, Vessel b)
      {
        foreach (System.Drawing.Point p in a.Locations)
        {
          if (Intersects(b, new System.Drawing.Point(p.X + 1, p.Y + 0)))
            return true;
          if (Intersects(b, new System.Drawing.Point(p.X + -1, p.Y + 0)))
            return true;
          if (Intersects(b, new System.Drawing.Point(p.X + 0, p.Y + 1)))
            return true;
          if (Intersects(b, new System.Drawing.Point(p.X + 0, p.Y + -1)))
            return true;
        }
        return false;
      }

      public static bool Intersects(Vessel ship, System.Drawing.Point position)
      {
        return ship.Orientation == VesselOrientation.Horizontal
        ? (ship.m_positions[0].Y == position.Y) && (ship.m_positions[0].X <= position.X) && (ship.m_positions[0].X + ship.m_positions.Count > position.X)
        : (ship.m_positions[0].X == position.X) && (ship.m_positions[0].Y <= position.Y) && (ship.m_positions[0].Y + ship.m_positions.Count > position.Y);
      }
      public static bool Intersects(Vessel a, Vessel b)
      {
        if (a.Orientation == VesselOrientation.Horizontal && b.Orientation == VesselOrientation.Horizontal)
        {
          return a.m_positions[0].Y == b.m_positions[0].Y && (b.m_positions[0].X < (a.m_positions[0].X + a.m_positions.Count)) && (a.m_positions[0].X < (b.m_positions[0].X + b.m_positions.Count));
        }
        else if (a.Orientation == VesselOrientation.Vertical && b.Orientation == VesselOrientation.Vertical)
        {
          return a.m_positions[0].X == b.m_positions[0].X && (b.m_positions[0].Y < (a.m_positions[0].Y + a.m_positions.Count)) && (a.m_positions[0].Y < (b.m_positions[0].Y + b.m_positions.Count));
        }
        else
        {
          Vessel h = a.Orientation == VesselOrientation.Horizontal ? a : b;
          Vessel v = a.Orientation == VesselOrientation.Horizontal ? b : a;

          return (h.m_positions[0].Y >= v.m_positions[0].Y) && (h.m_positions[0].Y < (v.m_positions[0].Y + v.m_positions.Count)) && (v.m_positions[0].X >= h.m_positions[0].X) && (v.m_positions[0].X < (h.m_positions[0].X + h.m_positions.Count));
        }
      }

      public static double ProximityProbabilities(int proximity) // Max length of 9, could leave wide open.
        => proximity >= 0 && proximity <= 9 ? 1.0 / (proximity + 1) : throw new System.ArgumentOutOfRangeException(nameof(proximity));

      public static System.Collections.Generic.List<Vessel> StageFleet(System.Drawing.Point gridSize, params int[] shipSizes)
      {
        var ships = new System.Collections.Generic.List<Vessel>();

        foreach (var size in shipSizes)
        {
          Vessel ship;

          do
          {
            ship = new Vessel(size, new System.Drawing.Point(Flux.RandomNumberGenerators.SscRng.Shared.Next(gridSize.X), Flux.RandomNumberGenerators.SscRng.Shared.Next(gridSize.Y)), (VesselOrientation)Flux.RandomNumberGenerators.SscRng.Shared.Next(2));
          }
          while (!ship.IsValid(gridSize) || ships.Any(s => Intersects(ship, s)));

          ships.Add(ship);
        }

        return ships;
      }
    }
  }
}
