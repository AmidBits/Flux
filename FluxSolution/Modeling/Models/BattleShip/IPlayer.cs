namespace Flux.Model.BattleShip
{
  public interface IPlayer
  {
    string Name { get; }

    System.Version Version { get; }

    void NewMatch(string opponent);

    void NewGame(Numerics.CartesianCoordinate2<int> size, System.TimeSpan timeSpan, int[] shipSizes);

    System.Collections.Generic.List<Vessel> PlaceShips();

    Numerics.CartesianCoordinate2<int> GetShot();

    void OpponentShot(Numerics.CartesianCoordinate2<int> shot);

    void ShotHit(Numerics.CartesianCoordinate2<int> shot, bool sunk);

    void ShotMiss(Numerics.CartesianCoordinate2<int> shot);

    void GameWon();

    void GameLost();

    void MatchOver();
  }
}
