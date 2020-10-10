using System.Linq;

namespace Flux.Model.Game.BattleShip
{
  public enum ShipOrientation
  {
    Horizontal = 0,
    Vertical = 1
  }

  public struct Ship
    : System.IEquatable<Ship>
  {
    public static readonly Ship Empty;
    public bool IsEmpty => Equals(Empty);

    private readonly System.Collections.Generic.List<System.Drawing.Point> m_locations;
    public System.Drawing.Point Location => m_locations.Count > 0 ? m_locations[0] : throw new System.InvalidOperationException(nameof(Empty));
    public System.Collections.Generic.IReadOnlyList<System.Drawing.Point> Locations => m_locations;

    public ShipOrientation Orientation { get; set; }

    public int Length
      => m_locations.Count;

    public Ship(int length, System.Drawing.Point location, ShipOrientation orientation)
    {
      if (length <= 1) throw new System.ArgumentOutOfRangeException(nameof(length));

      Orientation = orientation;

      m_locations = new System.Collections.Generic.List<System.Drawing.Point>();
      for (int i = 0; i < length; i++)
        m_locations.Add(Orientation == ShipOrientation.Horizontal ? new System.Drawing.Point(location.X + i, location.Y) : new System.Drawing.Point(location.X, location.Y + i));
    }

    public bool IsValid(System.Drawing.Size boardSize)
    {
      if (m_locations[0].X < 0 || m_locations[0].Y < 0)
        return false;

      if (Orientation == ShipOrientation.Horizontal)
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

    public static bool IsSunk(Ship ship, System.Collections.Generic.IEnumerable<System.Drawing.Point> allShots)
    {
      foreach (var location in ship.m_locations)
        if (!allShots.Any(s => s.X == location.X && s.Y == location.Y))
          return false;

      return true;
    }

    public static bool AreAdjacent(Ship a, Ship b)
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

    public static bool Intersects(Ship ship, System.Drawing.Point position)
    {
      return ship.Orientation == ShipOrientation.Horizontal
      ? (ship.m_locations[0].Y == position.Y) && (ship.m_locations[0].X <= position.X) && (ship.m_locations[0].X + ship.m_locations.Count > position.X)
      : (ship.m_locations[0].X == position.X) && (ship.m_locations[0].Y <= position.Y) && (ship.m_locations[0].Y + ship.m_locations.Count > position.Y);
    }
    public static bool Intersects(Ship a, Ship b)
    {
      if (a.Orientation == ShipOrientation.Horizontal && b.Orientation == ShipOrientation.Horizontal)
      {
        return a.m_locations[0].Y != b.m_locations[0].Y
          ? false
          : (b.m_locations[0].X < (a.m_locations[0].X + a.m_locations.Count)) && (a.m_locations[0].X < (b.m_locations[0].X + b.m_locations.Count));
      }
      else if (a.Orientation == ShipOrientation.Vertical && b.Orientation == ShipOrientation.Vertical)
      {
        return a.m_locations[0].X != b.m_locations[0].X
          ? false
          : (b.m_locations[0].Y < (a.m_locations[0].Y + a.m_locations.Count)) && (a.m_locations[0].Y < (b.m_locations[0].Y + b.m_locations.Count));
      }
      else
      {
        Ship h = a.Orientation == ShipOrientation.Horizontal ? a : b;
        Ship v = a.Orientation == ShipOrientation.Horizontal ? b : a;

        return (h.m_locations[0].Y >= v.m_locations[0].Y) && (h.m_locations[0].Y < (v.m_locations[0].Y + v.m_locations.Count)) && (v.m_locations[0].X >= h.m_locations[0].X) && (v.m_locations[0].X < (h.m_locations[0].X + h.m_locations.Count));
      }
    }

    public static System.Collections.Generic.List<Ship> StageFleet(System.Drawing.Size gridSize, params int[] shipSizes)
    {
      var ships = new System.Collections.Generic.List<Ship>();

      foreach (var size in shipSizes)
      {
        Ship ship;

        do
        {
          ship = new Ship(size, new System.Drawing.Point(Random.NumberGenerator.Crypto.Next(gridSize.Width), Random.NumberGenerator.Crypto.Next(gridSize.Height)), (ShipOrientation)Random.NumberGenerator.Crypto.Next(2));
        }
        while (!ship.IsValid(gridSize) || ships.Any(s => Intersects(ship, s)));

        ships.Add(ship);
      }

      return ships;
    }

    // Operators
    public static bool operator ==(Ship a, Ship b)
      => a.Equals(b);
    public static bool operator !=(Ship a, Ship b)
      => !a.Equals(b);

    // IEquatable
    public bool Equals(Ship other)
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
      => obj is VersionX o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_locations.CombineHashCore(), Orientation);
    public override string? ToString()
      => $"<{nameof(Ship)} {m_locations.Count}, {Orientation}>";
  }
}
