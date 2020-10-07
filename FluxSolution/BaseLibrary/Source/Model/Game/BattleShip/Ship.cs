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
    private readonly System.Drawing.Point m_location;
    public System.Drawing.Point Location => m_location;

    private readonly ShipOrientation m_orientation;
    public ShipOrientation Orientation => m_orientation;

    private readonly int m_length;
    public int Length => m_length;

    public Ship(int length, System.Drawing.Point location, ShipOrientation orientation)
    {
      if (length <= 1) throw new System.ArgumentOutOfRangeException(nameof(length));

      m_length = length;
      m_location = location;
      m_orientation = orientation;
    }

    public bool IsValid(System.Drawing.Size boardSize)
    {
      if (m_location.X < 0 || m_location.Y < 0)
        return false;

      if (m_orientation == ShipOrientation.Horizontal)
      {
        if (m_location.Y >= boardSize.Height || m_location.X + m_length > boardSize.Width)
          return false;
      }
      else
      {
        if (m_location.X >= boardSize.Width || m_location.Y + m_length > boardSize.Height)
          return false;
      }

      return true;
    }

    public bool IsAt(System.Drawing.Point location)
    {
      if (Orientation == ShipOrientation.Horizontal)
        return (m_location.Y == location.Y) && (m_location.X <= location.X) && (m_location.X + m_length > location.X);
      else
        return (m_location.X == location.X) && (m_location.Y <= location.Y) && (m_location.Y + m_length > location.Y);
    }

    public System.Collections.Generic.IEnumerable<System.Drawing.Point> GetAllLocations()
    {
      if (Orientation == ShipOrientation.Horizontal)
        for (int i = 0; i < m_length; i++)
          yield return new System.Drawing.Point(m_location.X + i, m_location.Y);
      else
        for (int i = 0; i < m_length; i++)
          yield return new System.Drawing.Point(m_location.X, m_location.Y + i);
    }

    public bool ConflictsWith(Ship other)
    {
      if (m_orientation == ShipOrientation.Horizontal && other.m_orientation == ShipOrientation.Horizontal)
      {
        if (m_location.Y != other.m_location.Y)
          return false;

        return (other.m_location.X < (m_location.X + m_length)) && (m_location.X < (other.m_location.X + other.m_length));
      }
      else if (m_orientation == ShipOrientation.Vertical && other.m_orientation == ShipOrientation.Vertical)
      {
        if (m_location.X != other.m_location.X)
          return false;

        return (other.m_location.Y < (m_location.Y + m_length)) && (m_location.Y < (other.m_location.Y + other.m_length));
      }
      else
      {
        Ship h, v;

        if (m_orientation == ShipOrientation.Horizontal)
        {
          h = this;
          v = other;
        }
        else
        {
          h = other;
          v = this;
        }

        return (h.m_location.Y >= v.m_location.Y) && (h.m_location.Y < (v.m_location.Y + v.m_length)) && (v.m_location.X >= h.m_location.X) && (v.m_location.X < (h.m_location.X + h.m_length));
      }
    }

    public bool IsSunk(System.Collections.Generic.IEnumerable<System.Drawing.Point> shots)
    {
      foreach (System.Drawing.Point location in GetAllLocations())
        if (!shots.Where(s => s.X == location.X && s.Y == location.Y).Any())
          return false;

      return true;
    }
  }
}
