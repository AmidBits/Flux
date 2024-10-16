﻿namespace Flux.Model.BattleShip
{
  public interface IPlayer
  {
    string Name { get; }

    System.Version Version { get; }

    void NewMatch(string opponent);

    void NewGame(System.Drawing.Point size, System.TimeSpan timeSpan, int[] shipSizes);

    System.Collections.Generic.List<Vessel> PlaceShips();

    System.Drawing.Point GetShot();

    void OpponentShot(System.Drawing.Point shot);

    void ShotHit(System.Drawing.Point shot, bool sunk);

    void ShotMiss(System.Drawing.Point shot);

    void GameWon();

    void GameLost();

    void MatchOver();
  }
}
