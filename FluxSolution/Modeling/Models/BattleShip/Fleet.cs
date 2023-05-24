namespace Flux.Model.BattleShip
{
  public record class Fleet
  {
    private readonly int[] m_shipSizes;
    private readonly Geometry.CartesianCoordinate2<int> m_seaSize;

    public Fleet(int[] shipSizes, Geometry.CartesianCoordinate2<int> seaSize)
    {
      if (shipSizes.Length < 1) throw new System.ArgumentOutOfRangeException(nameof(shipSizes));

      m_shipSizes = shipSizes;
      m_seaSize = seaSize;
    }

    public System.Collections.Generic.IReadOnlyList<int> ShipSizes
      => m_shipSizes;
    public Geometry.CartesianCoordinate2<int> SeaSize
      => m_seaSize;

    public bool IsValid(Vessel vessel)
    {
      if (vessel.Locations[0].X < 0 || vessel.Locations[0].Y < 0)
        return false;

      if (vessel.Orientation == VesselOrientation.Horizontal)
      {
        if (vessel.Locations[0].Y >= m_seaSize.Y || vessel.Locations[0].X + vessel.Locations.Count > m_seaSize.X)
          return false;
      }
      else
      {
        if (vessel.Locations[0].X >= m_seaSize.X || vessel.Locations[0].Y + vessel.Locations.Count > m_seaSize.Y)
          return false;
      }

      return true;
    }

    public System.Collections.Generic.List<Vessel> CreateFleet()
    {
      var ships = new System.Collections.Generic.List<Vessel>();

      foreach (var size in m_shipSizes)
      {
        Vessel ship;

        do
        {
          ship = new Vessel(size, new Geometry.CartesianCoordinate2<int>(Random.NumberGenerators.Crypto.Next(m_seaSize.X), Random.NumberGenerators.Crypto.Next(m_seaSize.Y)), (VesselOrientation)Random.NumberGenerators.Crypto.Next(2));
        }
        while (!ship.IsValid(m_seaSize) || ships.Any(s => Vessel.Intersects(ship, s)));

        ships.Add(ship);
      }

      return ships;
    }

    public static double ProximityProbability(int distance) // Max length of 9, could leave wide open.
      => distance >= 0 /*&& distance <= 9*/ ? 1.0 / (distance + 1) : throw new System.ArgumentOutOfRangeException(nameof(distance));
  }
}
