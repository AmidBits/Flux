namespace Flux.Model.BattleShip
{
  public interface IPlayer
  {
    string Name { get; }

    System.Version Version { get; }

    void NewMatch(string opponent);

    void NewGame(Size2 size, System.TimeSpan timeSpan, int[] shipSizes);

    System.Collections.Generic.List<Vessel> PlaceShips();

    GridCoordinate2 GetShot();

    void OpponentShot(GridCoordinate2 shot);

    void ShotHit(GridCoordinate2 shot, bool sunk);

    void ShotMiss(GridCoordinate2 shot);

    void GameWon();

    void GameLost();

    void MatchOver();
  }
}
