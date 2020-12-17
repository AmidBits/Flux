namespace Flux.Model.Game.Engine
{
	public interface IObjectParent
	{
		System.Collections.Generic.IList<IObject> ChildObjects { get; }
	}
}
