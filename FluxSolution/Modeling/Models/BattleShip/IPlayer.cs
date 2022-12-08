namespace Flux.Model.BattleShip
{
  public interface IPlayer
  {
    string Name { get; }

    System.Version Version { get; }

    void NewMatch(string opponent);

    void NewGame(Size2<int> size, System.TimeSpan timeSpan, int[] shipSizes);

    System.Collections.Generic.List<Vessel> PlaceShips();

    CartesianCoordinate2<int> GetShot();

    void OpponentShot(CartesianCoordinate2<int> shot);

    void ShotHit(CartesianCoordinate2<int> shot, bool sunk);

    void ShotMiss(CartesianCoordinate2<int> shot);

    void GameWon();

    void GameLost();

    void MatchOver();
  }
}
