namespace Flux.Model.Gaming.BattleShip
{
  public interface IPlayer
  {
    string Name { get; }

    System.Version Version { get; }

    void NewMatch(string opponent);

    void NewGame(Geometry.Size2 size, System.TimeSpan timeSpan, int[] shipSizes);

    System.Collections.Generic.IList<Vessel> PlaceShips();

    Geometry.Point2 GetShot();

    void OpponentShot(Geometry.Point2 shot);

    void ShotHit(Geometry.Point2 shot, bool sunk);

    void ShotMiss(Geometry.Point2 shot);

    void GameWon();

    void GameLost();

    void MatchOver();
  }
}
