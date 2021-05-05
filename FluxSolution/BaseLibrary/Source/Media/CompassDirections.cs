namespace Flux
{
	/// <summary>The four cardinal directions, or cardinal points, are the directions north, east, south, and west, commonly denoted by their initials N, E, S, and W. The directional values are the degrees they represent.</summary>
	/// <see cref="https://en.wikipedia.org/wiki/Cardinal_direction"/>
	/// <seealso cref="https://en.wikipedia.org/wiki/Points_of_the_compass"/>
	public enum CardinalDirection
	{
		N = 0,
		E = 90,
		S = 180,
		W = 270,
	}
	/// <summary>The intercardinal (intermediate, or, historically, ordinal[1]) directions are the four intermediate compass directions located halfway between each pair of cardinal directions. The directional values are the degrees they represent.</summary>
	/// <see cref="https://en.wikipedia.org/wiki/Cardinal_direction#Additional_points"/>
	/// <seealso cref="https://en.wikipedia.org/wiki/Points_of_the_compass"/>
	public enum InterCardinalDirection
	{
		NE = 45,
		SE = 135,
		SW = 225,
		NW = 315,
	}
	/// <summary>The eight principal winds (or main winds) are the four cardinals and four intercardinals considered together, that is: N, NE, E, SE, S, SW, W, NW. Each principal wind is 45° from its two neighbours. The directional values are the degrees they represent.</summary>
	/// <see cref="https://en.wikipedia.org/wiki/Points_of_the_compass#8-wind_compass_rose"/>
	public enum EightWindCompassRose
	{
		E = CardinalDirection.E,
		NE = InterCardinalDirection.NE,
		N = CardinalDirection.N,
		NW = InterCardinalDirection.NW,
		W = CardinalDirection.W,
		SW = InterCardinalDirection.SW,
		S = CardinalDirection.S,
		SE = InterCardinalDirection.SE,
	}

	/// <summary>The compass point directions.</summary>
	/// <see cref="https://en.wikipedia.org/wiki/Points_of_the_compass"/>
	public enum PointsOfTheCompass
	{
		CardinalDirections = 4,
		EightWinds = 8,
	}
}