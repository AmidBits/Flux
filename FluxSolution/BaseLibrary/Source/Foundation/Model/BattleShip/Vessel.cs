using System.Linq;

namespace Flux
{
  public static partial class VesselEm
  {
    public static string ToConsoleString(this System.Collections.Generic.IList<Model.BattleShip.Vessel> ships, Size2 size, int screenPositionLeft, int screenPositionTop)
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

      var placement = new char[size.Height, size.Width];
      for (int x = 0; x < size.Width; x++)
        for (int y = 0; y < size.Height; y++)
          placement[y, x] = '.';

      foreach (Model.BattleShip.Vessel s in ships)
        foreach (CartesianCoordinate2I p in s.Locations)
          placement[p.Y, p.X] = (char)('0' + s.Length);

      var sb = new System.Text.StringBuilder();

      sb.AppendLine($"Placement {countAdjacentShips}:");
      sb.AppendLine(Formatting.ArrayFormatter.NoSeparatorsUniform.TwoToConsoleString(placement));

      return sb.ToString();
    }
  }

  namespace Model.BattleShip
  {
    public sealed class Vessel
      : System.IEquatable<Vessel>
    {
      private readonly System.Collections.Generic.List<CartesianCoordinate2I> m_positions;

      public VesselOrientation m_orientation;

      public Vessel(int length, CartesianCoordinate2I location, VesselOrientation orientation)
      {
        if (length <= 1) throw new System.ArgumentOutOfRangeException(nameof(length));

        m_orientation = orientation;

        m_positions = new System.Collections.Generic.List<CartesianCoordinate2I>();

        for (int i = 0; i < length; i++)
          m_positions.Add(orientation == VesselOrientation.Horizontal ? new CartesianCoordinate2I(location.X + i, location.Y) : new CartesianCoordinate2I(location.X, location.Y + i));
      }

      public int Length
        => m_positions.Count;

      public CartesianCoordinate2I Location
        => m_positions[0];
      public System.Collections.Generic.IReadOnlyList<CartesianCoordinate2I> Locations
        => m_positions;

      public VesselOrientation Orientation
        => m_orientation;

      public bool IsValid(Size2 boardSize)
      {
        if (m_positions[0].X < 0 || m_positions[0].Y < 0)
          return false;

        if (Orientation == VesselOrientation.Horizontal)
        {
          if (m_positions[0].Y >= boardSize.Height || m_positions[0].X + m_positions.Count > boardSize.Width)
            return false;
        }
        else
        {
          if (m_positions[0].X >= boardSize.Width || m_positions[0].Y + m_positions.Count > boardSize.Height)
            return false;
        }

        return true;
      }

      public static bool AnyHitsOn(Vessel ship, System.Collections.Generic.IEnumerable<CartesianCoordinate2I> shots)
        => ship.m_positions.Any(location => shots.Any(shot => shot == location));
      public static bool AnyHits(System.Collections.Generic.IEnumerable<Vessel> ships, System.Collections.Generic.IEnumerable<CartesianCoordinate2I> shots)
        => ships.Any(ship => ship.m_positions.Any(location => shots.Any(shot => shot == location)));
      public static bool IsSunk(Vessel ship, System.Collections.Generic.IEnumerable<CartesianCoordinate2I> shots)
        => ship.m_positions.All(l => shots.Any(s => s == l));

      public static bool AreAdjacent(Vessel a, Vessel b)
      {
        foreach (CartesianCoordinate2I p in a.Locations)
        {
          if (Intersects(b, new CartesianCoordinate2I(p.X + 1, p.Y + 0)))
            return true;
          if (Intersects(b, new CartesianCoordinate2I(p.X + -1, p.Y + 0)))
            return true;
          if (Intersects(b, new CartesianCoordinate2I(p.X + 0, p.Y + 1)))
            return true;
          if (Intersects(b, new CartesianCoordinate2I(p.X + 0, p.Y + -1)))
            return true;
        }
        return false;
      }

      public static bool Intersects(Vessel ship, CartesianCoordinate2I position)
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

      public static System.Collections.Generic.List<Vessel> StageFleet(Size2 gridSize, params int[] shipSizes)
      {
        var ships = new System.Collections.Generic.List<Vessel>();

        foreach (var size in shipSizes)
        {
          Vessel ship;

          do
          {
            ship = new Vessel(size, new CartesianCoordinate2I(Randomization.NumberGenerator.Crypto.Next(gridSize.Width), Randomization.NumberGenerator.Crypto.Next(gridSize.Height)), (VesselOrientation)Randomization.NumberGenerator.Crypto.Next(2));
          }
          while (!ship.IsValid(gridSize) || ships.Any(s => Intersects(ship, s)));

          ships.Add(ship);
        }

        return ships;
      }

      #region Overloaded operators
      public static bool operator ==(Vessel a, Vessel b)
        => a.Equals(b);
      public static bool operator !=(Vessel a, Vessel b)
        => !a.Equals(b);
      #endregion Overloaded operators

      #region Implemented interfaces
      // IEquatable
      public bool Equals(Vessel? other)
      {
        if (other is null || Orientation != other.Orientation || Length != other.Length)
          return false;

        for (var index = 0; index < m_positions.Count; index++) // Ensure all positions are equal.
          if (m_positions[index] != other.m_positions[index])
            return false;

        return true;
      }
      #endregion Implemented interfaces

      #region Object overrides
      public override bool Equals(object? obj)
        => obj is Vessel o && Equals(o);
      public override int GetHashCode()
        => m_positions.GetHashCodes().Append(Orientation.GetHashCode()).CombineHashCodes();
      public override string? ToString()
        => $"{GetType().Name} {{ Size = {m_positions.Count}, Orientation = {Orientation} }}";
      #endregion Object overrides
    }
  }
}
