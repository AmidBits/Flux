using System.Linq;

namespace Flux.Model.Game.BattleShip
{
  public enum ShipOrientation
  {
    Horizontal = 0,
    Vertical = 1
  }

  public sealed class Ship
  {
    private readonly System.Collections.Generic.List<System.Drawing.Point> m_locations = new System.Collections.Generic.List<System.Drawing.Point>();
    public System.Drawing.Point Location => m_locations[0];
    public System.Collections.Generic.IReadOnlyList<System.Drawing.Point> Locations => m_locations;

    private readonly ShipOrientation m_orientation;
    public ShipOrientation Orientation => m_orientation;

    public int Length => m_locations.Count;

    public Ship(int length, System.Drawing.Point location, ShipOrientation orientation)
    {
      if (length <= 1) throw new System.ArgumentOutOfRangeException(nameof(length));

      m_orientation = orientation;

      for (int i = 0; i < length; i++)
        m_locations.Add(Orientation == ShipOrientation.Horizontal ? new System.Drawing.Point(location.X + i, location.Y) : new System.Drawing.Point(location.X, location.Y + i));
    }

    public bool IsValid(System.Drawing.Size boardSize)
    {
      if (m_locations[0].X < 0 || m_locations[0].Y < 0)
        return false;

      if (m_orientation == ShipOrientation.Horizontal)
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

    public bool IsAt(System.Drawing.Point location)
      => Orientation == ShipOrientation.Horizontal
      ? (m_locations[0].Y == location.Y) && (m_locations[0].X <= location.X) && (m_locations[0].X + m_locations.Count > location.X)
      : (m_locations[0].X == location.X) && (m_locations[0].Y <= location.Y) && (m_locations[0].Y + m_locations.Count > location.Y);

    public System.Collections.Generic.IReadOnlyList<System.Drawing.Point> GetAllLocations()
      => m_locations;

    public bool ConflictsWith(Ship other)
    {
      if (other is null) throw new System.ArgumentNullException(nameof(other));

      if (m_orientation == ShipOrientation.Horizontal && other.m_orientation == ShipOrientation.Horizontal)
      {
        return m_locations[0].Y != other.m_locations[0].Y
          ? false
          : (other.m_locations[0].X < (m_locations[0].X + m_locations.Count)) && (m_locations[0].X < (other.m_locations[0].X + other.m_locations.Count));
      }
      else if (m_orientation == ShipOrientation.Vertical && other.m_orientation == ShipOrientation.Vertical)
      {
        return m_locations[0].X != other.m_locations[0].X
          ? false
          : (other.m_locations[0].Y < (m_locations[0].Y + m_locations.Count)) && (m_locations[0].Y < (other.m_locations[0].Y + other.m_locations.Count));
      }
      else
      {
        Ship h = m_orientation == ShipOrientation.Horizontal ? this : other;
        Ship v = m_orientation == ShipOrientation.Horizontal ? other : this;

        return (h.m_locations[0].Y >= v.m_locations[0].Y) && (h.m_locations[0].Y < (v.m_locations[0].Y + v.m_locations.Count)) && (v.m_locations[0].X >= h.m_locations[0].X) && (v.m_locations[0].X < (h.m_locations[0].X + h.m_locations.Count));
      }
    }

    public bool IsSunk(System.Collections.Generic.IEnumerable<System.Drawing.Point> shots)
    {
      foreach (var location in m_locations)
        if (!shots.Where(s => s.X == location.X && s.Y == location.Y).Any())
          return false;

      return true;
    }

    public static bool AreAdjacent(Ship a, Ship b)
    {
     foreach (System.Drawing.Point p in a.Locations)
      {
        if (b.IsAt(new System.Drawing.Point(p.X + 1, p.Y + 0))) return true;
        if (b.IsAt(new System.Drawing.Point(p.X + -1, p.Y + 0))) return true;
        if (b.IsAt(new System.Drawing.Point(p.X + 0, p.Y + 1))) return true;
        if (b.IsAt(new System.Drawing.Point(p.X + 0, p.Y + -1))) return true;
      }
      return false;
    }
  }
}
