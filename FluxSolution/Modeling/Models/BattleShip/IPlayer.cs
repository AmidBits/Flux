namespace Flux.Model.BattleShip
{
  public interface IPlayer
  {
    string Name { get; }

    System.Version Version { get; }

    void NewMatch(string opponent);

    void NewGame(Geometry.CartesianCoordinate2<int> size, System.TimeSpan timeSpan, int[] shipSizes);

    System.Collections.Generic.List<Vessel> PlaceShips();

    Geometry.CartesianCoordinate2<int> GetShot();

    void OpponentShot(Geometry.CartesianCoordinate2<int> shot);

    void ShotHit(Geometry.CartesianCoordinate2<int> shot, bool sunk);

    void ShotMiss(Geometry.CartesianCoordinate2<int> shot);

    void GameWon();

    void GameLost();

    void MatchOver();
  }
}
