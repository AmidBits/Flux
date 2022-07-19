namespace Flux.Model.BattleShip
{
  public interface IPlayer
  {
    string Name { get; }

    System.Version Version { get; }

    void NewMatch(string opponent);

    void NewGame(Size2 size, System.TimeSpan timeSpan, int[] shipSizes);

    System.Collections.Generic.List<Vessel> PlaceShips();

    CartesianCoordinate2I GetShot();

    void OpponentShot(CartesianCoordinate2I shot);

    void ShotHit(CartesianCoordinate2I shot, bool sunk);

    void ShotMiss(CartesianCoordinate2I shot);

    void GameWon();

    void GameLost();

    void MatchOver();
  }
}
