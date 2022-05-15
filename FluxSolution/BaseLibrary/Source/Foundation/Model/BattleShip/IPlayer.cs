namespace Flux.Model.BattleShip
{
  public interface IPlayer
  {
    string Name { get; }

    System.Version Version { get; }

    void NewMatch(string opponent);

    void NewGame(Size2 size, System.TimeSpan timeSpan, int[] shipSizes);

    System.Collections.Generic.List<Vessel> PlaceShips();

    CartesianCoordinateI2 GetShot();

    void OpponentShot(CartesianCoordinateI2 shot);

    void ShotHit(CartesianCoordinateI2 shot, bool sunk);

    void ShotMiss(CartesianCoordinateI2 shot);

    void GameWon();

    void GameLost();

    void MatchOver();
  }
}
