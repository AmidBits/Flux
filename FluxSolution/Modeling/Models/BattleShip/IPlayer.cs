namespace Flux.Model.BattleShip
{
  public interface IPlayer
  {
    string Name { get; }

    System.Version Version { get; }

    void NewMatch(string opponent);

    void NewGame(Size2<int> size, System.TimeSpan timeSpan, int[] shipSizes);

    System.Collections.Generic.List<Vessel> PlaceShips();

    CoordinateSystems.CartesianCoordinate2<int> GetShot();

    void OpponentShot(CoordinateSystems.CartesianCoordinate2<int> shot);

    void ShotHit(CoordinateSystems.CartesianCoordinate2<int> shot, bool sunk);

    void ShotMiss(CoordinateSystems.CartesianCoordinate2<int> shot);

    void GameWon();

    void GameLost();

    void MatchOver();
  }
}
