namespace Flux.Model.Gaming.BattleShip
{
  public interface IPlayer
  {
    string Name { get; }

    System.Version Version { get; }

    void NewMatch(string opponent);

    void NewGame(Media.Geometry.Size2 size, System.TimeSpan timeSpan, int[] shipSizes);

    System.Collections.Generic.IList<Vessel> PlaceShips();

    Media.Geometry.Point2 GetShot();

    void OpponentShot(Media.Geometry.Point2 shot);

    void ShotHit(Media.Geometry.Point2 shot, bool sunk);

    void ShotMiss(Media.Geometry.Point2 shot);

    void GameWon();

    void GameLost();

    void MatchOver();
  }
}
