
/*
  var size = new Flux.Geometry.Size2(10, 10);

  int shotCount = 17;

  System.Collections.Generic.List<Flux.Model.Game.BattleShip.Ship> ships = default;
  System.Collections.Generic.List<Flux.Geometry.Point2> shots = default;

  System.ConsoleKey key = System.ConsoleKey.Escape;

  do
  {
    if (key == System.ConsoleKey.F || key == System.ConsoleKey.Escape)
      ships = Flux.Model.Game.BattleShip.Ship.StageFleet(size, 2, 3, 3, 4, 5);
    if (key == System.ConsoleKey.S || key == System.ConsoleKey.Escape)
      shots = System.Linq.Enumerable.Range(0, shotCount).Select(n => new Flux.Geometry.Point2(Flux.Random.NumberGenerator.Crypto.Next(size.Width), Flux.Random.NumberGenerator.Crypto.Next(size.Height))).ToList();

    System.Console.SetCursorPosition(0, 0);

    System.Console.WriteLine(ships.ToConsoleString(size));

    foreach (var ship in ships)
      System.Console.WriteLine($"Ship{ship.Locations.Count}: {string.Join(' ', ship.Locations)} ({(Flux.Model.Game.BattleShip.Ship.IsSunk(ship, shots) ? @"Sunk!" : Flux.Model.Game.BattleShip.Ship.AnyHits(ship, shots) ? @"Hit!" : @"Miss!")}) ");

    System.Console.WriteLine($"Shots: {string.Join(' ', shots)}");

    System.Console.WriteLine($"Any hits at all? {Flux.Model.Game.BattleShip.Ship.AnyHits(ships, shots)} ");

    key = System.Console.ReadKey().Key;
  }
  while (key != System.ConsoleKey.Escape);
*/
