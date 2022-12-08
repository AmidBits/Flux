namespace Flux.Model.BattleShip
{
  public interface IPlayer
  {
    string Name { get; }

    System.Version Version { get; }

    void NewMatch(string opponent);

    void NewGame(Size2<int> size, System.TimeSpan timeSpan, int[] shipSizes);

    System.Collections.Generic.List<Vessel> PlaceShips();

    Point2 GetShot();

    void OpponentShot(Point2 shot);

    void ShotHit(Point2 shot, bool sunk);

    void ShotMiss(Point2 shot);

    void GameWon();

    void GameLost();

    void MatchOver();
  }
}
